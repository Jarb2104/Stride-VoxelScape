using System;
using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Collections;

namespace Voxelscape.Utility.Common.Core.Collections
{
	/// <summary>
	///
	/// </summary>
	public class TypeSet : TypeSet<object>, ITypeSet
	{
		#region Constructors

		public TypeSet()
			: base()
		{
		}

		public TypeSet(int capacity)
			: base(capacity)
		{
		}

		public TypeSet(IEqualityComparer<Type> comparer)
			: base(comparer)
		{
		}

		public TypeSet(int capacity, IEqualityComparer<Type> comparer)
			: base(capacity, comparer)
		{
		}

		#endregion
	}
}
