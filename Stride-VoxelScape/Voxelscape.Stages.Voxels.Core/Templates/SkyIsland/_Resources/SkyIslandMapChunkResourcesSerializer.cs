using System;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Core.Serialization;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Voxels.Pact.Templates.SkyIsland;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Stages.Voxels.Core.Templates.SkyIsland
{
	/// <summary>
	///
	/// </summary>
	public class SkyIslandMapChunkResourcesSerializer : IInlineSerializerDeserializer<SkyIslandMapChunkResources>
	{
		private readonly BoundedIndexable2DSerializer<SkyIslandMaps> serializer;

		public SkyIslandMapChunkResourcesSerializer(
			IConstantSerializerDeserializer<SkyIslandMaps> serializer, IRasterChunkConfig<Index2D> config)
		{
			Contracts.Requires.That(serializer != null);
			Contracts.Requires.That(config != null);

			this.serializer = new BoundedIndexable2DSerializer<SkyIslandMaps>(serializer, config.Bounds.Dimensions);
		}

		/// <inheritdoc />
		public Endian Endianness => this.serializer.Endianness;

		/// <inheritdoc />
		public int GetSerializedLength(SkyIslandMapChunkResources value)
		{
			ISerializerContracts.GetSerializedLength(value);

			return this.serializer.GetSerializedLength(value.Maps);
		}

		/// <inheritdoc />
		public int Serialize(SkyIslandMapChunkResources value, Action<byte> writeByte)
		{
			ISerializerContracts.Serialize(value, writeByte);

			return this.serializer.Serialize(value.Maps, writeByte);
		}

		/// <inheritdoc />
		public int Serialize(SkyIslandMapChunkResources value, byte[] buffer, ref int index)
		{
			ISerializerContracts.Serialize(this, value, buffer, index);

			return this.serializer.Serialize(value.Maps, buffer, ref index);
		}

		/// <inheritdoc />
		public void DeserializeInline(
			IBufferedArray buffer, SkyIslandMapChunkResources result)
		{
			IInlineDeserializerContracts.DeserializeInline(buffer, result);

			this.serializer.DeserializeInline(buffer, result.Maps);
		}

		/// <inheritdoc />
		public void DeserializeInline(
			byte[] buffer, ref int index, SkyIslandMapChunkResources result)
		{
			IInlineDeserializerContracts.DeserializeInline(buffer, index, result);

			this.serializer.DeserializeInline(buffer, ref index, result.Maps);
		}
	}
}
