using System;

namespace Voxelscape.Utility.Common.Core.Conversions
{
	/// <summary>
	/// Provides utilities for converting types.
	/// </summary>
	public static class ConversionUtilities
	{
		/// <summary>
		/// Determines whether the specified source type is implicitly convertible to the specified result type.
		/// </summary>
		/// <typeparam name="TSource">The type of the source.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <returns>True if the source type can be implicitly converted to the result type, otherwise false.</returns>
		public static bool IsImplicitlyConvertibleTo<TSource, TResult>()
			where TResult : struct, IComparable, IFormattable, IConvertible, IComparable<TResult>, IEquatable<TResult> =>
			typeof(TSource).IsImplicitlyConvertibleTo(typeof(TResult));
	}
}
