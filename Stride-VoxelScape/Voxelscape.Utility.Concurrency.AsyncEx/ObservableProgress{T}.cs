using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Concurrency.AsyncEx
{
	/// <summary>
	/// A progress reporter that exposes progress updates as an observable stream. This is a hot observable.
	/// </summary>
	/// <typeparam name="T">The type of progress updates.</typeparam>
	/// <seealso href="https://gist.github.com/StephenCleary/4248e50b4cb52b933c0d"/>
	public sealed class ObservableProgress<T> : IObservable<T>, IDisposableProgress<T>
	{
		private readonly ISubject<T> subject;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="ObservableProgress{T}"/> class that uses a replay subject
		/// with a single-element buffer, ensuring all new subscriptions immediately receive the last progress update.
		/// </summary>
		public ObservableProgress()
			: this(new ReplaySubject<T>(1))
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ObservableProgress{T}"/> class that uses a replay subject
		/// with a single-element buffer, ensuring all new subscriptions immediately receive the last progress update.
		/// </summary>
		/// <param name="scheduler">The scheduler to inject into the replay subject.</param>
		public ObservableProgress(IScheduler scheduler)
			: this(new ReplaySubject<T>(1, scheduler))
		{
			Contracts.Requires.That(scheduler != null);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ObservableProgress{T}"/> class.
		/// </summary>
		/// <param name="subject">The subject used for progress updates.</param>
		public ObservableProgress(ISubject<T> subject)
		{
			Contracts.Requires.That(subject != null);

			this.subject = subject;
		}

		#endregion

		#region Factory Methods

		/// <summary>
		/// Creates a progress handler with common UI options: updates are sampled on <paramref name="sampleTimeSpan" /> intervals,
		/// and the <paramref name="handler" /> is executed on the UI thread. This method must be called from the UI thread.
		/// The UI should already be initialized with the default state; <paramref name="handler" /> is not invoked with an initial value.
		/// </summary>
		/// <param name="sampleTimeSpan">The time span interval to sample progress updates.</param>
		/// <param name="handler">The progress update handler that updates the UI.</param>
		/// <returns>The disposable observable progress handler.</returns>
		public static IDisposableProgress<T> CreateForUi(TimeSpan sampleTimeSpan, Action<T> handler)
		{
			var uiScheduler = SynchronizationContext.Current ?? new SynchronizationContext();
			var progress = new ObservableProgress<T>(new Subject<T>());
			var subscription = progress.Sample(sampleTimeSpan).ObserveOn(uiScheduler).Subscribe(handler);
			return new ObservableProgressWithSubscription(progress, subscription);
		}

		/// <summary>
		/// Creates a progress handler with common UI options: updates are sampled on <paramref name="sampleTimeSpan"/> intervals,
		/// and the <paramref name="handler"/> is executed on the UI thread. This method must be called from the UI thread.
		/// The UI should already be initialized with the default state; <paramref name="handler"/> is not invoked with an initial value.
		/// </summary>
		/// <param name="sampleTimeSpan">The time span interval to sample progress updates.</param>
		/// <param name="handler">The progress update handler that updates the UI.</param>
		/// <param name="scheduler">The scheduler to inject into the <c>Sample</c> operator.</param>
		/// <returns>The disposable observable progress handler.</returns>
		public static IDisposableProgress<T> CreateForUi(
			TimeSpan sampleTimeSpan, Action<T> handler, IScheduler scheduler)
		{
			var uiScheduler = SynchronizationContext.Current ?? new SynchronizationContext();
			var progress = new ObservableProgress<T>(new Subject<T>());
			var subscription = progress.Sample(sampleTimeSpan, scheduler).ObserveOn(uiScheduler).Subscribe(handler);
			return new ObservableProgressWithSubscription(progress, subscription);
		}

		/// <summary>
		/// Creates a progress handler with common UI options: updates are sampled on 100ms intervals, and the <paramref name="handler"/>
		/// is executed on the UI thread. This method must be called from the UI thread. The UI should already be initialized with the
		/// default state; <paramref name="handler"/> is not invoked with an initial value.
		/// </summary>
		/// <param name="handler">The progress update handler that updates the UI.</param>
		/// <returns>The disposable observable progress handler.</returns>
		public static IDisposableProgress<T> CreateForUi(Action<T> handler)
		{
			return CreateForUi(TimeSpan.FromMilliseconds(100), handler);
		}

		/// <summary>
		/// Creates a progress handler with common UI options: updates are sampled on 100ms intervals, and the <paramref name="handler"/>
		/// is executed on the UI thread. This method must be called from the UI thread. The UI should already be initialized with the
		/// default state; <paramref name="handler"/> is not invoked with an initial value.
		/// </summary>
		/// <param name="handler">The progress update handler that updates the UI.</param>
		/// <param name="scheduler">The scheduler to inject into the <c>Sample</c> operator.</param>
		/// <returns>The disposable observable progress handler.</returns>
		public static IDisposableProgress<T> CreateForUi(Action<T> handler, IScheduler scheduler)
		{
			return CreateForUi(TimeSpan.FromMilliseconds(100), handler, scheduler);
		}

		#endregion

		/// <inheritdoc />
		public void Report(T value)
		{
			this.subject.OnNext(value);
		}

		/// <inheritdoc />
		public void Dispose()
		{
			this.subject.OnCompleted();
			var disposableSubject = this.subject as IDisposable;
			disposableSubject?.Dispose();
		}

		/// <inheritdoc />
		public IDisposable Subscribe(IObserver<T> observer)
		{
			Contracts.Requires.That(observer != null);

			return this.subject.Subscribe(observer);
		}

		private sealed class ObservableProgressWithSubscription : IDisposableProgress<T>
		{
			private readonly IDisposableProgress<T> progress;

			private readonly IDisposable subscription;

			public ObservableProgressWithSubscription(ObservableProgress<T> progress, IDisposable subscription)
			{
				this.progress = progress;
				this.subscription = subscription;
			}

			/// <inheritdoc />
			public void Report(T value)
			{
				this.progress.Report(value);
			}

			/// <inheritdoc />
			public void Dispose()
			{
				this.subscription.Dispose();
				this.progress.Dispose();
			}
		}
	}
}
