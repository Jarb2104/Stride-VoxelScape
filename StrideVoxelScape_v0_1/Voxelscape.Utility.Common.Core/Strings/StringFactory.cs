using System;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Mathematics;

namespace Voxelscape.Utility.Common.Core.Strings
{
	/// <summary>
	/// Provides factory methods for creating strings.
	/// </summary>
	public static class StringFactory
	{
		/// <summary>
		/// Gets the set of uppercase characters as a string.
		/// </summary>
		/// <value>
		/// The uppercase characters.
		/// </value>
		public static string UppercaseChars
		{
			get { return "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
		}

		/// <summary>
		/// Gets the set of lowercase characters as a string.
		/// </summary>
		/// <value>
		/// The lowercase characters.
		/// </value>
		public static string LowercaseChars
		{
			get { return "abcdefghijklmnopqrstuvwxyz"; }
		}

		/// <summary>
		/// Gets the set of digit characters as a string.
		/// </summary>
		/// <value>
		/// The digit characters.
		/// </value>
		public static string DigitChars
		{
			get { return "0123456789"; }
		}

		/// <summary>
		/// Creates a random string of the specified length from the set of given characters.
		/// </summary>
		/// <param name="length">The length of the string to create.</param>
		/// <param name="characters">The characters to select from.</param>
		/// <returns>The randomly generated string.</returns>
		public static string CreateRandom(int length, string characters)
		{
			return CreateRandom(length, characters, ThreadLocalRandom.Instance);
		}

		/// <summary>
		/// Creates a random string of the specified length from the set of given characters.
		/// </summary>
		/// <param name="length">The length of the string to create.</param>
		/// <param name="characters">The characters to select from.</param>
		/// <param name="random">The Random used to select characters. This can be used to allow deterministic generation.</param>
		/// <returns>The randomly generated string.</returns>
		public static string CreateRandom(int length, string characters, Random random)
		{
			Contracts.Requires.That(length >= 0);
			Contracts.Requires.That(!characters.IsNullOrEmpty());
			Contracts.Requires.That(random != null);

			char[] result = new char[length];

			for (int index = 0; index < length; index++)
			{
				result[index] = characters[random.Next(characters.Length)];
			}

			return new string(result);
		}

		/// <summary>
		/// Creates a random string of the specified length from the set of given characters.
		/// </summary>
		/// <param name="minimumLength">The minimum length of the string to create.</param>
		/// <param name="maximumLength">The maximum length of the string to create.</param>
		/// <param name="characters">The characters to select from.</param>
		/// <returns>The randomly generated string.</returns>
		public static string CreateRandom(int minimumLength, int maximumLength, string characters)
		{
			return CreateRandom(minimumLength, maximumLength, characters, ThreadLocalRandom.Instance);
		}

		/// <summary>
		/// Creates a random string of the specified length from the set of given characters.
		/// </summary>
		/// <param name="minimumLength">The minimum length of the string to create.</param>
		/// <param name="maximumLength">The maximum length of the string to create.</param>
		/// <param name="characters">The characters to select from.</param>
		/// <param name="random">The Random used to select characters. This can be used to allow deterministic generation.</param>
		/// <returns>The randomly generated string.</returns>
		public static string CreateRandom(int minimumLength, int maximumLength, string characters, Random random)
		{
			Contracts.Requires.That(minimumLength >= 0);
			Contracts.Requires.That(minimumLength <= maximumLength);
			Contracts.Requires.That(!characters.IsNullOrEmpty());
			Contracts.Requires.That(random != null);

			// + 1 because Random.Next has an inclusive minimum but an exclusive maximum
			return CreateRandom(random.Next(minimumLength, maximumLength + 1), characters, random);
		}
	}
}
