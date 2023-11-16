using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Core.Types;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Data.Pact.Stores
{
	/// <summary>
	///
	/// </summary>
	public interface IKeyValueStore
	{
		Task<TryValue<T>> TryGetAsync<T>(
			IValueKey<T> key, CancellationToken cancellation = default(CancellationToken));

		Task AddAsync<T>(IValueKey<T> key, T value, CancellationToken cancellation = default(CancellationToken));

		Task AddOrIgnoreAsync<T>(
			IValueKey<T> key, T value, CancellationToken cancellation = default(CancellationToken));

		Task AddOrUpdateAsync<T>(
			IValueKey<T> key, T value, CancellationToken cancellation = default(CancellationToken));

		Task UpdateAsync<T>(IValueKey<T> key, T value, CancellationToken cancellation = default(CancellationToken));

		Task RemoveAsync<T>(IValueKey<T> key, CancellationToken cancellation = default(CancellationToken));
	}

	public static class IKeyValueStoreContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void TryGetAsync<T>(IValueKey<T> key)
		{
			Contracts.Requires.That(key != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void AddAsync<T>(IValueKey<T> key)
		{
			Contracts.Requires.That(key != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void AddOrIgnoreAsync<T>(IValueKey<T> key)
		{
			Contracts.Requires.That(key != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void AddOrUpdateAsync<T>(IValueKey<T> key)
		{
			Contracts.Requires.That(key != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void UpdateAsync<T>(IValueKey<T> key)
		{
			Contracts.Requires.That(key != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void RemoveAsync<T>(IValueKey<T> key)
		{
			Contracts.Requires.That(key != null);
		}
	}
}
