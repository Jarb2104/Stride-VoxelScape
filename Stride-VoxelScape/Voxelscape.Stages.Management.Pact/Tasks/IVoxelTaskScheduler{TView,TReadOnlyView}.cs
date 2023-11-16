using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Concurrency.Pact.LifeCycle;

namespace Voxelscape.Stages.Management.Pact.Tasks
{
	public interface IVoxelTaskScheduler<TView, TReadOnlyView> : IAsyncCompletable, IDisposed
		where TView : TReadOnlyView
	{
		Task RunAsync(IVoxelTask<TView> task, CancellationToken cancellation = default(CancellationToken));

		Task<TResult> RunAsync<TResult>(
			IVoxelTask<TView, TResult> task, CancellationToken cancellation = default(CancellationToken));

		// this might be a bad overload, cause it's a read only task that returns no result
		// which means it can only accomplish things through unsupported side effects. Not good
		Task RunAsync(
			IVoxelTask<TReadOnlyView> task, CancellationToken cancellation = default(CancellationToken));

		Task<TResult> RunAsync<TResult>(
			IVoxelTask<TReadOnlyView, TResult> task, CancellationToken cancellation = default(CancellationToken));
	}

	public static class IVoxelTaskSchedulerContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void RunAsync<TView, TReadOnlyView>(
			IVoxelTaskScheduler<TView, TReadOnlyView> instance, IVoxelTask<TView> task)
			where TView : TReadOnlyView
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(task != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void RunAsync<TView, TReadOnlyView, TResult>(
			IVoxelTaskScheduler<TView, TReadOnlyView> instance, IVoxelTask<TView, TResult> task)
			where TView : TReadOnlyView
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(task != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void RunAsync<TView, TReadOnlyView>(
			IVoxelTaskScheduler<TView, TReadOnlyView> instance, IVoxelTask<TReadOnlyView> task)
			where TView : TReadOnlyView
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(task != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void RunAsync<TView, TReadOnlyView, TResult>(
			IVoxelTaskScheduler<TView, TReadOnlyView> instance, IVoxelTask<TReadOnlyView, TResult> task)
			where TView : TReadOnlyView
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(task != null);
		}
	}
}
