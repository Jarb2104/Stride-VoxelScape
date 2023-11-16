using Voxelscape.Common.Indexing.Core.Enums;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;

namespace Voxelscape.Common.Indexing.Core.Bounds
{
	/// <summary>
	/// An abstract base class for extending to implement an <see cref="IDynamicIndexingBounds{Index4D}"/>.
	/// This base class provides implementations of methods that should not need re-implementing for most implementations.
	/// </summary>
	public abstract class AbstractDynamicIndexingBounds4D : IDynamicIndexingBounds<Index4D>
	{
		#region IDynamicIndexingBounds<Index4D> Members

		/// <inheritdoc />
		public int Rank
		{
			get { return 4; }
		}

		/// <inheritdoc />
		public virtual int CurrentLength
		{
			get
			{
				checked
				{
					return
						this.GetCurrentLength(Axis4D.X) *
						this.GetCurrentLength(Axis4D.Y) *
						this.GetCurrentLength(Axis4D.Z) *
						this.GetCurrentLength(Axis4D.W);
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
						((long)this.GetCurrentLength(Axis4D.X)) *
						((long)this.GetCurrentLength(Axis4D.Y)) *
						((long)this.GetCurrentLength(Axis4D.Z)) *
						((long)this.GetCurrentLength(Axis4D.W));
				}
			}
		}

		/// <inheritdoc />
		public virtual Index4D CurrentDimensions
		{
			get
			{
				return new Index4D(
					this.GetCurrentLength(Axis4D.X),
					this.GetCurrentLength(Axis4D.Y),
					this.GetCurrentLength(Axis4D.Z),
					this.GetCurrentLength(Axis4D.W));
			}
		}

		/// <inheritdoc />
		public virtual Index4D CurrentLowerBounds
		{
			get
			{
				return new Index4D(
					this.GetCurrentLowerBound(Axis4D.X),
					this.GetCurrentLowerBound(Axis4D.Y),
					this.GetCurrentLowerBound(Axis4D.Z),
					this.GetCurrentLowerBound(Axis4D.W));
			}
		}

		/// <inheritdoc />
		public virtual Index4D CurrentUpperBounds
		{
			get
			{
				return new Index4D(
					this.GetCurrentUpperBound(Axis4D.X),
					this.GetCurrentUpperBound(Axis4D.Y),
					this.GetCurrentUpperBound(Axis4D.Z),
					this.GetCurrentUpperBound(Axis4D.W));
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
