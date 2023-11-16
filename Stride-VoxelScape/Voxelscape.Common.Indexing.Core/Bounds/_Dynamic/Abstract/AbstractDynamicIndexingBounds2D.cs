using Voxelscape.Common.Indexing.Core.Enums;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;

namespace Voxelscape.Common.Indexing.Core.Bounds
{
	/// <summary>
	/// An abstract base class for extending to implement an <see cref="IDynamicIndexingBounds{Index2D}"/>.
	/// This base class provides implementations of methods that should not need re-implementing for most implementations.
	/// </summary>
	public abstract class AbstractDynamicIndexingBounds2D : IDynamicIndexingBounds<Index2D>
	{
		#region IDynamicIndexingBounds<Index2D> Members

		/// <inheritdoc />
		public int Rank
		{
			get { return 2; }
		}

		/// <inheritdoc />
		public virtual int CurrentLength
		{
			get
			{
				checked
				{
					return
						this.GetCurrentLength(Axis2D.X) *
						this.GetCurrentLength(Axis2D.Y);
				}
			}
		}

		/// <inheritdoc />
		public virtual long CurrentLongLength
		{
			get
			{
				checked
				{
					return
						((long)this.GetCurrentLength(Axis2D.X)) *
						((long)this.GetCurrentLength(Axis2D.Y));
				}
			}
		}

		/// <inheritdoc />
		public virtual Index2D CurrentDimensions
		{
			get
			{
				return new Index2D(
					this.GetCurrentLength(Axis2D.X),
					this.GetCurrentLength(Axis2D.Y));
			}
		}

		/// <inheritdoc />
		public virtual Index2D CurrentLowerBounds
		{
			get
			{
				return new Index2D(
					this.GetCurrentLowerBound(Axis2D.X),
					this.GetCurrentLowerBound(Axis2D.Y));
			}
		}

		/// <inheritdoc />
		public virtual Index2D CurrentUpperBounds
		{
			get
			{
				return new Index2D(
					this.GetCurrentUpperBound(Axis2D.X),
					this.GetCurrentUpperBound(Axis2D.Y));
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
