using Voxelscape.Utility.Common.Pact.Diagnostics;
using Stride.Core.Mathematics;
using Stride.Rendering;

/// <summary>
/// Provides extension methods for <see cref="Model"/>.
/// </summary>
public static class ModelExtensions
{
	public static void Dispose(this Model model)
	{
		Contracts.Requires.That(model != null);

		foreach (var mesh in model.Meshes)
		{
			mesh.Draw.IndexBuffer.Buffer.Dispose();
			foreach (var vertexBuffer in mesh.Draw.VertexBuffers)
			{
				vertexBuffer.Buffer.Dispose();
			}
		}
	}

	public static void RecalculateBounds(this Model model)
	{
		Contracts.Requires.That(model != null);

		BoundingBox box = BoundingBox.Empty;
		BoundingSphere sphere = BoundingSphere.Empty;

		foreach (var mesh in model.Meshes)
		{
			box = BoundingBox.Merge(box, mesh.BoundingBox);
			sphere = BoundingSphere.Merge(sphere, mesh.BoundingSphere);
		}

		model.BoundingBox = box;
		model.BoundingSphere = sphere;
	}
}
