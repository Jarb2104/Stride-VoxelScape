using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Stages.Management.Core.Generation
{
	/// <summary>
	///
	/// </summary>
	public class BatchSerializeGenerationOptions : BatchGenerationOptions
	{
		public static Endian DefaultSerializationEndianness => Endian.Little;

		public Endian SerializationEndianness { get; set; } = DefaultSerializationEndianness;
	}
}
