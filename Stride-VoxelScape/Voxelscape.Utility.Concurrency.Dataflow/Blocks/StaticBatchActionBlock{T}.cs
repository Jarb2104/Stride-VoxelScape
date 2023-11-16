using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Concurrency.Dataflow.Blocks
{
	public class StaticBatchActionBlock<T> : ITargetBlock<T>
	{
		private readonly BatchBlock<T> buffer;

		private readonly ActionBlock<T[]> actionBlock;

		#region Constructors

		public StaticBatchActionBlock(Action<IReadOnlyList<T>> action, int batchSize)
			: this(action, batchSize, new DataflowBlockOptions())
		{
		}

		public StaticBatchActionBlock(Action<IReadOnlyList<T>> action, int batchSize, DataflowBlockOptions options)
			: this(action, null, batchSize, options)
		{
		}

		public StaticBatchActionBlock(Func<IReadOnlyList<T>, Task> action, int batchSize)
			: this(action, batchSize, new DataflowBlockOptions())
		{
		}

		public StaticBatchActionBlock(Func<IReadOnlyList<T>, Task> action, int batchSize, DataflowBlockOptions options)
			: this(null, action, batchSize, options)
		{
		}

		private StaticBatchActionBlock(
			Action<IReadOnlyList<T>> action,
			Func<IReadOnlyList<T>, Task> asyncAction,
			int batchSize,
			DataflowBlockOptions options)
		{
			Contracts.Requires.That(
				(action != null && asyncAction == null) || (action == null && asyncAction != null));
			Contracts.Requires.That(options != null);
			Contracts.Requires.That(batchSize > 0);
			Contracts.Requires.That(
				batchSize <= options.BoundedCapacity || options.BoundedCapacity == DataflowBlockOptions.Unbounded);

			this.buffer = new BatchBlock<T>(batchSize, options.ConvertToGroupingOptions());

			var executionOptions = options.ConvertToExecutionOptions();
			executionOptions.MaxDegreeOfParallelism = 1;
			executionOptions.BoundedCapacity = 1;

			if (action != null)
			{
				this.actionBlock = new ActionBlock<T[]>(action, executionOptions);
			}
			else
			{
				this.actionBlock = new ActionBlock<T[]>(asyncAction, executionOptions);
			}

			this.buffer.LinkTo(this.actionBlock, new DataflowLinkOptions() { PropagateCompletion = true });
		}

		#endregion

		#region IDataflowBlock Members

		/// <inheritdoc />
		public Task Completion => this.actionBlock.Completion;

		/// <inheritdoc />
		public void Complete() => this.actionBlock.Complete();

		/// <inheritdoc />
		void IDataflowBlock.Fault(Exception exception) => ((IDataflowBlock)this.buffer).Fault(exception);

		#endregion

		#region ITargetBlock<T>

		/// <inheritdoc />
		DataflowMessageStatus ITargetBlock<T>.OfferMessage(
			DataflowMessageHeader messageHeader, T messageValue, ISourceBlock<T> source, bool consumeToAccept) =>
			((ITargetBlock<T>)this.buffer).OfferMessage(messageHeader, messageValue, source, consumeToAccept);

		#endregion
	}
}
