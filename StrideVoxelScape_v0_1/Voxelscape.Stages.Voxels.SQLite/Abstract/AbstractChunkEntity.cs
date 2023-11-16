using SQLite;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Utility.Common.Pact.Types;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Stages.Voxels.SQLite.Abstract
{
	/// <summary>
	///
	/// </summary>
	public abstract class AbstractChunkEntity : IKeyed<int?>, ISerializedData
	{
		/// <inheritdoc />
		[PrimaryKey]
		public int? Key { get; set; }

		[Indexed]
		public int X { get; set; }

		[Indexed]
		public int Y { get; set; }

		[Indexed]
		public int Z { get; set; }

		[Ignore]
		public ChunkKey ChunkKey
		{
			get
			{
				return new ChunkKey(this.X, this.Y, this.Z);
			}

			set
			{
				this.X = value.Index.X;
				this.Y = value.Index.Y;
				this.Z = value.Index.Z;
			}
		}

		/// <inheritdoc />
		public byte[] SerializedData { get; set; }
	}
}
