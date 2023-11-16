using System;
using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;

namespace Voxelscape.Utility.Concurrency.Core.Tasks
{
	// TODO make this async completable in order to finish any pending RunAsync calls?
	public class TaskThrottler : AbstractDisposable
	{
		private readonly SemaphoreSlim inProgress;

		public TaskThrottler(int maxDegreeOfParallelism)
		{
			Contracts.Requires.That(maxDegreeOfParallelism > 0);

			this.inProgress = new SemaphoreSlim(maxDegreeOfParallelism, maxDegreeOfParallelism);
		}

		public async Task<T> RunAsync<T>(
			Func<T> function, CancellationToken cancellation = default(CancellationToken))
		{
			Contracts.Requires.That(function != null);

			using (await this.inProgress.WaitInUsingBlockAsync(cancellation).DontMarshallContext())
			{
				return function();
			}
		}

		public async Task<T> RunAsync<T>(
			Func<Task<T>> function, CancellationToken cancellation = default(CancellationToken))
		{
			Contracts.Requires.That(function != null);

			using (await this.inProgress.WaitInUsingBlockAsync(cancellation).DontMarshallContext())
			{
				return await function().DontMarshallContext();
			}
		}

		/// <inheritdoc />
		protected override void ManagedDisposal() => this.inProgress.Dispose();
	}
}
