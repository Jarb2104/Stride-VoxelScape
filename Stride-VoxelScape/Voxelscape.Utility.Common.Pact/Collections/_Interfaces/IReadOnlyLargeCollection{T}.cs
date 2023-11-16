using System.Collections.Generic;

namespace Voxelscape.Utility.Common.Pact.Collections
{
	/// <summary>
	/// Represents a strongly-typed, read-only collection of elements that is potentially very large in quantity.
	/// </summary>
	/// <typeparam name="T">The type of the elements.</typeparam>
	public interface IReadOnlyLargeCollection<T> : IEnumerable<T>
	{
		/// <summary>
		/// Gets the number of elements in the collection.
		/// </summary>
		/// <value>
		/// The number of elements in the collection.
		/// </value>
		long Count { get; }
	}
}
