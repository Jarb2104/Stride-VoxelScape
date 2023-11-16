namespace Voxelscape.Utility.Common.Pact.Progress
{
	/// <summary>
	///
	/// </summary>
	/// <typeparam name="T">The type of the progress notifications.</typeparam>
	public interface IObservableDiscreteProgress<T> : IObservableProgress<T>
		where T : IDiscreteProgress
	{
		long ProgressTotalCount { get; }
	}
}
