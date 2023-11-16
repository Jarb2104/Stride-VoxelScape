using System;
using System.Collections;
using System.Collections.Generic;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Pact.Trees;

namespace Voxelscape.Common.Indexing.Core.Trees
{
	public class Octree<T> : IIndexableTree<Index3D, T>
	{
		/// <inheritdoc />
		public Index3D Dimensions
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <inheritdoc />
		public int Length
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <inheritdoc />
		public long LongLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <inheritdoc />
		public Index3D LowerBounds
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
		public int Rank
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <inheritdoc />
		public Index3D UpperBounds
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <inheritdoc />
		public T this[Index3D index]
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
		public IEnumerator<KeyValuePair<Index3D, TreeNode<T>>> GetBreadthFirstEnumerator()
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public IEnumerator<KeyValuePair<Index3D, TreeNode<T>>> GetDepthFirstEnumerator()
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public IEnumerator<KeyValuePair<Index3D, T>> GetEnumerator()
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public int GetLength(int dimension)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public int GetLowerBound(int dimension)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public TreeNode<T> GetNode(Index3D index)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public int GetUpperBound(int dimension)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public bool IsIndexValid(Index3D index)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public void SetNode(Index3D index, int depth, T value)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public bool TryGetValue(Index3D index, out T value)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		public bool TrySetValue(Index3D index, T value)
		{
			throw new NotImplementedException();
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
}
