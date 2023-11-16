using System.Diagnostics;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Concurrency.Pact.LifeCycle;

namespace Voxelscape.Utility.Data.Pact.Caching
{
	/// <summary>
	/// Defines a cache for temporarily storing values in for better performance that returns
	/// <see cref="IPinnedValue{TKey, TValue}" /> to manage the lifetimes of the cached values.
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	public interface IAsyncCache<TKey, TValue> : IAsyncCompletable, IDisposed
	{
		Task<IPinnedValue<TKey, TValue>> GetPinAsync(TKey key);
	}

	/// <summary>
	/// Provides contracts for <see cref="IAsyncCache{TKey, TState, TValue}"/>.
	/// </summary>
	public static partial class IAsyncCacheContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void GetPinAsync<TKey, TValue>(IAsyncCache<TKey, TValue> instance, TKey key)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(key != null);
		}
	}
}
