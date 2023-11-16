using System.Diagnostics;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Common.Indexing.Pact.Indices;
using Voxelscape.Utility.Common.Core.Mathematics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Pact.Trees
{
	public interface IIndexableTree<TIndex, TValue> :
		IReadOnlyIndexableTree<TIndex, TValue>, IBoundedIndexable<TIndex, TValue>
		where TIndex : IIndex
	{
		void SetNode(TIndex index, int depth, TValue value);
	}

	public static class IIndexableTreeContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void SetNode<TIndex, TValue>(
			IReadOnlyIndexableTree<TIndex, TValue> instance, TIndex index, int depth)
			where TIndex : IIndex
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(instance.IsIndexValid(index));
			Contracts.Requires.That(depth.IsIn(Range.New(0, instance.MaxDepth)));
		}
	}
}
