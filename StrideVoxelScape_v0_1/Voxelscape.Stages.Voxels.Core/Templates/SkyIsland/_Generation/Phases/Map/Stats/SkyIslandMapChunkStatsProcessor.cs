using System.Linq;
using System.Threading.Tasks;
using Voxelscape.Stages.Management.Pact.Generation;
using Voxelscape.Stages.Voxels.Pact.Templates.SkyIsland;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Concurrency.Core.Tasks;
using Voxelscape.Utility.Data.Pact.Stores;

namespace Voxelscape.Stages.Voxels.Core.Templates.SkyIsland
{
	/// <summary>
	///
	/// </summary>
	public class SkyIslandMapChunkStatsProcessor : IChunkProcessor<IReadOnlySkyIslandMapChunk>
	{
		private readonly SkyIslandMapStatsAggregator stats = new SkyIslandMapStatsAggregator();

		private readonly IKeyValueStore statsStore;

		public SkyIslandMapChunkStatsProcessor(IKeyValueStore statsStore)
		{
			Contracts.Requires.That(statsStore != null);

			this.statsStore = statsStore;
		}

		/// <inheritdoc />
		public Task InitializeAsync() => TaskUtilities.CompletedTask;

		/// <inheritdoc />
		public void ProcessChunk(IReadOnlySkyIslandMapChunk chunk)
		{
			IChunkProcessorContracts.ProcessChunk(chunk);

			// find min/max of this chunk
			var localStats = new SkyIslandMapStatsAggregator();
			foreach (var mapValues in chunk.MapsLocalView.Select(pair => pair.Value))
			{
				localStats.Update(mapValues);
			}

			// update shared min/max values thus found across all chunks
			lock (this.stats)
			{
				this.stats.Update(localStats.Min);
				this.stats.Update(localStats.Max);
			}
		}

		/// <inheritdoc />
		public async Task CompleteAsync()
		{
			SkyIslandMaps min;
			SkyIslandMaps max;

			lock (this.stats)
			{
				min = this.stats.Min;
				max = this.stats.Max;
			}

			var tasks = new Task[]
			{
				this.statsStore.AddOrUpdateAsync(SkyIslandMapStat.ShapePercentWeightMin, min.ShapePercentWeight),
				this.statsStore.AddOrUpdateAsync(SkyIslandMapStat.ShapePercentWeightMax, max.ShapePercentWeight),

				this.statsStore.AddOrUpdateAsync(SkyIslandMapStat.BaselineHeightMin, min.BaselineHeight),
				this.statsStore.AddOrUpdateAsync(SkyIslandMapStat.BaselineHeightMax, max.BaselineHeight),

				this.statsStore.AddOrUpdateAsync(
					SkyIslandMapStat.MountainHeightMultiplierMin, min.MountainHeightMultiplier),
				this.statsStore.AddOrUpdateAsync(
					SkyIslandMapStat.MountainHeightMultiplierMax, max.MountainHeightMultiplier),

				this.statsStore.AddOrUpdateAsync(SkyIslandMapStat.TopHeightMin, min.TopHeight),
				this.statsStore.AddOrUpdateAsync(SkyIslandMapStat.TopHeightMax, max.TopHeight),

				this.statsStore.AddOrUpdateAsync(SkyIslandMapStat.BottomHeightMin, min.BottomHeight),
				this.statsStore.AddOrUpdateAsync(SkyIslandMapStat.BottomHeightMax, max.BottomHeight),
			};

			await Task.WhenAll(tasks).DontMarshallContext();
		}
	}
}
