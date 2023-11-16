using System.Collections.Generic;
using System.Linq;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Exceptions;

namespace Voxelscape.Common.Indexing.Core.Enumerables
{
	/// <summary>
	/// Provides a collection of helper methods for use with indices.
	/// </summary>
	public static class Index
	{
		#region Range

		/// <summary>
		/// Generates a sequence of indices that are contained within a specified range.
		/// </summary>
		/// <param name="startIndex">The starting index.</param>
		/// <param name="dimensions">The dimensions of the range to generate.</param>
		/// <returns>An enumerable sequence of the indices inside of the specified range.</returns>
		public static IEnumerable<Index1D> Range(Index1D startIndex, Index1D dimensions)
		{
			Contracts.Requires.That(dimensions.IsAllPositiveOrZero());

			Index1D lastIndex = startIndex + dimensions;

			for (int iX = startIndex.X; iX < lastIndex.X; iX++)
			{
				yield return new Index1D(iX);
			}
		}

		/// <summary>
		/// Generates a sequence of indices that are contained within a specified range.
		/// </summary>
		/// <param name="startIndex">The starting index.</param>
		/// <param name="dimensions">The dimensions of the range to generate.</param>
		/// <returns>An enumerable sequence of the indices inside of the specified range.</returns>
		public static IEnumerable<Index2D> Range(Index2D startIndex, Index2D dimensions)
		{
			Contracts.Requires.That(dimensions.IsAllPositiveOrZero());

			Index2D lastIndex = startIndex + dimensions;

			for (int iY = startIndex.Y; iY < lastIndex.Y; iY++)
			{
				for (int iX = startIndex.X; iX < lastIndex.X; iX++)
				{
					yield return new Index2D(iX, iY);
				}
			}
		}

		/// <summary>
		/// Generates a sequence of indices that are contained within a specified range.
		/// </summary>
		/// <param name="startIndex">The starting index.</param>
		/// <param name="dimensions">The dimensions of the range to generate.</param>
		/// <returns>An enumerable sequence of the indices inside of the specified range.</returns>
		public static IEnumerable<Index3D> Range(Index3D startIndex, Index3D dimensions)
		{
			Contracts.Requires.That(dimensions.IsAllPositiveOrZero());

			Index3D lastIndex = startIndex + dimensions;

			for (int iZ = startIndex.Z; iZ < lastIndex.Z; iZ++)
			{
				for (int iY = startIndex.Y; iY < lastIndex.Y; iY++)
				{
					for (int iX = startIndex.X; iX < lastIndex.X; iX++)
					{
						yield return new Index3D(iX, iY, iZ);
					}
				}
			}
		}

		/// <summary>
		/// Generates a sequence of indices that are contained within a specified range.
		/// </summary>
		/// <param name="startIndex">The starting index.</param>
		/// <param name="dimensions">The dimensions of the range to generate.</param>
		/// <returns>An enumerable sequence of the indices inside of the specified range.</returns>
		public static IEnumerable<Index4D> Range(Index4D startIndex, Index4D dimensions)
		{
			Contracts.Requires.That(dimensions.IsAllPositiveOrZero());

			Index4D lastIndex = startIndex + dimensions;

			for (int iW = startIndex.W; iW < lastIndex.W; iW++)
			{
				for (int iZ = startIndex.Z; iZ < lastIndex.Z; iZ++)
				{
					for (int iY = startIndex.Y; iY < lastIndex.Y; iY++)
					{
						for (int iX = startIndex.X; iX < lastIndex.X; iX++)
						{
							yield return new Index4D(iX, iY, iZ, iW);
						}
					}
				}
			}
		}

		#endregion

		#region OscillateRange

		public static IEnumerable<Index2D> OscillateRange(
			Index2D startIndex, Index2D dimensions, OscillationOrder2D order = OscillationOrder2D.XY)
		{
			Contracts.Requires.That(dimensions.IsAllPositiveOrZero());

			switch (order)
			{
				case OscillationOrder2D.XY: return OscillateRangeXY(startIndex, dimensions);
				case OscillationOrder2D.YX: return OscillateRangeYX(startIndex, dimensions);
				default: throw InvalidEnumArgument.CreateException(nameof(order), order);
			}
		}

		public static IEnumerable<Index3D> OscillateRange(
			Index3D startIndex, Index3D dimensions, OscillationOrder3D order = OscillationOrder3D.XYZ)
		{
			Contracts.Requires.That(dimensions.IsAllPositiveOrZero());

			switch (order)
			{
				case OscillationOrder3D.XYZ: return OscillateRangeXYZ(startIndex, dimensions);
				case OscillationOrder3D.XZY: return OscillateRangeXZY(startIndex, dimensions);
				case OscillationOrder3D.YXZ: return OscillateRangeYXZ(startIndex, dimensions);
				case OscillationOrder3D.YZX: return OscillateRangeYZX(startIndex, dimensions);
				case OscillationOrder3D.ZXY: return OscillateRangeZXY(startIndex, dimensions);
				case OscillationOrder3D.ZYX: return OscillateRangeZYX(startIndex, dimensions);
				default: throw InvalidEnumArgument.CreateException(nameof(order), order);
			}
		}

		#endregion

		#region Private OscillateRange 2D

		private static IEnumerable<Index2D> OscillateRangeXY(Index2D startIndex, Index2D dimensions)
		{
			Contracts.Requires.That(dimensions.IsAllPositiveOrZero());

			var xAxis = new OscillatingRange(startIndex.X, dimensions.X);

			foreach (int iY in Enumerable.Range(startIndex.Y, dimensions.Y))
			{
				foreach (int iX in xAxis)
				{
					yield return new Index2D(iX, iY);
				}
			}
		}

		private static IEnumerable<Index2D> OscillateRangeYX(Index2D startIndex, Index2D dimensions)
		{
			Contracts.Requires.That(dimensions.IsAllPositiveOrZero());

			var yAxis = new OscillatingRange(startIndex.Y, dimensions.Y);

			foreach (int iX in Enumerable.Range(startIndex.X, dimensions.X))
			{
				foreach (int iY in yAxis)
				{
					yield return new Index2D(iX, iY);
				}
			}
		}

		#endregion

		#region Private OscillateRange 3D

		private static IEnumerable<Index3D> OscillateRangeXYZ(Index3D startIndex, Index3D dimensions)
		{
			Contracts.Requires.That(dimensions.IsAllPositiveOrZero());

			var xAxis = new OscillatingRange(startIndex.X, dimensions.X);
			var yAxis = new OscillatingRange(startIndex.Y, dimensions.Y);

			foreach (int iZ in Enumerable.Range(startIndex.Z, dimensions.Z))
			{
				foreach (int iY in yAxis)
				{
					foreach (int iX in xAxis)
					{
						yield return new Index3D(iX, iY, iZ);
					}
				}
			}
		}

		private static IEnumerable<Index3D> OscillateRangeXZY(Index3D startIndex, Index3D dimensions)
		{
			Contracts.Requires.That(dimensions.IsAllPositiveOrZero());

			var xAxis = new OscillatingRange(startIndex.X, dimensions.X);
			var zAxis = new OscillatingRange(startIndex.Z, dimensions.Z);

			foreach (int iY in Enumerable.Range(startIndex.Y, dimensions.Y))
			{
				foreach (int iZ in zAxis)
				{
					foreach (int iX in xAxis)
					{
						yield return new Index3D(iX, iY, iZ);
					}
				}
			}
		}

		private static IEnumerable<Index3D> OscillateRangeYXZ(Index3D startIndex, Index3D dimensions)
		{
			Contracts.Requires.That(dimensions.IsAllPositiveOrZero());

			var xAxis = new OscillatingRange(startIndex.X, dimensions.X);
			var yAxis = new OscillatingRange(startIndex.Y, dimensions.Y);

			foreach (int iZ in Enumerable.Range(startIndex.Z, dimensions.Z))
			{
				foreach (int iX in xAxis)
				{
					foreach (int iY in yAxis)
					{
						yield return new Index3D(iX, iY, iZ);
					}
				}
			}
		}

		private static IEnumerable<Index3D> OscillateRangeYZX(Index3D startIndex, Index3D dimensions)
		{
			Contracts.Requires.That(dimensions.IsAllPositiveOrZero());

			var yAxis = new OscillatingRange(startIndex.Y, dimensions.Y);
			var zAxis = new OscillatingRange(startIndex.Z, dimensions.Z);

			foreach (int iX in Enumerable.Range(startIndex.X, dimensions.X))
			{
				foreach (int iZ in zAxis)
				{
					foreach (int iY in yAxis)
					{
						yield return new Index3D(iX, iY, iZ);
					}
				}
			}
		}

		private static IEnumerable<Index3D> OscillateRangeZXY(Index3D startIndex, Index3D dimensions)
		{
			Contracts.Requires.That(dimensions.IsAllPositiveOrZero());

			var xAxis = new OscillatingRange(startIndex.X, dimensions.X);
			var zAxis = new OscillatingRange(startIndex.Z, dimensions.Z);

			foreach (int iY in Enumerable.Range(startIndex.Y, dimensions.Y))
			{
				foreach (int iX in xAxis)
				{
					foreach (int iZ in zAxis)
					{
						yield return new Index3D(iX, iY, iZ);
					}
				}
			}
		}

		private static IEnumerable<Index3D> OscillateRangeZYX(Index3D startIndex, Index3D dimensions)
		{
			Contracts.Requires.That(dimensions.IsAllPositiveOrZero());

			var yAxis = new OscillatingRange(startIndex.Y, dimensions.Y);
			var zAxis = new OscillatingRange(startIndex.Z, dimensions.Z);

			foreach (int iX in Enumerable.Range(startIndex.X, dimensions.X))
			{
				foreach (int iY in yAxis)
				{
					foreach (int iZ in zAxis)
					{
						yield return new Index3D(iX, iY, iZ);
					}
				}
			}
		}

		#endregion
	}
}
