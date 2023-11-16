using Stride.Graphics;
using Stride.Rendering;
using System.Threading.Tasks;
using Voxelscape.Common.Contouring.Core.Meshing;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Utility.Common.Core.Factories;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Concurrency.Core.Tasks;
using Voxelscape.Utility.Concurrency.Pact.LifeCycle;
using Voxelscape.Utility.Data.Core.Pools;
using Voxelscape.Utility.Data.Pact.Pools;
using Voxelscape.Stride.Utility.Core.Meshing;
using Voxelscape.Stride.Utility.Core.Vertices;
using Voxelscape.Stride.Utility.Pact.Meshing;

namespace Voxelscape.Stride.Game.Scenes.Streaming
{
	/// <summary>
	///
	/// </summary>
	public class StreamingChunkGenerator : AbstractAsyncCompletable
	{
		private readonly StreamingStageMeshFactory meshFactory;

		private readonly IPool<MeshDataTransfer16<VertexPositionNormalColorTexture>> transferPool;

		private readonly AsyncTaskThrottler<ChunkKey, Model> throttler;

		private readonly int maxVertices;

		private readonly MaterialInstance material;

		private readonly ProceduralMeshFactory16<VertexPositionNormalColorTexture> proceduralMesh;

		public StreamingChunkGenerator(
			StreamingStageMeshFactory meshFactory, MaterialInstance material, GraphicsDevice device)
		{
			Contracts.Requires.That(meshFactory != null);
			Contracts.Requires.That(material != null);
			Contracts.Requires.That(device != null);

			this.meshFactory = meshFactory;
			this.material = material;

			this.proceduralMesh = new ProceduralMeshFactory16<VertexPositionNormalColorTexture>(
				VertexPositionNormalColorTexture.Format, device);

			var transferOptions = new TrackingPoolOptions<MeshDataTransfer16<VertexPositionNormalColorTexture>>()
			{
				BoundedCapacity = this.meshFactory.ThreadsCount,
			};

			this.transferPool = Pool.New(transferOptions);

			var meshOptions = MeshDataTransferOptions.New16Bit();
			meshOptions.InitialIndices = meshOptions.InitialVertices * 2;
			this.maxVertices = meshOptions.MaxVertices;

			this.transferPool.GiveUntilFull(
				Factory.From(() => new MeshDataTransfer16<VertexPositionNormalColorTexture>(meshOptions)));

			this.throttler = new AsyncTaskThrottler<ChunkKey, Model>(
				this.DoGenerateModelAsync, this.meshFactory.ThreadsCount);
				////key => Task.Run(() => this.DoGenerateModelAsync(key)), this.meshFactory.ThreadsCount);
		}

		public Task<Model> GenerateModelAsync(ChunkKey chunkKey) => this.throttler.RunAsync(chunkKey);

		/// <inheritdoc />
		protected override async Task CompleteAsync()
		{
			await this.meshFactory.CompleteAndAwaitAsync().DontMarshallContext();
			this.transferPool.Dispose();
		}

		private async Task<Model> DoGenerateModelAsync(ChunkKey key)
		{
			using (var mesh = await this.meshFactory.CreateAsync(key).DontMarshallContext())
			{
				if (mesh.Value.IsEmpty())
				{
					return null;
				}

				using (var transfer = await this.transferPool.TakeLoanAsync().DontMarshallContext())
				{
					var model = new Model();
					model.Add(this.material);

					foreach (var splitMesh in DivisibleMesh.Split(this.maxVertices, mesh.Value))
					{
						transfer.Value.CopyIn(
							splitMesh.Convert(vertex => vertex.ToStride()), StrideMeshConstants.VertexWindingOrder);

						model.Add(this.proceduralMesh.Create(transfer.Value));
					}

					return model;
				}
			}
		}
	}
}
