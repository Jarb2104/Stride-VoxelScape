using System;
using System.Collections.Generic;

namespace Voxelscape.Utility.Concurrency.Core.Collections
{
	/// <summary>
	///
	/// </summary>
	public class ConcurrentTypeSet : ConcurrentTypeSet<object>
	{
		public ConcurrentTypeSet()
			: base()
		{
		}

		public ConcurrentTypeSet(IEqualityComparer<Type> comparer)
			: base(comparer)
		{
		}
	}
}
