using Voxelscape.Utility.Common.Core.Mathematics;
using Voxelscape.Utility.Common.Pact.Exceptions;

/// <summary>
/// Provides extension methods for <see cref="RangeClusivity"/>.
/// </summary>
public static class RangeClusivityExtensions
{
	public static Clusivity GetMinClusivity(this RangeClusivity clusivity)
	{
		switch (clusivity)
		{
			case RangeClusivity.Inclusive: return Clusivity.Inclusive;
			case RangeClusivity.Exclusive: return Clusivity.Exclusive;
			case RangeClusivity.InclusiveMin: return Clusivity.Inclusive;
			case RangeClusivity.InclusiveMax: return Clusivity.Exclusive;
			default: throw InvalidEnumArgument.CreateException(nameof(clusivity), clusivity);
		}
	}

	public static Clusivity GetMaxClusivity(this RangeClusivity clusivity)
	{
		switch (clusivity)
		{
			case RangeClusivity.Inclusive: return Clusivity.Inclusive;
			case RangeClusivity.Exclusive: return Clusivity.Exclusive;
			case RangeClusivity.InclusiveMin: return Clusivity.Exclusive;
			case RangeClusivity.InclusiveMax: return Clusivity.Inclusive;
			default: throw InvalidEnumArgument.CreateException(nameof(clusivity), clusivity);
		}
	}
}
