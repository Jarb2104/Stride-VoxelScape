using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Collections
{
	public class ExpandableArray<T>
	{
		public ExpandableArray(int initialCapacity)
		{
			Contracts.Requires.That(initialCapacity >= 0);

			this.Array = new T[initialCapacity];
		}

		public T[] Array { get; private set; }

		// does not copy values from old array to new one
		public void EnsureCapacity(int capacity, int maxArrayLength = Capacity.Unbounded)
		{
			Contracts.Requires.That(capacity >= 0);
			Contracts.Requires.That(maxArrayLength >= this.Array.Length || maxArrayLength == Capacity.Unbounded);
			Contracts.Requires.That(capacity <= maxArrayLength || maxArrayLength == Capacity.Unbounded);

			if (capacity <= this.Array.Length)
			{
				return;
			}

			int newSize = this.Array.Length;

			if (newSize == 0)
			{
				// default starting size
				newSize = 4;
			}

			do
			{
				newSize *= 2;
			}
			while (newSize < capacity);

			if (maxArrayLength != Capacity.Unbounded)
			{
				newSize = newSize.ClampUpper(maxArrayLength);
			}

			this.Array = new T[newSize];
		}

		public void CopyIn(CountedEnumerable<T> values, int maxArrayLength = Capacity.Unbounded)
		{
			Contracts.Requires.That(values.Values != null);
			Contracts.Requires.That(values.Count <= maxArrayLength || maxArrayLength == Capacity.Unbounded);

			this.EnsureCapacity(values.Count, maxArrayLength);

			int index = 0;
			foreach (var value in values.Values)
			{
				this.Array[index] = value;
				index++;
			}
		}
	}
}
