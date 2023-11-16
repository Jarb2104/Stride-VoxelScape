using Stride.Core.Mathematics;
using Voxelscape.Common.Contouring.Pact.DualContouring;

namespace Voxelscape.Common.Contouring.Core.DualContouring
{
	public class CubeSurfaceGenerator<TVoxel, TSurfaceData> : ISurfaceValueGenerator<TVoxel, TSurfaceData, Vector3>
		where TVoxel : struct
		where TSurfaceData : class
	{
		#region ISurfaceValueGenerator<...> Members

		public void GenerateValues(
			IVoxelProjection<TVoxel, TSurfaceData> projection,
			out Vector3 topLeftValue,
			out Vector3 topRightValue,
			out Vector3 bottomLeftValue,
			out Vector3 bottomRightValue)
		{
			topLeftValue = projection.ConvertToAbsoluteVectorPosition(new Vector3(-.5f, .5f, -.5f));
			topRightValue = projection.ConvertToAbsoluteVectorPosition(new Vector3(.5f, .5f, -.5f));
			bottomLeftValue = projection.ConvertToAbsoluteVectorPosition(new Vector3(-.5f, -.5f, -.5f));
			bottomRightValue = projection.ConvertToAbsoluteVectorPosition(new Vector3(.5f, -.5f, -.5f));
		}

		#endregion
	}
}
