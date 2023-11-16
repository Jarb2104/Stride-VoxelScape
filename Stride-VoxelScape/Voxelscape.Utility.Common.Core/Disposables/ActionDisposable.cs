using System;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;

namespace Voxelscape.Utility.Common.Core.Disposables
{
	/// <summary>
	/// A disposable that will execute an action delegate when it is disposed.
	/// </summary>
	public class ActionDisposable : AbstractDisposable
	{
		/// <summary>
		/// The action to perform to handle managed disposal.
		/// </summary>
		private Action onDispose;

		/// <summary>
		/// Initializes a new instance of the <see cref="ActionDisposable"/> class.
		/// </summary>
		/// <param name="onDispose">The action to perform to handle managed disposal.</param>
		public ActionDisposable(Action onDispose)
		{
			Contracts.Requires.That(onDispose != null);

			this.onDispose = onDispose;
		}

		#region AbstractDisposable Overrides

		/// <inheritdoc />
		protected override void ManagedDisposal()
		{
			// setting the delegates to null is to avoid accidentally hanging onto references
			// after disposing if the delegate creates a closure over any objects
			this.onDispose();
			this.onDispose = null;
		}

		#endregion
	}
}
