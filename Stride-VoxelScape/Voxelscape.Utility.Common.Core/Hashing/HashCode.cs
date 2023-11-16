namespace Voxelscape.Utility.Common.Core.Hashing
{
	/// <summary>
	/// The preferred order for trying these hash code functions is;
	/// <see cref="OneAtATime"/>, <see cref="FNV"/>, <see cref="ELF"/>, <see cref="Bernstein"/>, <see cref="ShiftAddXor"/>.
	/// </summary>
	/// <seealso href="http://stackoverflow.com/questions/1646807/quick-and-simple-hash-code-combinations"/>
	public static class HashCode
	{
		public static OneAtATimeHash Start { get; } = OneAtATime;

		public static BernsteinHash Bernstein { get; } = new BernsteinHash(17);

		public static ShiftAddXorHash ShiftAddXor { get; } = new ShiftAddXorHash(0);

		public static FNVHash FNV { get; } = new FNVHash(2166136261);

		public static OneAtATimeHash OneAtATime { get; } = new OneAtATimeHash(0);

		public static ELFHash ELF { get; } = new ELFHash(0);
	}
}
