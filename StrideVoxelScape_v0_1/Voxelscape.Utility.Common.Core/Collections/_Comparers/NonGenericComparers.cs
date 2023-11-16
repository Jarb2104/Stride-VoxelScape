using System.Collections;
using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Collections
{
	/// <summary>
	/// Provides factory methods to wrap existing generic type comparers in their non generic counterparts in order to
	/// support older non generic APIs. If you can modify the types, prefer subclassing <see cref="EqualityComparer{T}"/>
	/// and <see cref="Comparer{T}"/> instead of using this factory to wrap your comparer.
	/// </summary>
	public static class NonGenericComparers
	{
		#region Wrap Methods

		/// <summary>
		/// Wraps the specified <see cref="IEqualityComparer{T}"/> in an <see cref="IEqualityComparer"/> if it doesn't already
		/// implement <see cref="IEqualityComparer"/>. If it does already implement the non generic interface then the original
		/// comparer is returned.
		/// </summary>
		/// <typeparam name="T">The type of value the comparer compares.</typeparam>
		/// <param name="comparer">The comparer to wrap.</param>
		/// <returns>
		/// A wrapped <see cref="IEqualityComparer{T}"/> or the original comparer if it already implements
		/// <see cref="IEqualityComparer"/>.
		/// </returns>
		public static IEqualityComparer Wrap<T>(IEqualityComparer<T> comparer)
		{
			Contracts.Requires.That(comparer != null);

			IEqualityComparer nongenericComparer = comparer as IEqualityComparer;
			if (nongenericComparer != null)
			{
				return nongenericComparer;
			}
			else
			{
				return new EqualityComparerWrapper<T>(comparer);
			}
		}

		/// <summary>
		/// Wraps the specified <see cref="IComparer{T}"/> in an <see cref="IComparer"/> if it doesn't already
		/// implement <see cref="IComparer"/>. If it does already implement the non generic interface then the original
		/// comparer is returned.
		/// </summary>
		/// <typeparam name="T">The type of value the comparer compares.</typeparam>
		/// <param name="comparer">The comparer to wrap.</param>
		/// <returns>
		/// A wrapped <see cref="IComparer{T}"/> or the original comparer if it already implements <see cref="IComparer"/>.
		/// </returns>
		public static IComparer Wrap<T>(IComparer<T> comparer)
		{
			Contracts.Requires.That(comparer != null);

			IComparer nongenericComparer = comparer as IComparer;
			if (nongenericComparer != null)
			{
				return nongenericComparer;
			}
			else
			{
				return new ComparerWrapper<T>(comparer);
			}
		}

		#endregion

		#region Private Classes

		/// <summary>
		/// Wraps an <see cref="IEqualityComparer{T}"/> to add the non generic counterpart, <see cref="IEqualityComparer"/>.
		/// </summary>
		/// <typeparam name="T">The type of value the comparer compares.</typeparam>
		private class EqualityComparerWrapper<T> : EqualityComparer<T>
		{
			/// <summary>
			/// The wrapped generic comparer.
			/// </summary>
			private readonly IEqualityComparer<T> comparer;

			/// <summary>
			/// Initializes a new instance of the <see cref="EqualityComparerWrapper{T}"/> class.
			/// </summary>
			/// <param name="comparer">The generic comparer to wrap.</param>
			public EqualityComparerWrapper(IEqualityComparer<T> comparer)
			{
				Contracts.Requires.That(comparer != null);

				this.comparer = comparer;
			}

			#region Implementing Abstract EqualityComparer<T> Members

			/// <inheritdoc />
			public override bool Equals(T x, T y)
			{
				return this.comparer.Equals(x, y);
			}

			/// <inheritdoc />
			public override int GetHashCode(T obj)
			{
				IEqualityComparerContracts.GetHashCode(obj);

				return this.comparer.GetHashCode(obj);
			}

			#endregion
		}

		/// <summary>
		/// Wraps an <see cref="IComparer{T}"/> to add the non generic counterpart, <see cref="IComparer"/>.
		/// </summary>
		/// <typeparam name="T">The type of value the comparer compares.</typeparam>
		private class ComparerWrapper<T> : Comparer<T>
		{
			/// <summary>
			/// The wrapped generic comparer.
			/// </summary>
			private readonly IComparer<T> comparer;

			/// <summary>
			/// Initializes a new instance of the <see cref="ComparerWrapper{T}"/> class.
			/// </summary>
			/// <param name="comparer">The generic comparer to wrap.</param>
			public ComparerWrapper(IComparer<T> comparer)
			{
				Contracts.Requires.That(comparer != null);

				this.comparer = comparer;
			}

			#region Implementing Abstract Comparer<T> Members

			/// <inheritdoc />
			public override int Compare(T x, T y)
			{
				return this.comparer.Compare(x, y);
			}

			#endregion
		}

		#endregion
	}
}
