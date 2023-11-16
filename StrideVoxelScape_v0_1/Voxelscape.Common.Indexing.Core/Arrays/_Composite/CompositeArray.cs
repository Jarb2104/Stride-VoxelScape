using System.Collections.Generic;
using System.Linq;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Core.Arrays
{
	/// <summary>
	/// Provides factory methods for creating <see cref="IBoundedIndexable{Index3D, TValue}"/> instances by combining other
	/// <see cref="IBoundedIndexable{Index3D, TValue}"/> instances into composites.
	/// </summary>
	public static class CompositeArray
	{
		/// <summary>
		/// Creates an <see cref="IBoundedIndexable{Index3D, TValue}"/> by combing the specified instances into a 3 by 1 by 3 indexable.
		/// </summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="arrays">The indexable arrays to combine into a composite.</param>
		/// <returns>The composite indexable array.</returns>
		/// <remarks>
		/// The resulting composite is 3 times wider in both horizontal directions than it is tall. The (0, 0, 0,) origin of the
		/// composite is automatically aligned to the (0, 0, 0) origin of the center most indexable in the composite.
		/// </remarks>
		public static IBoundedIndexable<Index3D, T> Create<T>(IBoundedIndexable<Index3D, T>[,] arrays)
		{
			Contracts.Requires.That(arrays != null);
			Contracts.Requires.That(arrays.GetLength(0) == 3);
			Contracts.Requires.That(arrays.GetLength(1) == 3);
			Contracts.Requires.That(AreAllSameDimensionsAndZeroBounded(arrays));

			return new Composite3By3Array3D<T>(arrays);
		}

		/// <summary>
		/// Creates an <see cref="IBoundedIndexable{Index3D, TValue}"/> by combing the specified instances into a 3 by 3 by 3 indexable.
		/// </summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="arrays">The indexable arrays to combine into a composite.</param>
		/// <returns>The composite indexable array.</returns>
		/// <remarks>
		/// The (0, 0, 0,) origin of the composite is automatically aligned to the (0, 0, 0) origin of the center
		/// most indexable in the composite.
		/// </remarks>
		public static IBoundedIndexable<Index3D, T> Create<T>(IBoundedIndexable<Index3D, T>[,,] arrays)
		{
			Contracts.Requires.That(arrays != null);
			Contracts.Requires.That(arrays.GetLength(0) == 3);
			Contracts.Requires.That(arrays.GetLength(1) == 3);
			Contracts.Requires.That(arrays.GetLength(2) == 3);
			Contracts.Requires.That(AreAllSameDimensionsAndZeroBounded(arrays));

			return new Composite3By3By3Array3D<T>(arrays);
		}

		/// <summary>
		/// Creates an <see cref="IBoundedIndexable{Index3D, TValue}" /> by combing the specified instances into a composite indexable.
		/// </summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="arrays">The indexable arrays to combine into a composite.</param>
		/// <param name="originOffset">The origin offset for indexing into the composite.</param>
		/// <returns>The composite indexable array.</returns>
		public static IBoundedIndexable<Index3D, T> Create<T>(
			IBoundedIndexable<Index3D, T>[,,] arrays, Index3D originOffset)
		{
			Contracts.Requires.That(arrays != null);
			Contracts.Requires.That(AreAllSameDimensionsAndZeroBounded(arrays));

			return new CompositeArray3D<T>(arrays, originOffset);
		}

		/// <summary>
		/// Creates an <see cref="IBoundedReadOnlyIndexable{Index3D, TValue}" /> by combing the specified instances
		/// into a composite indexable.
		/// </summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="arrays">The indexable arrays to combine into a composite.</param>
		/// <param name="originOffset">The origin offset for indexing into the composite.</param>
		/// <returns>The composite indexable array.</returns>
		public static IBoundedReadOnlyIndexable<Index3D, T> Create<T>(
			IBoundedReadOnlyIndexable<Index3D, T>[,,] arrays, Index3D originOffset)
		{
			Contracts.Requires.That(arrays != null);
			Contracts.Requires.That(AreAllSameDimensionsAndZeroBounded(arrays));

			return new ReadOnlyCompositeArray3D<T>(arrays, originOffset);
		}

		/// <summary>
		/// Determines if all the indexable arrays have the same dimensions and that their lower bounds are <see cref="Index3D.Zero"/>.
		/// </summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="arrays">The indexable arrays to check.</param>
		/// <returns>True if all the indexable arrays have the same dimensions and bounds; otherwise false.</returns>
		public static bool AreAllSameDimensionsAndZeroBounded<T>(IBoundedReadOnlyIndexable<Index3D, T>[,] arrays)
		{
			return AreAllSameDimensionsAndZeroBoundedHelper(arrays.Cast<IBoundedReadOnlyIndexable<Index3D, T>>());
		}

		/// <summary>
		/// Determines if all the indexable arrays have the same dimensions and that their lower bounds are <see cref="Index3D.Zero"/>.
		/// </summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="arrays">The indexable arrays to check.</param>
		/// <returns>True if all the indexable arrays have the same dimensions and bounds; otherwise false.</returns>
		public static bool AreAllSameDimensionsAndZeroBounded<T>(IBoundedReadOnlyIndexable<Index3D, T>[,,] arrays)
		{
			return AreAllSameDimensionsAndZeroBoundedHelper(arrays.Cast<IBoundedReadOnlyIndexable<Index3D, T>>());
		}

		/// <summary>
		/// A helper method to determine if all the indexable arrays have the same dimensions and that their lower bounds are zero.
		/// </summary>
		/// <typeparam name="T">The type of the value.</typeparam>
		/// <param name="arrays">The indexable arrays to check.</param>
		/// <returns>True if all the indexable arrays have the same dimensions and bounds; otherwise false.</returns>
		private static bool AreAllSameDimensionsAndZeroBoundedHelper<T>(IEnumerable<IBoundedReadOnlyIndexable<Index3D, T>> arrays)
		{
			if (arrays == null)
			{
				return false;
			}

			IBoundedReadOnlyIndexable<Index3D, T> firstValue = arrays.FirstOrDefault();

			if (firstValue == null)
			{
				return false;
			}

			Index3D dimensions = firstValue.Dimensions;

			foreach (var value in arrays)
			{
				if (value == null ||
					value.Dimensions != dimensions ||
					value.LowerBounds != Index3D.Zero)
				{
					return false;
				}
			}

			return true;
		}
	}
}
