namespace Voxelscape.Common.Contouring.Pact.DualContouring
{
	/// <summary>
	///
	/// </summary>
	public interface ISeedWritable : ISeedReadable
	{
		new float OriginalSeed { get; set; }

		new float AdjustedSeed { get; set; }
	}
}
