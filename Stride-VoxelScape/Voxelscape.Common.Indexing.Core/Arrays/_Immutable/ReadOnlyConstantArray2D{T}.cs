using System.Collections.Generic;
using Voxelscape.Common.Indexing.Core.Enumerables;
using Voxelscape.Common.Indexing.Core.Indices;

namespace Voxelscape.Common.Indexing.Core.Arrays
{
	public class ReadOnlyConstantArray2D<T> : ReadOnlyConstantArray<Index2D, T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ReadOnlyConstantArray2D{TValue}"/> class.
		/// </summary>
		/// <param name="dimensions">The dimensions of the indexable.</param>
		/// <param name="value">The value the indexable will always return.</param>
		public ReadOnlyConstantArray2D(Index2D dimensions, T value)
			: base(dimensions, value)
		{
		}

		/// <inheritdoc />
		public override Index2D LowerBounds => Index2D.Zero;

		/// <inheritdoc />
		public override Index2D UpperBounds => this.Dimensions - new Index2D(1);

		/// <inheritdoc />
		public override IEnumerator<KeyValuePair<Index2D, T>> GetEnumerator()
		{
			foreach (var index in Index.Range(this.LowerBounds, this.Dimensions))
			{
				yield return new KeyValuePair<Index2D, T>(index, this.ConstantValue);
			}
		}
	}
}
