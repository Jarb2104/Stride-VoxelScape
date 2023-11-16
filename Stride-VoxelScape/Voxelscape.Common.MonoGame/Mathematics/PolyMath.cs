
using Stride.Core.Mathematics;

namespace Voxelscape.Common.MonoGame.Mathematics
{
	/// <summary>
	/// Provides utility methods for the <see cref="Vector3"/> type.
	/// </summary>
	public static class PolyMath
	{
		/// <summary>
		/// Calculates the surface normal of a triangle defined by vectors A, B, C.
		/// </summary>
		/// <param name="a">The vector A.</param>
		/// <param name="b">The vector B.</param>
		/// <param name="c">The vector C.</param>
		/// <returns>The surface normal of the triangle.</returns>
		public static Vector3 GetSurfaceNormal(Vector3 a, Vector3 b, Vector3 c) =>
			Vector3.Normalize(-Vector3.Cross(b - a, c - a));

		/// <summary>
		/// Averages two vectors and normalizes the result.
		/// </summary>
		/// <param name="a">The first vector.</param>
		/// <param name="b">The second vector.</param>
		/// <returns>The normalized average of the vectors.</returns>
		public static Vector3 GetNormalizedAverage(Vector3 a, Vector3 b) => Vector3.Normalize((a + b) / 2);

		/// <summary>
		/// Calculates the surface area of a triangle defined by vectors A, B, C.
		/// </summary>
		/// <param name="a">The vector A.</param>
		/// <param name="b">The vector B.</param>
		/// <param name="c">The vector C.</param>
		/// <returns>The surface area of the triangle.</returns>
		public static float GetArea(Vector3 a, Vector3 b, Vector3 c) => Vector3.Cross(b - a, c - a).Length() / 2;

		public static Vector3 GetMidpoint(Vector3 a, Vector3 b, Vector3 c) => (a + b + c) / 3;

		public static Vector2 GetMidpoint(Vector2 a, Vector2 b, Vector2 c) => (a + b + c) / 3;
	}
}
