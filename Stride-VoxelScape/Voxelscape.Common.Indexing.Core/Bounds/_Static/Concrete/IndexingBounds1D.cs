using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Core.Bounds
{
	/// <summary>
	///
	/// </summary>
	public class IndexingBounds1D : AbstractIndexingBounds1D
	{
		public IndexingBounds1D(Index1D dimensions)
			: this(Index1D.Zero, dimensions)
		{
		}

		public IndexingBounds1D(Index1D lowerBounds, Index1D dimensions)
		{
			Contracts.Requires.That(dimensions.IsAllPositiveOrZero());

			this.LowerBounds = lowerBounds;
			this.Dimensions = dimensions;
			this.UpperBounds = lowerBounds + dimensions - new Index1D(1);
		}

		/// <inheritdoc />
		public override Index1D LowerBounds { get; }

		/// <inheritdoc />
		public override Index1D UpperBounds { get; }

		/// <inheritdoc />
		public override Index1D Dimensions { get; }

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
