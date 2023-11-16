using System;
using System.Reactive.Linq;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Concurrency.Core.Reactive;
using Voxelscape.Utility.Concurrency.Core.Synchronization;

/// <summary>
/// Provides extension methods for <see cref="IObservable{T}"/>.
/// </summary>
public static class IObservableExtensions
{
	public static IObservable<T> IgnoreError<T>(this IObservable<T> source)
	{
		Contracts.Requires.That(source != null);

		return Observable.Create<T>(observer => source.Subscribe(observer.OnNext, observer.OnCompleted));
	}

	public static IObservable<T> IgnoreCompletion<T>(this IObservable<T> source)
	{
		Contracts.Requires.That(source != null);

		return Observable.Create<T>(observer => source.Subscribe(observer.OnNext, observer.OnError));
	}

	public static IObservable<T> IgnoreTermination<T>(this IObservable<T> source)
	{
		Contracts.Requires.That(source != null);

		return Observable.Create<T>(observer => source.Subscribe(observer.OnNext));
	}

	public static IObservable<T> OnlyError<T>(this IObservable<T> source)
	{
		Contracts.Requires.That(source != null);

		return Observable.Create<T>(observer => source.Subscribe(value => { }, observer.OnError));
	}

	public static IObservable<T> OnlyCompletion<T>(this IObservable<T> source)
	{
		Contracts.Requires.That(source != null);

		return Observable.Create<T>(observer => source.Subscribe(value => { }, observer.OnCompleted));
	}

	// equivalent to IgnoreElements
	public static IObservable<T> OnlyTermination<T>(this IObservable<T> source)
	{
		Contracts.Requires.That(source != null);

		return source.IgnoreElements();
	}

	// equivalent to IgnoreTermination
	public static IObservable<T> OnlyElements<T>(this IObservable<T> source)
	{
		Contracts.Requires.That(source != null);

		return source.IgnoreTermination();
	}

	public static IObservable<IDisposableValue<T>> ShareDisposable<T>(this IObservable<IDisposableValue<T>> source)
	{
		Contracts.Requires.That(source != null);

		var published = source.Select(value => new SharedDisposable<T>(value)).Publish();
		var counter = new SubscriptionCounter<SharedDisposable<T>>(published);
		published.Connect();
		return counter.CountedSource.Select(value => value.GetValue(counter.SubscriberCount));
	}

	private class SharedDisposable<T>
	{
		private const int Uninitialized = -1;

		private readonly IDisposableValue<T> value;

		private readonly AtomicInt count;

		public SharedDisposable(IDisposableValue<T> value)
		{
			this.value = value;
			this.count = new AtomicInt(Uninitialized);
		}

		public IDisposableValue<T> GetValue(int subscriberCount)
		{
			Contracts.Requires.That(subscriberCount >= 0);

			this.count.CompareExchange(subscriberCount, Uninitialized);
			return new ValuePin(this);
		}

		private class ValuePin : AbstractDisposable, IDisposableValue<T>
		{
			private readonly SharedDisposable<T> parent;

			public ValuePin(SharedDisposable<T> parent)
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
