namespace Voxelscape.Utility.Common.Pact.Types
{
	/// <summary>
	/// Defines a key for uniquely identifying an instance.
	/// </summary>
	/// <typeparam name="T">The type of the key value.</typeparam>
	public interface IKeyed<T>
	{
		/// <summary>
		/// Gets the key to uniquely identify this instance.
		/// </summary>
		/// <value>
		/// The key.
		/// </value>
		T Key { get; }
	}
}
