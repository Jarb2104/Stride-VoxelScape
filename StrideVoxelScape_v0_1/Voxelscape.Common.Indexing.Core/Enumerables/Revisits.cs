namespace Voxelscape.Common.Indexing.Core.Enumerables
{
	/// <summary>
	/// An enumeration that determines whether or not to re-yield previously enumerated values again.
	/// </summary>
	public enum Revisits
	{
		/// <summary>
		/// Yield already previously yielded values, without using a cached value. This matters if the values
		/// could have possible changed since they were previously yielded, for example; if the values are
		/// modified as part of the iteration.
		/// </summary>
		Yield,

		/// <summary>
		/// Yield a cached copy of the enumerated value. This can potentially save on performance if retrieving the
		/// values from the indexable is expensive, for example; if the indexable is a tree structure. However, this
		/// should only be used it the values aren't modified as part of the iteration.
		/// </summary>
		YieldCached,

		/// <summary>
		/// Don't yield or cache already previously yielded values.
		/// </summary>
		DontYield,
	}
}
