using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voxelscape.Common.Indexing.Core.Enumerables;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Utility.Common.Core.Disposables;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Index = Voxelscape.Common.Indexing.Core.Enumerables.Index;

namespace Voxelscape.Stages.Voxels.Core.Meshing
{
	// TODO clean up this class!!!
	public class ChunkConglomerator<TChunk>
		where TChunk : class
	{
		private readonly Func<Index3D, Task<IDisposableValue<TChunk>>> pinChunk;

		private readonly Index3D dimensionsInChunks;

		private readonly int numberOfChunks;

		public ChunkConglomerator(Func<Index3D, Task<IDisposableValue<TChunk>>> pinChunk, Index3D dimensionsInChunks)
		{
			Contracts.Requires.That(pinChunk != null);
			Contracts.Requires.That(dimensionsInChunks.IsAllPositive());

			this.pinChunk = pinChunk;
			this.dimensionsInChunks = dimensionsInChunks;
			this.numberOfChunks = this.dimensionsInChunks.MultiplyCoordinates();
		}

		public async Task<IDisposableValue<TChunk[,,]>> PinChunksAsync(Index3D originChunkIndex)
		{
			var tasks = new List<Task>(this.numberOfChunks);
			var chunkPins = this.dimensionsInChunks.CreateArray<IDisposableValue<TChunk>>();

			// collect the pins for all the chunks
			foreach (var index in Index.Range(Index3D.Zero, this.dimensionsInChunks))
			{
				tasks.Add(Task.Run(async () => chunkPins[index.X, index.Y, index.Z] =
					await this.pinChunk(index + originChunkIndex).DontMarshallContext()));
			}

			await Task.WhenAll(tasks).DontMarshallContext();

			return new CompositeDisposableWrapper<TChunk[,,]>(
				chunkPins.ConvertAll(pin => pin.Value), chunkPins.Cast<IDisposable>());
		}
	}
}
