using System;
using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;

namespace Voxelscape.Utility.Concurrency.Core.Tasks
{
	// TODO make this async completable in order to finish any pending RunAsync calls?
	public class TaskThrottler<TValue, TResult> : AbstractDisposable
	{
		private readonly Func<TValue, TResult> function;

		private readonly SemaphoreSlim inProgress;

		public TaskThrottler(Func<TValue, TResult> function, int maxDegreeOfParallelism)
		{
			Contracts.Requires.That(function != null);
			Contracts.Requires.That(maxDegreeOfParallelism > 0);

			this.function = function;
			this.inProgress = new SemaphoreSlim(maxDegreeOfParallelism, maxDegreeOfParallelism);
		}

		public async Task<TResult> RunAsync(TValue value, CancellationToken cancellation = default(CancellationToken))
		{
			using (await this.inProgress.WaitInUsingBlockAsync(cancellation).DontMarshallContext())
			{
				return this.function(value);
			}
		}

		/// <inheritdoc />
		protected override void ManagedDisposal() => this.inProgress.Dispose();
	}
}
