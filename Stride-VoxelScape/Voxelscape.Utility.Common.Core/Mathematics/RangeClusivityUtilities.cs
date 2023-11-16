using Voxelscape.Utility.Common.Pact.Exceptions;

namespace Voxelscape.Utility.Common.Core.Mathematics
{
	/// <summary>
	///
	/// </summary>
	public static class RangeClusivityUtilities
	{
		public static RangeClusivity Combine(Clusivity min, Clusivity max)
		{
			switch (min)
			{
				case Clusivity.Exclusive:
					switch (max)
					{
						case Clusivity.Exclusive: return RangeClusivity.Exclusive;
						case Clusivity.Inclusive: return RangeClusivity.InclusiveMax;
						default: throw InvalidEnumArgument.CreateException(nameof(max), max);
					}

				case Clusivity.Inclusive:
					switch (max)
					{
						case Clusivity.Exclusive: return RangeClusivity.InclusiveMin;
						case Clusivity.Inclusive: return RangeClusivity.Inclusive;
						default: throw InvalidEnumArgument.CreateException(nameof(max), max);
					}

				default: throw InvalidEnumArgument.CreateException(nameof(min), min);
			}
		}
	}
}
