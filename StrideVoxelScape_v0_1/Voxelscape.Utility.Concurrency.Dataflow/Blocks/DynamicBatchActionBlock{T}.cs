using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Concurrency.Dataflow.Blocks
{
	public class DynamicBatchActionBlock<T> : ITargetBlock<T>
	{
		private readonly BufferBlock<T> buffer;

		private readonly CancellationToken cancellation;

		private readonly Action<IReadOnlyList<T>> action;

		private readonly Func<IReadOnlyList<T>, Task> asyncAction;

		#region Constructors

		public DynamicBatchActionBlock(Action<IReadOnlyList<T>> action)
			: this(action, new DataflowBlockOptions())
		{
		}

		public DynamicBatchActionBlock(Action<IReadOnlyList<T>> action, DataflowBlockOptions options)
			: this(action, null, options)
		{
		}

		public DynamicBatchActionBlock(Func<IReadOnlyList<T>, Task> action)
			: this(action, new DataflowBlockOptions())
		{
		}

		public DynamicBatchActionBlock(Func<IReadOnlyList<T>, Task> action, DataflowBlockOptions options)
			: this(null, action, options)
		{
		}

		private DynamicBatchActionBlock(
			Action<IReadOnlyList<T>> action, Func<IReadOnlyList<T>, Task> asyncAction, DataflowBlockOptions options)
		{
			Contracts.Requires.That(
				(action != null && asyncAction == null) || (action == null && asyncAction != null));
			Contracts.Requires.That(options != null);

			this.buffer = new BufferBlock<T>(options);
			this.cancellation = options.CancellationToken;
			this.action = action;
			this.asyncAction = asyncAction;
			this.Completion = this.RepeatActionAsync();
		}

		#endregion

		#region IDataflowBlock Members

		/// <inheritdoc />
		public Task Completion { get; }

		/// <inheritdoc />
		public void Complete() => this.buffer.Complete();

		/// <inheritdoc />
		void IDataflowBlock.Fault(Exception exception) => ((IDataflowBlock)this.buffer).Fault(exception);

		#endregion

		#region ITargetBlock<T>

		/// <inheritdoc />
		DataflowMessageStatus ITargetBlock<T>.OfferMessage(
			DataflowMessageHeader messageHeader, T messageValue, ISourceBlock<T> source, bool consumeToAccept) =>
			((ITargetBlock<T>)this.buffer).OfferMessage(messageHeader, messageValue, source, consumeToAccept);

		#endregion

		private async Task RepeatActionAsync()
		{
			try
			{
				// wait until there is at least one value available to process (or end when the buffer completes)
				while (await this.buffer.OutputAvailableAsync(this.cancellation).DontMarshallContext())
				{
					// dequeue and process all available values
					IList<T> values;
					if (this.buffer.TryReceiveAllBugFixed(out values))
					{
						// use whichever action delegate was provided to the constructor
						if (this.action != null)
						{
							this.action(values.AsReadOnlyList());
						}
						else
						{
							await this.asyncAction(values.AsReadOnlyList()).DontMarshallContext();
						}
					}
				}
			}
			catch (Exception exception)
			{
				// store the exception by faulting the buffer and do not rethrow
				// instead await the completion of the buffer below
				((IDataflowBlock)this.buffer).Fault(exception);
			}
			finally
			{
				this.buffer.Complete();

				// await the buffer completion so this task always ends in the same way as the buffer
				await this.buffer.Completion.DontMarshallContext();
			}
		}
	}
}
