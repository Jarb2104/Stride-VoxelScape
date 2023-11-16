using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for <see cref="IDataflowBlock"/>.
/// </summary>
public static class IDataflowBlockExtensions
{
	public static Task CompleteAndAwaitAsync(this IDataflowBlock block)
	{
		Contracts.Requires.That(block != null);

		block.Complete();
		return block.Completion;
	}
}
