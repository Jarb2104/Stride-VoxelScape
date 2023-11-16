using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Pact.Trees
{
	public struct TreeNode<TValue>
	{
		public TreeNode(int depth, TValue value)
		{
			Contracts.Requires.That(depth >= 0);

			this.Depth = depth;
			this.Value = value;
		}

		public int Depth { get; }

		public TValue Value { get; }
	}
}
