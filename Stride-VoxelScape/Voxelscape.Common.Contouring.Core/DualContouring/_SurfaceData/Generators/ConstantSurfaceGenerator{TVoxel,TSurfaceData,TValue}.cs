using Voxelscape.Common.Contouring.Pact.DualContouring;

namespace Voxelscape.Common.Contouring.Core.DualContouring
{
	public class ConstantSurfaceGenerator<TVoxel, TSurfaceData, TValue> :
		ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue>
		where TVoxel : struct
		where TSurfaceData : class
		where TValue : struct
	{
		private readonly TValue topLeftValue;

		private readonly TValue topRightValue;

		private readonly TValue bottomLeftValue;

		private readonly TValue bottomRightValue;

		public ConstantSurfaceGenerator(TValue value)
			: this(value, value, value, value)
		{
		}

		public ConstantSurfaceGenerator(TValue topLeftValue, TValue topRightValue, TValue bottomLeftValue, TValue bottomRightValue)
		{
			this.topLeftValue = topLeftValue;
			this.topRightValue = topRightValue;
			this.bottomLeftValue = bottomLeftValue;
			this.bottomRightValue = bottomRightValue;
		}

		#region ISurfaceValueGenerator<...> Members

		public void GenerateValues(
			IVoxelProjection<TVoxel, TSurfaceData> projection,
			out TValue topLeftValue,
			out TValue topRightValue,
			out TValue bottomLeftValue,
			out TValue bottomRightValue)
		{
			topLeftValue = this.topLeftValue;
			topRightValue = this.topRightValue;
			bottomLeftValue = this.bottomLeftValue;
			bottomRightValue = this.bottomRightValue;
		}

		#endregion
	}
}
