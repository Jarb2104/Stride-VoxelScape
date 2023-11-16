using Voxelscape.Common.Indexing.Pact.Indices;

namespace Voxelscape.Common.Indexing.Core.Indices
{
	/// <summary>
	/// Provides constants to be used in <see cref="IIndex"/> implementations.
	/// </summary>
	internal static class IndexConstants
	{
		/// <summary>
		/// The justification message for the unreachable code exceptions.
		/// </summary>
		public static readonly string UnreachableCodeExceptionJustificationMessage =
			"Contract requires dimension be within the interval [0, Rank)";
	}
}
