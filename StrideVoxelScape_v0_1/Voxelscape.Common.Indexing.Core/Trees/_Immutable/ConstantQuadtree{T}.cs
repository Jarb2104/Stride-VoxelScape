using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Indexables;
using Voxelscape.Common.Indexing.Pact.Trees;

namespace Voxelscape.Common.Indexing.Core.Trees
{
	public class ConstantQuadtree<T> : ReadOnlyConstantQuadtree<T>, IIndexableTree<Index2D, T>
	{
		public ConstantQuadtree(int treeDepth, T value)
			: base(treeDepth, value)
		{
		}

		/// <inheritdoc />
		public new T this[Index2D index]
		{
			get { return base[index]; }
			set { IIndexableContracts.IndexerSet(this, index); }
		}

		/// <inheritdoc />
		public void SetNode(Index2D index, int depth, T value) => IIndexableTreeContracts.SetNode(this, index, depth);

		/// <inheritdoc />
		public bool TrySetValue(Index2D index, T value) => IndexableUtilities.TrySetValue(this, index, value);
	}
}
