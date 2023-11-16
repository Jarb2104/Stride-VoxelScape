using Voxelscape.Utility.Common.Pact.Disposables;

namespace Voxelscape.Utility.Concurrency.Pact.LifeCycle
{
	/// <summary>
	///
	/// </summary>
	public abstract class AbstractAsyncDisposedCancelable : AbstractAsyncCancelable, IDisposed
	{
		/// <inheritdoc />
		public bool IsDisposed => this.IsCompletionStarted;
	}
}
