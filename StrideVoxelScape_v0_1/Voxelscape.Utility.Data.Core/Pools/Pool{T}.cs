using System;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
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
	public class Pool<T> : AbstractDisposable, IPool<T>
	{
		private readonly Action<T> resetAction;

		private readonly Action<T> releaseAction;

		private readonly BufferBlock<T> buffer;

		public Pool(IPoolOptions<T> options = null)
		{
			Contracts.Requires.That(options?.IsValid() ?? true);

			options = options ?? PoolOptions.Default<T>();

			this.resetAction = options.ResetAction;
			this.releaseAction = options.ReleaseAction;
			this.BoundedCapacity = options.BoundedCapacity;

			this.buffer = new BufferBlock<T>(new DataflowBlockOptions()
			{
				BoundedCapacity = this.BoundedCapacity,
			});
		}

		/// <inheritdoc />
		public int AvailableCount => this.buffer.Count;

		/// <inheritdoc />
		public int BoundedCapacity { get; }

		/// <inheritdoc />
		public bool TryTake(out T value)
		{
			IPoolContracts.TryTake(this);

			return this.buffer.TryReceive(out value);
		}

		/// <inheritdoc />
		public T Take()
		{
			IPoolContracts.Take(this);

			return this.buffer.Receive();
		}

		/// <inheritdoc />
		public Task<T> TakeAsync()
		{
			IPoolContracts.TakeAsync(this);

			return this.buffer.ReceiveAsync();
		}

		/// <inheritdoc />
		public void Give(T value)
		{
			IPoolContracts.Give(this);

			this.resetAction(value);
			if (!this.buffer.Post(value))
			{
				this.releaseAction(value);
			}
		}

		/// <inheritdoc />
		protected override void ManagedDisposal()
		{
			this.buffer.Complete();

			// TODO Steven - If TPL Dataflow ever updates maybe switch this to using TryReceiveAll
			// as it stands right now TryReceiveAllBugFixed is actually less efficient
			// so I'm using TryReceive instead
			T value;
			while (this.buffer.TryReceive(out value))
			{
				this.releaseAction(value);
			}
		}
	}
}
