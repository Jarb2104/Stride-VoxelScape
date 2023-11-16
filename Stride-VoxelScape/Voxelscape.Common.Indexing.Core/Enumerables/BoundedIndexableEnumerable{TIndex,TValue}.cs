using System.Collections;
using System.Collections.Generic;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Common.Indexing.Pact.Indices;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Exceptions;

namespace Voxelscape.Common.Indexing.Core.Enumerables
{
	/// <summary>
	/// Enumerates the values of an <see cref="IBoundedIndexable{TIndex, TValue}"/> according to the order provided by an
	/// <see cref="IEnumerable{TIndex}"/>.
	/// </summary>
	/// <typeparam name="TIndex">The type of the index.</typeparam>
	/// <typeparam name="TValue">The type of the value.</typeparam>
	public class BoundedIndexableEnumerable<TIndex, TValue> : IEnumerable<KeyValuePair<TIndex, TValue>>
		where TIndex : IIndex
	{
		#region Private Fields

		/// <summary>
		/// The indexable bounds containing the values to enumerate.
		/// </summary>
		private readonly IBoundedIndexable<TIndex, TValue> bounds;

		/// <summary>
		/// The enumerable that dictates the order of the enumeration.
		/// </summary>
		private readonly IEnumerable<TIndex> enumerable;

		/// <summary>
		/// The maximum count. Once the count of indices stored in the enumeratedValues collection reaches this
		/// limit the enumeration ends.
		/// </summary>
		private readonly int maxCount;

		/// <summary>
		/// A value indicating whether to re-yield already previously yield indices.
		/// </summary>
		private readonly bool yieldRevisits;

		/// <summary>
		/// A value indicating whether to cached previously yielded values to possibly re-yield later.
		/// </summary>
		private readonly bool cacheRevisits;

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="BoundedIndexableEnumerable{TIndex, TValue}"/> class.
		/// </summary>
		/// <param name="bounds">The bounds used to limit the enumeration.</param>
		/// <param name="enumerable">The enumerable that determines the order in which the indices are enumerated.</param>
		/// <param name="revisitPolicy">The policy for whether or not to re-yield previously enumerated indices again.</param>
		public BoundedIndexableEnumerable(
			IBoundedIndexable<TIndex, TValue> bounds,
			IEnumerable<TIndex> enumerable,
			Revisits revisitPolicy = Revisits.Yield)
		{
			Contracts.Requires.That(bounds != null);
			Contracts.Requires.That(enumerable != null);

			this.bounds = bounds;
			this.enumerable = enumerable;

			this.maxCount = this.bounds.LowerBounds.CalculateVolume(this.bounds.UpperBounds);

			switch (revisitPolicy)
			{
				case Revisits.Yield:
					this.yieldRevisits = true;
					this.cacheRevisits = false;
					break;

				case Revisits.YieldCached:
					this.yieldRevisits = true;
					this.cacheRevisits = true;
					break;

				case Revisits.DontYield:
					this.yieldRevisits = false;
					this.cacheRevisits = false;
					break;

				default:
					throw InvalidEnumArgument.CreateException(nameof(revisitPolicy), revisitPolicy);
			}
		}

		/// <inheritdoc />
		public IEnumerator<KeyValuePair<TIndex, TValue>> GetEnumerator()
		{
			// The cached enumerated values. This is also used to track what indices have been enumerated.
			IDictionary<TIndex, TValue> enumeratedValues = new Dictionary<TIndex, TValue>(this.maxCount);

			foreach (TIndex index in this.enumerable)
			{
				if (!this.bounds.IsIndexValid(index))
				{
					continue;
				}

				TValue value;
				if (enumeratedValues.TryGetValue(index, out value))
				{
					if (this.yieldRevisits)
					{
						if (!this.cacheRevisits)
						{
							value = this.bounds[index];
						}

						yield return new KeyValuePair<TIndex, TValue>(index, value);
					}
				}
				else
				{
					value = this.bounds[index];

					if (this.cacheRevisits)
					{
						enumeratedValues[index] = value;
					}
					else
					{
						// the yield index still needs to be remembered, but the value associated with it doesn't matter
						// because we're not caching the actual values
						enumeratedValues[index] = default(TValue);
					}

					yield return new KeyValuePair<TIndex, TValue>(index, value);

					if (enumeratedValues.Count == this.maxCount)
					{
						yield break;
					}
				}
			}
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
}
