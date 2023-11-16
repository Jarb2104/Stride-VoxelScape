using System.Collections;
using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Collections
{
	public class ReadOnlySet<T> : Pact.Collections.IReadOnlySet<T>
    {
		private readonly ISet<T> set;

		public ReadOnlySet(ISet<T> set)
		{
			Contracts.Requires.That(set != null);

			this.set = set;
		}

		/// <inheritdoc />
		public int Count => this.set.Count;

		/// <inheritdoc />
		public bool Contains(T value) => this.set.Contains(value);

		/// <inheritdoc />
		public IEnumerator<T> GetEnumerator() => this.set.GetEnumerator();

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
}
