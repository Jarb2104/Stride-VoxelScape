using System.Collections;
using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Core.Enumerables
{
	/// <summary>
	///
	/// </summary>
	public class OscillatingRange : IEnumerable<int>
	{
		private readonly int min;

		private readonly int max;

		private bool isIncreasing;

		public OscillatingRange(int start, int count)
		{
			Contracts.Requires.That(count >= 0);

			this.min = start;
			this.max = start + count - 1;
			this.isIncreasing = true;
		}

		/// <inheritdoc />
		public IEnumerator<int> GetEnumerator()
		{
			if (this.isIncreasing)
			{
				for (int number = this.min; number <= this.max; number++)
				{
					yield return number;
				}
			}
			else
			{
				for (int number = this.max; number >= this.min; number--)
				{
					yield return number;
				}
			}

			this.isIncreasing = !this.isIncreasing;
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
}
