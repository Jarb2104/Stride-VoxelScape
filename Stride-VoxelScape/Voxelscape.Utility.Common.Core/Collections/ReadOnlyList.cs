using System;
using System.Collections.Generic;

namespace Voxelscape.Utility.Common.Core.Collections
{
	/// <summary>
	///
	/// </summary>
	/// <remarks>
	/// All of these classes function by 'reading through' from a wrapper to the source lists.
	/// They do not do bulk copying of values to newly created lists.
	/// </remarks>
	public static class ReadOnlyList
	{
		public static IReadOnlyList<T> Empty<T>() => ArrayUtilities.Empty<T>();

		public static IReadOnlyList<TResult> Convert<TSource, TResult>(
			IReadOnlyList<TSource> source, Converter<TSource, TResult> converter) =>
			new ReadOnlyListConverter<TSource, TResult>(source, converter);

		public static IReadOnlyList<T> Combine<T>(IEnumerable<IReadOnlyList<T>> lists) =>
			new ReadOnlyListComposite<T>(lists);

		public static IReadOnlyList<T> CombineParams<T>(params IReadOnlyList<T>[] lists) =>
			new ReadOnlyListComposite<T>(lists);

		public static IReadOnlyList<T> Partition<T>(IReadOnlyList<T> list, int startIndex, int length) =>
			new ReadOnlyListPartition<T>(list, startIndex, length);
	}
}
