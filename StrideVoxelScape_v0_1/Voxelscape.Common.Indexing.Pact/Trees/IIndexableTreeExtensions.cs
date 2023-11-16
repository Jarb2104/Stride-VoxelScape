using Voxelscape.Common.Indexing.Pact.Indices;
using Voxelscape.Common.Indexing.Pact.Trees;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for <see cref="IIndexableTree{TIndex, TValue}"/>.
/// </summary>
public static class IIndexableTreeExtensions
{
	public static void SetNode<TIndex, TValue>(IIndexableTree<TIndex, TValue> tree, TIndex index, TreeNode<TValue> node)
		where TIndex : IIndex
	{
		Contracts.Requires.That(tree != null);
		Contracts.Requires.That(tree.IsIndexValid(index));

		tree.SetNode(index, node.Depth, node.Value);
	}
}
