using System;
using System.Collections;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Voxelscape.Common.Indexing.Pact.Indices;
using Voxelscape.Stages.Management.Pact.Interests;
using Voxelscape.Utility.Common.Core.Disposables;
using Voxelscape.Utility.Common.Core.Types;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;

namespace Voxelscape.Stages.Management.Core.Interests
{
	/// <summary>
	/// An area of interest that follows an <see cref="IObservable{TKey}"/>, adding and removing interests
	/// from an <see cref="IInterestMap{TKey, TInterest}"/> as the key changes.
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TInterest">The type of the interest.</typeparam>
	/// <remarks>
	/// Dispose of this instance to remove all the interests it has pinned and to unsubscribe from the
	/// observable key sequence. If the observable key sequence completes this instance will dispose itself.
	/// </remarks>
	public abstract class AbstractAreaOfInterest<TKey, TInterest> : AbstractDisposable
		where TKey : struct, IIndex
	{
		/// <summary>
		/// The observable sequence used to end the origin observable sequence when this instance is disposed.
		/// </summary>
		private readonly Subject<VoidStruct> disposed = new Subject<VoidStruct>();

		/// <summary>
		/// The interest map to add and remove interests from.
		/// </summary>
		private readonly IInterestMap<TKey, TInterest> map;

		/// <summary>
		/// The interest to add and remove from the interest map.
		/// </summary>
		private readonly TInterest interest;

		/// <summary>
		/// The keys of the cells to apply interests to.
		/// </summary>
		private readonly Utility.Common.Pact.Collections.IReadOnlySet<TKey> keys;

		/// <summary>
		/// The subscriptions to the observable origin.
		/// </summary>
		private readonly IDisposable subscriptions;

		/// <summary>
		/// Initializes a new instance of the <see cref="AbstractAreaOfInterest{TKey, TInterest}" /> class.
		/// </summary>
		/// <param name="map">The interest map to add and remove interests from.</param>
		/// <param name="interest">The interest to add and remove.</param>
		/// <param name="observableOrigin">The origin of the area of interest to follow.</param>
		/// <param name="keys">The keys of the cells to apply interests to.</param>
		public AbstractAreaOfInterest(
			IInterestMap<TKey, TInterest> map,
			TInterest interest,
			IObservable<TKey> observableOrigin,
			Utility.Common.Pact.Collections.IReadOnlySet<TKey> keys)
		{
			Contracts.Requires.That(map != null);
			Contracts.Requires.That(observableOrigin != null);
			Contracts.Requires.That(keys != null);

			this.map = map;
			this.interest = interest;
			this.keys = keys;

			IObservable<TKey> firstKey, lastKey;
			var pairKeys = observableOrigin.TakeUntil(this.disposed).PairWithPrevious(out firstKey, out lastKey);

			this.subscriptions = new AggregateDisposable(
				pairKeys.Subscribe(
					origin => this.map.UpdateInterests(
						this.interest,
						addTo: this.GetKeys(origin.Next),
						removeFrom: this.GetKeys(origin.Previous))),
				firstKey.Subscribe(
					origin => this.map.AddInterests(this.interest, this.GetKeys(origin))),
				lastKey.Subscribe(
					origin => this.map.RemoveInterests(this.interest, this.GetKeys(origin)),
					() => this.Dispose()));
		}

		/// <inheritdoc />
		protected override void ManagedDisposal()
		{
			this.disposed.OnNext(default(VoidStruct));
			this.disposed.OnCompletedAndDispose();
			this.subscriptions.Dispose();
		}

		/// <summary>
		/// Adds the two keys.
		/// </summary>
		/// <param name="lhs">The left hand side key.</param>
		/// <param name="rhs">The right hand side key.</param>
		/// <returns>The result of adding the keys.</returns>
		/// <remarks>This method is required because the key type for this base class is abstract.</remarks>
		protected abstract TKey Add(TKey lhs, TKey rhs);

		/// <summary>
		/// Subtracts the <paramref name="rhs"/> from <paramref name="lhs"/>.
		/// </summary>
		/// <param name="lhs">The left hand side key.</param>
		/// <param name="rhs">The right hand side key.</param>
		/// <returns>The result of subtracting the keys.</returns>
		/// <remarks>This method is required because the key type for this base class is abstract.</remarks>
		protected abstract TKey Subtract(TKey lhs, TKey rhs);

		/// <summary>
		/// Gets the enumerable of interest keys to update.
		/// </summary>
		/// <param name="origin">The origin key of the area of interest.</param>
		/// <returns>The enumerable of keys to update.</returns>
		private Utility.Common.Pact.Collections.IReadOnlySet<TKey> GetKeys(TKey origin) => new OffsetSet(this, origin);

		private class OffsetSet : Utility.Common.Pact.Collections.IReadOnlySet<TKey>
		{
			private readonly AbstractAreaOfInterest<TKey, TInterest> parent;

			private readonly TKey origin;

			public OffsetSet(AbstractAreaOfInterest<TKey, TInterest> parent, TKey origin)
			{
				Contracts.Requires.That(parent != null);

				this.parent = parent;
				this.origin = origin;
			}

			/// <inheritdoc />
			public int Count => this.parent.keys.Count;

			/// <inheritdoc />
			public bool Contains(TKey key) => this.parent.keys.Contains(this.parent.Subtract(key, this.origin));

			/// <inheritdoc />
			public IEnumerator<TKey> GetEnumerator()
			{
				foreach (var key in this.parent.keys)
				{
					yield return this.parent.Add(key, this.origin);
				}
			}

			/// <inheritdoc />
			IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
		}
	}
}
