using System;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Core.Serialization;
using Voxelscape.Stages.Management.Pact.Chunks;
using Voxelscape.Stages.Voxels.Pact.Voxels;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Stages.Voxels.Core.Voxels.Grid
{
	/// <summary>
	///
	/// </summary>
	public class VoxelGridChunkResourcesSerializer : IInlineSerializerDeserializer<VoxelGridChunkResources>
	{
		private readonly BoundedIndexable3DSerializer<TerrainVoxel> serializer;

		public VoxelGridChunkResourcesSerializer(
			IConstantSerializerDeserializer<TerrainVoxel> serializer, IRasterChunkConfig<Index3D> config)
		{
			Contracts.Requires.That(serializer != null);
			Contracts.Requires.That(config != null);

			this.serializer = new BoundedIndexable3DSerializer<TerrainVoxel>(serializer, config.Bounds.Dimensions);
		}

		/// <inheritdoc />
		public Endian Endianness => this.serializer.Endianness;

		/// <inheritdoc />
		public int GetSerializedLength(VoxelGridChunkResources value)
		{
			ISerializerContracts.GetSerializedLength(value);

			return this.serializer.GetSerializedLength(value.Voxels);
		}

		/// <inheritdoc />
		public int Serialize(VoxelGridChunkResources value, Action<byte> writeByte)
		{
			ISerializerContracts.Serialize(value, writeByte);

			return this.serializer.Serialize(value.Voxels, writeByte);
		}

		/// <inheritdoc />
		public int Serialize(VoxelGridChunkResources value, byte[] buffer, ref int index)
		{
			ISerializerContracts.Serialize(this, value, buffer, index);

			return this.serializer.Serialize(value.Voxels, buffer, ref index);
		}

		/// <inheritdoc />
		public void DeserializeInline(
			IBufferedArray buffer, VoxelGridChunkResources result)
		{
			IInlineDeserializerContracts.DeserializeInline(buffer, result);

			this.serializer.DeserializeInline(buffer, result.Voxels);
		}

		/// <inheritdoc />
		public void DeserializeInline(
			byte[] buffer, ref int index, VoxelGridChunkResources result)
		{
			IInlineDeserializerContracts.DeserializeInline(buffer, index, result);

			this.serializer.DeserializeInline(buffer, ref index, result.Voxels);
		}
	}
}
