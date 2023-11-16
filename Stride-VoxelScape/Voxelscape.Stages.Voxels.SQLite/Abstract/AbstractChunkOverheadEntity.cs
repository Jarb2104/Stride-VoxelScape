using SQLite;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Utility.Common.Pact.Types;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Stages.Voxels.SQLite.Abstract
{
	/// <summary>
	///
	/// </summary>
	internal abstract class AbstractChunkOverheadEntity : IKeyed<int?>, ISerializedData
	{
		/// <inheritdoc />
		[PrimaryKey]
		public int? Key { get; set; }

		[Indexed]
		public int X { get; set; }

		[Indexed]
		public int Y { get; set; }

		[Ignore]
		public ChunkOverheadKey ChunkKey
		{
			get
			{
				return new ChunkOverheadKey(this.X, this.Y);
			}

			set
			{
				this.X = value.Index.X;
				this.Y = value.Index.Y;
			}
		}

		/// <inheritdoc />
		public byte[] SerializedData { get; set; }
	}
}
