using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Progress;

namespace Voxelscape.Utility.Common.Core.Progress
{
	/// <summary>
	///
	/// </summary>
	public class DiscreteProgress : IDiscreteProgress
	{
		public DiscreteProgress(long countCompleted, long totalCount)
		{
			Contracts.Requires.That(countCompleted >= 0);
			Contracts.Requires.That(totalCount >= 0);
			Contracts.Requires.That(countCompleted <= totalCount);

			this.CountCompleted = countCompleted;
			this.TotalCount = totalCount;
		}

		/// <inheritdoc />
		public long CountCompleted { get; }

		/// <inheritdoc />
		public long TotalCount { get; }

		public static DiscreteProgress operator +(DiscreteProgress acc, DiscreteProgress next) =>
			new DiscreteProgress(acc.CountCompleted + next.CountCompleted, next.TotalCount);
	}
}
