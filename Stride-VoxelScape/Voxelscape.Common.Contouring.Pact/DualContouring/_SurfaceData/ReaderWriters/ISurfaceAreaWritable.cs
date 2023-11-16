namespace Voxelscape.Common.Contouring.Pact.DualContouring
{
	/// <summary>
	///
	/// </summary>
	public interface ISurfaceAreaWritable : ISurfaceAreaReadable
	{
		new float SurfaceArea { get; set; }
	}
}
