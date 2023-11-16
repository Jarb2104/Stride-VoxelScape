using Voxelscape.Common.Indexing.Core.Enums;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;

namespace Voxelscape.Common.Indexing.Core.Bounds
{
	/// <summary>
	/// An abstract base class for extending to implement an <see cref="IIndexingBounds{Index1D}"/>.
	/// This base class provides implementations of methods that should never need re-implementing for any implementation.
	/// </summary>
	public abstract class AbstractIndexingBounds1D : IIndexingBounds<Index1D>
	{
		#region IIndexingBounds<Index1D> Members

		/// <inheritdoc />
		public int Rank => 1;

		/// <inheritdoc />
		public virtual int Length => this.GetLength(Axis1D.X);

		/// <inheritdoc />
		public virtual long LongLength => this.GetLength(Axis1D.X);

		/// <inheritdoc />
		public virtual Index1D Dimensions => new Index1D(this.GetLength(Axis1D.X));

		/// <inheritdoc />
		public virtual Index1D LowerBounds => new Index1D(this.GetLowerBound(Axis1D.X));

		/// <inheritdoc />
		public virtual Index1D UpperBounds => new Index1D(this.GetUpperBound(Axis1D.X));

		/// <inheritdoc />
		public abstract int GetLength(int dimension);

		/// <inheritdoc />
		public abstract int GetLowerBound(int dimension);

		/// <inheritdoc />
		public abstract int GetUpperBound(int dimension);

		#endregion
	}
}
