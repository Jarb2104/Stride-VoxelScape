namespace Voxelscape.Utility.Common.Pact.Disposables
{
	/// <summary>
	///
	/// </summary>
	public interface ITryDisposable : IDisposed
	{
		bool TryDispose();
	}
}
