using System.IO;
using Voxelscape.Utility.Common.Core.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	public class BufferedByteStream : IBufferedArray
	{
		private readonly Stream stream;

		private readonly ExpandableArray<byte> buffer;

		public BufferedByteStream(Stream source)
			: this(source, ByteLength.Decimal)
		{
		}

		public BufferedByteStream(Stream stream, int initialBufferSize)
		{
			Contracts.Requires.That(stream != null);
			Contracts.Requires.That(stream.CanRead);
			Contracts.Requires.That(initialBufferSize >= 0);

			this.stream = stream;
			this.buffer = new ExpandableArray<byte>(initialBufferSize);
			this.HasNext = true;
		}

		/// <inheritdoc />
		public byte[] Buffer => this.buffer.Array;

		/// <inheritdoc />
		public int? TotalRemainingLength => null;

		/// <inheritdoc />
		public bool HasNext
		{
			get;
			private set;
		}

		/// <inheritdoc />
		public int BufferNext(int count)
		{
			IBufferedArrayContracts.BufferNext(count);

			this.buffer.EnsureCapacity(count);
			int countBuffered = 0;

			while (countBuffered < count)
			{
				int read = this.stream.Read(this.buffer.Array, countBuffered, count - countBuffered);
				if (read == 0)
				{
					this.HasNext = false;
					break;
				}
				else
				{
					countBuffered += read;
				}
			}

			return countBuffered;
		}
	}
}
