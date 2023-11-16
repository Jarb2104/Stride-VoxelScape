using Voxelscape.Utility.Common.Pact.Diagnostics;
using Stride.Graphics;

/// <summary>
/// Provides extension methods for <see cref="GraphicsDevice"/>.
/// </summary>
public static class GraphicsDeviceExtensions
{
	public static bool Supports32BitMeshIndices(this GraphicsDevice device)
	{
		Contracts.Requires.That(device != null);

		return device.Features.CurrentProfile > GraphicsProfile.Level_9_3;
	}
}
