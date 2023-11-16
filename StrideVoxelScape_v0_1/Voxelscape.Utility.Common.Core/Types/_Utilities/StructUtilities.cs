using System;

namespace Voxelscape.Utility.Common.Core.Types
{
	/// <summary>
	///
	/// </summary>
	public static class StructUtilities
	{
		public static bool Equals<TStruct>(TStruct value, object obj)
			where TStruct : struct, IEquatable<TStruct> =>
			(obj is TStruct) ? value.Equals((TStruct)obj) : false;
	}
}
