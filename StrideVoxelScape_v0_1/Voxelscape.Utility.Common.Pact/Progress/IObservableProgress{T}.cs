using System;

namespace Voxelscape.Utility.Common.Pact.Progress
{
	/// <summary>
	///
	/// </summary>
	/// <typeparam name="T">The type of the progress notifications.</typeparam>
	public interface IObservableProgress<T>
	{
		IObservable<T> Progress { get; }
	}
}
