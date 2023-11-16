using Voxelscape.Common.Indexing.Core.Enums;

/// <summary>
/// Provides extension methods for the <see cref="Orthant2D"/> enum.
/// </summary>
public static class OrthantExtensions2D
{
	/// <summary>
	/// Determines whether the specified orthant contains the specified flag or flags (all of them).
	/// This implementation is faster than the Enum.HasFlag method because it avoids boxing of values.
	/// </summary>
	/// <param name="orthant">The orthant to check for containing flags.</param>
	/// <param name="flag">The flag or combined flags to check for.</param>
	/// <returns>True if the orthant contains the flag(s); otherwise false.</returns>
	public static bool HasFlagEfficient(this Orthant2D orthant, Orthant2D flag)
	{
		return (orthant & flag) == flag;
	}

	/// <summary>
	/// Determines whether the specified orthant contains any of the specified flags.
	/// </summary>
	/// <param name="orthant">The orthant to check for containing flags.</param>
	/// <param name="flags">The flags to check for.</param>
	/// <returns>True if the orthant contains any of the flags; otherwise false.</returns>
	public static bool HasAnyFlag(this Orthant2D orthant, Orthant2D flags)
	{
		return (orthant & flags) != 0;
	}

	/// <summary>
	/// Determines whether the specified orthant contains only less than flags.
	/// </summary>
	/// <param name="orthant">The orthant to check.</param>
	/// <returns>True if the orthant contains only less than flags; otherwise false.</returns>
	public static bool IsAllLess(this Orthant2D orthant)
	{
		return orthant == Orthant2D.AllLess;
	}

	/// <summary>
	/// Determines whether the specified orthant contains only equal to flags.
	/// </summary>
	/// <param name="orthant">The orthant to check.</param>
	/// <returns>True if the orthant contains only equal to flags; otherwise false.</returns>
	public static bool IsAllEqual(this Orthant2D orthant)
	{
		return orthant == Orthant2D.AllEqual;
	}

	/// <summary>
	/// Determines whether the specified orthant contains only greater than flags.
	/// </summary>
	/// <param name="orthant">The orthant to check.</param>
	/// <returns>True if the orthant contains only greater than flags; otherwise false.</returns>
	public static bool IsAllGreater(this Orthant2D orthant)
	{
		return orthant == Orthant2D.AllGreater;
	}

	/// <summary>
	/// Determines whether the specified orthant contains only less than or equal to flags.
	/// </summary>
	/// <param name="orthant">The orthant to check.</param>
	/// <returns>True if the orthant contains only less than or equal to flags; otherwise false.</returns>
	public static bool IsAllLessOrEqual(this Orthant2D orthant)
	{
		return !(orthant == Orthant2D.None ||
			orthant.HasAnyFlag(Orthant2D.GreaterX | Orthant2D.GreaterY));
	}

	/// <summary>
	/// Determines whether the specified orthant contains only greater than or equal to flags.
	/// </summary>
	/// <param name="orthant">The orthant to check.</param>
	/// <returns>True if the orthant contains only greater than or equal to flags; otherwise false.</returns>
	public static bool IsAllGreaterOrEqual(this Orthant2D orthant)
	{
		return !(orthant == Orthant2D.None ||
			orthant.HasAnyFlag(Orthant2D.LessX | Orthant2D.LessY));
	}
}
