using System.Collections;
using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Pact.Collections
{
	internal class ReadOnlyCollectionWrapper<T> : IReadOnlyCollection<T>
	{
		private readonly ICollection<T> collection;

		public ReadOnlyCollectionWrapper(ICollection<T> collection)
		{
			Contracts.Requires.That(collection != null);

			this.collection = collection;
		}

		/// <inheritdoc />
		public int Count => this.collection.Count;

		/// <inheritdoc />
		public IEnumerator<T> GetEnumerator() => this.collection.GetEnumerator();

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
}
