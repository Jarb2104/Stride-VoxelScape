using System;
using System.Threading;

namespace Voxelscape.Utility.Concurrency.Pact
{
	/// <summary>
	/// Provides utility methods for working with time intervals.
	/// </summary>
	public static class Time
	{
		/// <summary>
		/// Determines whether the specified number of milliseconds is zero, positive, or infinite.
		/// </summary>
		/// <param name="milliseconds">The number of milliseconds.</param>
		/// <returns>True if the number of milliseconds is a valid time interval, otherwise false.</returns>
		public static bool IsDuration(int milliseconds) =>
			milliseconds >= Milliseconds.InstantTimeout || milliseconds == Milliseconds.InfiniteTimeout;

		/// <summary>
		/// Determines whether the specified number of milliseconds is zero, positive, or infinite.
		/// </summary>
		/// <param name="milliseconds">The number of milliseconds.</param>
		/// <returns>True if the number of milliseconds is a valid time interval, otherwise false.</returns>
		public static bool IsDuration(long milliseconds) =>
			milliseconds >= Milliseconds.InstantTimeout || milliseconds == Milliseconds.InfiniteTimeout;

		/// <summary>
		/// Determines whether the specified timeSpan is zero, positive, or infinite.
		/// </summary>
		/// <param name="timeSpan">The timeSpan.</param>
		/// <returns>True if the timeSpan is a valid time interval, otherwise false.</returns>
		public static bool IsDuration(this TimeSpan timeSpan) =>
			timeSpan >= Span.InstantTimeout || timeSpan == Span.InfiniteTimeout;

		/// <summary>
		/// Determines whether the specified number of milliseconds is zero or positive.
		/// </summary>
		/// <param name="milliseconds">The number of milliseconds.</param>
		/// <returns>True if the number of milliseconds is a valid time interval, otherwise false.</returns>
		public static bool IsFiniteDuration(int milliseconds) => milliseconds >= Milliseconds.InstantTimeout;

		/// <summary>
		/// Determines whether the specified number of milliseconds is zero or positive.
		/// </summary>
		/// <param name="milliseconds">The number of milliseconds.</param>
		/// <returns>True if the number of milliseconds is a valid time interval, otherwise false.</returns>
		public static bool IsFiniteDuration(long milliseconds) => milliseconds >= Milliseconds.InstantTimeout;

		/// <summary>
		/// Determines whether the specified timeSpan is zero or positive.
		/// </summary>
		/// <param name="timeSpan">The timeSpan.</param>
		/// <returns>True if the timeSpan is a valid time interval, otherwise false.</returns>
		public static bool IsFiniteDuration(this TimeSpan timeSpan) => timeSpan >= Span.InstantTimeout;

		public static class Milliseconds
		{
			/// <summary>
			/// Gets the instant timeout value.
			/// </summary>
			/// <value>
			/// The instant timeout value.
			/// </value>
			public static int InstantTimeout => 0;

			/// <summary>
			/// Gets the infinite timeout value.
			/// </summary>
			/// <value>
			/// The infinite timeout value.
			/// </value>
			public static int InfiniteTimeout => Timeout.Infinite;
		}

		public static class Span
		{
			/// <summary>
			/// Gets the instant timeout <see cref="TimeSpan"/> value.
			/// </summary>
			/// <value>
			/// The instant timeout value.
			/// </value>
			public static TimeSpan InstantTimeout => TimeSpan.Zero;

			/// <summary>
			/// Gets the infinite timeout <see cref="TimeSpan"/> value.
			/// </summary>
			/// <value>
			/// The infinite timeout value.
			/// </value>
			public static TimeSpan InfiniteTimeout => Timeout.InfiniteTimeSpan;
		}
	}
}
