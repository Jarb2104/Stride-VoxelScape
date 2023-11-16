namespace Voxelscape.Stages.Voxels.Pact.Templates.SkyIsland
{
	/// <summary>
	///
	/// </summary>
	public struct SkyIslandMaps
	{
		public SkyIslandMaps(
			double shapePercentWeight,
			double baselineHeight,
			double mountainHeightMultiplier,
			double topHeight,
			double bottomHeight)
			: this(
				  (float)shapePercentWeight,
				  (float)baselineHeight,
				  (float)mountainHeightMultiplier,
				  (float)topHeight,
				  (float)bottomHeight)
		{
		}

		public SkyIslandMaps(
			float shapePercentWeight,
			float baselineHeight,
			float mountainHeightMultiplier,
			float topHeight,
			float bottomHeight)
		{
			this.ShapePercentWeight = shapePercentWeight;
			this.BaselineHeight = baselineHeight;
			this.MountainHeightMultiplier = mountainHeightMultiplier;
			this.TopHeight = topHeight;
			this.BottomHeight = bottomHeight;
		}

		// -1 to 1
		public float ShapePercentWeight { get; }

		// actual height in gamespace
		public float BaselineHeight { get; }

		// how many times taller than usual the space is
		public float MountainHeightMultiplier { get; }

		// actual height in gamespace
		public float TopHeight { get; }

		// actual height in gamespace
		public float BottomHeight { get; }
	}
}
