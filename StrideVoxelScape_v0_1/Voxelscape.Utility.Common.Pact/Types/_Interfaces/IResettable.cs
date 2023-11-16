namespace Voxelscape.Utility.Common.Pact.Types
{
	/// <summary>
	/// Defines a type capable of resetting its internal state.
	/// </summary>
	public interface IResettable
	{
		/// <summary>
		/// Resets this instance.
		/// </summary>
		/// <remarks>
		/// There should be no observable state left over from prior to calling this method.
		/// </remarks>
		void Reset();
	}
}
