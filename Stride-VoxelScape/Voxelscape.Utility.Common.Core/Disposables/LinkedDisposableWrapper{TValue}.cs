using Voxelscape.Utility.Common.Pact.Disposables;

namespace Voxelscape.Utility.Common.Core.Disposables
{
	public class LinkedDisposableWrapper<TValue> : LinkedDisposableWrapper<IDisposed, TValue>
	{
		public LinkedDisposableWrapper(IDisposed linkTo, TValue value)
			: base(linkTo, value)
		{
		}
	}
}
