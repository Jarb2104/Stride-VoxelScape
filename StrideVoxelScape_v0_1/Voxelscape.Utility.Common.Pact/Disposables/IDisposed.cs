namespace Voxelscape.Utility.Common.Pact.Disposables
{
	/// <summary>
	/// Defines a way for publicly checking if an instance is in a disposed state
	/// without also being able to dispose it directly.
	/// </summary>
	public interface IDisposed
	{
		/// <summary>
		/// Gets a value indicating whether this instance is disposed of.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance is disposed; otherwise, <c>false</c>.
		/// </value>
		bool IsDisposed { get; }
	}
}
