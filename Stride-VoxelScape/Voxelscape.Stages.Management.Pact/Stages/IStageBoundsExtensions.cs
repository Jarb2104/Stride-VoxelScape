using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Management.Pact.Stages;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for <see cref="IStageBounds"/>.
/// </summary>
public static class IStageBoundsExtensions
{
	public static bool Contains(this IStageBounds bounds, ChunkOverheadKey key)
	{
		Contracts.Requires.That(bounds != null);

		return key.Index.IsIn(bounds.InOverheadChunks.LowerBounds, bounds.InOverheadChunks.UpperBounds);
	}

	public static bool Contains(this IStageBounds bounds, ChunkKey key)
	{
		Contracts.Requires.That(bounds != null);

		return key.Index.IsIn(bounds.InChunks.LowerBounds, bounds.InChunks.UpperBounds);
	}
}
