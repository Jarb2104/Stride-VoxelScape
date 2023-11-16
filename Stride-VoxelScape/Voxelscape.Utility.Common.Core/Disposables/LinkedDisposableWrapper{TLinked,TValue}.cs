using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;

namespace Voxelscape.Utility.Common.Core.Disposables
{
	/// <summary>
	/// A disposable wrapper for a value where the value can only be accessed until the wrapper is disposed of and where
	/// the disposed status of the wrapper is linked to the disposed status of another instance. The wrapped will be
	/// considered disposed of if the linked instance is disposed of.
	/// </summary>
	/// <typeparam name="TLinked">The type of the linked disposable.</typeparam>
	/// <typeparam name="TValue">The type of the wrapped value.</typeparam>
	public class LinkedDisposableWrapper<TLinked, TValue> : DisposableWrapper<TValue>
		where TLinked : IDisposed
	{
		/// <summary>
		/// The disposable instance this instance is linked to.
		/// </summary>
		private readonly TLinked linkedDisposable;

		/// <summary>
		/// Initializes a new instance of the <see cref="LinkedDisposableWrapper{TDisposable, TValue}"/> class.
		/// </summary>
		/// <param name="linkTo">The linked disposable.</param>
		/// <param name="value">The value to wrap.</param>
		public LinkedDisposableWrapper(TLinked linkTo, TValue value)
			: base(value)
		{
			Contracts.Requires.That(linkTo != null);

			this.linkedDisposable = linkTo;
		}

		#region AbstractDisposable Overrides

		/// <inheritdoc />
		public override bool IsDisposed
		{
			get
			{
				if (this.linkedDisposable.IsDisposed)
				{
					// Mutating state inside an accessor is generally bad practice, but this is supposed to be considered
					// disposed of if its linked disposable is disposed of, and calling Dispose can help with garbage
					// collection depending on the subclass's implementation. Mutating the state here does not change
					// the behavior of the disposable value in any publicly visible way because the value can't be
					// accessed after disposal and so should be safe.
					this.Dispose();
					return true;
				}
				else
				{
					return base.IsDisposed;
				}
			}
		}

		#endregion

		/// <summary>
		/// Gets the disposable instance this instance is linked to.
		/// </summary>
		/// <value>
		/// The disposable instance this instance is linked to.
		/// </value>
		protected TLinked LinkedDisposable => this.linkedDisposable;
	}
}
