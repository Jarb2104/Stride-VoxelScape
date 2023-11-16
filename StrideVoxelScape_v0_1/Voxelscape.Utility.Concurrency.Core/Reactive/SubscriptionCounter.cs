using System;
using System.Reactive.Linq;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Concurrency.Core.Synchronization;

namespace Voxelscape.Utility.Concurrency.Core.Reactive
{
	public class SubscriptionCounter<T>
	{
		private readonly AtomicInt count = new AtomicInt(0);

		public SubscriptionCounter(IObservable<T> source)
		{
			Contracts.Requires.That(source != null);

			this.CountedSource = Observable.Create<T>(observer =>
			{
				this.count.Increment();
				return new Subscription(source.Subscribe(observer), this.count);
			});
		}

		public int SubscriberCount => this.count.Read();

		public IObservable<T> CountedSource { get; }

		private class Subscription : AbstractDisposable
		{
			private readonly IDisposable subscription;

			private readonly AtomicInt count;

			public Subscription(IDisposable subscription, AtomicInt count)
			{
				Contracts.Requires.That(subscription != null);
				Contracts.Requires.That(count != null);

				this.subscription = subscription;
				this.count = count;
			}

			/// <inheritdoc />
			protected override void ManagedDisposal()
			{
				this.subscription.Dispose();
				this.count.Decrement();
			}
		}
	}
}
