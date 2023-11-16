namespace Voxelscape.Common.Procedural.Pact.Noise
{
	/// <summary>
	/// Noise function to generate one dimensional pseudo random density points that are continuous with their neighbors.
	/// </summary>
	public interface INoise1D
	{
		/// <summary>
		/// Generate a density value for a given point.
		/// </summary>
		/// <param name="x">X value of the input point.</param>
		/// <returns>The density value.</returns>
		double Noise(double x);
	}
}
