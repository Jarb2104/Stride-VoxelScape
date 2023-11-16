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
	public class ChunkOverheadKeyCollection : IReadOnlyLargeCollection<ChunkOverheadKey>
	{
		private readonly IReadOnlyLargeCollection<ChunkOverheadKey> keys;

		public ChunkOverheadKeyCollection(IStageBounds bounds)
			: this(bounds, GetOscillationOrder(bounds))
		{
		}

		public ChunkOverheadKeyCollection(IStageBounds bounds, OscillationOrder2D order)
			: this(bounds, (start, dimensions) => Index.OscillateRange(start, dimensions, order))
		{
		}

		public ChunkOverheadKeyCollection(
			IStageBounds bounds, Func<Index2D, Index2D, IEnumerable<Index2D>> enumerationOrder)
		{
			Contracts.Requires.That(bounds != null);
			Contracts.Requires.That(enumerationOrder != null);

			var chunkKeys =
				enumerationOrder(bounds.InOverheadChunks.LowerBounds, bounds.InOverheadChunks.Dimensions)
				.Select(index => new ChunkOverheadKey(index));

			this.keys = new ReadOnlyLargeCollection<ChunkOverheadKey>(
				chunkKeys, bounds.InOverheadChunks.LongLength);
		}

		/// <inheritdoc />
		public long Count => this.keys.Count;

		/// <inheritdoc />
		public IEnumerator<ChunkOverheadKey> GetEnumerator() => this.keys.GetEnumerator();

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

		private static OscillationOrder2D GetOscillationOrder(IStageBounds bounds)
		{
			Contracts.Requires.That(bounds != null);

			var dimensions = bounds.InOverheadChunks.Dimensions;
			return dimensions.X <= dimensions.Y ? OscillationOrder2D.XY : OscillationOrder2D.YX;
		}
	}
}
