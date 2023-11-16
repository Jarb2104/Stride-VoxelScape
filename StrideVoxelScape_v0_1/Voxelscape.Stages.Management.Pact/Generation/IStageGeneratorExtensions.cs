using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Stages.Management.Pact.Generation;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for <see cref="IStageGenerator"/>.
/// </summary>
public static class IStageGeneratorExtensions
{
	public static async Task GenerateStageAsync(
		this IStageGenerator stageGenerator, IStageSeed stageSeed, CancellationToken cancellation)
	{
		Contracts.Requires.That(stageGenerator != null);
		Contracts.Requires.That(stageSeed != null);

		using (stageSeed.LinkCancellation(cancellation))
		{
			await stageGenerator.GenerateStageAsync(stageSeed).DontMarshallContext();
		}
	}
}
