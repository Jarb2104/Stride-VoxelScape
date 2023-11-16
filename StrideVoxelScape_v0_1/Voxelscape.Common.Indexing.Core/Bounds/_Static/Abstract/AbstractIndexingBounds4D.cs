using Voxelscape.Common.Indexing.Core.Enums;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;

namespace Voxelscape.Common.Indexing.Core.Bounds
{
	/// <summary>
	/// An abstract base class for extending to implement an <see cref="IIndexingBounds{Index4D}"/>.
	/// This base class provides implementations of methods that should never need re-implementing for any implementation.
	/// </summary>
	public abstract class AbstractIndexingBounds4D : IIndexingBounds<Index4D>
	{
		#region IIndexingBounds<Index4D> Members

		/// <inheritdoc />
		public int Rank => 4;

		/// <inheritdoc />
		public virtual int Length =>
			checked(this.Dimensions.X * this.Dimensions.Y * this.Dimensions.Z * this.Dimensions.W);

		/// <inheritdoc />
		public virtual long LongLength => this.Dimensions.MultiplyCoordinatesLong();

		/// <inheritdoc />
		public virtual Index4D Dimensions => new Index4D(
			this.GetLength(Axis4D.X),
			this.GetLength(Axis4D.Y),
			this.GetLength(Axis4D.Z),
			this.GetLength(Axis4D.W));

		/// <inheritdoc />
		public virtual Index4D LowerBounds => new Index4D(
			this.GetLowerBound(Axis4D.X),
			this.GetLowerBound(Axis4D.Y),
			this.GetLowerBound(Axis4D.Z),
			this.GetLowerBound(Axis4D.W));

		/// <inheritdoc />
		public virtual Index4D UpperBounds => new Index4D(
			this.GetUpperBound(Axis4D.X),
			this.GetUpperBound(Axis4D.Y),
			this.GetUpperBound(Axis4D.Z),
			this.GetUpperBound(Axis4D.W));

		/// <inheritdoc />
		public abstract int GetLength(int dimension);

		/// <inheritdoc />
		public abstract int GetLowerBound(int dimension);

		/// <inheritdoc />
		public abstract int GetUpperBound(int dimension);

		#endregion
	}
}
