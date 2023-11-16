using System;
using System.Threading;

namespace Voxelscape.Utility.Common.Pact.Mathematics
{
	/// <summary>
	/// Provides a <see cref="Random"/> instance that is guaranteed to return a random sequence of numbers. This corrects
	/// the problem discussed <see href="http://csharpindepth.com/Articles/Chapter12/Random.aspx">here</see>.
	/// </summary>
	public static class ThreadLocalRandom
	{
		/// <summary>
		/// The seed of the previous random instance. Interlocked.Increment will increment the value before returning it.
		/// </summary>
		private static int seed = Environment.TickCount;

		/// <summary>
		/// The thread local random. This will return a unique copy of the random instance per thread but must start
		/// as null because of the use of <see cref="ThreadStaticAttribute"/>.
		/// </summary>
		[ThreadStatic]
		private static Random threadLocalRandom = null;

		/// <summary>
		/// Gets the <see cref="Instance"/> instance. This will return a unique instance per thread.
		/// </summary>
		/// <value>
		/// The random instance.
		/// </value>
		/// <remarks>Do not share or pass this instance between threads.</remarks>
		public static Random Instance
		{
			get
			{
				if (threadLocalRandom != null)
				{
					return threadLocalRandom;
				}
				else
				{
					threadLocalRandom = new Random(Interlocked.Increment(ref seed));
					return threadLocalRandom;
				}
			}
		}
	}
}
