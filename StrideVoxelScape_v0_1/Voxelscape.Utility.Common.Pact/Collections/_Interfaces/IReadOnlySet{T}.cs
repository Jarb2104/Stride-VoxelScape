using System.Collections.Generic;

namespace Voxelscape.Utility.Common.Pact.Collections
{
	public interface IReadOnlySet<T> : IReadOnlyCollection<T>
	{
		bool Contains(T value);
	}
}
