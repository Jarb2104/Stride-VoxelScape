using System.Collections.Generic;
using Voxelscape.Utility.Common.Core.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	public class BufferedArray : IBufferedArray
	{
		private readonly IEnumerator<byte> source;

		private readonly int sourceCount;

		private readonly ExpandableArray<byte> buffer;

		private int totalCountBuffered = 0;

		public BufferedArray(CountedEnumerable<byte> source)
			: this(source, ByteLength.Decimal)
		{
		}

		public BufferedArray(CountedEnumerable<byte> source, int initialBufferSize)
		{
			Contracts.Requires.That(source.Values != null);
			Contracts.Requires.That(initialBufferSize >= 0);

			this.source = source.Values.GetEnumerator();
			this.sourceCount = source.Count;
			this.buffer = new ExpandableArray<byte>(initialBufferSize);
		}

		/// <inheritdoc />
		public byte[] Buffer => this.buffer.Array;

		/// <inheritdoc />
		public int? TotalRemainingLength => this.GetTotalRemainingLength();

		/// <inheritdoc />
		public bool HasNext => this.totalCountBuffered < this.sourceCount;

		/// <inheritdoc />
		public int BufferNext(int count)
		{
			IBufferedArrayContracts.BufferNext(count);

			if (count == 0)
			{
				return 0;
			}

			int countToBuffer = this.GetTotalRemainingLength().ClampUpper(count);
			this.buffer.EnsureCapacity(countToBuffer);

			int countBuffered;
			for (countBuffered = 0; countBuffered < countToBuffer; countBuffered++)
			{
				if (!this.source.MoveNext())
				{
					break;
				}

				this.buffer.Array[countBuffered] = this.source.Current;
			}

			this.totalCountBuffered += countBuffered;
			return countBuffered;
		}

		private int GetTotalRemainingLength() => this.sourceCount - this.totalCountBuffered;
	}
}
