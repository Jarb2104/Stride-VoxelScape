using Voxelscape.Common.Indexing.Core.Enums;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;

namespace Voxelscape.Common.Indexing.Core.Bounds
{
	/// <summary>
	/// An abstract base class for extending to implement an <see cref="IIndexingBounds{Index2D}"/>.
	/// This base class provides implementations of methods that should never need re-implementing for any implementation.
	/// </summary>
	public abstract class AbstractIndexingBounds2D : IIndexingBounds<Index2D>
	{
		#region IIndexingBounds<Index2D> Members

		/// <inheritdoc />
		public int Rank => 2;

		/// <inheritdoc />
		public virtual int Length => checked(this.Dimensions.X * this.Dimensions.Y);

		/// <inheritdoc />
		public virtual long LongLength => this.Dimensions.MultiplyCoordinatesLong();

		/// <inheritdoc />
		public virtual Index2D Dimensions =>
			new Index2D(this.GetLength(Axis2D.X), this.GetLength(Axis2D.Y));

		/// <inheritdoc />
		public virtual Index2D LowerBounds =>
			new Index2D(this.GetLowerBound(Axis2D.X), this.GetLowerBound(Axis2D.Y));

		/// <inheritdoc />
		public virtual Index2D UpperBounds =>
			new Index2D(this.GetUpperBound(Axis2D.X), this.GetUpperBound(Axis2D.Y));

		/// <inheritdoc />
		public abstract int GetLength(int dimension);

		/// <inheritdoc />
		public abstract int GetLowerBound(int dimension);

		/// <inheritdoc />
		public abstract int GetUpperBound(int dimension);

		#endregion
	}
}
