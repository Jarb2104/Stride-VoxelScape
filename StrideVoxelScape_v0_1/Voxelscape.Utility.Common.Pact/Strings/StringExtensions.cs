using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for use with <see cref="string"/> class.
/// </summary>
public static class StringExtensions
{
	/// <summary>
	/// Determines whether the string is empty. This is more efficient then comparing to <see cref="string.Empty"/>.
	/// </summary>
	/// <param name="value">The string.</param>
	/// <returns>True if the string is empty, false otherwise.</returns>
	public static bool IsEmpty(this string value)
	{
		Contracts.Requires.That(value != null);

		return value.Length == 0;
	}

	/// <summary>
	/// Determines whether the specified string is null or an empty string.
	/// </summary>
	/// <param name="value">The string.</param>
	/// <returns>True if the string is null or empty, false otherwise.</returns>
	public static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value);

	/// <summary>
	/// Determines whether a specified string is null, empty, or consists only of white-space characters.
	/// </summary>
	/// <param name="value">The string.</param>
	/// <returns>True if the string is null, empty, or white-space, false otherwise.</returns>
	public static bool IsNullOrWhiteSpace(this string value) => string.IsNullOrWhiteSpace(value);

	/// <summary>
	/// Reports the zero-based index of the nth occurrence of the specified Unicode character in this string.
	/// </summary>
	/// <param name="source">The source string.</param>
	/// <param name="value">The character to seek.</param>
	/// <param name="nthOccurrence">
	/// The nth number of the occurrence to seek.
	/// For example, 1 find the first occurrence, 2 the second, and so on.
	/// </param>
	/// <returns>The zero-based index position of the nth value if that character is found, or -1 if it is not.</returns>
	public static int IndexOfNthOccurrence(this string source, char value, int nthOccurrence)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(nthOccurrence >= 1);

		int count = 0;

		for (int index = 0; index < source.Length; index++)
		{
			if (source[index] == value)
			{
				count++;
				if (count == nthOccurrence)
				{
					return index;
				}
			}
		}

		return -1;
	}
}
