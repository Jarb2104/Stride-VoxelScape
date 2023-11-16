using System;

namespace Voxelscape.Utility.Common.Pact.Disposables
{
	/// <summary>
	/// Provides utilities for disposing of <see cref="IDisposable"/> objects.
	/// </summary>
	public static class Disposable
	{
		/// <summary>
		/// Gets the disposable that does nothing when disposed.
		/// </summary>
		/// <value>
		/// The disposable that does nothing when disposed.
		/// </value>
		/// <remarks>
		/// This always returns the same instance as it is completely immutable.
		/// </remarks>
		public static IDisposable EmptyDisposable { get; } = new DoNothingDisposable();

		/// <summary>
		/// Creates a new visibly disposable instance that does nothing except update the <see cref="IDisposed.IsDisposed"/>
		/// property when disposed.
		/// </summary>
		/// <returns>The disposable that does nothing when disposed except become disposed.</returns>
		public static IVisiblyDisposable CreateEmptyVisiblyDisposable() => new DoNothingVisiblyDisposable();

		/// <summary>
		/// Disposes the object if the object implements <see cref="IDisposable"/>.
		/// </summary>
		/// <param name="obj">The object to dispose if able.</param>
		/// <returns>True if the object was disposed; otherwise false.</returns>
		public static bool DisposeObjectIfAble(object obj)
		{
			var disposable = obj as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
				return true;
			}
			else
			{
				return false;
			}
		}

		#region Private Classes

		/// <summary>
		/// A disposable type that does nothing when disposed.
		/// </summary>
		private sealed class DoNothingDisposable : IDisposable
		{
			/// <inheritdoc />
			public void Dispose()
			{
			}
		}

		/// <summary>
		/// A visibly disposable type that does nothing when disposed except update the <see cref="IDisposed.IsDisposed"/> property.
		/// </summary>
		private sealed class DoNothingVisiblyDisposable : AbstractDisposable
		{
			/// <inheritdoc />
			protected override void ManagedDisposal()
			{
			}
		}

		#endregion
	}
}
