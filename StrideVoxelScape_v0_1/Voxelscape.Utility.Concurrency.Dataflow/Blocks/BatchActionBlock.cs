using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Exceptions;

namespace Voxelscape.Utility.Concurrency.Dataflow.Blocks
{
	public static class BatchActionBlock
	{
		public static ITargetBlock<T> Create<T>(
			Action<IReadOnlyList<T>> action, int maxBatchSize, Batching batching) =>
			Create(action, maxBatchSize, batching, new DataflowBlockOptions());

		public static ITargetBlock<T> Create<T>(
			Action<IReadOnlyList<T>> action, int maxBatchSize, Batching batching, DataflowBlockOptions options)
		{
			Contracts.Requires.That(action != null);
			Contracts.Requires.That(options != null);

			switch (batching)
			{
				case Batching.Dynamic:
					options = options.CreateCopy();
					options.BoundedCapacity = maxBatchSize;
					return new DynamicBatchActionBlock<T>(action, options);
				case Batching.Static:
					return new StaticBatchActionBlock<T>(action, maxBatchSize, options);
				default: throw InvalidEnumArgument.CreateException(nameof(batching), batching);
			}
		}

		public static ITargetBlock<T> Create<T>(
			Func<IReadOnlyList<T>, Task> action, int maxBatchSize, Batching batching) =>
			Create(action, maxBatchSize, batching, new DataflowBlockOptions());

		public static ITargetBlock<T> Create<T>(
			Func<IReadOnlyList<T>, Task> action, int maxBatchSize, Batching batching, DataflowBlockOptions options)
		{
			Contracts.Requires.That(action != null);
			Contracts.Requires.That(options != null);

			switch (batching)
			{
				case Batching.Dynamic:
					options = options.CreateCopy();
					options.BoundedCapacity = maxBatchSize;
					return new DynamicBatchActionBlock<T>(action, options);
				case Batching.Static:
					return new StaticBatchActionBlock<T>(action, maxBatchSize, options);
				default: throw InvalidEnumArgument.CreateException(nameof(batching), batching);
			}
		}
	}
}
