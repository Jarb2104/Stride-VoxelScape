namespace Voxelscape.Stride.Utility.Pact.Meshing
{
	public interface IMeshData32<TVertex> : IMeshData<TVertex>
		where TVertex : struct
	{
		int[] Indices32 { get; }
	}
}
