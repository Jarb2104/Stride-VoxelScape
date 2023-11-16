using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Voxelscape.Common.Indexing.Core.Enumerables;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Management.Pact.Stages;
using Voxelscape.Utility.Common.Core.Collections;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Index = Voxelscape.Common.Indexing.Core.Enumerables.Index;

namespace Voxelscape.Stages.Management.Core.Generation
{
	/// <summary>
	///
	/// </summary>
	public class ChunkKeyCollection : IReadOnlyLargeCollection<ChunkKey>
	{
		private readonly IReadOnlyLargeCollection<ChunkKey> keys;

		public ChunkKeyCollection(IStageBounds bounds)
			: this(bounds, GetOscillationOrder(bounds))
		{
		}

		public ChunkKeyCollection(IStageBounds bounds, OscillationOrder3D order)
			: this(bounds, (start, dimensions) => Index.OscillateRange(start, dimensions, order))
		{
		}

		public ChunkKeyCollection(
			IStageBounds bounds, Func<Index3D, Index3D, IEnumerable<Index3D>> enumerationOrder)
		{
			Contracts.Requires.That(bounds != null);
			Contracts.Requires.That(enumerationOrder != null);

			var chunkKeys =
				enumerationOrder(bounds.InChunks.LowerBounds, bounds.InChunks.Dimensions)
				.Select(index => new ChunkKey(index));

			this.keys = new ReadOnlyLargeCollection<ChunkKey>(
				chunkKeys, bounds.InChunks.LongLength);
		}

		/// <inheritdoc />
		public long Count => this.keys.Count;

		/// <inheritdoc />
		public IEnumerator<ChunkKey> GetEnumerator() => this.keys.GetEnumerator();

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

		private static OscillationOrder3D GetOscillationOrder(IStageBounds bounds)
		{
			Contracts.Requires.That(bounds != null);

			var dimensions = bounds.InChunks.Dimensions;
			return dimensions.X <= dimensions.Z ? OscillationOrder3D.YXZ : OscillationOrder3D.YZX;
		}
	}
}
