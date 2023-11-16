using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;
using Voxelscape.Utility.Data.Pact.Pools;

namespace Voxelscape.Utility.Data.Core.Pools
{
	public class AsyncFactoryPool<T> : AbstractPoolWrapper<T>
	{
		private readonly IAsyncFactory<T> factory;

		public AsyncFactoryPool(IAsyncFactory<T> factory, IPool<T> pool)
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

			return this.factory.CreateAsync().Result;
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

			return await this.factory.CreateAsync().DontMarshallContext();
		}
	}
}
