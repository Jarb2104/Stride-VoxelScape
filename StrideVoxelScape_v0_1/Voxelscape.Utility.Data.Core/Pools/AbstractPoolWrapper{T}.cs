using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Data.Pact.Pools;

namespace Voxelscape.Utility.Data.Core.Pools
{
	/// <summary>
	///
	/// </summary>
	/// <typeparam name="T">The type of the pooled values.</typeparam>
	/// <threadsafety static="true" instance="true" />
	public abstract class AbstractPoolWrapper<T> : AbstractDisposable, IPool<T>
	{
		public AbstractPoolWrapper(IPool<T> pool)
		{
			Contracts.Requires.That(pool != null);

			this.Pool = pool;
		}

		/// <inheritdoc />
		public virtual int AvailableCount => this.Pool.AvailableCount;

		/// <inheritdoc />
		public virtual int BoundedCapacity => this.Pool.BoundedCapacity;

		protected IPool<T> Pool { get; }

		/// <inheritdoc />
		public virtual bool TryTake(out T value)
		{
			IPoolContracts.TryTake(this);

			return this.Pool.TryTake(out value);
		}

		/// <inheritdoc />
		public virtual T Take()
		{
			IPoolContracts.Take(this);

			return this.Pool.Take();
		}

		/// <inheritdoc />
		public virtual Task<T> TakeAsync()
		{
			IPoolContracts.TakeAsync(this);

			return this.Pool.TakeAsync();
		}

		/// <inheritdoc />
		public virtual void Give(T value)
		{
			IPoolContracts.Give(this);

			this.Pool.Give(value);
		}

		/// <inheritdoc />
		protected override void ManagedDisposal() => this.Pool.Dispose();
	}
}
