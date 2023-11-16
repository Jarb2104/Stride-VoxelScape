namespace Voxelscape.Common.Contouring.Pact.DualContouring
{
	public interface ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue>
		where TVoxel : struct
		where TSurfaceData : class
		where TValue : struct
	{
		void GenerateValues(
			IVoxelProjection<TVoxel, TSurfaceData> projection,
			out TValue topLeftValue,
			out TValue topRightValue,
			out TValue bottomLeftValue,
			out TValue bottomRightValue);
	}
}
