using System.Collections.Generic;
using System.Linq;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Data.Pact.Serialization
{
	/// <summary>
	///
	/// </summary>
	public static class EndianUtilities
	{
		public static bool AllSameEndianness(params IEndianSpecific[] values) =>
			AllSameEndianness((IEnumerable<IEndianSpecific>)values);

		public static bool AllSameEndianness(IEnumerable<IEndianSpecific> values)
		{
			Contracts.Requires.That(values.AllAndSelfNotNull());

			return values.Select(value => value.Endianness).AllEqual();
		}
	}
}
