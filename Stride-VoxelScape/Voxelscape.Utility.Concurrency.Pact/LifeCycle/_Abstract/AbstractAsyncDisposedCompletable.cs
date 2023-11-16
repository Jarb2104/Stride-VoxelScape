using Voxelscape.Utility.Common.Pact.Disposables;

namespace Voxelscape.Utility.Concurrency.Pact.LifeCycle
{
	/// <summary>
	///
	/// </summary>
	public abstract class AbstractAsyncDisposedCompletable : AbstractAsyncCompletable, IDisposed
	{
		/// <inheritdoc />
		public bool IsDisposed => this.IsCompletionStarted;
	}
}
