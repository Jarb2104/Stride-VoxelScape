namespace Voxelscape.Utility.Concurrency.Core.Reactive
{
	/// <summary>
	///
	/// </summary>
	public static class SequentialPair
	{
		public static SequentialPair<T> New<T>(T previous, T next) => new SequentialPair<T>(previous, next);
	}
}
