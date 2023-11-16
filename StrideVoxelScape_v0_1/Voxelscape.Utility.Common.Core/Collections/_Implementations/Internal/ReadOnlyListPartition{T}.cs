using System.Collections;
using System.Collections.Generic;
using Voxelscape.Utility.Common.Core.Mathematics;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Collections
{
	internal class ReadOnlyListPartition<T> : IReadOnlyList<T>
	{
		private readonly IReadOnlyList<T> list;

		private readonly int startIndex;

		public ReadOnlyListPartition(IReadOnlyList<T> list, int startIndex, int length)
		{
			Contracts.Requires.That(list != null);
			Contracts.Requires.That(length >= 0);
			Contracts.Requires.That(list.GetIndexRange().Contains(Range.FromLength(startIndex, length)));

			this.list = list;
			this.startIndex = startIndex;
			this.Count = length;
		}

		/// <inheritdoc />
		public int Count { get; }

		/// <inheritdoc />
		public T this[int index]
		{
			get
			{
				IReadOnlyListContracts.Indexer(this, index);

				return this.list[index + this.startIndex];
			}
		}

		/// <inheritdoc />
		public IEnumerator<T> GetEnumerator()
		{
			for (int index = this.startIndex; index < this.startIndex + this.Count; index++)
			{
				yield return this.list[index];
			}
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
}
