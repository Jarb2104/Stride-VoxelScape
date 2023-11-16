using Voxelscape.Stages.Management.Pact.Stages;

namespace Voxelscape.Stages.Management.Core.Stages
{
	/// <summary>
	///
	/// </summary>
	public class StageIdentity : IStageIdentity
	{
		public StageIdentity(string name)
		{
			IStageIdentityContracts.Constructor(name);

			this.Name = name;
		}

		/// <inheritdoc />
		public string Name { get; }

		/// <inheritdoc />
		public override string ToString() => this.Name.ToString();
	}
}
