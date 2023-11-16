namespace Voxelscape.Common.Procedural.Pact.Noise
{
	/// <summary>
	/// Noise function to generate four dimensional pseudo random density points that are continuous with their neighbors.
	/// </summary>
	public interface INoise4D : INoise3D
	{
		/// <summary>
		/// Generate a density value for a given point.
		/// </summary>
		/// <param name="x">X value of the input point.</param>
		/// <param name="y">Y value of the input point.</param>
		/// <param name="z">Z value of the input point.</param>
		/// <param name="w">W value of the input point.</param>
		/// <returns>The density value.</returns>
		double Noise(double x, double y, double z, double w);
	}
}
