using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;

namespace Voxelscape.Utility.Common.Core.Diagnostics
{
	/// <summary>
	///
	/// </summary>
	public class DeveloperModeStatus : AbstractDisposable, IDeveloperModeStatus
	{
		private readonly Subject<bool> isActiveChanged = new Subject<bool>();

		public DeveloperModeStatus(bool isAllowed = true)
		{
			this.IsAllowed = isAllowed;
			this.IsActiveChanged = this.isActiveChanged.AsObservable();
		}

		/// <inheritdoc />
		public bool IsAllowed { get; }

		/// <inheritdoc />
		public bool IsActive { get; private set; } = false;

		/// <inheritdoc />
		public IObservable<bool> IsActiveChanged { get; }

		public void SetIsActive(bool isActive)
		{
			if (this.IsDisposed || !this.IsAllowed)
			{
				return;
			}

			if (this.IsActive != isActive)
			{
				this.IsActive = isActive;
				this.isActiveChanged.OnNext(this.IsActive);
			}
		}

		public void ToggleIsActive() => this.SetIsActive(!this.IsActive);

		/// <inheritdoc />
		protected override void ManagedDisposal()
		{
			this.isActiveChanged.OnCompleted();
			this.isActiveChanged.Dispose();
		}
	}
}
