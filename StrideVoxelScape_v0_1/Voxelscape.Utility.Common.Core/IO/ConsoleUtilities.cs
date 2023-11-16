using System;
using System.Threading;

namespace Voxelscape.Utility.Common.Core.IO
{
	/// <summary>
	/// Provides helpers methods for working with console input and output.
	/// </summary>
	public static class ConsoleUtilities
	{
		/// <summary>
		/// Gets the lock for controlling multithreaded access to the console. Use of the lock while printing console output from
		/// multiple threads can help eliminate jumbled up text.
		/// </summary>
		/// <value>
		/// The console lock.
		/// </value>
		public static object ConsoleLock { get; } = new object();

		/// <summary>
		/// Locks the console for single threaded use. Be sure to call UnlockConsole to release the console as soon as possible.
		/// </summary>
		public static void LockConsole() => Monitor.Enter(ConsoleLock);

		/// <summary>
		/// Unlocks the console. Only call this from the thread that currently holds the lock.
		/// </summary>
		public static void UnlockConsole() => Monitor.Exit(ConsoleLock);

		/// <summary>
		/// Delays the console window from exiting. The window won't close until the user presses 'enter'.
		/// </summary>
		public static void DelayConsoleWindowExit()
		{
			Console.Out.WriteLine();
			Console.Out.Write("Press 'enter' to end.");
			Console.ReadLine();
		}
	}
}
