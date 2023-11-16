using System.Collections.Generic;
using System.Linq;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Collections
{
	public struct CountedEnumerable<T>
	{
		public CountedEnumerable(IReadOnlyCollection<T> values)
		{
			Contracts.Requires.That(values != null);

			this.Values = values;
			this.Count = values.Count;
		}

		public CountedEnumerable(ICollection<T> values)
		{
			Contracts.Requires.That(values != null);

			this.Values = values;
			this.Count = values.Count;
		}

		public CountedEnumerable(IEnumerable<T> values, int count)
		{
			Contracts.Requires.That(values != null);
			Contracts.Requires.That(count >= 0);
			Contracts.Requires.That(values.Count() == count);

			this.Values = values;
			this.Count = count;
		}

		public IEnumerable<T> Values { get; }

		public int Count { get; }

		public T[] ToArray()
		{
			var result = new T[this.Count];
			this.Values.CopyTo(result, 0);
			return result;
		}
	}
}
