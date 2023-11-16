namespace Voxelscape.Utility.Common.Pact.Disposables
{
	/// <summary>
	/// Defines a disposable wrapper for a value where the value can only be accessed
	/// until the wrapper is disposed of.
	/// </summary>
	/// <typeparam name="T">The type of the value.</typeparam>
	public interface IDisposableValue<out T> : ITemporaryValue<T>, IVisiblyDisposable
	{
	}
}
