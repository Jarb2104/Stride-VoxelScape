using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Core.Types;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Concurrency.Core.Synchronization;
using Voxelscape.Utility.Data.Pact.Pools;

namespace Voxelscape.Utility.Data.Core.Pools
{
	/// <summary>
	///
	/// </summary>
	/// <typeparam name="T">The type of the pooled values.</typeparam>
	/// <threadsafety static="true" instance="true" />
	public class TrackingPool<T> : AbstractPoolWrapper<T>
	{
		private readonly Subject<VoidStruct> disposed = new Subject<VoidStruct>();

		private readonly Subject<int> availableCountChanged = new Subject<int>();

		private readonly Subject<long> takenCountChanged = new Subject<long>();

		private readonly Subject<long> givenCountChanged = new Subject<long>();

		private readonly Subject<long> onLoanCountChanged = new Subject<long>();

		private readonly AtomicLong takenCount = new AtomicLong(0);

		private readonly AtomicLong givenCount = new AtomicLong(0);

		private readonly AtomicLong onLoanCount = new AtomicLong(0);

		private readonly AtomicLong releasedCount;

		/// <summary>
		/// Initializes a new instance of the <see cref="TrackingPool{T}"/> class.
		/// </summary>
		/// <param name="pool">The pool.</param>
		/// <param name="options">The options.</param>
		/// <remarks>
		/// Use this constructor to enable the <see cref="ReleasedCount"/> property. The
		/// <see cref="TrackingPoolOptions{T}"/> passed to this constructor must also be passed
		/// to the constructor of the pool passed in as <paramref name="pool"/>.
		/// </remarks>
		public TrackingPool(IPool<T> pool, TrackingPoolOptions<T> options)
			: base(pool)
		{
			Contracts.Requires.That(options != null);

			this.releasedCount = options.ReleasedCount;
			this.ReleasedCountChanged = options.ReleasedCountChanged.TakeUntil(this.disposed);

			this.AvailableCountChanged =
				this.TakenCountChanged.Merge(this.GivenCountChanged).Merge(this.ReleasedCountChanged)
				.Select(value => this.Pool.AvailableCount).DistinctUntilChanged().TakeUntil(this.disposed);

			this.TakenCountChanged = this.takenCountChanged.AsObservable();
			this.GivenCountChanged = this.givenCountChanged.AsObservable();
			this.OnLoanCountChanged = this.onLoanCountChanged.AsObservable();
		}

		public long TakenCount => this.takenCount.Read();

		public long GivenCount => this.givenCount.Read();

		public long ReleasedCount => this.releasedCount.Read();

		public long OnLoanCount => this.onLoanCount.Read().ClampLower(0);

		public IObservable<int> AvailableCountChanged { get; }

		public IObservable<long> TakenCountChanged { get; }

		public IObservable<long> GivenCountChanged { get; }

		public IObservable<long> OnLoanCountChanged { get; }

		public IObservable<long> ReleasedCountChanged { get; }

		/// <inheritdoc />
		public override T Take()
		{
			IPoolContracts.Take(this);

			var result = base.Take();
			this.IncrementTakenCount();
			return result;
		}

		/// <inheritdoc />
		public override async Task<T> TakeAsync()
		{
			IPoolContracts.TakeAsync(this);

			var result = await base.TakeAsync().DontMarshallContext();
			this.IncrementTakenCount();
			return result;
		}

		/// <inheritdoc />
		public override bool TryTake(out T value)
		{
			IPoolContracts.TryTake(this);

			if (base.TryTake(out value))
			{
				this.IncrementTakenCount();
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <inheritdoc />
		public override void Give(T value)
		{
			IPoolContracts.Give(this);

			base.Give(value);
			this.givenCountChanged.OnNext(this.givenCount.Increment());

			long onLoan;
			if (this.onLoanCount.TryDecrementClampLower(0, out onLoan))
			{
				this.onLoanCountChanged.OnNext(onLoan);
			}
		}

		/// <inheritdoc />
		protected override void ManagedDisposal()
		{
			base.ManagedDisposal();

			this.disposed.OnNext(default(VoidStruct));
			this.disposed.OnCompletedAndDispose();

			this.takenCountChanged.OnCompletedAndDispose();
			this.givenCountChanged.OnCompletedAndDispose();
			this.onLoanCountChanged.OnCompletedAndDispose();
		}

		private void IncrementTakenCount()
		{
			this.takenCountChanged.OnNext(this.takenCount.Increment());
			this.onLoanCountChanged.OnNext(this.onLoanCount.Increment());
		}
	}
}
