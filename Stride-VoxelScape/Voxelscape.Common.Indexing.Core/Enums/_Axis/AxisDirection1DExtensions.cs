using Voxelscape.Common.Indexing.Core.Enums;
using Voxelscape.Utility.Common.Pact.Exceptions;

/// <summary>
/// Provides extension methods for the <see cref="AxisDirection1D"/> enum.
/// </summary>
public static class AxisDirection1DExtensions
{
	/// <summary>
	/// Gets the axis of the specified axis direction combination.
	/// </summary>
	/// <param name="axisDirection">The axis direction.</param>
	/// <returns>The axis.</returns>
	public static Axis1D GetAxis(this AxisDirection1D axisDirection)
	{
		switch (axisDirection)
		{
			case AxisDirection1D.PositiveX: return Axis1D.X;
			case AxisDirection1D.NegativeX: return Axis1D.X;
			default:
				throw InvalidEnumArgument.CreateException(nameof(axisDirection), axisDirection);
		}
	}
}
