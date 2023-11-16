using System;
using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Disposables
{
	/// <summary>
	/// A disposable wrapper for a value where the value can only be accessed until the wrapper is disposed of
	/// and when disposed a collection of associated disposables will also be disposed of.
	/// </summary>
	/// <typeparam name="T">The type of the value.</typeparam>
	public class CompositeDisposableWrapper<T> : DisposableWrapper<T>
	{
		/// <summary>
		/// The disposable to dispose of when this instance is disposed.
		/// </summary>
		private readonly IDisposable disposable;

		/// <summary>
		/// Initializes a new instance of the <see cref="CompositeDisposableWrapper{T}"/> class.
		/// </summary>
		/// <param name="value">The value to wrap.</param>
		/// <param name="disposable">The disposable to dispose of when this instance is disposed.</param>
		public CompositeDisposableWrapper(T value, IDisposable disposable)
			: base(value)
		{
			Contracts.Requires.That(disposable != null);

			this.disposable = disposable;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CompositeDisposableWrapper{T}"/> class.
		/// </summary>
		/// <param name="value">The value to wrap.</param>
		/// <param name="disposables">The disposables to dispose of when this instance is disposed.</param>
		public CompositeDisposableWrapper(T value, params IDisposable[] disposables)
			: this(value, (IEnumerable<IDisposable>)disposables)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="CompositeDisposableWrapper{T}"/> class.
		/// </summary>
		/// <param name="value">The value to wrap.</param>
		/// <param name="disposables">The disposables to dispose of when this instance is disposed.</param>
		public CompositeDisposableWrapper(T value, IEnumerable<IDisposable> disposables)
			: this(value, new AggregateDisposable(disposables))
		{
			Contracts.Requires.That(disposables.AllAndSelfNotNull());
		}

		/// <inheritdoc />
		protected override void ManagedDisposal(T value) => this.disposable.Dispose();
	}
}
