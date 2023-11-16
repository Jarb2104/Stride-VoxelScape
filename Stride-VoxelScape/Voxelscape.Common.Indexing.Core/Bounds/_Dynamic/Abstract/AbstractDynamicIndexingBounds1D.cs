using Voxelscape.Common.Indexing.Core.Enums;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;

namespace Voxelscape.Common.Indexing.Core.Bounds
{
	/// <summary>
	/// An abstract base class for extending to implement an <see cref="IDynamicIndexingBounds{Index1D}"/>.
	/// This base class provides implementations of methods that should not need re-implementing for most implementations.
	/// </summary>
	public abstract class AbstractDynamicIndexingBounds1D : IDynamicIndexingBounds<Index1D>
	{
		#region IDynamicIndexingBounds<Index1D> Members

		/// <inheritdoc />
		public int Rank
		{
			get { return 1; }
		}

		/// <inheritdoc />
		public virtual int CurrentLength
		{
			get
			{
				return this.GetCurrentLength(Axis1D.X);
			}
		}

		/// <inheritdoc />
		public virtual long CurrentLongLength
		{
			get
			{
				return this.GetCurrentLength(Axis1D.X);
			}
		}

		/// <inheritdoc />
		public virtual Index1D CurrentDimensions
		{
			get
			{
				return new Index1D(this.GetCurrentLength(Axis1D.X));
			}
		}

		/// <inheritdoc />
		public virtual Index1D CurrentLowerBounds
		{
			get
			{
				return new Index1D(this.GetCurrentLowerBound(Axis1D.X));
			}
		}

		/// <inheritdoc />
		public virtual Index1D CurrentUpperBounds
		{
			get
			{
				return new Index1D(this.GetCurrentUpperBound(Axis1D.X));
			}
		}

		/// <inheritdoc />
		public abstract int GetCurrentLength(int dimension);

		/// <inheritdoc />
		public abstract int GetCurrentLowerBound(int dimension);

		/// <inheritdoc />
		public abstract int GetCurrentUpperBound(int dimension);

		#endregion
	}
}
