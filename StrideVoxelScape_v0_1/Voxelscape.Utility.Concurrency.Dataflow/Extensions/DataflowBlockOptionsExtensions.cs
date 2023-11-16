using System.Threading.Tasks.Dataflow;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for <see cref="DataflowBlockOptions"/>.
/// </summary>
public static class DataflowBlockOptionsExtensions
{
	public static DataflowBlockOptions CreateCopy(this DataflowBlockOptions options)
	{
		Contracts.Requires.That(options != null);

		var result = new DataflowBlockOptions();
		options.CopyTo(result);
		return result;
	}

	public static void CopyTo(this DataflowBlockOptions source, DataflowBlockOptions destination)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(destination != null);

		destination.BoundedCapacity = source.BoundedCapacity;
		destination.CancellationToken = source.CancellationToken;
		destination.MaxMessagesPerTask = source.MaxMessagesPerTask;
		destination.NameFormat = source.NameFormat;
		destination.TaskScheduler = source.TaskScheduler;
	}

	public static ExecutionDataflowBlockOptions ConvertToExecutionOptions(this DataflowBlockOptions options)
	{
		Contracts.Requires.That(options != null);

		var result = new ExecutionDataflowBlockOptions();
		options.CopyTo(result);
		return result;
	}

	public static GroupingDataflowBlockOptions ConvertToGroupingOptions(this DataflowBlockOptions options)
	{
		Contracts.Requires.That(options != null);

		var result = new GroupingDataflowBlockOptions();
		options.CopyTo(result);
		return result;
	}
}
