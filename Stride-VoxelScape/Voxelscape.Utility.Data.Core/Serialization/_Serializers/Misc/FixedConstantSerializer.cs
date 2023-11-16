using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	/// <summary>
	///
	/// </summary>
	public static class FixedConstantSerializer
	{
		public static IEndianProvider<FixedConstantSerializer<T>> NewProvider<T>(T constantValue) =>
			EndianProvider.New(
				new FixedConstantSerializer<T>(constantValue, Endian.Big),
				new FixedConstantSerializer<T>(constantValue, Endian.Little));
	}
}
