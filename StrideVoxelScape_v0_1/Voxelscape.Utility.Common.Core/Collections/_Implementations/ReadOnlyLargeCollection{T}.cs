using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Collections
{
	public class ReadOnlyLargeCollection<T> : IReadOnlyLargeCollection<T>
	{
		private readonly IEnumerable<T> enumerable;

		public ReadOnlyLargeCollection(IEnumerable<T> enumerable, long count)
		{
			Contracts.Requires.That(enumerable != null);
			Contracts.Requires.That(count >= 0);
			Contracts.Requires.That(enumerable.LongCount() == count);

			this.enumerable = enumerable;
			this.Count = count;
		}

		/// <inheritdoc />
		public long Count { get; }

		/// <inheritdoc />
		public IEnumerator<T> GetEnumerator() => this.enumerable.GetEnumerator();

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
}
