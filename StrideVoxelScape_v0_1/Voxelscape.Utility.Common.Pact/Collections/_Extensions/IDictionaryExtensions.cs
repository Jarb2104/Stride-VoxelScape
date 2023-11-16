using System;
using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for the <see cref="IDictionaryExtensions"/> class.
/// </summary>
public static class IDictionaryExtensions
{
	/// <summary>
	/// Adds the value for the given key if the dictionary does not already contain the key.
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	/// <param name="dictionary">The dictionary to possibly add to.</param>
	/// <param name="key">The key of the value to add.</param>
	/// <param name="value">The value to add for the given key.</param>
	/// <returns>True if the key and value were added; otherwise false (the key was already present).</returns>
	public static bool AddIfNewKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
	{
		Contracts.Requires.That(dictionary != null);
		Contracts.Requires.That(key != null);

		if (dictionary.ContainsKey(key))
		{
			return false;
		}
		else
		{
			dictionary[key] = value;
			return true;
		}
	}

	/// <summary>
	/// Adds the value for the given key if the dictionary does not already contain the key.
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	/// <param name="dictionary">The dictionary to possibly add to.</param>
	/// <param name="key">The key of the value to add.</param>
	/// <param name="valueFactory">
	/// The function used to create the value to add for the given key. This is only executed if adding the value.
	/// </param>
	/// <returns>True if the key and value were added; otherwise false (the key was already present).</returns>
	public static bool AddIfNewKey<TKey, TValue>(
		this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> valueFactory)
	{
		Contracts.Requires.That(dictionary != null);
		Contracts.Requires.That(key != null);
		Contracts.Requires.That(valueFactory != null);

		if (dictionary.ContainsKey(key))
		{
			return false;
		}
		else
		{
			dictionary[key] = valueFactory(key);
			return true;
		}
	}

	public static TValue GetOrAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
	{
		Contracts.Requires.That(dictionary != null);
		Contracts.Requires.That(key != null);

		TValue result;
		if (dictionary.TryGetValue(key, out result))
		{
			return result;
		}
		else
		{
			dictionary[key] = value;
			return value;
		}
	}

	public static TValue GetOrAdd<TKey, TValue>(
		this IDictionary<TKey, TValue> dictionary, TKey key, Func<TKey, TValue> valueFactory)
	{
		Contracts.Requires.That(dictionary != null);
		Contracts.Requires.That(key != null);
		Contracts.Requires.That(valueFactory != null);

		TValue result;
		if (dictionary.TryGetValue(key, out result))
		{
			return result;
		}
		else
		{
			result = valueFactory(key);
			dictionary[key] = result;
			return result;
		}
	}

	public static TValue AddOrUpdate<TKey, TValue>(
		this IDictionary<TKey, TValue> dictionary,
		TKey key,
		TValue addValue,
		Func<TKey, TValue, TValue> updateValueFactory)
	{
		Contracts.Requires.That(dictionary != null);
		Contracts.Requires.That(key != null);
		Contracts.Requires.That(updateValueFactory != null);

		TValue value = dictionary.TryGetValue(key, out value) ? updateValueFactory(key, value) : addValue;
		dictionary[key] = value;
		return value;
	}

	public static TValue AddOrUpdate<TKey, TValue>(
		this IDictionary<TKey, TValue> dictionary,
		TKey key,
		Func<TKey, TValue> addValueFactory,
		Func<TKey, TValue, TValue> updateValueFactory)
	{
		Contracts.Requires.That(dictionary != null);
		Contracts.Requires.That(key != null);
		Contracts.Requires.That(addValueFactory != null);
		Contracts.Requires.That(updateValueFactory != null);

		TValue value = dictionary.TryGetValue(key, out value) ? updateValueFactory(key, value) : addValueFactory(key);
		dictionary[key] = value;
		return value;
	}
}
