using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Voxelscape.Utility.Common.Core.Diagnostics;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;

namespace Voxelscape.Utility.Common.Core.Disposables
{
	/// <summary>
	/// Represents a mutable set of <see cref="IDisposable"/> objects that will be disposed of as a single group
	/// when this instance is disposed of.
	/// </summary>
	/// <remarks>
	/// Disposed instances of this class can no longer be mutated, but won't throw exceptions. The <see cref="Add"/> method
	/// will always return false and the mutating <see cref="ISet{IDisposable}"/> members will not change the collection.
	/// Upon disposing the collection also clears itself of all disposable instances it contains to avoid keeping disposed
	/// instances from being garbage collected.
	/// </remarks>
	[DebuggerTypeProxy(typeof(EnumerableDebugView<>))]
	public class AggregateDisposableSet : AbstractDisposable, ISet<IDisposable>
	{
		/// <summary>
		/// The set of disposables.
		/// </summary>
		private readonly ISet<IDisposable> disposables;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="AggregateDisposableSet"/> class.
		/// </summary>
		public AggregateDisposableSet()
		{
			this.disposables = new HashSet<IDisposable>();
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AggregateDisposableSet"/> class.
		/// </summary>
		/// <param name="disposables">The initial set of disposables. Duplicates will be ignored.</param>
		public AggregateDisposableSet(IEnumerable<IDisposable> disposables)
		{
			Contracts.Requires.That(disposables != null);

			this.disposables = new HashSet<IDisposable>(disposables);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AggregateDisposableSet"/> class.
		/// </summary>
		/// <param name="disposables">The initial set of disposables. Duplicates will be ignored.</param>
		public AggregateDisposableSet(params IDisposable[] disposables)
			: this((IEnumerable<IDisposable>)disposables)
		{
			Contracts.Requires.That(disposables != null);
		}

		#endregion

		#region ICollection<IDisposable> Members

		/// <inheritdoc />
		public int Count
		{
			get { return this.disposables.Count; }
		}

		/// <inheritdoc />
		public bool IsReadOnly
		{
			get { return false; }
		}

		/// <inheritdoc />
		void ICollection<IDisposable>.Add(IDisposable item)
		{
			ICollectionContracts.Add(this);

			this.Add(item);
		}

		/// <inheritdoc />
		public void Clear()
		{
			ICollectionContracts.Clear(this);

			this.disposables.Clear();
		}

		/// <inheritdoc />
		public bool Contains(IDisposable item)
		{
			return this.disposables.Contains(item);
		}

		/// <inheritdoc />
		public void CopyTo(IDisposable[] array, int arrayIndex)
		{
			ICollectionContracts.CopyTo(this, array, arrayIndex);

			this.disposables.CopyTo(array, arrayIndex);
		}

		/// <inheritdoc />
		public bool Remove(IDisposable item)
		{
			ICollectionContracts.Remove(this);

			return this.disposables.Remove(item);
		}

		#region ISet<IDisposable> Members

		/// <inheritdoc />
		public bool Add(IDisposable item)
		{
			if (this.IsDisposed)
			{
				return false;
			}

			return this.disposables.Add(item);
		}

		/// <inheritdoc />
		public void ExceptWith(IEnumerable<IDisposable> other)
		{
			ISetContracts.ExceptWith(this, other);

			if (this.IsDisposed)
			{
				return;
			}

			this.disposables.ExceptWith(other);
		}

		/// <inheritdoc />
		public void IntersectWith(IEnumerable<IDisposable> other)
		{
			ISetContracts.IntersectWith(this, other);

			if (this.IsDisposed)
			{
				return;
			}

			this.disposables.IntersectWith(other);
		}

		/// <inheritdoc />
		public void SymmetricExceptWith(IEnumerable<IDisposable> other)
		{
			ISetContracts.SymmetricExceptWith(this, other);

			if (this.IsDisposed)
			{
				return;
			}

			this.disposables.SymmetricExceptWith(other);
		}

		/// <inheritdoc />
		public void UnionWith(IEnumerable<IDisposable> other)
		{
			ISetContracts.UnionWith(this, other);

			if (this.IsDisposed)
			{
				return;
			}

			this.disposables.UnionWith(other);
		}

		/// <inheritdoc />
		public bool IsProperSubsetOf(IEnumerable<IDisposable> other)
		{
			ISetContracts.IsProperSubsetOf(other);

			return this.disposables.IsProperSubsetOf(other);
		}

		/// <inheritdoc />
		public bool IsProperSupersetOf(IEnumerable<IDisposable> other)
		{
			ISetContracts.IsProperSupersetOf(other);

			return this.disposables.IsProperSupersetOf(other);
		}

		/// <inheritdoc />
		public bool IsSubsetOf(IEnumerable<IDisposable> other)
		{
			ISetContracts.IsSubsetOf(other);

			return this.disposables.IsSubsetOf(other);
		}

		/// <inheritdoc />
		public bool IsSupersetOf(IEnumerable<IDisposable> other)
		{
			ISetContracts.IsSupersetOf(other);

			return this.disposables.IsSupersetOf(other);
		}

		/// <inheritdoc />
		public bool Overlaps(IEnumerable<IDisposable> other)
		{
			ISetContracts.Overlaps(other);

			return this.disposables.Overlaps(other);
		}

		/// <inheritdoc />
		public bool SetEquals(IEnumerable<IDisposable> other)
		{
			ISetContracts.SetEquals(other);

			return this.disposables.SetEquals(other);
		}

		#endregion

		#endregion

		#region IEnumerable<IDisposable> Members

		/// <inheritdoc />
		public IEnumerator<IDisposable> GetEnumerator() => this.disposables.GetEnumerator();

		#endregion

		#region IEnumerable Members

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

		#endregion

		#region AbstractDisposable Overrides

		/// <inheritdoc />
		/// <remarks>
		/// Disposed instances of this class can no longer be mutated, but won't throw exceptions. The <see cref="Add"/> method
		/// will always return false and the mutating <see cref="ISet{IDisposable}"/> members will not change the collection.
		/// Upon disposing the collection also clears itself of all disposable instances it contains to avoid keeping disposed
		/// instances from being garbage collected.
		/// </remarks>
		protected override void ManagedDisposal()
		{
			foreach (IDisposable item in this.disposables)
			{
				item?.Dispose();
			}

			this.disposables.Clear();
		}

		#endregion
	}
}
