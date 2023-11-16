using System;

namespace Voxelscape.Utility.Common.Pact.Disposables
{
	/// <summary>
	/// Provides a mechanism for releasing unmanaged resources and also the ability
	/// to observe if the resources have been released yet.
	/// </summary>
	public interface IVisiblyDisposable : IDisposable, IDisposed
	{
	}
}
