using Voxelscape.Utility.Common.Core.Disposables;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Data.Pact.Pools
{
	internal class LoanedValue<T> : DisposableWrapper<T>
	{
		private readonly IPool<T> pool;

		public LoanedValue(IPool<T> pool, T value)
			: base(value)
		{
			Contracts.Requires.That(pool != null);

			this.pool = pool;
		}

		/// <inheritdoc />
		protected override void ManagedDisposal(T value) => this.pool.Give(value);
	}
}
