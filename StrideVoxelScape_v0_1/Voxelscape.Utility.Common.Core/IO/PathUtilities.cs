using System.IO;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.IO
{
	/// <summary>
	/// Performs operations on String instances that contain file or directory path information.
	/// These operations are performed in a cross-platform manner.
	/// </summary>
	public static class PathUtilities
	{
		public static string Combine(params string[] paths)
		{
			Contracts.Requires.That(paths.AllAndSelfNotNull());
			Contracts.Requires.That(paths.Length >= 2);

			string result = paths[0];
			for (int index = 1; index < paths.Length; index++)
			{
				result = Path.Combine(result, paths[index]);
			}

			return result;
		}
	}
}
