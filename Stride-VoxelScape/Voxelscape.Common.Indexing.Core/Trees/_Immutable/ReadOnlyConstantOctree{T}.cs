using System.Collections.Generic;
using Voxelscape.Common.Indexing.Core.Arrays;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Trees;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Mathematics;

namespace Voxelscape.Common.Indexing.Core.Trees
{
	public class ReadOnlyConstantOctree<T> : ReadOnlyConstantArray3D<T>, IReadOnlyIndexableTree<Index3D, T>
	{
		public ReadOnlyConstantOctree(int treeDepth, T value)
			: base(CreateDimensions(treeDepth), value)
		{
			this.MaxDepth = treeDepth;
		}

		/// <inheritdoc />
		public int MaxDepth { get; }

		/// <inheritdoc />
		public IEnumerator<KeyValuePair<Index3D, TreeNode<T>>> GetBreadthFirstEnumerator()
		{
			yield return new KeyValuePair<Index3D, TreeNode<T>>(Index3D.Zero, new TreeNode<T>(0, this.ConstantValue));
		}

		/// <inheritdoc />
		public IEnumerator<KeyValuePair<Index3D, TreeNode<T>>> GetDepthFirstEnumerator() => this.GetBreadthFirstEnumerator();

		/// <inheritdoc />
		public TreeNode<T> GetNode(Index3D index)
		{
			IReadOnlyIndexableTreeContracts.GetNode(this, index);

			return new TreeNode<T>(0, this.ConstantValue);
		}

		private static Index3D CreateDimensions(int treeDepth)
		{
			Contracts.Requires.That(treeDepth >= 0);

			return new Index3D(MathUtilities.IntegerPower(2, treeDepth));
		}
	}
}
