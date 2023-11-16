using Voxelscape.Stages.Voxels.Pact.Terrain;
using Voxelscape.Utility.Common.Core.Enums;

namespace Voxelscape.Stages.Voxels.Core.Terrain
{
	/// <summary>
	///
	/// </summary>
	public class TerrainMaterialValidator : AbstractEnumValidator<TerrainMaterial>
	{
		private TerrainMaterialValidator()
		{
		}

		public static TerrainMaterialValidator Instance { get; } = new TerrainMaterialValidator();

		/// <inheritdoc />
		public override bool IsValidEnumValue(TerrainMaterial value) =>
			value >= this.MinValue && value <= this.MaxValue;
	}
}
