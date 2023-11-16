namespace Voxelscape.Utility.Common.Pact.Shareables
{
	public interface IShareableValue<T> : IShareable
	{
		T Value { get; }
	}
}
