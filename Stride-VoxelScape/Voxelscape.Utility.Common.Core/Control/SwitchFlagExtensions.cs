using Voxelscape.Utility.Common.Core.Control;

/// <summary>
/// Provides extension methods for the <see cref="SwitchFlagExtensions"/> class.
/// </summary>
public static class SwitchFlagExtensions
{
	/// <summary>
	/// Determines whether the current switch flags value contains the specified flag or flags (all of them).
	/// This implementation is faster than the Enum.HasFlag method because it avoids boxing of values.
	/// </summary>
	/// <param name="current">The current value to check for containing flags.</param>
	/// <param name="flag">The flag or combined flags to check for.</param>
	/// <returns>True if the current value contains the flag(s); otherwise false.</returns>
	public static bool HasFlagEfficient(this SwitchFlag current, SwitchFlag flag)
	{
		return (current & flag) == flag;
	}

	/// <summary>
	/// Determines whether the current switch flags value contains any of the specified flags.
	/// </summary>
	/// <param name="current">The current value to check for containing flags.</param>
	/// <param name="flags">The flags to check for.</param>
	/// <returns>True if the current value contains any of the flags; otherwise false.</returns>
	public static bool HasAnyFlag(this SwitchFlag current, SwitchFlag flags)
	{
		return (current & flags) != 0;
	}
}
