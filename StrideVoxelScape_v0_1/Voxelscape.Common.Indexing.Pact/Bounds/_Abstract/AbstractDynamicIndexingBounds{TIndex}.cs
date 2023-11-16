using Voxelscape.Common.Indexing.Pact.Indices;

namespace Voxelscape.Common.Indexing.Pact.Bounds
{
	/// <summary>
	/// An abstract base class for extending to implement an <see cref="IDynamicIndexingBounds{TIndex}"/>.
	/// This base class provides implementations of methods that should not need re-implementing for most implementations.
	/// </summary>
	/// <typeparam name="TIndex">The type of the index used to access values.</typeparam>
	public abstract class AbstractDynamicIndexingBounds<TIndex> : IDynamicIndexingBounds<TIndex>
		where TIndex : IIndex
	{
		#region IDynamicIndexingBounds<TIndex> Members

		/// <inheritdoc />
		public abstract int Rank
		{
			get;
		}

		/// <inheritdoc />
		public int CurrentLength
		{
			get
			{
				if (this.Rank == 0)
				{
					return 0;
				}

				checked
				{
					int result = 1;
					for (int dimension = 0; dimension < this.Rank; dimension++)
					{
						result *= this.GetCurrentLength(dimension);
					}

					return result;
				}
			}
		}

		/// <inheritdoc />
		public long CurrentLongLength
		{
			get
			{
				if (this.Rank == 0)
				{
					return 0;
				}

				checked
				{
					long result = 1;
					for (int dimension = 0; dimension < this.Rank; dimension++)
					{
						result *= this.GetCurrentLength(dimension);
					}

					return result;
				}
			}
		}

		/// <inheritdoc />
		public abstract TIndex CurrentDimensions
		{
			get;
		}

		/// <inheritdoc />
		public abstract TIndex CurrentLowerBounds
		{
			get;
		}

		/// <inheritdoc />
		public abstract TIndex CurrentUpperBounds
		{
			get;
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
