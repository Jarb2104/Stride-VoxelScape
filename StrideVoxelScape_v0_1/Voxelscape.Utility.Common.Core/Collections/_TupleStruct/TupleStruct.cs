namespace Voxelscape.Utility.Common.Core.Collections
{
	/// <summary>
	/// Provides factory methods for creating struct tuples. This removes repetitive generic declarations by using type inference.
	/// </summary>
	public static class TupleStruct
	{
		/// <summary>
		/// Creates an 1-tuple, or singleton.
		/// </summary>
		/// <typeparam name="T1">The type of the first value.</typeparam>
		/// <param name="item1">The first value.</param>
		/// <returns>The new tuple.</returns>
		public static TupleStruct<T1> Create<T1>(T1 item1) => new TupleStruct<T1>(item1);

		/// <summary>
		/// Creates an 2-tuple, or pair.
		/// </summary>
		/// <typeparam name="T1">The type of the first value.</typeparam>
		/// <typeparam name="T2">The type of the second value.</typeparam>
		/// <param name="item1">The first value.</param>
		/// <param name="item2">The second value.</param>
		/// <returns>The new tuple.</returns>
		public static TupleStruct<T1, T2> Create<T1, T2>(T1 item1, T2 item2) => new TupleStruct<T1, T2>(item1, item2);

		/// <summary>
		/// Creates an 3-tuple, or triple.
		/// </summary>
		/// <typeparam name="T1">The type of the first value.</typeparam>
		/// <typeparam name="T2">The type of the second value.</typeparam>
		/// <typeparam name="T3">The type of the third value.</typeparam>
		/// <param name="item1">The first value.</param>
		/// <param name="item2">The second value.</param>
		/// <param name="item3">The third value.</param>
		/// <returns>The new tuple.</returns>
		public static TupleStruct<T1, T2, T3> Create<T1, T2, T3>(
			T1 item1, T2 item2, T3 item3) => new TupleStruct<T1, T2, T3>(item1, item2, item3);

		/// <summary>
		/// Creates an 4-tuple, or quadruple.
		/// </summary>
		/// <typeparam name="T1">The type of the first value.</typeparam>
		/// <typeparam name="T2">The type of the second value.</typeparam>
		/// <typeparam name="T3">The type of the third value.</typeparam>
		/// <typeparam name="T4">The type of the forth value.</typeparam>
		/// <param name="item1">The first value.</param>
		/// <param name="item2">The second value.</param>
		/// <param name="item3">The third value.</param>
		/// <param name="item4">The forth value.</param>
		/// <returns>The new tuple.</returns>
		public static TupleStruct<T1, T2, T3, T4> Create<T1, T2, T3, T4>(
			T1 item1, T2 item2, T3 item3, T4 item4) => new TupleStruct<T1, T2, T3, T4>(item1, item2, item3, item4);

		/// <summary>
		/// Creates an 5-tuple, or quintuple.
		/// </summary>
		/// <typeparam name="T1">The type of the first value.</typeparam>
		/// <typeparam name="T2">The type of the second value.</typeparam>
		/// <typeparam name="T3">The type of the third value.</typeparam>
		/// <typeparam name="T4">The type of the forth value.</typeparam>
		/// <typeparam name="T5">The type of the fifth value.</typeparam>
		/// <param name="item1">The first value.</param>
		/// <param name="item2">The second value.</param>
		/// <param name="item3">The third value.</param>
		/// <param name="item4">The forth value.</param>
		/// <param name="item5">The fifth value.</param>
		/// <returns>The new tuple.</returns>
		public static TupleStruct<T1, T2, T3, T4, T5> Create<T1, T2, T3, T4, T5>(
			T1 item1, T2 item2, T3 item3, T4 item4, T5 item5) =>
			new TupleStruct<T1, T2, T3, T4, T5>(item1, item2, item3, item4, item5);

		/// <summary>
		/// Creates an 6-tuple, or sextuple.
		/// </summary>
		/// <typeparam name="T1">The type of the first value.</typeparam>
		/// <typeparam name="T2">The type of the second value.</typeparam>
		/// <typeparam name="T3">The type of the third value.</typeparam>
		/// <typeparam name="T4">The type of the forth value.</typeparam>
		/// <typeparam name="T5">The type of the fifth value.</typeparam>
		/// <typeparam name="T6">The type of the sixth value.</typeparam>
		/// <param name="item1">The first value.</param>
		/// <param name="item2">The second value.</param>
		/// <param name="item3">The third value.</param>
		/// <param name="item4">The forth value.</param>
		/// <param name="item5">The fifth value.</param>
		/// <param name="item6">The sixth value.</param>
		/// <returns>The new tuple.</returns>
		public static TupleStruct<T1, T2, T3, T4, T5, T6> Create<T1, T2, T3, T4, T5, T6>(
			T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6) =>
			new TupleStruct<T1, T2, T3, T4, T5, T6>(item1, item2, item3, item4, item5, item6);

		/// <summary>
		/// Creates an 7-tuple, or septuple.
		/// </summary>
		/// <typeparam name="T1">The type of the first value.</typeparam>
		/// <typeparam name="T2">The type of the second value.</typeparam>
		/// <typeparam name="T3">The type of the third value.</typeparam>
		/// <typeparam name="T4">The type of the forth value.</typeparam>
		/// <typeparam name="T5">The type of the fifth value.</typeparam>
		/// <typeparam name="T6">The type of the sixth value.</typeparam>
		/// <typeparam name="T7">The type of the seventh value.</typeparam>
		/// <param name="item1">The first value.</param>
		/// <param name="item2">The second value.</param>
		/// <param name="item3">The third value.</param>
		/// <param name="item4">The forth value.</param>
		/// <param name="item5">The fifth value.</param>
		/// <param name="item6">The sixth value.</param>
		/// <param name="item7">The seventh value.</param>
		/// <returns>The new tuple.</returns>
		public static TupleStruct<T1, T2, T3, T4, T5, T6, T7> Create<T1, T2, T3, T4, T5, T6, T7>(
			T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7) =>
			new TupleStruct<T1, T2, T3, T4, T5, T6, T7>(item1, item2, item3, item4, item5, item6, item7);

		/// <summary>
		/// Creates an 8-tuple, or octuple.
		/// </summary>
		/// <typeparam name="T1">The type of the first value.</typeparam>
		/// <typeparam name="T2">The type of the second value.</typeparam>
		/// <typeparam name="T3">The type of the third value.</typeparam>
		/// <typeparam name="T4">The type of the forth value.</typeparam>
		/// <typeparam name="T5">The type of the fifth value.</typeparam>
		/// <typeparam name="T6">The type of the sixth value.</typeparam>
		/// <typeparam name="T7">The type of the seventh value.</typeparam>
		/// <typeparam name="T8">The type of the eighth value.</typeparam>
		/// <param name="item1">The first value.</param>
		/// <param name="item2">The second value.</param>
		/// <param name="item3">The third value.</param>
		/// <param name="item4">The forth value.</param>
		/// <param name="item5">The fifth value.</param>
		/// <param name="item6">The sixth value.</param>
		/// <param name="item7">The seventh value.</param>
		/// <param name="item8">The eighth value.</param>
		/// <returns>The new tuple.</returns>
		public static TupleStruct<T1, T2, T3, T4, T5, T6, T7, T8> Create<T1, T2, T3, T4, T5, T6, T7, T8>(
			T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8) =>
			new TupleStruct<T1, T2, T3, T4, T5, T6, T7, T8>(item1, item2, item3, item4, item5, item6, item7, item8);
	}
}
