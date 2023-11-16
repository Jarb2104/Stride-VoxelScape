using System;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Common.Indexing.Pact.Indices;

namespace Voxelscape.Common.Indexing.Pact.Rasterization
{
	/// <summary>
	/// A base class for extending when implementing <see cref="IRasterizableMask{TIndex, TValue}" />.
	/// </summary>
	/// <typeparam name="TIndex">The type of the index. This determines the dimensions of the mask.</typeparam>
	/// <typeparam name="TValue">The type of the value of the mask.</typeparam>
	public abstract class AbstractRasterizableMask<TIndex, TValue> : IRasterizableMask<TIndex, TValue>
		where TIndex : IIndex
	{
		#region IRasterizableMask<TIndex,TValue> Members

		/// <inheritdoc />
		public IBoundedIndexable<TIndex, TValue> Rasterize(
			float cellLength, TValue internalValue, TValue externalValue)
		{
			IRasterizableMaskContracts.Rasterize(cellLength, internalValue, externalValue);

			return this.Rasterize(cellLength, _ => internalValue, _ => externalValue);
		}

		/// <inheritdoc />
		public IBoundedIndexable<TIndex, TValue> Rasterize(
			float cellLength, Func<TValue> internalValueFactory, Func<TValue> externalValueFactory)
		{
			IRasterizableMaskContracts.Rasterize(cellLength, internalValueFactory, externalValueFactory);

			return this.Rasterize(cellLength, _ => internalValueFactory(), _ => externalValueFactory());
		}

		/// <inheritdoc />
		public abstract IBoundedIndexable<TIndex, TValue> Rasterize(
			float cellLength, Func<TIndex, TValue> internalValueFactory, Func<TIndex, TValue> externalValueFactory);

		#endregion
	}
}
