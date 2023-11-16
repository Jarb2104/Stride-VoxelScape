using System.Text;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for use with the <see cref="StringBuilder"/> class.
/// </summary>
public static class StringBuilderExtensions
{
	/// <summary>
	/// Clears all text from the builder.
	/// </summary>
	/// <param name="builder">The builder to clear.</param>
	public static void Clear(this StringBuilder builder)
	{
		Contracts.Requires.That(builder != null);

		builder.Remove(0, builder.Length);
	}
}
