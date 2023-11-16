using System.Collections;
using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Pact.Collections
{
	internal class ReadOnlyListWrapper<T> : IReadOnlyList<T>
	{
		private readonly IList<T> list;

		public ReadOnlyListWrapper(IList<T> list)
		{
			Contracts.Requires.That(list != null);

			this.list = list;
		}

		/// <inheritdoc />
		public int Count => this.list.Count;

		/// <inheritdoc />
		public T this[int index]
		{
			get
			{
				IReadOnlyListContracts.Indexer(this, index);

				return this.list[index];
			}
		}

		/// <inheritdoc />
		public IEnumerator<T> GetEnumerator() => this.list.GetEnumerator();

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
}
