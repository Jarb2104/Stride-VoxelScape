using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;
using Voxelscape.Utility.Data.Pact.Pools;

namespace Voxelscape.Utility.Data.Core.Pools
{
	public class FactoryPool<T> : AbstractPoolWrapper<T>
	{
		private readonly IFactory<T> factory;

		public FactoryPool(IFactory<T> factory, IPool<T> pool)
			: base(pool)
		{
			Contracts.Requires.That(factory != null);

			this.factory = factory;
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

			return this.factory.Create();
		}

		/// <inheritdoc />
		public override Task<T> TakeAsync()
		{
			IPoolContracts.TakeAsync(this);

			T value;
			if (this.TryTake(out value))
			{
				return Task.FromResult(value);
			}

			return Task.FromResult(this.factory.Create());
		}
	}
}
