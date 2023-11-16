using System;
using System.Threading;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Concurrency.Pact.Synchronization
{
	public struct ReleaseStruct : IDisposable
	{
		private readonly SemaphoreSlim semaphore;

		internal ReleaseStruct(SemaphoreSlim semaphore)
		{
			Contracts.Requires.That(semaphore != null);

			this.semaphore = semaphore;
		}

		// ?. because the default struct constructor leaves the field as null
		/// <inheritdoc />
		public void Dispose() => this.semaphore?.Release();
	}
}
