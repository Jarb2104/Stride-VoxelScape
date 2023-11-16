namespace Voxelscape.Common.Indexing.Pact.Indices
{
	public interface IMortonCode<TIndex>
		where TIndex : IIndex
	{
		TIndex ToIndex();

		int ToInt();
	}
}
