using System;

namespace Voxelscape.Utility.Common.Core.Disposables
{
	/// <summary>
	/// A disposable wrapper for a value where the value can only be accessed until the wrapper is disposed of
	/// and when the wrapper is disposed the wrapped value will also be disposed.
	/// </summary>
	/// <typeparam name="T">The type of the value.</typeparam>
	public class DisposableValue<T> : DisposableWrapper<T>
		where T : IDisposable
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="DisposableValue{T}" /> class.
		/// </summary>
		/// <param name="value">The value to wrap.</param>
		public DisposableValue(T value)
			: base(value)
		{
		}

		/// <inheritdoc />
		/// <remarks>
		/// If overridden, calling <c>base.ManagedDisposal(value)</c> will dispose <paramref name="value"/>.
		/// Normally this should be called at the end of the overriding implementation.
		/// </remarks>
		protected override void ManagedDisposal(T value) => value?.Dispose();
	}
}
