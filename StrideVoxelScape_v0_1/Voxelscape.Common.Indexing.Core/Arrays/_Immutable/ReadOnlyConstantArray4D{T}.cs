using System.Collections.Generic;
using Voxelscape.Common.Indexing.Core.Enumerables;
using Voxelscape.Common.Indexing.Core.Indices;

namespace Voxelscape.Common.Indexing.Core.Arrays
{
	public class ReadOnlyConstantArray4D<T> : ReadOnlyConstantArray<Index4D, T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ReadOnlyConstantArray4D{TValue}"/> class.
		/// </summary>
		/// <param name="dimensions">The dimensions of the indexable.</param>
		/// <param name="value">The value the indexable will always return.</param>
		public ReadOnlyConstantArray4D(Index4D dimensions, T value)
			: base(dimensions, value)
		{
		}

		/// <inheritdoc />
		public override Index4D LowerBounds => Index4D.Zero;

		/// <inheritdoc />
		public override Index4D UpperBounds => this.Dimensions - new Index4D(1);

		/// <inheritdoc />
		public override IEnumerator<KeyValuePair<Index4D, T>> GetEnumerator()
		{
			foreach (var index in Index.Range(this.LowerBounds, this.Dimensions))
			{
				yield return new KeyValuePair<Index4D, T>(index, this.ConstantValue);
			}
		}
	}
}
