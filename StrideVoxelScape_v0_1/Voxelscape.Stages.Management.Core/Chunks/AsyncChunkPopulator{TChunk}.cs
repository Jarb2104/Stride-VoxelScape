using System.Threading.Tasks;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Concurrency.Core.Tasks;

namespace Voxelscape.Stages.Management.Core.Chunks
{
	public class AsyncChunkPopulator<TChunk> : IAsyncChunkPopulator<TChunk>
	{
		private readonly IChunkPopulator<TChunk> populator;

		public AsyncChunkPopulator(IChunkPopulator<TChunk> populator)
		{
			Contracts.Requires.That(populator != null);

			this.populator = populator;
		}

		/// <inheritdoc />
		public Task PopulateAsync(TChunk chunk)
		{
			IAsyncChunkPopulatorContracts.PopulateAsync(chunk);

			this.populator.Populate(chunk);
			return TaskUtilities.CompletedTask;
		}
	}
}
