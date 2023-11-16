using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Collections
{
	/// <summary>
	/// A dictionary that always returns a value, using a default value or function when it does not contain a key.
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	/// <remarks>
	/// <para>
	/// When a key is used that the dictionary does not contain it will create a default value to associate with
	/// that key and return it, making that key and value now a member of the dictionary.
	/// </para><para>
	/// ContainsKey will always return true. The keys collection, values collection, enumeration, and count
	/// all reflect the keys the dictionary currently contains rather than an infinite set of keys value pairs.
	/// </para>
	/// </remarks>
	[SuppressMessage("StyleCop", "SA1201", Justification = "Grouping by interface.")]
	public class DefaultDictionary<TKey, TValue> : DictionaryWrapper<TKey, TValue>
	{
		/// <summary>
		/// The factory for creating default values.
		/// </summary>
		private readonly Func<TKey, TValue> producer;

		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultDictionary{TKey, TValue}" /> class.
		/// </summary>
		/// <param name="dictionary">The dictionary to wrap.</param>
		/// <param name="producer">
		/// The function to use for creating the default values.
		/// The key of the default value being generated will be passed into the factory method as its first argument.
		/// </param>
		public DefaultDictionary(IDictionary<TKey, TValue> dictionary, Func<TKey, TValue> producer)
			: base(dictionary)
		{
			Contracts.Requires.That(producer != null);

			this.producer = producer;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultDictionary{TKey, TValue}" /> class.
		/// </summary>
		/// <param name="defaultValueProducer">
		/// The function to use for creating the default values.
		/// The key of the default value being generated will be passed into the factory method as its first argument.
		/// </param>
		public DefaultDictionary(Func<TKey, TValue> defaultValueProducer)
			: this(new Dictionary<TKey, TValue>(), defaultValueProducer)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultDictionary{TKey, TValue}"/> class.
		/// </summary>
		/// <param name="dictionary">The dictionary to wrap.</param>
		public DefaultDictionary(IDictionary<TKey, TValue> dictionary)
			: this(dictionary, key => default(TValue))
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultDictionary{TKey, TValue}"/> class.
		/// </summary>
		public DefaultDictionary()
			: this(new Dictionary<TKey, TValue>(), key => default(TValue))
		{
		}

		/// <inheritdoc />
		public override TValue this[TKey key]
		{
			get
			{
				IDictionaryContracts.IndexerGet(this, key);

				TValue result;
				this.TryGetValue(key, out result);
				return result;
			}

			set
			{
				IDictionaryContracts.IndexerSet(this, key);

				this.Dictionary[key] = value;
			}
		}

		/// <inheritdoc />
		public override bool ContainsKey(TKey key)
		{
			IDictionaryContracts.ContainsKey(key);

			TValue unused;
			return this.TryGetValue(key, out unused);
		}

		/// <inheritdoc />
		public override bool TryGetValue(TKey key, out TValue value)
		{
			IDictionaryContracts.TryGetValue(key);

			if (!this.Dictionary.TryGetValue(key, out value))
			{
				value = this.producer(key);
				this.Dictionary[key] = value;
			}

			return true;
		}

		/// <inheritdoc />
		public override void Add(TKey key, TValue value)
		{
			// custom set of contracts used because this dictionary always returns true for containing any key
			// but add normally requires that the dictionary does not contain the key before adding
			Contracts.Requires.That(!this.IsReadOnly);
			Contracts.Requires.That(key != null);

			this.Dictionary.Add(key, value);
		}
	}
}
