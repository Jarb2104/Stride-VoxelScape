using System;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Concurrency.Core.Synchronization;

namespace Voxelscape.Utility.Concurrency.Core.Reactive
{
	public class SharedDisposableValueSubject<T> : AbstractDisposable, ISubject<IDisposableValue<T>>
	{
		private readonly Subject<SharedDisposable> subject;

		private readonly SubscriptionCounter<SharedDisposable> counter;

		private readonly IObservable<IDisposableValue<T>> observable;

		public SharedDisposableValueSubject()
		{
			this.subject = new Subject<SharedDisposable>();
			this.counter = new SubscriptionCounter<SharedDisposable>(this.subject);
			this.observable = this.counter.CountedSource.Select(value => value.GetValue());
		}

		/// <inheritdoc />
		public void OnCompleted() => this.subject.OnCompleted();

		/// <inheritdoc />
		public void OnError(Exception error) => this.subject.OnError(error);

		/// <inheritdoc />
		public void OnNext(IDisposableValue<T> value) =>
			this.subject.OnNext(new SharedDisposable(value, this.counter.SubscriberCount));

		/// <inheritdoc />
		public IDisposable Subscribe(IObserver<IDisposableValue<T>> observer) => this.observable.Subscribe(observer);

		/// <inheritdoc />
		protected override void ManagedDisposal() => this.subject.Dispose();

		private class SharedDisposable
		{
			private readonly IDisposableValue<T> value;

			private readonly AtomicInt count;

			public SharedDisposable(IDisposableValue<T> value, int count)
			{
				Contracts.Requires.That(count >= 0);

				this.value = value;
				this.count = new AtomicInt(count);

				if (count == 0)
				{
					this.value?.Dispose();
				}
			}

			public IDisposableValue<T> GetValue() => new ValuePin(this);

			private class ValuePin : AbstractDisposable, IDisposableValue<T>
			{
				private readonly SharedDisposable parent;

				public ValuePin(SharedDisposable parent)
				{
					Contracts.Requires.That(parent != null);

					this.parent = parent;
				}

				/// <inheritdoc />
				public T Value => this.parent.value != null ? this.parent.value.Value : default(T);

				/// <inheritdoc />
				protected override void ManagedDisposal()
				{
					if (this.parent.count.Decrement() == 0)
					{
						this.parent.value?.Dispose();
					}
				}
			}
		}
	}
}
