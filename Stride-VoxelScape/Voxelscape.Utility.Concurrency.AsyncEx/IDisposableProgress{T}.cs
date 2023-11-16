using System;

namespace Voxelscape.Utility.Concurrency.AsyncEx
{
	/// <summary>
	/// An <see cref="IProgress{T}"/> that is disposable.
	/// </summary>
	/// <typeparam name="T">The type of progress updates.</typeparam>
	public interface IDisposableProgress<in T> : IProgress<T>, IDisposable
	{
	}
}
