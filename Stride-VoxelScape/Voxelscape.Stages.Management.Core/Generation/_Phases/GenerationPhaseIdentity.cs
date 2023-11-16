using Voxelscape.Stages.Management.Pact.Generation;

namespace Voxelscape.Stages.Management.Core.Generation
{
	/// <summary>
	///
	/// </summary>
	public class GenerationPhaseIdentity : IGenerationPhaseIdentity
	{
		public GenerationPhaseIdentity(string name)
		{
			IGenerationPhaseIdentityContracts.Constructor(name);

			this.Name = name;
		}

		/// <inheritdoc />
		public string Name { get; }

		/// <inheritdoc />
		public override string ToString() => this.Name.ToString();
	}
}
