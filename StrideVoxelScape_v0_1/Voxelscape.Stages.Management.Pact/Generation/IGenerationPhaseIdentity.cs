using System.Diagnostics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Management.Pact.Generation
{
	/// <summary>
	///
	/// </summary>
	public interface IGenerationPhaseIdentity
	{
		string Name { get; }
	}

	public static class IGenerationPhaseIdentityContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Constructor(string name)
		{
			Contracts.Requires.That(!name.IsNullOrWhiteSpace());
		}
	}
}
