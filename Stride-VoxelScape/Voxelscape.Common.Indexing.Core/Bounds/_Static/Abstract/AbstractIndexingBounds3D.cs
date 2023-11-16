using Voxelscape.Common.Indexing.Core.Enums;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;

namespace Voxelscape.Common.Indexing.Core.Bounds
{
	/// <summary>
	/// An abstract base class for extending to implement an <see cref="IIndexingBounds{Index3D}"/>.
	/// This base class provides implementations of methods that should never need re-implementing for any implementation.
	/// </summary>
	public abstract class AbstractIndexingBounds3D : IIndexingBounds<Index3D>
	{
		#region IIndexingBounds<Index3D> Members

		/// <inheritdoc />
		public int Rank => 3;

		/// <inheritdoc />
		public virtual int Length => checked(this.Dimensions.X * this.Dimensions.Y * this.Dimensions.Z);

		/// <inheritdoc />
		public virtual long LongLength => this.Dimensions.MultiplyCoordinatesLong();

		/// <inheritdoc />
		public virtual Index3D Dimensions =>
			new Index3D(this.GetLength(Axis3D.X), this.GetLength(Axis3D.Y), this.GetLength(Axis3D.Z));

		/// <inheritdoc />
		public virtual Index3D LowerBounds =>
			new Index3D(this.GetLowerBound(Axis3D.X), this.GetLowerBound(Axis3D.Y), this.GetLowerBound(Axis3D.Z));

		/// <inheritdoc />
		public virtual Index3D UpperBounds =>
			new Index3D(this.GetUpperBound(Axis3D.X), this.GetUpperBound(Axis3D.Y), this.GetUpperBound(Axis3D.Z));

		/// <inheritdoc />
		public abstract int GetLength(int dimension);

		/// <inheritdoc />
		public abstract int GetLowerBound(int dimension);

		/// <inheritdoc />
		public abstract int GetUpperBound(int dimension);

		#endregion
	}
}
