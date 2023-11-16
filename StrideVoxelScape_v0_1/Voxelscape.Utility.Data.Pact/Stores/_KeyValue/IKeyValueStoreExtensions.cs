using System;
using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Stores;

/// <summary>
/// Provides extension methods for <see cref="IKeyValueStore"/>.
/// </summary>
public static class IKeyValueStoreExtensions
{
	public static async Task<T> GetAsync<T>(
		this IKeyValueStore store, IValueKey<T> key, CancellationToken cancellation = default(CancellationToken))
	{
		Contracts.Requires.That(store != null);
		Contracts.Requires.That(key != null);

		var result = await store.TryGetAsync(key, cancellation).DontMarshallContext();
		if (result.HasValue)
		{
			return result.Value;
		}
		else
		{
			throw new InvalidOperationException();
		}
	}

	public static async Task<T> GetOrDefaultAsync<T>(
		this IKeyValueStore store, IValueKey<T> key, CancellationToken cancellation = default(CancellationToken))
	{
		Contracts.Requires.That(store != null);
		Contracts.Requires.That(key != null);

		var result = await store.TryGetAsync(key, cancellation).DontMarshallContext();
		return result.HasValue ? result.Value : default(T);
	}
}
