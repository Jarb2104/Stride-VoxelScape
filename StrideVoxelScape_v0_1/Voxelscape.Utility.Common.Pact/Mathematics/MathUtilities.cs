using System;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Pact.Mathematics
{
	/// <summary>
	/// Provides additional constants and static methods for trigonometric, logarithmic, and other common mathematical functions
	/// to be used along with the <see cref="Math"/> class.
	/// </summary>
	public static class MathUtilities
	{
		#region ActualModulo

		/// <summary>
		/// Calculates the actual modulo value. This is not the same as the % operator, because the % operator is not
		/// actually modulus, it's the remainder. The two functions differ in how they handle negative numbers.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="mod">The modulo to divide by.</param>
		/// <returns>The result of the modulo division.</returns>
		/// <remarks>
		/// See <see href="http://stackoverflow.com/questions/10065080/mod-explanation">this</see> and
		/// <see href="http://stackoverflow.com/questions/1082917/mod-of-negative-number-is-melting-my-brain">this</see>
		/// for more information about modulo in C#.
		/// </remarks>
		public static int ActualModulo(int value, int mod)
		{
			Contracts.Requires.That(mod != 0);

			mod = Math.Abs(mod);
			int result = value % mod;
			return result < 0 ? result + mod : result;
		}

		/// <summary>
		/// Calculates the actual modulo value. This is not the same as the % operator, because the % operator is not
		/// actually modulus, it's the remainder. The two functions differ in how they handle negative numbers.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="mod">The modulo to divide by.</param>
		/// <returns>The result of the modulo division.</returns>
		/// <remarks>
		/// See <see href="http://stackoverflow.com/questions/10065080/mod-explanation">this</see> and
		/// <see href="http://stackoverflow.com/questions/1082917/mod-of-negative-number-is-melting-my-brain">this</see>
		/// for more information about modulo in C#.
		/// </remarks>
		public static long ActualModulo(long value, long mod)
		{
			Contracts.Requires.That(mod != 0);

			mod = Math.Abs(mod);
			long result = value % mod;
			return result < 0 ? result + mod : result;
		}

		/// <summary>
		/// Calculates the actual modulo value. This is not the same as the % operator, because the % operator is not
		/// actually modulus, it's the remainder. The two functions differ in how they handle negative numbers.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="mod">The modulo to divide by.</param>
		/// <returns>The result of the modulo division.</returns>
		/// <remarks>
		/// See <see href="http://stackoverflow.com/questions/10065080/mod-explanation">this</see> and
		/// <see href="http://stackoverflow.com/questions/1082917/mod-of-negative-number-is-melting-my-brain">this</see>
		/// for more information about modulo in C#.
		/// </remarks>
		public static float ActualModulo(float value, float mod)
		{
			Contracts.Requires.That(mod != 0);

			mod = Math.Abs(mod);
			float result = value % mod;
			return result < 0 ? result + mod : result;
		}

		/// <summary>
		/// Calculates the actual modulo value. This is not the same as the % operator, because the % operator is not
		/// actually modulus, it's the remainder. The two functions differ in how they handle negative numbers.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="mod">The modulo to divide by.</param>
		/// <returns>The result of the modulo division.</returns>
		/// <remarks>
		/// See <see href="http://stackoverflow.com/questions/10065080/mod-explanation">this</see> and
		/// <see href="http://stackoverflow.com/questions/1082917/mod-of-negative-number-is-melting-my-brain">this</see>
		/// for more information about modulo in C#.
		/// </remarks>
		public static double ActualModulo(double value, double mod)
		{
			Contracts.Requires.That(mod != 0);

			mod = Math.Abs(mod);
			double result = value % mod;
			return result < 0 ? result + mod : result;
		}

		/// <summary>
		/// Calculates the actual modulo value. This is not the same as the % operator, because the % operator is not
		/// actually modulus, it's the remainder. The two functions differ in how they handle negative numbers.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <param name="mod">The modulo to divide by.</param>
		/// <returns>The result of the modulo division.</returns>
		/// <remarks>
		/// See <see href="http://stackoverflow.com/questions/10065080/mod-explanation">this</see> and
		/// <see href="http://stackoverflow.com/questions/1082917/mod-of-negative-number-is-melting-my-brain">this</see>
		/// for more information about modulo in C#.
		/// </remarks>
		public static decimal ActualModulo(decimal value, decimal mod)
		{
			Contracts.Requires.That(mod != 0);

			mod = Math.Abs(mod);
			decimal result = value % mod;
			return result < 0 ? result + mod : result;
		}

		#endregion

		public static int IntegerPower(int baseNumber, int exponent)
		{
			Contracts.Requires.That(exponent >= 0);

			int result = 1;
			for (int count = 0; count < exponent; count++)
			{
				result *= baseNumber;
			}

			return result;
		}

		// round up means towards the greater number
		// round down means towards the lesser number
		// this applies for negatives as well
		public static int IntegerMidpoint(int value1, int value2, bool roundUp = false)
		{
			int sum = value1 + value2;
			var result = sum / 2;

			if (sum.IsOdd())
			{
				if (sum > 0)
				{
					if (roundUp)
					{
						result++;
					}
				}
				else
				{
					if (!roundUp)
					{
						result--;
					}
				}
			}

			return result;
		}
	}
}
