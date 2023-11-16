using System.Collections.Generic;

namespace Voxelscape.Utility.Common.Core.Collections
{
	/// <summary>
	///
	/// </summary>
	public static class CountedEnumerable
	{
		public static CountedEnumerable<T> New<T>(IReadOnlyCollection<T> values) =>
			new CountedEnumerable<T>(values);

		public static CountedEnumerable<T> New<T>(ICollection<T> values) =>
			new CountedEnumerable<T>(values);

		public static CountedEnumerable<T> New<T>(IEnumerable<T> values, int count) =>
			new CountedEnumerable<T>(values, count);
	}
}
