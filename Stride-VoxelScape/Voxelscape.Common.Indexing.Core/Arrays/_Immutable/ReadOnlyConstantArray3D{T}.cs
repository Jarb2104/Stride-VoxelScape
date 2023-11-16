using System.Collections.Generic;
using Voxelscape.Common.Indexing.Core.Enumerables;
using Voxelscape.Common.Indexing.Core.Indices;

namespace Voxelscape.Common.Indexing.Core.Arrays
{
	public class ReadOnlyConstantArray3D<T> : ReadOnlyConstantArray<Index3D, T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ReadOnlyConstantArray3D{TValue}"/> class.
		/// </summary>
		/// <param name="dimensions">The dimensions of the indexable.</param>
		/// <param name="value">The value the indexable will always return.</param>
		public ReadOnlyConstantArray3D(Index3D dimensions, T value)
			: base(dimensions, value)
		{
		}

		/// <inheritdoc />
		public override Index3D LowerBounds => Index3D.Zero;

		/// <inheritdoc />
		public override Index3D UpperBounds => this.Dimensions - new Index3D(1);

		/// <inheritdoc />
		public override IEnumerator<KeyValuePair<Index3D, T>> GetEnumerator()
		{
			foreach (var index in Index.Range(this.LowerBounds, this.Dimensions))
			{
				yield return new KeyValuePair<Index3D, T>(index, this.ConstantValue);
			}
		}
	}
}
