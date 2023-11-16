using System.Collections.Generic;
using System.Linq;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Collections
{
	/// <summary>
	///
	/// </summary>
	public static class ReadOnlyCollection
	{
		public static IReadOnlyCollection<T> Combine<T>(IEnumerable<IReadOnlyCollection<T>> collections)
		{
			Contracts.Requires.That(collections.AllAndSelfNotNull());

			return new ReadOnlyCollection<T>(
				collections.SelectMany(collection => collection),
				collections.Select(collection => collection.Count).Sum());
		}

		public static IReadOnlyCollection<T> CombineParams<T>(params IReadOnlyCollection<T>[] collections) =>
			Combine(collections);
	}
}
