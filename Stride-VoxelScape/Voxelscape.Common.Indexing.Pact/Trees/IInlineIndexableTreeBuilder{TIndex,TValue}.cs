using System.Diagnostics;
using Voxelscape.Common.Indexing.Pact.Indices;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Types;

namespace Voxelscape.Common.Indexing.Pact.Trees
{
	public interface IInlineIndexableTreeBuilder<TIndex, TValue> : IIndexableTree<TIndex, TValue>, IResettable
		where TIndex : IIndex
	{
		IIndexableTree<TIndex, TValue> AsTree { get; }

		bool IsTreeInitialized { get; }

		void InitializeTree();
	}

	public static class IInlineIndexableTreeBuilderContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void AsTree<TIndex, TValue>(IInlineIndexableTreeBuilder<TIndex, TValue> instance)
			where TIndex : IIndex
		{
			Contracts.Requires.That(instance.IsTreeInitialized);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void InitializeTree<TIndex, TValue>(IInlineIndexableTreeBuilder<TIndex, TValue> instance)
			where TIndex : IIndex
		{
			Contracts.Requires.That(!instance.IsTreeInitialized);
		}
	}
}
