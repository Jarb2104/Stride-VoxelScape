using System.IO;
using Voxelscape.Utility.Common.Core.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	public class ByteStreamSerializer<T>
	{
		private readonly Stream stream;

		private readonly ISerializerDeserializer<T> serializer;

		private readonly ExpandableArray<byte> byteBuffer;

		public ByteStreamSerializer(Stream stream, ISerializerDeserializer<T> serializer)
			: this(stream, serializer, ByteLength.Decimal)
		{
		}

		public ByteStreamSerializer(Stream stream, ISerializerDeserializer<T> serializer, int initialBufferSize)
		{
			Contracts.Requires.That(stream != null);
			Contracts.Requires.That(stream.CanWrite);
			Contracts.Requires.That(serializer != null);
			Contracts.Requires.That(initialBufferSize >= 0);

			this.stream = stream;
			this.serializer = serializer;
			this.byteBuffer = new ExpandableArray<byte>(initialBufferSize);
		}

		public void SerializeToStream(T value)
		{
			Contracts.Requires.That(value != null);

			int serializedLength = this.serializer.GetSerializedLength(value);
			this.byteBuffer.EnsureCapacity(serializedLength);

			// TODO Steven - this causes the entire type to be serialized before writing it out to stream
			// that could potentially require a lot of memory. Is this really the best approach?
			// Is this class even needed when there's the int Serialize(T value, Action<byte> writeByte) method?
			this.serializer.Serialize(value, this.byteBuffer.Array);
			this.stream.Write(this.byteBuffer.Array, 0, serializedLength);
			this.stream.Flush();
		}
	}
}
