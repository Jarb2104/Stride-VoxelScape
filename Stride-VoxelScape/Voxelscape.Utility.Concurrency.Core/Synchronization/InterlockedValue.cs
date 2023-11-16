using System.Threading;

namespace Voxelscape.Utility.Concurrency.Core.Synchronization
{
	/// <summary>
	///
	/// </summary>
	public static class InterlockedValue
	{
		public static int Read(ref int value) => Interlocked.CompareExchange(ref value, 0, 0);

		public static long Read(ref long value) => Interlocked.CompareExchange(ref value, 0, 0);

		public static float Read(ref float value) => Interlocked.CompareExchange(ref value, 0, 0);

		public static double Read(ref double value) => Interlocked.CompareExchange(ref value, 0, 0);
	}
}
