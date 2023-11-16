using System.Collections.Generic;
using System.Linq;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Collections
{
	/// <summary>
	/// Provides already completed implementations of most of the <see cref="ISet{T}"/> methods
	/// for use when implementing <see cref="ISet{T}"/> in a new class. These implementations use
	/// other methods from the <see cref="ICollection{T}"/> interface to achieve their results.
	/// </summary>
	/// <remarks>
	/// The methods of this class handle checking precondition contracts.
	/// </remarks>
	public static class SetUtilities
	{
		#region ExceptWith

		/// <summary>
		/// Provides an implementation of <see cref="ISet{T}.ExceptWith"/>.
		/// </summary>
		/// <typeparam name="T">The type of values in the collections.</typeparam>
		/// <param name="source">The source collection that is implementing <see cref="ISet{T}"/>.</param>
		/// <param name="other">The other enumerable.</param>
		/// <remarks>
		/// From <see href="https://msdn.microsoft.com/en-us/library/dd382043(v=vs.110).aspx"/>;
		/// Removes all elements in the specified collection from the current set.
		/// </remarks>
		public static void ExceptWith<T>(ISet<T> source, IEnumerable<T> other)
		{
			Contracts.Requires.That(source != null);
			ISetContracts.ExceptWith(source, other);

			if (source.IsReadOnly)
			{
				return;
			}

			foreach (T value in other)
			{
				source.Remove(value);
			}
		}

		#endregion

		#region IntersectWith (Overloaded)

		/// <summary>
		/// Provides an implementation of <see cref="ISet{T}.IntersectWith"/>.
		/// </summary>
		/// <typeparam name="T">The type of values in the collections.</typeparam>
		/// <param name="source">The source collection that is implementing <see cref="ISet{T}"/>.</param>
		/// <param name="other">The other enumerable.</param>
		/// <remarks>
		/// From <see href="https://msdn.microsoft.com/en-us/library/dd394889(v=vs.110).aspx"/>;
		/// Modifies the current set so that it contains only elements that are also in a specified collection.
		/// </remarks>
		public static void IntersectWith<T>(ISet<T> source, HashSet<T> other)
		{
			Contracts.Requires.That(source != null);
			ISetContracts.IntersectWith(source, other);

			if (source.IsReadOnly)
			{
				return;
			}

			List<T> valuesToRemove = new List<T>();

			foreach (T value in source)
			{
				if (!other.Contains(value))
				{
					valuesToRemove.Add(value);
				}
			}

			source.RemoveMany(valuesToRemove);
		}

		/// <summary>
		/// Provides an implementation of <see cref="ISet{T}.IntersectWith"/>.
		/// </summary>
		/// <typeparam name="T">The type of values in the collections.</typeparam>
		/// <param name="source">The source collection that is implementing <see cref="ISet{T}"/>.</param>
		/// <param name="other">The other enumerable.</param>
		/// <remarks>
		/// From <see href="https://msdn.microsoft.com/en-us/library/dd394889(v=vs.110).aspx"/>;
		/// Modifies the current set so that it contains only elements that are also in a specified collection.
		/// </remarks>
		public static void IntersectWith<T>(ISet<T> source, IEnumerable<T> other) =>
			IntersectWith(source, other, EqualityComparer<T>.Default);

		/// <summary>
		/// Provides an implementation of <see cref="ISet{T}.IntersectWith" />.
		/// </summary>
		/// <typeparam name="T">The type of values in the collections.</typeparam>
		/// <param name="source">The source collection that is implementing <see cref="ISet{T}" />.</param>
		/// <param name="other">The other enumerable.</param>
		/// <param name="comparer">The comparer.</param>
		/// <remarks>
		/// From <see href="https://msdn.microsoft.com/en-us/library/dd394889(v=vs.110).aspx" />;
		/// Modifies the current set so that it contains only elements that are also in a specified collection.
		/// </remarks>
		public static void IntersectWith<T>(ISet<T> source, IEnumerable<T> other, IEqualityComparer<T> comparer)
		{
			Contracts.Requires.That(source != null);
			Contracts.Requires.That(comparer != null);
			ISetContracts.IntersectWith(source, other);

			if (source.IsReadOnly)
			{
				return;
			}

			IntersectWith(source, other as HashSet<T> ?? new HashSet<T>(other, comparer));
		}

		#endregion

		#region SymmetricExceptWith

		/// <summary>
		/// Provides an implementation of <see cref="ISet{T}.SymmetricExceptWith"/>.
		/// </summary>
		/// <typeparam name="T">The type of values in the collections.</typeparam>
		/// <param name="source">The source collection that is implementing <see cref="ISet{T}"/>.</param>
		/// <param name="other">The other enumerable.</param>
		/// <remarks>
		/// From <see href="https://msdn.microsoft.com/en-us/library/dd411718(v=vs.110).aspx"/>;
		/// Modifies the current set so that it contains only elements that are present either in
		/// the current set or in the specified collection, but not both.
		/// </remarks>
		public static void SymmetricExceptWith<T>(ISet<T> source, IEnumerable<T> other)
		{
			Contracts.Requires.That(source != null);
			ISetContracts.SymmetricExceptWith(source, other);

			if (source.IsReadOnly)
			{
				return;
			}

			foreach (T value in other)
			{
				// if source contains the value, remove it, otherwise add it
				if (!source.Remove(value))
				{
					source.Add(value);
				}
			}
		}

		#endregion

		#region UnionWith

		/// <summary>
		/// Provides an implementation of <see cref="ISet{T}.UnionWith"/>.
		/// </summary>
		/// <typeparam name="T">The type of values in the collections.</typeparam>
		/// <param name="source">The source collection that is implementing <see cref="ISet{T}"/>.</param>
		/// <param name="other">The other enumerable.</param>
		/// <remarks>
		/// From <see href="https://msdn.microsoft.com/en-us/library/dd411713(v=vs.110).aspx"/>;
		/// Modifies the current set so that it contains all elements that are present in the current set,
		/// in the specified collection, or in both.
		/// </remarks>
		public static void UnionWith<T>(ISet<T> source, IEnumerable<T> other)
		{
			Contracts.Requires.That(source != null);
			ISetContracts.UnionWith(source, other);

			if (source.IsReadOnly)
			{
				return;
			}

			foreach (T value in other)
			{
				source.Add(value);
			}
		}

		#endregion

		#region Overlaps

		/// <summary>
		/// Provides an implementation of <see cref="ISet{T}.Overlaps"/>.
		/// </summary>
		/// <typeparam name="T">The type of values in the collections.</typeparam>
		/// <param name="source">The source collection that is implementing <see cref="ISet{T}"/>.</param>
		/// <param name="other">The other enumerable.</param>
		/// <returns>The result of <see cref="ISet{T}.Overlaps"/>.</returns>
		/// <remarks>
		/// From <see href="https://msdn.microsoft.com/en-us/library/dd412095(v=vs.110).aspx"/>;
		/// Determines whether the current set overlaps with the specified collection.
		/// </remarks>
		public static bool Overlaps<T>(ISet<T> source, IEnumerable<T> other)
		{
			Contracts.Requires.That(source != null);
			ISetContracts.Overlaps(other);

			if (source.Count == 0 || other.IsEmpty())
			{
				return false;
			}

			return other.Any(source.Contains);
		}

		#endregion

		#region SetEquals (Overloaded)

		/// <summary>
		/// Provides an implementation of <see cref="ISet{T}.SetEquals"/>.
		/// </summary>
		/// <typeparam name="T">The type of values in the collections.</typeparam>
		/// <param name="source">The source collection that is implementing <see cref="ISet{T}"/>.</param>
		/// <param name="other">The other enumerable.</param>
		/// <returns>The result of <see cref="ISet{T}.SetEquals"/>.</returns>
		/// <remarks>
		/// From <see href="https://msdn.microsoft.com/en-us/library/dd412096(v=vs.110).aspx"/>;
		/// Determines whether the current set and the specified collection contain the same elements.
		/// </remarks>
		public static bool SetEquals<T>(ISet<T> source, HashSet<T> other)
		{
			Contracts.Requires.That(source != null);
			ISetContracts.SetEquals(other);

			if (source.Count != other.Count)
			{
				return false;
			}

			return other.SetEquals(source);
		}

		/// <summary>
		/// Provides an implementation of <see cref="ISet{T}.SetEquals"/>.
		/// </summary>
		/// <typeparam name="T">The type of values in the collections.</typeparam>
		/// <param name="source">The source collection that is implementing <see cref="ISet{T}"/>.</param>
		/// <param name="other">The other enumerable.</param>
		/// <returns>The result of <see cref="ISet{T}.SetEquals"/>.</returns>
		/// <remarks>
		/// From <see href="https://msdn.microsoft.com/en-us/library/dd412096(v=vs.110).aspx"/>;
		/// Determines whether the current set and the specified collection contain the same elements.
		/// </remarks>
		public static bool SetEquals<T>(ISet<T> source, IEnumerable<T> other) =>
			SetEquals(source, other, EqualityComparer<T>.Default);

		/// <summary>
		/// Provides an implementation of <see cref="ISet{T}.SetEquals" />.
		/// </summary>
		/// <typeparam name="T">The type of values in the collections.</typeparam>
		/// <param name="source">The source collection that is implementing <see cref="ISet{T}" />.</param>
		/// <param name="other">The other enumerable.</param>
		/// <param name="comparer">The comparer.</param>
		/// <returns>The result of <see cref="ISet{T}.SetEquals"/>.</returns>
		/// <remarks>
		/// From <see href="https://msdn.microsoft.com/en-us/library/dd412096(v=vs.110).aspx" />;
		/// Determines whether the current set and the specified collection contain the same elements.
		/// </remarks>
		public static bool SetEquals<T>(ISet<T> source, IEnumerable<T> other, IEqualityComparer<T> comparer)
		{
			Contracts.Requires.That(source != null);
			Contracts.Requires.That(comparer != null);
			ISetContracts.SetEquals(other);

			return SetEquals(source, other as HashSet<T> ?? new HashSet<T>(other, comparer));
		}

		#endregion

		#region IsProperSubsetOf (Overloaded)

		/// <summary>
		/// Provides an implementation of <see cref="ISet{T}.IsProperSubsetOf"/>.
		/// </summary>
		/// <typeparam name="T">The type of values in the collections.</typeparam>
		/// <param name="source">The source collection that is implementing <see cref="ISet{T}"/>.</param>
		/// <param name="other">The other enumerable.</param>
		/// <returns>The result of <see cref="ISet{T}.IsProperSubsetOf"/>.</returns>
		/// <remarks>
		/// <para>
		/// From <see href="https://msdn.microsoft.com/en-us/library/dd321100(v=vs.110).aspx"/>;
		/// Determines whether the current set is a proper (strict) subset of a specified collection.
		/// </para><para>
		/// If the current set is a proper subset of other, other must have at least one element that the current
		/// set does not have.
		/// </para><para>
		/// An empty set is a proper subset of any other collection.Therefore, this method returns true if the
		/// current set is empty, unless the other parameter is also an empty set.
		/// </para><para>
		/// This method always returns false if the current set has more or the same number of elements than other.
		/// </para>
		/// </remarks>
		public static bool IsProperSubsetOf<T>(ISet<T> source, HashSet<T> other)
		{
			Contracts.Requires.That(source != null);
			ISetContracts.IsProperSubsetOf(other);

			if (source.Count >= other.Count)
			{
				return false;
			}

			return other.IsProperSupersetOf(source);
		}

		/// <summary>
		/// Provides an implementation of <see cref="ISet{T}.IsProperSubsetOf"/>.
		/// </summary>
		/// <typeparam name="T">The type of values in the collections.</typeparam>
		/// <param name="source">The source collection that is implementing <see cref="ISet{T}"/>.</param>
		/// <param name="other">The other enumerable.</param>
		/// <returns>The result of <see cref="ISet{T}.IsProperSubsetOf"/>.</returns>
		/// <remarks>
		/// <para>
		/// From <see href="https://msdn.microsoft.com/en-us/library/dd321100(v=vs.110).aspx"/>;
		/// Determines whether the current set is a proper (strict) subset of a specified collection.
		/// </para><para>
		/// If the current set is a proper subset of other, other must have at least one element that the current
		/// set does not have.
		/// </para><para>
		/// An empty set is a proper subset of any other collection.Therefore, this method returns true if the
		/// current set is empty, unless the other parameter is also an empty set.
		/// </para><para>
		/// This method always returns false if the current set has more or the same number of elements than other.
		/// </para>
		/// </remarks>
		public static bool IsProperSubsetOf<T>(ISet<T> source, IEnumerable<T> other) =>
			IsProperSubsetOf(source, other, EqualityComparer<T>.Default);

		/// <summary>
		/// Provides an implementation of <see cref="ISet{T}.IsProperSubsetOf" />.
		/// </summary>
		/// <typeparam name="T">The type of values in the collections.</typeparam>
		/// <param name="source">The source collection that is implementing <see cref="ISet{T}" />.</param>
		/// <param name="other">The other enumerable.</param>
		/// <param name="comparer">The comparer.</param>
		/// <returns>The result of <see cref="ISet{T}.IsProperSubsetOf" />.</returns>
		/// <remarks>
		/// <para>
		/// From <see href="https://msdn.microsoft.com/en-us/library/dd321100(v=vs.110).aspx" />;
		/// Determines whether the current set is a proper (strict) subset of a specified collection.
		/// </para>
		/// <para>
		/// If the current set is a proper subset of other, other must have at least one element that the current
		/// set does not have.
		/// </para>
		/// <para>
		/// An empty set is a proper subset of any other collection.Therefore, this method returns true if the
		/// current set is empty, unless the other parameter is also an empty set.
		/// </para>
		/// <para>
		/// This method always returns false if the current set has more or the same number of elements than other.
		/// </para></remarks>
		public static bool IsProperSubsetOf<T>(ISet<T> source, IEnumerable<T> other, IEqualityComparer<T> comparer)
		{
			Contracts.Requires.That(source != null);
			Contracts.Requires.That(comparer != null);
			ISetContracts.IsProperSubsetOf(other);

			return IsProperSubsetOf(source, other as HashSet<T> ?? new HashSet<T>(other, comparer));
		}

		#endregion

		#region IsProperSupersetOf (Overloaded)

		/// <summary>
		/// Provides an implementation of <see cref="ISet{T}.IsProperSupersetOf"/>.
		/// </summary>
		/// <typeparam name="T">The type of values in the collections.</typeparam>
		/// <param name="source">The source collection that is implementing <see cref="ISet{T}"/>.</param>
		/// <param name="other">The other enumerable.</param>
		/// <returns>The result of <see cref="ISet{T}.IsProperSupersetOf"/>.</returns>
		/// <remarks>
		/// <para>
		/// From <see href="https://msdn.microsoft.com/en-us/library/dd411711(v=vs.110).aspx"/>;
		/// Determines whether the current set is a proper (strict) superset of a specified collection.
		/// </para><para>
		/// If the current set is a proper superset of other, the current set must have at least one element
		/// that other does not have.
		/// </para><para>
		/// An empty set is a proper superset of any other collection.Therefore, this method returns true if
		/// the collection represented by the other parameter is empty, unless the current set is also empty.
		/// </para><para>
		/// This method always returns false if the number of elements in the current set is less than or equal
		/// to the number of elements in other.
		/// </para>
		/// </remarks>
		public static bool IsProperSupersetOf<T>(ISet<T> source, HashSet<T> other)
		{
			Contracts.Requires.That(source != null);
			ISetContracts.IsProperSupersetOf(other);

			if (source.Count <= other.Count)
			{
				return false;
			}

			return other.IsProperSubsetOf(source);
		}

		/// <summary>
		/// Provides an implementation of <see cref="ISet{T}.IsProperSupersetOf"/>.
		/// </summary>
		/// <typeparam name="T">The type of values in the collections.</typeparam>
		/// <param name="source">The source collection that is implementing <see cref="ISet{T}"/>.</param>
		/// <param name="other">The other enumerable.</param>
		/// <returns>The result of <see cref="ISet{T}.IsProperSupersetOf"/>.</returns>
		/// <remarks>
		/// <para>
		/// From <see href="https://msdn.microsoft.com/en-us/library/dd411711(v=vs.110).aspx"/>;
		/// Determines whether the current set is a proper (strict) superset of a specified collection.
		/// </para><para>
		/// If the current set is a proper superset of other, the current set must have at least one element
		/// that other does not have.
		/// </para><para>
		/// An empty set is a proper superset of any other collection.Therefore, this method returns true if
		/// the collection represented by the other parameter is empty, unless the current set is also empty.
		/// </para><para>
		/// This method always returns false if the number of elements in the current set is less than or equal
		/// to the number of elements in other.
		/// </para>
		/// </remarks>
		public static bool IsProperSupersetOf<T>(ISet<T> source, IEnumerable<T> other) =>
			IsProperSupersetOf(source, other, EqualityComparer<T>.Default);

		/// <summary>
		/// Provides an implementation of <see cref="ISet{T}.IsProperSupersetOf"/>.
		/// </summary>
		/// <typeparam name="T">The type of values in the collections.</typeparam>
		/// <param name="source">The source collection that is implementing <see cref="ISet{T}"/>.</param>
		/// <param name="other">The other enumerable.</param>
		/// <param name="comparer">The comparer.</param>
		/// <returns>The result of <see cref="ISet{T}.IsProperSupersetOf"/>.</returns>
		/// <remarks>
		/// <para>
		/// From <see href="https://msdn.microsoft.com/en-us/library/dd411711(v=vs.110).aspx"/>;
		/// Determines whether the current set is a proper (strict) superset of a specified collection.
		/// </para><para>
		/// If the current set is a proper superset of other, the current set must have at least one element
		/// that other does not have.
		/// </para><para>
		/// An empty set is a proper superset of any other collection.Therefore, this method returns true if
		/// the collection represented by the other parameter is empty, unless the current set is also empty.
		/// </para><para>
		/// This method always returns false if the number of elements in the current set is less than or equal
		/// to the number of elements in other.
		/// </para>
		/// </remarks>
		public static bool IsProperSupersetOf<T>(ISet<T> source, IEnumerable<T> other, IEqualityComparer<T> comparer)
		{
			Contracts.Requires.That(source != null);
			Contracts.Requires.That(comparer != null);
			ISetContracts.IsProperSupersetOf(other);

			return IsProperSupersetOf(source, other as HashSet<T> ?? new HashSet<T>(other, comparer));
		}

		#endregion

		#region IsSubsetOf (Overloaded)

		/// <summary>
		/// Provides an implementation of <see cref="ISet{T}.IsSubsetOf"/>.
		/// </summary>
		/// <typeparam name="T">The type of values in the collections.</typeparam>
		/// <param name="source">The source collection that is implementing <see cref="ISet{T}"/>.</param>
		/// <param name="other">The other enumerable.</param>
		/// <returns>The result of <see cref="ISet{T}.IsSubsetOf"/>.</returns>
		/// <remarks>
		/// <para>
		/// From <see href="https://msdn.microsoft.com/en-us/library/dd412074(v=vs.110).aspx"/>;
		/// Determines whether a set is a subset of a specified collection.
		/// </para><para>
		/// If other contains the same elements as the current set, the current set is still considered a
		/// subset of other.
		/// </para><para>
		/// This method always returns false if the current set has elements that are not in other.
		/// </para>
		/// </remarks>
		public static bool IsSubsetOf<T>(ISet<T> source, HashSet<T> other)
		{
			Contracts.Requires.That(source != null);
			ISetContracts.IsSubsetOf(other);

			if (source.Count > other.Count)
			{
				return false;
			}

			return source.All(other.Contains);
		}

		/// <summary>
		/// Provides an implementation of <see cref="ISet{T}.IsSubsetOf"/>.
		/// </summary>
		/// <typeparam name="T">The type of values in the collections.</typeparam>
		/// <param name="source">The source collection that is implementing <see cref="ISet{T}"/>.</param>
		/// <param name="other">The other enumerable.</param>
		/// <returns>The result of <see cref="ISet{T}.IsSubsetOf"/>.</returns>
		/// <remarks>
		/// <para>
		/// From <see href="https://msdn.microsoft.com/en-us/library/dd412074(v=vs.110).aspx"/>;
		/// Determines whether a set is a subset of a specified collection.
		/// </para><para>
		/// If other contains the same elements as the current set, the current set is still considered a
		/// subset of other.
		/// </para><para>
		/// This method always returns false if the current set has elements that are not in other.
		/// </para>
		/// </remarks>
		public static bool IsSubsetOf<T>(ISet<T> source, IEnumerable<T> other) =>
			IsSubsetOf(source, other, EqualityComparer<T>.Default);

		/// <summary>
		/// Provides an implementation of <see cref="ISet{T}.IsSubsetOf"/>.
		/// </summary>
		/// <typeparam name="T">The type of values in the collections.</typeparam>
		/// <param name="source">The source collection that is implementing <see cref="ISet{T}"/>.</param>
		/// <param name="other">The other enumerable.</param>
		/// <param name="comparer">The comparer.</param>
		/// <returns>The result of <see cref="ISet{T}.IsSubsetOf"/>.</returns>
		/// <remarks>
		/// <para>
		/// From <see href="https://msdn.microsoft.com/en-us/library/dd412074(v=vs.110).aspx"/>;
		/// Determines whether a set is a subset of a specified collection.
		/// </para><para>
		/// If other contains the same elements as the current set, the current set is still considered a
		/// subset of other.
		/// </para><para>
		/// This method always returns false if the current set has elements that are not in other.
		/// </para>
		/// </remarks>
		public static bool IsSubsetOf<T>(ISet<T> source, IEnumerable<T> other, IEqualityComparer<T> comparer)
		{
			Contracts.Requires.That(source != null);
			Contracts.Requires.That(comparer != null);
			ISetContracts.IsSubsetOf(other);

			return IsSubsetOf(source, other as HashSet<T> ?? new HashSet<T>(other, comparer));
		}

		#endregion

		#region IsSupersetOf

		/// <summary>
		/// Provides an implementation of <see cref="ISet{T}.IsSupersetOf"/>.
		/// </summary>
		/// <typeparam name="T">The type of values in the collections.</typeparam>
		/// <param name="source">The source collection that is implementing <see cref="ISet{T}"/>.</param>
		/// <param name="other">The other enumerable.</param>
		/// <returns>The result of <see cref="ISet{T}.IsSupersetOf"/>.</returns>
		/// <remarks>
		/// <para>
		/// From <see href="https://msdn.microsoft.com/en-us/library/dd382354(v=vs.110).aspx"/>;
		/// Determines whether the current set is a superset of a specified collection.
		/// </para><para>
		/// If other contains the same elements as the current set, the current set is still considered
		/// a superset of other.
		/// </para><para>
		/// This method always returns false if the current set has fewer elements than other.
		/// </para>
		/// </remarks>
		public static bool IsSupersetOf<T>(ISet<T> source, IEnumerable<T> other)
		{
			Contracts.Requires.That(source != null);
			ISetContracts.IsSupersetOf(other);

			ICollection<T> otherCollection = other as ICollection<T>;
			if (otherCollection != null && source.Count < otherCollection.Count)
			{
				return false;
			}

			return other.All(source.Contains);
		}

		#endregion
	}
}
