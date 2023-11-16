using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Concurrency.Dataflow.Blocks
{
	public class TransformManyBoundedBlock<TInput, TOutput> :
		IPropagatorBlock<TInput, TOutput>, IReceivableSourceBlock<TOutput>
	{
		private readonly Func<TInput, IEnumerable<TOutput>> transform;

		private readonly ActionBlock<TInput> action;

		private readonly BufferBlock<TOutput> buffer;

		public TransformManyBoundedBlock(Func<TInput, IEnumerable<TOutput>> transform)
			: this(transform, new ExecutionDataflowBlockOptions())
		{
		}

		public TransformManyBoundedBlock(
			Func<TInput, IEnumerable<TOutput>> transform, ExecutionDataflowBlockOptions inputOptions)
			: this(transform, inputOptions, inputOptions)
		{
		}

		public TransformManyBoundedBlock(
			Func<TInput, IEnumerable<TOutput>> transform,
			ExecutionDataflowBlockOptions inputOptions,
			DataflowBlockOptions outputOptions)
		{
			Contracts.Requires.That(transform != null);
			Contracts.Requires.That(inputOptions != null);
			Contracts.Requires.That(outputOptions != null);

			this.transform = transform;
			this.action = new ActionBlock<TInput>(this.EnumerateAsync, inputOptions);
			this.buffer = new BufferBlock<TOutput>(outputOptions);
			this.Completion = this.CompleteAsync();
		}

		/// <inheritdoc />
		public Task Completion { get; }

		/// <summary>
		/// Gets the number of input items waiting to be processed by this block.
		/// </summary>
		/// <value>
		/// The number of input items.
		/// </value>
		/// <remarks>
		/// The InputCount does not include any items currently being processed by the block
		/// or any items that have already been processed by the block.
		/// </remarks>
		public int InputCount => this.action.InputCount;

		/// <summary>
		/// Gets the number of output items available to be received from this block.
		/// </summary>
		/// <value>
		/// The number of output items.
		/// </value>
		public int OutputCount => this.buffer.Count;

		/// <inheritdoc />
		public void Complete() => this.action.Complete();

		/// <inheritdoc />
		public IDisposable LinkTo(ITargetBlock<TOutput> target, DataflowLinkOptions linkOptions) =>
			this.buffer.LinkTo(target, linkOptions);

		/// <inheritdoc />
		public bool TryReceive(Predicate<TOutput> filter, out TOutput item) =>
			this.buffer.TryReceive(filter, out item);

		/// <inheritdoc />
		public bool TryReceiveAll(out IList<TOutput> items) => this.buffer.TryReceiveAll(out items);

		/// <inheritdoc />
		void IDataflowBlock.Fault(Exception exception)
		{
			((IDataflowBlock)this.action).Fault(exception);
			((IDataflowBlock)this.buffer).Fault(exception);
		}

		/// <inheritdoc />
		DataflowMessageStatus ITargetBlock<TInput>.OfferMessage(
			DataflowMessageHeader messageHeader, TInput messageValue, ISourceBlock<TInput> source, bool consumeToAccept) =>
			((ITargetBlock<TInput>)this.action).OfferMessage(messageHeader, messageValue, source, consumeToAccept);

		/// <inheritdoc />
		TOutput ISourceBlock<TOutput>.ConsumeMessage(
			DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target, out bool messageConsumed) =>
			((ISourceBlock<TOutput>)this.buffer).ConsumeMessage(messageHeader, target, out messageConsumed);

		/// <inheritdoc />
		void ISourceBlock<TOutput>.ReleaseReservation(
			DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target) =>
			((ISourceBlock<TOutput>)this.buffer).ReleaseReservation(messageHeader, target);

		/// <inheritdoc />
		bool ISourceBlock<TOutput>.ReserveMessage(
			DataflowMessageHeader messageHeader, ITargetBlock<TOutput> target) =>
			((ISourceBlock<TOutput>)this.buffer).ReserveMessage(messageHeader, target);

		private async Task EnumerateAsync(TInput input)
		{
			// iterate every value, but only as previous values have been consumed from the buffer
			foreach (var value in this.transform(input))
			{
				await this.buffer.SendAsync(value).DontMarshallContext();
			}
		}

		private async Task CompleteAsync()
		{
			try
			{
				await this.action.Completion.DontMarshallContext();
				this.buffer.Complete();
				await this.buffer.Completion.DontMarshallContext();
			}
			catch (Exception exception)
			{
				((IDataflowBlock)this.buffer).Fault(exception);
				throw;
			}
		}
	}
}
