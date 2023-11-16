using System.Collections.Generic;
using System.Collections.ObjectModel;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Collections
{
	public class OrderedHashSet<T> : KeyedCollection<T, T>, ISet<T>, Voxelscape.Utility.Common.Pact.Collections.IReadOnlySet<T>
	{
		public OrderedHashSet()
			: base()
		{
		}

		public OrderedHashSet(IEnumerable<T> values)
			: base()
		{
			Contracts.Requires.That(values != null);

			this.AddMany(values);
		}

		public OrderedHashSet(IEnumerable<T> values, IEqualityComparer<T> comparer)
			: base(comparer)
		{
			Contracts.Requires.That(values != null);

			this.AddMany(values);
		}

		public OrderedHashSet(IEqualityComparer<T> comparer)
			: base(comparer)
		{
		}

		/// <inheritdoc />
		public new bool Add(T item)
		{
			if (this.Contains(item))
			{
				return false;
			}
			else
			{
				base.Add(item);
				return true;
			}
		}

		/// <inheritdoc />
		public void ExceptWith(IEnumerable<T> other) => SetUtilities.ExceptWith(this, other);

		/// <inheritdoc />
		public void IntersectWith(IEnumerable<T> other) => SetUtilities.IntersectWith(this, other);

		/// <inheritdoc />
		public bool IsProperSubsetOf(IEnumerable<T> other) => SetUtilities.IsProperSubsetOf(this, other);

		/// <inheritdoc />
		public bool IsProperSupersetOf(IEnumerable<T> other) => SetUtilities.IsProperSupersetOf(this, other);

		/// <inheritdoc />
		public bool IsSubsetOf(IEnumerable<T> other) => SetUtilities.IsSubsetOf(this, other);

		/// <inheritdoc />
		public bool IsSupersetOf(IEnumerable<T> other) => SetUtilities.IsSupersetOf(this, other);

		/// <inheritdoc />
		public bool Overlaps(IEnumerable<T> other) => SetUtilities.Overlaps(this, other);

		/// <inheritdoc />
		public bool SetEquals(IEnumerable<T> other) => SetUtilities.SetEquals(this, other);

		/// <inheritdoc />
		public void SymmetricExceptWith(IEnumerable<T> other) => SetUtilities.SymmetricExceptWith(this, other);

		/// <inheritdoc />
		public void UnionWith(IEnumerable<T> other) => SetUtilities.UnionWith(this, other);

		/// <inheritdoc />
		protected override T GetKeyForItem(T item) => item;
	}
}
