using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Exceptions;

namespace Voxelscape.Utility.Common.Core.Collections
{
	internal class ReadOnlyListComposite<T> : IReadOnlyList<T>
	{
		private readonly IEnumerable<IReadOnlyList<T>> lists;

		public ReadOnlyListComposite(params IReadOnlyList<T>[] lists)
			: this((IEnumerable<IReadOnlyList<T>>)lists)
		{
		}

		public ReadOnlyListComposite(IEnumerable<IReadOnlyList<T>> lists)
		{
			Contracts.Requires.That(lists.AllAndSelfNotNull());

			this.lists = lists;
		}

		/// <inheritdoc />
		public int Count => this.lists.Select(list => list.Count).Sum();

		/// <inheritdoc />
		public T this[int index]
		{
			get
			{
				IReadOnlyListContracts.Indexer(this, index);

				foreach (var list in this.lists)
				{
					if (index.IsIn(list.GetIndexRange()))
					{
						return list[index];
					}
					else
					{
						index -= list.Count;
					}
				}

				throw new UnreachableCodeException("Contracts should guarantee this line is never reached.");
			}
		}

		/// <inheritdoc />
		public IEnumerator<T> GetEnumerator()
		{
			foreach (var list in this.lists)
			{
				foreach (var value in list)
				{
					yield return value;
				}
			}
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
}
