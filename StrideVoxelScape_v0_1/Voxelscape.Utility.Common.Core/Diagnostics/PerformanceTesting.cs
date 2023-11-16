using System;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Diagnostics
{
	/// <summary>
	/// Provides methods for timing how long code takes to execute.
	/// </summary>
	public static class PerformanceTesting
	{
		/// <summary>
		/// Runs the specified action in a loop for the specified duration, returning the average time per iteration.
		/// </summary>
		/// <param name="subject">The action to run.</param>
		/// <param name="duration">The duration to run for.</param>
		/// <returns>The average time per iteration of the action.</returns>
		public static TimeSpan RunFor(Action subject, TimeSpan duration)
		{
			Contracts.Requires.That(subject != null);
			Contracts.Requires.That(duration >= TimeSpan.Zero);

			long count = 0;
			DateTime start = DateTime.Now;
			DateTime end = start.Add(duration);
			DateTime current = DateTime.Now;

			while (current < end)
			{
				subject();
				count++;
				current = DateTime.Now;
			}

			TimeSpan actualDuration = current - start;

			return new TimeSpan(actualDuration.Ticks / count);
		}
	}
}
