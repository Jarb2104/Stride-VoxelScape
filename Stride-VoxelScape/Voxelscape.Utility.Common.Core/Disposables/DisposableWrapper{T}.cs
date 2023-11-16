using Voxelscape.Utility.Common.Pact.Disposables;

namespace Voxelscape.Utility.Common.Core.Disposables
{
	/// <summary>
	/// A disposable wrapper for a value where the value can only be accessed until the wrapper is disposed of.
	/// </summary>
	/// <typeparam name="T">The type of the value.</typeparam>
	public class DisposableWrapper<T> : AbstractDisposable, IDisposableValue<T>
	{
		/// <summary>
		/// The value wrapped by the disposable instance.
		/// </summary>
		private T value;

		/// <summary>
		/// Initializes a new instance of the <see cref="DisposableWrapper{T}" /> class.
		/// </summary>
		/// <param name="value">The value to wrap.</param>
		public DisposableWrapper(T value)
		{
			this.value = value;
		}

		/// <inheritdoc />
		public T Value
		{
			get
			{
				ITemporaryValueContracts.Value(this);

				return this.value;
			}
		}

		/// <inheritdoc />
		protected sealed override void ManagedDisposal()
		{
			this.ManagedDisposal(this.value);

			// small optimization in case the wrapper is held onto for a long time after disposing it
			this.value = default(T);
		}

		/// <summary>
		/// Releases managed resources.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <remarks>
		/// This overload provides access to the <see cref="Value"/> property that is no longer
		/// accessible due to this instance having been disposed.
		/// </remarks>
		protected virtual void ManagedDisposal(T value)
		{
		}
	}
}
