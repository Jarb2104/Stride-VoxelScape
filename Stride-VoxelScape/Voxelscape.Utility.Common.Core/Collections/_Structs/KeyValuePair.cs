using System.Collections.Generic;

namespace Voxelscape.Utility.Common.Core.Collections
{
	/// <summary>
	///
	/// </summary>
	public static class KeyValuePair
	{
		public static KeyValuePair<TKey, TValue> New<TKey, TValue>(TKey key, TValue value) =>
			new KeyValuePair<TKey, TValue>(key, value);
	}
}
