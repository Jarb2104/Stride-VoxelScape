using System.Collections.Generic;
using System.Diagnostics;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Common.Indexing.Pact.Indices;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Pact.Trees
{
	public interface IReadOnlyIndexableTree<TIndex, TValue> : IBoundedReadOnlyIndexable<TIndex, TValue>
		where TIndex : IIndex
	{
		int MaxDepth { get; }

		TreeNode<TValue> GetNode(TIndex index);

		IEnumerator<KeyValuePair<TIndex, TreeNode<TValue>>> GetBreadthFirstEnumerator();

		IEnumerator<KeyValuePair<TIndex, TreeNode<TValue>>> GetDepthFirstEnumerator();
	}

	public static class IReadOnlyIndexableTreeContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void GetNode<TIndex, TValue>(IReadOnlyIndexableTree<TIndex, TValue> instance, TIndex index)
			where TIndex : IIndex
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(instance.IsIndexValid(index));
		}
	}
}
