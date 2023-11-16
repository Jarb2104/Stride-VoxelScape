using Voxelscape.Common.Indexing.Core.Indices;

namespace Voxelscape.Common.Indexing.Core.Arrays
{
	/// <summary>
	/// Provides algorithmic method implementations for dynamic arrays to share.
	/// </summary>
	internal static class DynamicArrayUtilities
	{
		/// <summary>
		/// The default initial size of a dynamic array.
		/// </summary>
		private const int DefaultSize = 10;

		/// <summary>
		/// Gets the default size for a one dimensional dynamic array.
		/// </summary>
		/// <value>
		/// The default size.
		/// </value>
		public static Index1D DefaultSize1D
		{
			get { return new Index1D(DefaultSize); }
		}

		/// <summary>
		/// Gets the default size for a two dimensional dynamic array.
		/// </summary>
		/// <value>
		/// The default size.
		/// </value>
		public static Index2D DefaultSize2D
		{
			get { return new Index2D(DefaultSize, DefaultSize); }
		}

		/// <summary>
		/// Gets the default size for a three dimensional dynamic array.
		/// </summary>
		/// <value>
		/// The default size.
		/// </value>
		public static Index3D DefaultSize3D
		{
			get { return new Index3D(DefaultSize, DefaultSize, DefaultSize); }
		}

		/// <summary>
		/// Gets the default size for a four dimensional dynamic array.
		/// </summary>
		/// <value>
		/// The default size.
		/// </value>
		public static Index4D DefaultSize4D
		{
			get { return new Index4D(DefaultSize, DefaultSize, DefaultSize, DefaultSize); }
		}

		/// <summary>
		/// Handles determining the new size for a dimension of a dynamic array.
		/// </summary>
		/// <param name="length">The current length of the dimension.</param>
		/// <param name="index">The current index being attempting to be accessed.</param>
		/// <returns>The new length of the dimension.</returns>
		public static int HandleAxis(int length, int index)
		{
			while (length <= index)
			{
				length *= 2;
			}

			return length;
		}

		/// <summary>
		/// Handles determining the new size for a dimension of a multidirectional dynamic array.
		/// Doing so also adjusts the index being accessed, the origin of the array, and the offset
		/// for copying the old array into the new one.
		/// </summary>
		/// <param name="length">The current length of the dimension.</param>
		/// <param name="index">The current index being attempting to be accessed.</param>
		/// <param name="origin">The current origin index of the dimension.</param>
		/// <param name="offset">The offset into the new array to start copying the old array into the new one.</param>
		/// <returns>The new length of the dimension.</returns>
		public static int HandleAxis(int length, ref int index, ref int origin, out int offset)
		{
			index += origin;
			int newLength = length;

			if (index < 0)
			{
				do
				{
					index += newLength;
					origin += newLength;
					newLength *= 2;
				}
				while (index < 0);

				offset = newLength - length;
			}
			else
			{
				while (newLength <= index)
				{
					newLength *= 2;
				}

				offset = 0;
			}

			return newLength;
		}
	}
}
