using Voxelscape.Common.Indexing.Pact.Indices;

namespace Voxelscape.Common.Indexing.Pact.Bounds
{
	/// <summary>
	/// An abstract base class for extending to implement an <see cref="IIndexingBounds{TIndex}"/>.
	/// This base class provides implementations of methods that should never need re-implementing for any implementation.
	/// </summary>
	/// <typeparam name="TIndex">The type of the index used to access values.</typeparam>
	public abstract class AbstractIndexingBounds<TIndex> : IIndexingBounds<TIndex>
		where TIndex : IIndex
	{
		#region IIndexingBounds<TIndex> Members

		/// <inheritdoc />
		public abstract int Rank
		{
			get;
		}

		/// <inheritdoc />
		public virtual int Length
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
						result *= this.GetLength(dimension);
					}

					return result;
				}
			}
		}

		/// <inheritdoc />
		public virtual long LongLength
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
						result *= this.GetLength(dimension);
					}

					return result;
				}
			}
		}

		/// <inheritdoc />
		public abstract TIndex Dimensions
		{
			get;
		}

		/// <inheritdoc />
		public abstract TIndex LowerBounds
		{
			get;
		}

		/// <inheritdoc />
		public abstract TIndex UpperBounds
		{
			get;
		}

		/// <inheritdoc />
		public abstract int GetLength(int dimension);

		/// <inheritdoc />
		public abstract int GetLowerBound(int dimension);

		/// <inheritdoc />
		public abstract int GetUpperBound(int dimension);

		#endregion
	}
}
