namespace Voxelscape.Common.Contouring.Pact.DualContouring
{
	public interface ISeedThreshold<TVoxel, TSurfaceData, TValue> :
		ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue>
		where TVoxel : struct
		where TSurfaceData : class, ISeedReadable
		where TValue : struct
	{
		bool IsWithinThreshold(float seed);
	}
}
