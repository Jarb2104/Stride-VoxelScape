using System.Diagnostics;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;

namespace Voxelscape.Utility.Data.Pact.Pools
{
	public interface IPool<T> : IVisiblyDisposable
	{
		int AvailableCount { get; }

		int BoundedCapacity { get; }

		void Give(T value);

		bool TryTake(out T value);

		T Take();

		Task<T> TakeAsync();
	}

	public static class IPoolContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Give<T>(IPool<T> instance)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void TryTake<T>(IPool<T> instance)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Take<T>(IPool<T> instance)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void TakeAsync<T>(IPool<T> instance)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
		}
	}
}
