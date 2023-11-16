using System.Runtime.CompilerServices;

/// <summary>
/// Provides extension methods for <see cref="object"/>'s base methods.
/// </summary>
public static class ObjectExtensions
{
	/// <summary>
	/// Performs a null safe equality comparison.
	/// </summary>
	/// <typeparam name="T1">The type of the first value to check.</typeparam>
	/// <typeparam name="T2">The type of the second value to check.</typeparam>
	/// <param name="lhs">The left hand side value.</param>
	/// <param name="rhs">The right hand side value.</param>
	/// <returns>True if the two objects are equal or both null, false otherwise.</returns>
	public static bool EqualsNullSafe<T1, T2>(this T1 lhs, T2 rhs)
	{
		if (lhs == null)
		{
			return rhs == null;
		}
		else
		{
			return lhs.Equals(rhs);
		}
	}

	/// <summary>
	/// Performs a null safe reference equality or identity check.
	/// </summary>
	/// <typeparam name="T1">The type of the first reference to compare.</typeparam>
	/// <typeparam name="T2">The type of the second reference to compare.</typeparam>
	/// <param name="lhs">The left hand side value.</param>
	/// <param name="rhs">The right hand side value.</param>
	/// <returns>
	/// True if the two objects are the same object or both are null, false otherwise.
	/// </returns>
	/// <remarks>
	/// <para>
	/// Unlike the Equals method and the equality operator, the EqualsByReferenceNullSafe method cannot be overridden.
	/// Because of this, if you want to test two object references for equality and you are unsure about the implementation
	/// of the Equals method, you can call the <see cref="EqualsByReferenceNullSafe"/> method.
	/// </para>
	/// <para>
	/// However, the return value of the <see cref="EqualsByReferenceNullSafe"/> method may appear to be anomalous in these
	/// two scenarios: when comparing value types and when comparing strings. See
	/// <see href="https://msdn.microsoft.com/en-us/library/system.object.referenceequals%28v=vs.110%29.aspx">this MSDN article</see>
	/// for more information on the problem.
	/// </para></remarks>
	public static bool EqualsByReferenceNullSafe<T1, T2>(this T1 lhs, T2 rhs)
		where T1 : class
		where T2 : class => ReferenceEquals(lhs, rhs);

	/// <summary>
	/// Gets the hash code of an object by reference or identity only.
	/// </summary>
	/// <typeparam name="T">The type of the reference.</typeparam>
	/// <param name="obj">The object.</param>
	/// <returns>The hash code.</returns>
	/// <remarks>
	/// This method always calls the Object.GetHashCode method non-virtually, even if the object's type has overridden
	/// the Object.GetHashCode method. Therefore, using this method might differ from calling GetHashCode directly on
	/// the object with the Object.GetHashCode method.
	/// </remarks>
	public static int GetHashCodeByReferenceNullSafe<T>(this T obj)
		where T : class => RuntimeHelpers.GetHashCode(obj);

	/// <summary>
	/// Gets the hash code of an object if it is not null, otherwise returns 0.
	/// </summary>
	/// <typeparam name="T">The type of the value.</typeparam>
	/// <param name="value">The value.</param>
	/// <returns>The hash code if the object is not null; 0 otherwise.</returns>
	public static int GetHashCodeNullSafe<T>(this T value) => value?.GetHashCode() ?? 0;

	/// <summary>
	/// Gets the hash code of an object if it is not null, otherwise returns a specified null hash code value.
	/// </summary>
	/// <typeparam name="T">The type of the value.</typeparam>
	/// <param name="value">The value.</param>
	/// <param name="nullHashcode">The null hash code fallback value.</param>
	/// <returns>The hash code if the object is not null; <paramref name="nullHashcode"/> otherwise.</returns>
	public static int GetHashCodeNullSafe<T>(this T value, int nullHashcode) => value?.GetHashCode() ?? nullHashcode;

	/// <summary>
	/// Gets the string representation of an object if it is not null, otherwise returns the empty string.
	/// </summary>
	/// <typeparam name="T">The type of the value.</typeparam>
	/// <param name="value">The value.</param>
	/// <returns>The string representation of the object if it is not null; the empty string otherwise.</returns>
	public static string ToStringNullSafe<T>(this T value) => value?.ToString() ?? string.Empty;

	/// <summary>
	/// Gets the string representation of an object if it is not null, otherwise returns a specified null string.
	/// </summary>
	/// <typeparam name="T">The type of the value.</typeparam>
	/// <param name="value">The value.</param>
	/// <param name="nullString">The null string fallback value.</param>
	/// <returns>The string representation of the object if it is not null; <paramref name="nullString"/> otherwise.</returns>
	public static string ToStringNullSafe<T>(this T value, string nullString) => value?.ToString() ?? nullString;
}
