using System;
using System.Collections;
using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Collections
{
	/// <summary>
	/// Provides the ability to perform customized structural equality on collection types containing mixed subtypes.
	/// Individual <see cref="IEqualityComparer{T}"/> implementations can be registered for different subtypes and
	/// they will be used during the structural equality comparison.
	/// </summary>
	public class StructuralEqualityComparer : IEqualityComparer
	{
		/// <summary>
		/// The registered comparer to use per type.
		/// </summary>
		private readonly IDictionary<Type, IEqualityComparer> comparerPerType = new Dictionary<Type, IEqualityComparer>();

		/// <summary>
		/// Initializes a new instance of the <see cref="StructuralEqualityComparer"/> class.
		/// </summary>
		public StructuralEqualityComparer()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="StructuralEqualityComparer"/> class.
		/// </summary>
		/// <param name="comparers">The type comparers to use.</param>
		public StructuralEqualityComparer(IEnumerable<KeyValuePair<Type, IEqualityComparer>> comparers)
		{
			Contracts.Requires.That(comparers != null);
			Contracts.Requires.That(comparers.IsUnique(KeyEqualityComparer<Type, IEqualityComparer>.Instance));

			foreach (KeyValuePair<Type, IEqualityComparer> entry in comparers)
			{
				this.comparerPerType[entry.Key] = entry.Value;
			}
		}

		/// <summary>
		/// Adds the specified comparer for its given type.
		/// </summary>
		/// <typeparam name="T">The type to add the comparer for.</typeparam>
		/// <param name="comparer">The comparer.</param>
		public void AddComparerForType<T>(IEqualityComparer<T> comparer)
		{
			Contracts.Requires.That(comparer != null);
			Contracts.Requires.That(!this.ContainsComparerForType<T>());

			this.comparerPerType[typeof(T)] = NonGenericComparers.Wrap<T>(comparer);
		}

		/// <summary>
		/// Determines whether there is a comparer for the specified type already registered.
		/// </summary>
		/// <typeparam name="TType">The type to check for a comparer for.</typeparam>
		/// <returns>True if there is a comparer for the type registered; otherwise false.</returns>
		public bool ContainsComparerForType<TType>()
		{
			return this.comparerPerType.ContainsKey(typeof(TType));
		}

		#region IEqualityComparer Members

		/// <inheritdoc />
		public new bool Equals(object x, object y)
		{
			if (x == null && y == null)
			{
				return true;
			}

			if (x == null || y == null)
			{
				return false;
			}

			// if there is a comparer registered for the type then use that
			IEqualityComparer comparer;
			if (this.comparerPerType.TryGetValue(x.GetType(), out comparer))
			{
				return comparer.Equals(x, y);
			}

			// check if the type is structurally equatable, and if so pass this comparer as the structural comparer
			IStructuralEquatable structuralEquatable = x as IStructuralEquatable;
			if (structuralEquatable != null)
			{
				return structuralEquatable.Equals(y, this);
			}

			// otherwise fall back on regular equality
			return x.Equals(y);
		}

		/// <inheritdoc />
		public int GetHashCode(object obj)
		{
			IEqualityComparerContracts.GetHashCode(obj);

			// if there is a comparer registered for the type then use that
			IEqualityComparer comparer;
			if (this.comparerPerType.TryGetValue(obj.GetType(), out comparer))
			{
				return comparer.GetHashCode(obj);
			}

			// check if the type is structurally equatable, and if so pass this comparer as the structural comparer
			IStructuralEquatable structuralEquatable = obj as IStructuralEquatable;
			if (structuralEquatable != null)
			{
				return structuralEquatable.GetHashCode(this);
			}

			// otherwise fall back on regular hashcode
			return obj.GetHashCode();
		}

		#endregion
	}
}
