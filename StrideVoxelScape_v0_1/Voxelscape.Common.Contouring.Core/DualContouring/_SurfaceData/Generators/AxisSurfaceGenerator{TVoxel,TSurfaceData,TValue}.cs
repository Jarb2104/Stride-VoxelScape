using Voxelscape.Common.Contouring.Pact.DualContouring;
using Voxelscape.Common.Indexing.Core.Enums;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Exceptions;

namespace Voxelscape.Common.Contouring.Core.DualContouring
{
	public class AxisSurfaceGenerator<TVoxel, TSurfaceData, TValue> :
		ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue>
		where TVoxel : struct
		where TSurfaceData : class
		where TValue : struct
	{
		private readonly ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue> positiveXAxisGenerator;

		private readonly ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue> negativeXAxisGenerator;

		private readonly ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue> positiveYAxisGenerator;

		private readonly ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue> negativeYAxisGenerator;

		private readonly ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue> positiveZAxisGenerator;

		private readonly ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue> negativeZAxisGenerator;

		public AxisSurfaceGenerator(
			ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue> positiveXAxisGenerator,
			ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue> negativeXAxisGenerator,
			ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue> positiveYAxisGenerator,
			ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue> negativeYAxisGenerator,
			ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue> positiveZAxisGenerator,
			ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue> negativeZAxisGenerator)
		{
			Contracts.Requires.That(positiveXAxisGenerator != null);
			Contracts.Requires.That(negativeXAxisGenerator != null);
			Contracts.Requires.That(positiveYAxisGenerator != null);
			Contracts.Requires.That(negativeYAxisGenerator != null);
			Contracts.Requires.That(positiveZAxisGenerator != null);
			Contracts.Requires.That(negativeZAxisGenerator != null);

			this.positiveXAxisGenerator = positiveXAxisGenerator;
			this.negativeXAxisGenerator = negativeXAxisGenerator;
			this.positiveYAxisGenerator = positiveYAxisGenerator;
			this.negativeYAxisGenerator = negativeYAxisGenerator;
			this.positiveZAxisGenerator = positiveZAxisGenerator;
			this.negativeZAxisGenerator = negativeZAxisGenerator;
		}

		public AxisSurfaceGenerator(
			ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue> xAxisGenerator,
			ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue> yAxisGenerator,
			ISurfaceValueGenerator<TVoxel, TSurfaceData, TValue> zAxisGenerator)
			: this(xAxisGenerator, xAxisGenerator, yAxisGenerator, yAxisGenerator, zAxisGenerator, zAxisGenerator)
		{
			Contracts.Requires.That(xAxisGenerator != null);
			Contracts.Requires.That(yAxisGenerator != null);
			Contracts.Requires.That(zAxisGenerator != null);
		}

		#region ISurfaceValueGenerator<...> Members

		public void GenerateValues(
			IVoxelProjection<TVoxel, TSurfaceData> projection,
			out TValue topLeftValue,
			out TValue topRightValue,
			out TValue bottomLeftValue,
			out TValue bottomRightValue)
		{
			switch (projection.AxisDirection)
			{
				case AxisDirection3D.PositiveX:
					this.positiveXAxisGenerator.GenerateValues(
						projection,
						out topLeftValue,
						out topRightValue,
						out bottomLeftValue,
						out bottomRightValue);
					break;

				case AxisDirection3D.NegativeX:
					this.negativeXAxisGenerator.GenerateValues(
						projection,
						out topLeftValue,
						out topRightValue,
						out bottomLeftValue,
						out bottomRightValue);
					break;

				case AxisDirection3D.PositiveY:
					this.positiveYAxisGenerator.GenerateValues(
						projection,
						out topLeftValue,
						out topRightValue,
						out bottomLeftValue,
						out bottomRightValue);
					break;

				case AxisDirection3D.NegativeY:
					this.negativeYAxisGenerator.GenerateValues(
						projection,
						out topLeftValue,
						out topRightValue,
						out bottomLeftValue,
						out bottomRightValue);
					break;

				case AxisDirection3D.PositiveZ:
					this.positiveZAxisGenerator.GenerateValues(
						projection,
						out topLeftValue,
						out topRightValue,
						out bottomLeftValue,
						out bottomRightValue);
					break;

				case AxisDirection3D.NegativeZ:
					this.negativeZAxisGenerator.GenerateValues(
						projection,
						out topLeftValue,
						out topRightValue,
						out bottomLeftValue,
						out bottomRightValue);
					break;

				default:
					throw InvalidEnumArgument.CreateException(nameof(projection.AxisDirection), projection.AxisDirection);
			}
		}

		#endregion
	}
}
