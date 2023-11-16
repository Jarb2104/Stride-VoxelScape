using System.Collections.Generic;
using Voxelscape.Common.Indexing.Core.Enumerables;
using Voxelscape.Common.Indexing.Core.Indices;

namespace Voxelscape.Common.Indexing.Core.Arrays
{
	public class ConstantArray1D<T> : ConstantArray<Index1D, T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ConstantArray1D{TValue}"/> class.
		/// </summary>
		/// <param name="dimensions">The dimensions of the indexable.</param>
		/// <param name="value">The value the indexable will always return.</param>
		/// <param name="setThrowsException">
		/// True if attempting to set the value at an index should throw an exception,
		/// false to just ignore the attempt.
		/// </param>
		public ConstantArray1D(Index1D dimensions, T value, bool setThrowsException = true)
			: base(dimensions, value, setThrowsException)
		{
		}

		/// <inheritdoc />
		public override Index1D LowerBounds => Index1D.Zero;

		/// <inheritdoc />
		public override Index1D UpperBounds => this.Dimensions - new Index1D(1);

		/// <inheritdoc />
		public override IEnumerator<KeyValuePair<Index1D, T>> GetEnumerator()
		{
			foreach (var index in Index.Range(this.LowerBounds, this.Dimensions))
			{
				yield return new KeyValuePair<Index1D, T>(index, this.ConstantValue);
			}
		}
	}
}
