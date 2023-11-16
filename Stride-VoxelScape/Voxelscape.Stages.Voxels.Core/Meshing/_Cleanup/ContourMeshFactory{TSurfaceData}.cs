using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Voxelscape.Common.Contouring.Core.DualContouring;
using Voxelscape.Common.Contouring.Core.Vertices;
using Voxelscape.Common.Contouring.Pact.DualContouring;
using Voxelscape.Common.Contouring.Pact.Meshing;
using Voxelscape.Common.Indexing.Core.Arrays;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Voxels.Pact.Voxels;
using Voxelscape.Stages.Voxels.Pact.Voxels.Grid;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Common.Pact.Factories;
using Voxelscape.Utility.Common.Pact.Types;
using Voxelscape.Utility.Data.Pact.Pools;

namespace Voxelscape.Stages.Voxels.Core.Meshing
{
	// TODO clean up this class!!!
	// TODO this type does not dispose the pool. Is that okay?
	public class ContourMeshFactory<TSurfaceData> :
		IAsyncFactory<ChunkKey, IDisposableValue<IDivisibleMesh<NormalColorTextureVertex>>>
		where TSurfaceData : class, IResettable, new()
	{
		private readonly Index3D voxelChunkDimensionsInVoxels;

		private readonly IPool<IMutableDivisibleMesh<NormalColorTextureVertex>> meshBuilderPool;

		private readonly IDualContourer<TerrainVoxel, TSurfaceData, NormalColorTextureVertex> contourer;

		private readonly ChunkConglomerator<IReadOnlyVoxelGridChunk> chunkConglomerator;

		public ContourMeshFactory(
			IRasterChunkConfig<Index3D> voxelChunkConfig,
			IAsyncFactory<ChunkKey, IDisposableValue<IReadOnlyVoxelGridChunk>> voxelChunkFactory,
			IPool<IMutableDivisibleMesh<NormalColorTextureVertex>> meshBuilderPool,
			IDualContourer<TerrainVoxel, TSurfaceData, NormalColorTextureVertex> contourer)
		{
			Contracts.Requires.That(voxelChunkConfig != null);
			Contracts.Requires.That(voxelChunkFactory != null);
			Contracts.Requires.That(meshBuilderPool != null);
			Contracts.Requires.That(contourer != null);

			this.voxelChunkDimensionsInVoxels = voxelChunkConfig.Bounds.Dimensions;
			this.meshBuilderPool = meshBuilderPool;
			this.contourer = contourer;

			this.chunkConglomerator = new ChunkConglomerator<IReadOnlyVoxelGridChunk>(
				async index => await voxelChunkFactory.CreateAsync(
					new ChunkKey(index - new Index3D(1))).DontMarshallContext(),
				new Index3D(3));
		}

		/// <inheritdoc />
		public async Task<IDisposableValue<IDivisibleMesh<NormalColorTextureVertex>>> CreateAsync(ChunkKey chunkKey)
		{
			var getContourable = this.CreateContourableViewAsync(chunkKey);
			var getMeshBuilder = this.meshBuilderPool.TakeLoanAsync();

			await Task.WhenAll(getContourable, getMeshBuilder).DontMarshallContext();

			using (var contourable = await getContourable.DontMarshallContext())
			{
				var pooledMeshBuilder = await getMeshBuilder.DontMarshallContext();
				this.contourer.Contour(contourable, pooledMeshBuilder.Value);
				return pooledMeshBuilder;
			}
		}

		private async Task<ContourableVoxelView> CreateContourableViewAsync(ChunkKey chunkKey) =>
			new ContourableVoxelView(
				await this.chunkConglomerator.PinChunksAsync(chunkKey.Index).DontMarshallContext(),
				this.voxelChunkDimensionsInVoxels,
				chunkKey.Index * this.voxelChunkDimensionsInVoxels);

		private class ContourableVoxelView : AbstractDisposable, IDualContourable<TerrainVoxel, TSurfaceData>
		{
			private readonly Index3D voxelChunkDimensionsInVoxels;

			private readonly Index3D stageVoxelIndexOfViewOrigin;

			private readonly IDisposable pins;

			private readonly IBoundedReadOnlyIndexable<Index3D, TerrainVoxel> voxels;

			public ContourableVoxelView(
				IDisposableValue<IReadOnlyVoxelGridChunk[,,]> voxelChunkPins,
				Index3D voxelChunkDimensionsInVoxels,
				Index3D stageVoxelIndexOfViewOrigin)
			{
				Contracts.Requires.That(voxelChunkPins != null);

				this.voxelChunkDimensionsInVoxels = voxelChunkDimensionsInVoxels;
				this.stageVoxelIndexOfViewOrigin = stageVoxelIndexOfViewOrigin;
				this.pins = voxelChunkPins;

				this.voxels = CompositeArray.Create(
					voxelChunkPins.Value.ConvertAll(chunk => chunk.VoxelsLocalView),
					this.voxelChunkDimensionsInVoxels);
			}

			/// <inheritdoc />
			public IEnumerable<IVoxelProjection<TerrainVoxel, TSurfaceData>> GetContourableProjections(
				IContourDeterminer<TerrainVoxel> contourDeterminer)
			{
				IDualContourableContracts.GetContourableProjections(contourDeterminer);

				return new EnumerableProjectionsIndexable<TerrainVoxel, TSurfaceData>(
					contourDeterminer,
					this.voxels,
					Index3D.Zero,
					this.voxelChunkDimensionsInVoxels,
					this.stageVoxelIndexOfViewOrigin);
			}

			/// <inheritdoc />
			protected override void ManagedDisposal() => this.pins.Dispose();
		}
	}
}
