using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Exceptions;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	public class EndianProvider<T> : IEndianProvider<T>
		where T : IEndianSpecific
	{
		public EndianProvider(T endian1, T endian2)
		{
			Contracts.Requires.That(endian1 != null);
			Contracts.Requires.That(endian2 != null);
			Contracts.Requires.That(endian1.Endianness != endian2.Endianness);

			if (endian1.Endianness == Endian.Big)
			{
				this.BigEndian = endian1;
				this.LittleEndian = endian2;
			}
			else
			{
				this.BigEndian = endian2;
				this.LittleEndian = endian1;
			}
		}

		/// <inheritdoc />
		public T BigEndian { get; }

		/// <inheritdoc />
		public T LittleEndian { get; }

		/// <inheritdoc />
		public T this[Endian endianness]
		{
			get
			{
				switch (endianness)
				{
					case Endian.Big: return this.BigEndian;
					case Endian.Little: return this.LittleEndian;
					default: throw InvalidEnumArgument.CreateException(nameof(endianness), endianness);
				}
			}
		}
	}
}
