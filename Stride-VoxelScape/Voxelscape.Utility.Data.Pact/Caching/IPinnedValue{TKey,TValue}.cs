using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Common.Pact.Types;

namespace Voxelscape.Utility.Data.Pact.Caching
{
	public interface IPinnedValue<TKey, out TValue> : IKeyed<TKey>, IDisposableValue<TValue>
	{
	}
}
