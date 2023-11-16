using System;
using System.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Types;

/// <summary>
/// Provides extension methods for the <see cref="BitArray"/> class.
/// </summary>
public static class BitArrayExtensions
{
	/// <summary>
	/// Copies the contents of a <see cref="BitArray"/> to an array of integers.
	/// </summary>
	/// <param name="bitArray">The bit array.</param>
	/// <returns>The array of integers.</returns>
	public static int[] CopyToIntArray(this BitArray bitArray)
	{
		Contracts.Requires.That(bitArray != null);

		// casting to double ensures floating point division
		int size = (int)Math.Ceiling(
			((double)bitArray.Count) / PrimitiveTypeSizes.IntNumberOfBits);

		int[] result = new int[size];
		bitArray.CopyTo(result, 0);
		return result;
	}
}
