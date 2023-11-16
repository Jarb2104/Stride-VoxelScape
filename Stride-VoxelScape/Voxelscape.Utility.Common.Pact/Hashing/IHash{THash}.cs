namespace Voxelscape.Utility.Common.Pact.Hashing
{
	public interface IHash<THash>
	{
		int Result { get; }

		THash And<T>(T value);
	}
}
