namespace Voxelscape.Utility.Common.Pact.Progress
{
	/// <summary>
	///
	/// </summary>
	public interface IDiscreteProgress
	{
		long CountCompleted { get; }

		long TotalCount { get; }
	}
}
