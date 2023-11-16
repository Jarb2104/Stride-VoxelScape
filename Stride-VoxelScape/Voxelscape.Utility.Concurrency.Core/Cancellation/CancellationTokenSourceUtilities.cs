using System;
using System.Threading;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Concurrency.Pact;

namespace Voxelscape.Utility.Concurrency.Core.Cancellation
{
	/// <summary>
	///
	/// </summary>
	public static class CancellationTokenSourceUtilities
	{
		public static CancellationTokenSource CreateLinkedTimer(CancellationToken cancellation, TimeSpan timeout)
		{
			Contracts.Requires.That(timeout.IsDuration());

			if (cancellation.CanBeCanceled)
			{
				var result = CancellationTokenSource.CreateLinkedTokenSource(cancellation);
				result.CancelAfter(timeout);
				return result;
			}
			else
			{
				return new CancellationTokenSource(timeout);
			}
		}
	}
}
