using Voxelscape.Common.Indexing.Core.Enums;
using Voxelscape.Utility.Common.Pact.Exceptions;

/// <summary>
/// Provides extension methods for the <see cref="AxisDirection2D"/> enum.
/// </summary>
public static class AxisDirection2DExtensions
{
	/// <summary>
	/// Gets the axis of the specified axis direction combination.
	/// </summary>
	/// <param name="axisDirection">The axis direction.</param>
	/// <returns>The axis.</returns>
	public static Axis2D GetAxis(this AxisDirection2D axisDirection)
	{
		switch (axisDirection)
		{
			case AxisDirection2D.PositiveX: return Axis2D.X;
			case AxisDirection2D.NegativeX: return Axis2D.X;
			case AxisDirection2D.PositiveY: return Axis2D.Y;
			case AxisDirection2D.NegativeY: return Axis2D.Y;
			default:
				throw InvalidEnumArgument.CreateException(nameof(axisDirection), axisDirection);
		}
	}
}
