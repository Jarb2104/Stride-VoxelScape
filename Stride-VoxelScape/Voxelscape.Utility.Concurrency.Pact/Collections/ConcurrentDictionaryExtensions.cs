using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nito.AsyncEx;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for the <see cref="ConcurrentDictionary{TKey, TValue}"/> class.
/// </summary>
/// <threadsafety static="true" instance="true" />
public static class ConcurrentDictionaryExtensions
{
	/// <summary>
	/// Attempts to remove and return the value that has the specified key from the dictionary.
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	/// <param name="dictionary">The dictionary.</param>
	/// <param name="key">The key of the element to remove.</param>
	/// <returns>True if the element was removed successfully; otherwise false.</returns>
	public static bool TryRemove<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary, TKey key)
	{
		Contracts.Requires.That(dictionary != null);

		TValue unused;
		return dictionary.TryRemove(key, out unused);
	}

	#region Lazy methods

	public static TValue AddOrUpdateLazy<TKey, TValue>(
		this ConcurrentDictionary<TKey, Lazy<TValue>> dictionary,
		TKey key,
		Func<TKey, TValue> addValueFactory,
		Func<TKey, TValue, TValue> updateValueFactory)
	{
		Contracts.Requires.That(dictionary != null);
		Contracts.Requires.That(key != null);
		Contracts.Requires.That(addValueFactory != null);
		Contracts.Requires.That(updateValueFactory != null);

		return dictionary.AddOrUpdate(
			key,
			new Lazy<TValue>(() => addValueFactory(key)),
			(updateKey, old) => new Lazy<TValue>(() => updateValueFactory(updateKey, old.Value))).Value;
	}

	public static async Task<TValue> AddOrUpdateLazyAsync<TKey, TValue>(
		this ConcurrentDictionary<TKey, AsyncLazy<TValue>> dictionary,
		TKey key,
		Func<TKey, Task<TValue>> addValueFactory,
		Func<TKey, TValue, TValue> updateValueFactory)
	{
		Contracts.Requires.That(dictionary != null);
		Contracts.Requires.That(key != null);
		Contracts.Requires.That(addValueFactory != null);
		Contracts.Requires.That(updateValueFactory != null);

		return await dictionary.AddOrUpdate(
			key,
			new AsyncLazy<TValue>(() => addValueFactory(key)),
			(updateKey, old) => new AsyncLazy<TValue>(async () => updateValueFactory(updateKey, await old)));
	}

	public static async Task<TValue> AddOrUpdateLazyAsync<TKey, TValue>(
		this ConcurrentDictionary<TKey, AsyncLazy<TValue>> dictionary,
		TKey key,
		Func<TKey, Task<TValue>> addValueFactory,
		Func<TKey, TValue, Task<TValue>> updateValueFactory)
	{
		Contracts.Requires.That(dictionary != null);
		Contracts.Requires.That(key != null);
		Contracts.Requires.That(addValueFactory != null);
		Contracts.Requires.That(updateValueFactory != null);

		return await dictionary.AddOrUpdate(
			key,
			new AsyncLazy<TValue>(() => addValueFactory(key)),
			(updateKey, old) => new AsyncLazy<TValue>(
				async () => await updateValueFactory(updateKey, await old).DontMarshallContext()));
	}

	public static TValue GetOrAddLazy<TKey, TValue>(
		this ConcurrentDictionary<TKey, Lazy<TValue>> dictionary, TKey key, Func<TKey, TValue> valueFactory)
	{
		Contracts.Requires.That(dictionary != null);
		Contracts.Requires.That(key != null);
		Contracts.Requires.That(valueFactory != null);

		return dictionary.GetOrAdd(key, new Lazy<TValue>(() => valueFactory(key))).Value;
	}

	public static async Task<TValue> GetOrAddLazyAsync<TKey, TValue>(
		this ConcurrentDictionary<TKey, AsyncLazy<TValue>> dictionary,
		TKey key,
		Func<TKey, Task<TValue>> valueFactory)
	{
		Contracts.Requires.That(dictionary != null);
		Contracts.Requires.That(key != null);
		Contracts.Requires.That(valueFactory != null);

		return await dictionary.GetOrAdd(key, new AsyncLazy<TValue>(() => valueFactory(key)));
	}

	#endregion

	#region GetOrAdd with 'wasValueAdded' output parameter

	/// <summary>
	/// Adds a key/value pair to the <see cref="ConcurrentDictionary{TKey, TValue}"/> by using the specified function,
	/// if the key does not already exist.
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	/// <param name="dictionary">The dictionary to get or add from/to.</param>
	/// <param name="key">The key of the element to add.</param>
	/// <param name="value">The value to be added, if the key does not already exist.</param>
	/// <param name="wasValueAdded">If set to <c>true</c> the value was added; otherwise false.</param>
	/// <returns>
	/// The value for the key. This will be either the existing value for the key if the key is already in the dictionary,
	/// or the new value for the key as added if the key was not in the dictionary.
	/// </returns>
	public static TValue GetOrAdd<TKey, TValue>(
		this ConcurrentDictionary<TKey, TValue> dictionary,
		TKey key,
		TValue value,
		out bool wasValueAdded)
	{
		Contracts.Requires.That(dictionary != null);

		TValue result;
		while (true)
		{
			if (dictionary.TryGetValue(key, out result))
			{
				wasValueAdded = false;
				return result;
			}

			if (dictionary.TryAdd(key, value))
			{
				wasValueAdded = true;
				return value;
			}
		}
	}

	/// <summary>
	/// Adds a key/value pair to the <see cref="ConcurrentDictionary{TKey, TValue}"/> by using the specified function,
	/// if the key does not already exist.
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	/// <param name="dictionary">The dictionary to get or add from/to.</param>
	/// <param name="key">The key of the element to add.</param>
	/// <param name="valueFactory">The function used to generate a value for the key.</param>
	/// <param name="wasValueAdded">If set to <c>true</c> the value was added; otherwise false.</param>
	/// <returns>
	/// The value for the key. This will be either the existing value for the key if the key is already in the dictionary,
	/// or the new value for the key as returned by valueFactory if the key was not in the dictionary.
	/// </returns>
	public static TValue GetOrAdd<TKey, TValue>(
		this ConcurrentDictionary<TKey, TValue> dictionary,
		TKey key,
		Func<TKey, TValue> valueFactory,
		out bool wasValueAdded)
	{
		Contracts.Requires.That(dictionary != null);
		Contracts.Requires.That(valueFactory != null);

		TValue value;
		while (true)
		{
			if (dictionary.TryGetValue(key, out value))
			{
				wasValueAdded = false;
				return value;
			}

			value = valueFactory(key);
			if (dictionary.TryAdd(key, value))
			{
				wasValueAdded = true;
				return value;
			}
		}
	}

	#endregion

	#region LockFree versions of existing API

	/// <summary>
	/// Gets the number of key/value pairs contained in the dictionary.
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	/// <param name="dictionary">The dictionary.</param>
	/// <returns>The number of key/value pairs contained in the dictionary.</returns>
	/// <remarks>
	/// <para>
	/// This is a lock free implementation that does not use snapshot semantics. However, it is O(n) performance.
	/// This should still be preferred over using the <see cref="ICollection{T}.Count"/> property as such requires acquiring all
	/// the dictionary's locks. If you need to check if the collection is empty use <see cref="IsEmptyLockFree"/> instead.
	/// </para><para>
	/// This value could become invalid as soon as it returns. Therefore it is only suitable for debugging purposes.
	/// </para>
	/// </remarks>
	public static int CountLockFree<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary)
	{
		Contracts.Requires.That(dictionary != null);

		return dictionary.Select(item => item).Count();
	}

	/// <summary>
	/// Gets a value that indicates whether the dictionary is empty.
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	/// <param name="dictionary">The dictionary.</param>
	/// <returns>True if the dictionary is empty; otherwise, false.</returns>
	/// <remarks>
	/// <para>
	/// This is a lock free implementation that does not use snapshot semantics.
	/// </para><para>
	/// This value could become invalid as soon as it returns. Therefore it is only suitable for debugging purposes.
	/// </para>
	/// </remarks>
	public static bool IsEmptyLockFree<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary)
	{
		Contracts.Requires.That(dictionary != null);

		// Skip(0) is required here because LINQ is optimized for ICollections,
		// and will attempt to use the Count property when it is available
		return !dictionary.Skip(0).Any();
	}

	/// <summary>
	/// Gets an enumerable containing the keys in the dictionary.
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	/// <param name="dictionary">The dictionary.</param>
	/// <returns>An enumerable containing the keys in the dictionary.</returns>
	/// <remarks>
	/// <para>
	/// This is a lock free implementation that does not use snapshot semantics.
	/// </para><para>
	/// Keys can be removed from the dictionary the moment this enumerator returns them or added
	/// while the enumerator is running without returning them.
	/// </para>
	/// </remarks>
	public static IEnumerable<TKey> KeysLockFree<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary)
	{
		Contracts.Requires.That(dictionary != null);

		return dictionary.Select(item => item.Key);
	}

	/// <summary>
	/// Gets an enumerable containing the values in the dictionary.
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	/// <param name="dictionary">The dictionary.</param>
	/// <returns>An enumerable containing the values in the dictionary.</returns>
	/// <remarks>
	/// <para>
	/// This is a lock free implementation that does not use snapshot semantics.
	/// </para><para>
	/// Values can be removed from the dictionary the moment this enumerator returns them or added
	/// while the enumerator is running without returning them.
	/// </para>
	/// </remarks>
	public static IEnumerable<TValue> ValuesLockFree<TKey, TValue>(this ConcurrentDictionary<TKey, TValue> dictionary)
	{
		Contracts.Requires.That(dictionary != null);

		return dictionary.Select(item => item.Value);
	}

	#endregion
}
