using Voxelscape.Common.Contouring.Pact.Meshing;
using Voxelscape.Utility.Common.Pact.Collections;

namespace Voxelscape.Stride.Utility.Core.Meshing
{
	/// <summary>
	///
	/// </summary>
	public class MeshDataTransferOptions
	{
		public int InitialIndices { get; set; }

		public int InitialVertices { get; set; }

		public int MaxVertices { get; set; }

		public static MeshDataTransferOptions New16Bit() =>
			new MeshDataTransferOptions()
			{
				InitialIndices = MeshConstants.MaxVerticesSupportedBy16BitIndices,
				InitialVertices = MeshConstants.MaxVerticesSupportedBy16BitIndices,
				MaxVertices = MeshConstants.MaxVerticesSupportedBy16BitIndices,
			};

		public static MeshDataTransferOptions New32Bit() =>
			new MeshDataTransferOptions()
			{
				InitialIndices = MeshConstants.MaxVerticesSupportedBy16BitIndices,
				InitialVertices = MeshConstants.MaxVerticesSupportedBy16BitIndices,
				MaxVertices = Capacity.Unbounded,
			};

		public bool IsValid()
		{
			if (this.MaxVertices == Capacity.Unbounded)
			{
				return true;
			}

			return this.InitialVertices <= this.MaxVertices;
		}

		public bool IsValid16Bit()
		{
			if (this.MaxVertices > MeshConstants.MaxVerticesSupportedBy16BitIndices)
			{
				return false;
			}

			return this.InitialVertices <= this.MaxVertices;
		}
	}
}
