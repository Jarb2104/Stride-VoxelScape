using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;
using Voxelscape.Utility.Concurrency.Core.Synchronization;
using Voxelscape.Utility.Data.Pact.Pools;

namespace Voxelscape.Utility.Data.Core.Pools
{
	public class BoundedFactoryPool<T> : AbstractPoolWrapper<T>
	{
		private readonly IFactory<T> factory;

		private readonly AtomicInt countLeft;

		public BoundedFactoryPool(IFactory<T> factory, IPool<T> pool)
			: base(pool)
		{
			Contracts.Requires.That(factory != null);

			this.factory = factory;
			this.countLeft = BoundedFactoryUtilities.GetRemainingCount(pool);
		}

		/// <inheritdoc />
		public override T Take()
		{
			IPoolContracts.Take(this);

			T value;
			if (this.TryTake(out value))
			{
				return value;
			}

			if (this.countLeft?.TryDecrementClampLower(0) ?? true)
			{
				return this.factory.Create();
			}

			return this.Pool.Take();
		}

		/// <inheritdoc />
		public override async Task<T> TakeAsync()
		{
			IPoolContracts.TakeAsync(this);

			T value;
			if (this.TryTake(out value))
			{
				return value;
			}

			if (this.countLeft?.TryDecrementClampLower(0) ?? true)
			{
				return this.factory.Create();
			}

			return await this.Pool.TakeAsync().DontMarshallContext();
		}
	}
}
