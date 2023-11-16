using System;
using System.Threading.Tasks;
using Nito.AsyncEx;
using Voxelscape.Utility.Common.Core.Types;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Concurrency.Core.Collections;

/// <summary>
/// Provides extension methods for <see cref="ConcurrentTypeSet{TBase}"/>.
/// </summary>
public static class ConcurrentTypeSetExtensions
{
	public static T AddOrUpdateLazy<T>(
		this ConcurrentTypeSet<object> set, Func<T> addValueFactory, Func<T, T> updateValueFactory)
	{
		Contracts.Requires.That(set != null);
		Contracts.Requires.That(addValueFactory != null);
		Contracts.Requires.That(updateValueFactory != null);

		return set.AddOrUpdate(
			new Lazy<T>(addValueFactory),
			old => new Lazy<T>(() => updateValueFactory(old.Value))).Value;
	}

	public static async Task<T> AddOrUpdateLazyAsync<T>(
		this ConcurrentTypeSet<object> set, Func<Task<T>> addValueFactory, Func<T, T> updateValueFactory)
	{
		Contracts.Requires.That(set != null);
		Contracts.Requires.That(addValueFactory != null);
		Contracts.Requires.That(updateValueFactory != null);

		return await set.AddOrUpdate(
			new AsyncLazy<T>(addValueFactory),
			old => new AsyncLazy<T>(async () => updateValueFactory(await old)));
	}

	public static async Task<T> AddOrUpdateLazyAsync<T>(
		this ConcurrentTypeSet<object> set,
		Func<Task<T>> addValueFactory,
		Func<T, Task<T>> updateValueFactory)
	{
		Contracts.Requires.That(set != null);
		Contracts.Requires.That(addValueFactory != null);
		Contracts.Requires.That(updateValueFactory != null);

		return await set.AddOrUpdate(
			new AsyncLazy<T>(addValueFactory),
			old => new AsyncLazy<T>(async () => await updateValueFactory(await old).DontMarshallContext()));
	}

	public static T GetOrAddLazy<T>(this ConcurrentTypeSet<object> set, Func<T> valueFactory)
	{
		Contracts.Requires.That(set != null);
		Contracts.Requires.That(valueFactory != null);

		return set.GetOrAdd(new Lazy<T>(valueFactory)).Value;
	}

	public static async Task<T> GetOrAddLazyAsync<T>(this ConcurrentTypeSet<object> set, Func<Task<T>> valueFactory)
	{
		Contracts.Requires.That(set != null);
		Contracts.Requires.That(valueFactory != null);

		return await set.GetOrAdd(new AsyncLazy<T>(valueFactory));
	}

	public static bool TryAddLazy<T>(this ConcurrentTypeSet<object> set, Func<T> valueFactory)
	{
		Contracts.Requires.That(set != null);
		Contracts.Requires.That(valueFactory != null);

		return set.TryAdd(new Lazy<T>(valueFactory));
	}

	public static bool TryAddAsyncLazy<T>(this ConcurrentTypeSet<object> set, Func<Task<T>> valueFactory)
	{
		Contracts.Requires.That(set != null);
		Contracts.Requires.That(valueFactory != null);

		return set.TryAdd(new AsyncLazy<T>(valueFactory));
	}

	public static bool TryGetValueLazy<T>(this ConcurrentTypeSet<object> set, out T value)
	{
		Contracts.Requires.That(set != null);

		Lazy<T> lazy;
		bool result = set.TryGetValue(out lazy);
		value = lazy.Value;
		return result;
	}

	public static async Task<TryValue<T>> TryGetValueLazyAsync<T>(this ConcurrentTypeSet<object> set)
	{
		Contracts.Requires.That(set != null);

		AsyncLazy<T> lazy;
		return set.TryGetValue(out lazy) ? TryValue.New(await lazy) : TryValue.None<T>();
	}

	public static bool TryRemoveLazy<T>(this ConcurrentTypeSet<object> set, out T value)
	{
		Contracts.Requires.That(set != null);

		Lazy<T> lazy;
		bool result = set.TryRemove(out lazy);
		value = lazy.Value;
		return result;
	}

	public static async Task<TryValue<T>> TryRemoveLazyAsync<T>(this ConcurrentTypeSet<object> set)
	{
		Contracts.Requires.That(set != null);

		AsyncLazy<T> lazy;
		return set.TryRemove(out lazy) ? TryValue.New(await lazy) : TryValue.None<T>();
	}

	public static bool TryRemoveLazy<T>(this ConcurrentTypeSet<object> set)
	{
		Contracts.Requires.That(set != null);

		return set.TryRemove<Lazy<T>>();
	}

	public static bool TryRemoveAsyncLazy<T>(this ConcurrentTypeSet<object> set)
	{
		Contracts.Requires.That(set != null);

		return set.TryRemove<AsyncLazy<T>>();
	}
}
