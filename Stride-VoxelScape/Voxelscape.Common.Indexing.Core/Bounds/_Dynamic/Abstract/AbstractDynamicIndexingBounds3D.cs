using Voxelscape.Common.Indexing.Core.Enums;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;

namespace Voxelscape.Common.Indexing.Core.Bounds
{
	/// <summary>
	/// An abstract base class for extending to implement an <see cref="IDynamicIndexingBounds{Index3D}"/>.
	/// This base class provides implementations of methods that should not need re-implementing for most implementations.
	/// </summary>
	public abstract class AbstractDynamicIndexingBounds3D : IDynamicIndexingBounds<Index3D>
	{
		#region IDynamicIndexingBounds<Index3D> Members

		/// <inheritdoc />
		public int Rank
		{
			get { return 3; }
		}

		/// <inheritdoc />
		public virtual int CurrentLength
		{
			get
			{
				checked
				{
					return
						this.GetCurrentLength(Axis3D.X) *
						this.GetCurrentLength(Axis3D.Y) *
						this.GetCurrentLength(Axis3D.Z);
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
						((long)this.GetCurrentLength(Axis3D.X)) *
						((long)this.GetCurrentLength(Axis3D.Y)) *
						((long)this.GetCurrentLength(Axis3D.Z));
				}
			}
		}

		/// <inheritdoc />
		public virtual Index3D CurrentDimensions
		{
			get
			{
				return new Index3D(
					this.GetCurrentLength(Axis3D.X),
					this.GetCurrentLength(Axis3D.Y),
					this.GetCurrentLength(Axis3D.Z));
			}
		}

		/// <inheritdoc />
		public virtual Index3D CurrentLowerBounds
		{
			get
			{
				return new Index3D(
					this.GetCurrentLowerBound(Axis3D.X),
					this.GetCurrentLowerBound(Axis3D.Y),
					this.GetCurrentLowerBound(Axis3D.Z));
			}
		}

		/// <inheritdoc />
		public virtual Index3D CurrentUpperBounds
		{
			get
			{
				return new Index3D(
					this.GetCurrentUpperBound(Axis3D.X),
					this.GetCurrentUpperBound(Axis3D.Y),
					this.GetCurrentUpperBound(Axis3D.Z));
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
