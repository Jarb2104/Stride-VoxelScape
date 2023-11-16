using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Core.Bounds
{
	/// <summary>
	///
	/// </summary>
	public class IndexingBounds3D : AbstractIndexingBounds3D
	{
		public IndexingBounds3D(Index3D dimensions)
			: this(Index3D.Zero, dimensions)
		{
		}

		public IndexingBounds3D(Index3D lowerBounds, Index3D dimensions)
		{
			Contracts.Requires.That(dimensions.IsAllPositiveOrZero());

			this.LowerBounds = lowerBounds;
			this.Dimensions = dimensions;
			this.UpperBounds = lowerBounds + dimensions - new Index3D(1);
		}

		/// <inheritdoc />
		public override Index3D LowerBounds { get; }

		/// <inheritdoc />
		public override Index3D UpperBounds { get; }

		/// <inheritdoc />
		public override Index3D Dimensions { get; }

		/// <inheritdoc />
		public override int GetLength(int dimension)
		{
			IIndexingBoundsContracts.GetLength(this, dimension);

			return this.Dimensions[dimension];
		}

		/// <inheritdoc />
		public override int GetLowerBound(int dimension)
		{
			IIndexingBoundsContracts.GetLowerBound(this, dimension);

			return this.LowerBounds[dimension];
		}

		/// <inheritdoc />
		public override int GetUpperBound(int dimension)
		{
			IIndexingBoundsContracts.GetUpperBound(this, dimension);

			return this.UpperBounds[dimension];
		}
	}
}
