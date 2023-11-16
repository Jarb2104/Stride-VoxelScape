using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Concurrency.Core.Synchronization;

namespace Voxelscape.Utility.Data.Core.Pools
{
	/// <summary>
	/// Provides optional parameters for instances of <see cref="TrackingPool{T}"/>.
	/// </summary>
	/// <typeparam name="T">The type of pooled values.</typeparam>
	public class TrackingPoolOptions<T> : PoolOptions<T>
	{
		private readonly Subject<long> releasedCountChanged = new Subject<long>();

		public TrackingPoolOptions()
		{
			this.ReleaseAction = value => this.IncrementReleasedCount();
			this.ReleasedCountChanged = this.releasedCountChanged.AsObservable();
		}

		/// <inheritdoc />
		public override Action<T> ReleaseAction
		{
			get
			{
				return base.ReleaseAction;
			}

			set
			{
				Contracts.Requires.That(value != null);

				if (value == this.ReleaseAction)
				{
					return;
				}

				base.ReleaseAction = releaseValue =>
				{
					value(releaseValue);
					this.IncrementReleasedCount();
				};
			}
		}

		internal AtomicLong ReleasedCount { get; } = new AtomicLong(0);

		// this observable will never complete
		internal IObservable<long> ReleasedCountChanged { get; }

		private void IncrementReleasedCount() => this.releasedCountChanged.OnNext(this.ReleasedCount.Increment());
	}
}
