using System.Collections;
using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Collections
{
	public class CountingSet<T> : IEnumerable<KeyValuePair<T, int>>
	{
		private readonly DefaultDictionary<T, int> counts;

		public CountingSet()
			: this(null)
		{
		}

		public CountingSet(IEqualityComparer<T> comparer)
		{
			this.counts = new DefaultDictionary<T, int>(new Dictionary<T, int>(comparer));
		}

		public void Increment(T value)
		{
			Contracts.Requires.That(value != null);

			this.counts[value] = this.counts[value] + 1;
		}

		public void Decrement(T value)
		{
			Contracts.Requires.That(value != null);

			this.counts[value] = this.counts[value] - 1;
		}

		public int CountOf(T value) => this.counts[value];

		/// <inheritdoc />
		public IEnumerator<KeyValuePair<T, int>> GetEnumerator() => this.counts.GetEnumerator();

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
}
