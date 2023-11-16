using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Indexables;
using Voxelscape.Common.Indexing.Pact.Trees;

namespace Voxelscape.Common.Indexing.Core.Trees
{
	public class ConstantOctree<T> : ReadOnlyConstantOctree<T>, IIndexableTree<Index3D, T>
	{
		public ConstantOctree(int treeDepth, T value)
			: base(treeDepth, value)
		{
		}

		/// <inheritdoc />
		public new T this[Index3D index]
		{
			get { return base[index]; }
			set { IIndexableContracts.IndexerSet(this, index); }
		}

		/// <inheritdoc />
		public void SetNode(Index3D index, int depth, T value) => IIndexableTreeContracts.SetNode(this, index, depth);

		/// <inheritdoc />
		public bool TrySetValue(Index3D index, T value) => IndexableUtilities.TrySetValue(this, index, value);
	}
}
