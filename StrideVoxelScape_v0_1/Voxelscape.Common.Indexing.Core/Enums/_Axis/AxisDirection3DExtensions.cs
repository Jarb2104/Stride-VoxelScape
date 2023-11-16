using Voxelscape.Common.Indexing.Core.Enums;
using Voxelscape.Utility.Common.Pact.Exceptions;

/// <summary>
/// Provides extension methods for the <see cref="AxisDirection3D"/> enum.
/// </summary>
public static class AxisDirection3DExtensions
{
	/// <summary>
	/// Gets the axis of the specified axis direction combination.
	/// </summary>
	/// <param name="axisDirection">The axis direction.</param>
	/// <returns>The axis.</returns>
	public static Axis3D GetAxis(this AxisDirection3D axisDirection)
	{
		switch (axisDirection)
		{
			case AxisDirection3D.PositiveX: return Axis3D.X;
			case AxisDirection3D.NegativeX: return Axis3D.X;
			case AxisDirection3D.PositiveY: return Axis3D.Y;
			case AxisDirection3D.NegativeY: return Axis3D.Y;
			case AxisDirection3D.PositiveZ: return Axis3D.Z;
			case AxisDirection3D.NegativeZ: return Axis3D.Z;
			default:
				throw InvalidEnumArgument.CreateException(nameof(axisDirection), axisDirection);
		}
	}
}
