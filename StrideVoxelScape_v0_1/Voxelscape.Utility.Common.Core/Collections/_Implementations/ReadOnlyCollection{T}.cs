using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Collections
{
	public class ReadOnlyCollection<T> : IReadOnlyCollection<T>
	{
		private readonly IEnumerable<T> enumerable;

		public ReadOnlyCollection(IReadOnlyCollection<T> values)
		{
			Contracts.Requires.That(values != null);

			this.enumerable = values;
			this.Count = values.Count;
		}

		public ReadOnlyCollection(ICollection<T> values)
		{
			Contracts.Requires.That(values != null);

			this.enumerable = values;
			this.Count = values.Count;
		}

		public ReadOnlyCollection(IEnumerable<T> enumerable, int count)
		{
			Contracts.Requires.That(enumerable != null);
			Contracts.Requires.That(count >= 0);
			Contracts.Requires.That(enumerable.Count() == count);

			this.enumerable = enumerable;
			this.Count = count;
		}

		/// <inheritdoc />
		public int Count { get; }

		/// <inheritdoc />
		public IEnumerator<T> GetEnumerator() => this.enumerable.GetEnumerator();

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
}
