using Voxelscape.Common.Indexing.Core.Enums;
using Voxelscape.Utility.Common.Pact.Exceptions;

/// <summary>
/// Provides extension methods for the <see cref="AxisDirection4D"/> enum.
/// </summary>
public static class AxisDirection4DExtensions
{
	/// <summary>
	/// Gets the axis of the specified axis direction combination.
	/// </summary>
	/// <param name="axisDirection">The axis direction.</param>
	/// <returns>The axis.</returns>
	public static Axis4D GetAxis(this AxisDirection4D axisDirection)
	{
		switch (axisDirection)
		{
			case AxisDirection4D.PositiveX: return Axis4D.X;
			case AxisDirection4D.NegativeX: return Axis4D.X;
			case AxisDirection4D.PositiveY: return Axis4D.Y;
			case AxisDirection4D.NegativeY: return Axis4D.Y;
			case AxisDirection4D.PositiveZ: return Axis4D.Z;
			case AxisDirection4D.NegativeZ: return Axis4D.Z;
			case AxisDirection4D.PositiveW: return Axis4D.W;
			case AxisDirection4D.NegativeW: return Axis4D.W;
			default:
				throw InvalidEnumArgument.CreateException(nameof(axisDirection), axisDirection);
		}
	}
}
