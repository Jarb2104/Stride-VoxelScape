using System;
using System.Threading.Tasks;
using Voxelscape.Stages.Management.Pact.Generation;
using Voxelscape.Stages.Management.Pact.Stages;
using Voxelscape.Stages.Voxels.Pact.Templates.SkyIsland;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Stores;

namespace Voxelscape.Stages.Voxels.Core.Templates.SkyIsland
{
	/// <summary>
	///
	/// </summary>
	public class SkyIslandMapChunkImagesProcessor : IChunkProcessor<IReadOnlySkyIslandMapChunk>
	{
		private readonly IStageBounds stageBounds;

		private readonly IKeyValueStore statsStore;

		private readonly SkyIslandMapImagesOptions imagesOptions;

		private SkyIslandMaps min;

		private SkyIslandMaps max;

		public SkyIslandMapChunkImagesProcessor(
			IStageBounds stageBounds, IKeyValueStore statsStore, SkyIslandMapImagesOptions imagesOptions = null)
		{
			Contracts.Requires.That(stageBounds != null);
			Contracts.Requires.That(statsStore != null);

			this.stageBounds = stageBounds;
			this.statsStore = statsStore;
			this.imagesOptions = imagesOptions;
		}

		/// <inheritdoc />
		public async Task InitializeAsync()
		{
			if (string.IsNullOrWhiteSpace(this.imagesOptions?.ShapePercentWeightMapImagePath) &&
				string.IsNullOrWhiteSpace(this.imagesOptions?.BaselineHeightImagePath) &&
				string.IsNullOrWhiteSpace(this.imagesOptions?.MountainHeightMultiplierImagePath) &&
				string.IsNullOrWhiteSpace(this.imagesOptions?.TopHeightImagePath) &&
				string.IsNullOrWhiteSpace(this.imagesOptions?.BottomHeightImagePath))
			{
				return;
			}

			var shapeMinTask = this.statsStore.GetAsync(SkyIslandMapStat.ShapePercentWeightMin);
			var shapeMaxTask = this.statsStore.GetAsync(SkyIslandMapStat.ShapePercentWeightMax);

			var baselineMinTask = this.statsStore.GetAsync(SkyIslandMapStat.BaselineHeightMin);
			var baselineMaxTask = this.statsStore.GetAsync(SkyIslandMapStat.BaselineHeightMax);

			var mountainMinTask = this.statsStore.GetAsync(SkyIslandMapStat.MountainHeightMultiplierMin);
			var mountainMaxTask = this.statsStore.GetAsync(SkyIslandMapStat.MountainHeightMultiplierMax);

			var topMinTask = this.statsStore.GetAsync(SkyIslandMapStat.TopHeightMin);
			var topMaxTask = this.statsStore.GetAsync(SkyIslandMapStat.TopHeightMax);

			var bottomMinTask = this.statsStore.GetAsync(SkyIslandMapStat.BottomHeightMin);
			var bottomMaxTask = this.statsStore.GetAsync(SkyIslandMapStat.BottomHeightMax);

			await Task.WhenAll(
				shapeMinTask,
				shapeMaxTask,
				baselineMinTask,
				baselineMaxTask,
				mountainMinTask,
				mountainMaxTask,
				topMinTask,
				topMaxTask,
				bottomMinTask,
				bottomMaxTask).DontMarshallContext();

			this.min = new SkyIslandMaps(
				shapePercentWeight: shapeMinTask.Result,
				baselineHeight: baselineMinTask.Result,
				mountainHeightMultiplier: mountainMinTask.Result,
				topHeight: topMinTask.Result,
				bottomHeight: bottomMinTask.Result);

			this.max = new SkyIslandMaps(
				shapePercentWeight: shapeMaxTask.Result,
				baselineHeight: baselineMaxTask.Result,
				mountainHeightMultiplier: mountainMaxTask.Result,
				topHeight: topMaxTask.Result,
				bottomHeight: bottomMaxTask.Result);
		}

		/// <inheritdoc />
		public void ProcessChunk(IReadOnlySkyIslandMapChunk chunk)
		{
			IChunkProcessorContracts.ProcessChunk(chunk);

			// TODO need to actually implement the image phase, assuming I actually care anymore
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public Task CompleteAsync()
		{
			// TODO need to actually implement the image phase, assuming I actually care anymore
			throw new NotImplementedException();
		}
	}
}
