using System.Diagnostics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Management.Pact.Stages
{
	/// <summary>
	///
	/// </summary>
	public interface IStageIdentity
	{
		string Name { get; }
	}

	public static class IStageIdentityContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Constructor(string name)
		{
			Contracts.Requires.That(!name.IsNullOrWhiteSpace());
		}
	}
}
