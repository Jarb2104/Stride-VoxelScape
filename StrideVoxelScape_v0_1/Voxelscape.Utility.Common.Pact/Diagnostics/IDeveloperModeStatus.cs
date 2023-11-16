using System;

namespace Voxelscape.Utility.Common.Pact.Diagnostics
{
	/// <summary>
	///
	/// </summary>
	public interface IDeveloperModeStatus
	{
		bool IsAllowed { get; }

		bool IsActive { get; }

		IObservable<bool> IsActiveChanged { get; }
	}
}
