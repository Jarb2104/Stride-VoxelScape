using System;
using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Collections
{
	public class FilteredDictionary<TKey, TValue> : DictionaryWrapper<TKey, TValue>
	{
		private readonly Predicate<KeyValuePair<TKey, TValue>> addValue;

		public FilteredDictionary(
			Predicate<KeyValuePair<TKey, TValue>> addValue, IDictionary<TKey, TValue> dictionary)
			: base(dictionary)
		{
			Contracts.Requires.That(addValue != null);

			this.addValue = addValue;
		}

		public FilteredDictionary(Predicate<KeyValuePair<TKey, TValue>> addValue, IEqualityComparer<TKey> comparer)
			: this(addValue, new Dictionary<TKey, TValue>(comparer))
		{
		}

		public FilteredDictionary(Predicate<KeyValuePair<TKey, TValue>> addValue)
			: this(addValue, new Dictionary<TKey, TValue>())
		{
		}

		/// <inheritdoc />
		public override TValue this[TKey key]
		{
			get
			{
				IDictionaryContracts.IndexerGet(this, key);

				return this.Dictionary[key];
			}

			set
			{
				IDictionaryContracts.IndexerSet(this, key);

				if (this.addValue(KeyValuePair.New(key, value)))
				{
					this.Dictionary[key] = value;
				}
			}
		}

		/// <inheritdoc />
		public override void Add(KeyValuePair<TKey, TValue> item)
		{
			ICollectionContracts.Add(this);

			if (this.addValue(item))
			{
				this.Dictionary.Add(item);
			}
		}

		/// <inheritdoc />
		public override void Add(TKey key, TValue value)
		{
			IDictionaryContracts.Add(this, key);

			if (this.addValue(KeyValuePair.New(key, value)))
			{
				this.Dictionary.Add(key, value);
			}
		}
	}
}
