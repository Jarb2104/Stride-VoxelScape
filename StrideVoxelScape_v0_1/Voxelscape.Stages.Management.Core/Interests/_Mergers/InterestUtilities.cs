using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Management.Core.Interests
{
	/// <summary>
	///
	/// </summary>
	internal static class InterestUtilities
	{
		public static string ToString(IEnumerable<int> interests, int maxInterest)
		{
			Contracts.Requires.That(interests != null);
			Contracts.Requires.That(maxInterest >= 0);

			var chars = new char[maxInterest];
			for (var index = 0; index < chars.Length; index++)
			{
				chars[index] = '0';
			}

			var lastIndex = chars.Length - 1;
			foreach (var index in interests)
			{
				chars[lastIndex - index] = '1';
			}

			return new string(chars);
		}
	}
}
