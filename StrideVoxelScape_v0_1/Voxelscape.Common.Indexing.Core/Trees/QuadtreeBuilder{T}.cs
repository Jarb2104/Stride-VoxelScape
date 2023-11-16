using System;
using System.Collections.Generic;
using Voxelscape.Common.Indexing.Core.Bounds;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Trees;

namespace Voxelscape.Common.Indexing.Core.Trees
{
	public class QuadtreeBuilder<T> : AbstractBoundedIndexable2D<T>, IInlineIndexableTreeBuilder<Index2D, T>
	{
		/// <inheritdoc />
		public IIndexableTree<Index2D, T> AsTree
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <inheritdoc />
		public bool IsTreeInitialized
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <inheritdoc />
		public int MaxDepth
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <inheritdoc />
		public override T this[Index2D index]
		{
			get
			{
				throw new NotImplementedException();
			}

			set
			{
				throw new NotImplementedException();
			}
		}

		/// <inheritdoc />
		public override IEnumerator<KeyValuePair<Index2D, T>> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public override int GetLength(int dimension)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public override int GetLowerBound(int dimension)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public override int GetUpperBound(int dimension)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public void InitializeTree()
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public void Reset()
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public void SetNode(Index2D index, int depth, T value)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public TreeNode<T> GetNode(Index2D index)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public IEnumerator<KeyValuePair<Index2D, TreeNode<T>>> GetBreadthFirstEnumerator()
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public IEnumerator<KeyValuePair<Index2D, TreeNode<T>>> GetDepthFirstEnumerator()
		{
			throw new NotImplementedException();
		}
	}
}
