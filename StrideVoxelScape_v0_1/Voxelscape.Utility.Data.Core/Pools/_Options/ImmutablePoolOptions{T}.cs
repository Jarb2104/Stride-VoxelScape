using System;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Pools;

namespace Voxelscape.Utility.Data.Core.Pools
{
	/// <summary>
	/// Provides optional parameters for instances of <see cref="IPool{T}"/>.
	/// </summary>
	/// <typeparam name="T">The type of pooled values.</typeparam>
	/// <threadsafety static="true" instance="true" />
	public class ImmutablePoolOptions<T> : IPoolOptions<T>
	{
		public ImmutablePoolOptions(Action<T> resetAction, Action<T> releaseAction, int boundedCapacity)
		{
			Contracts.Requires.That(resetAction != null);
			Contracts.Requires.That(releaseAction != null);
			Contracts.Requires.That(boundedCapacity > 0 || boundedCapacity == Capacity.Unbounded);

			this.ResetAction = resetAction;
			this.ReleaseAction = releaseAction;
			this.BoundedCapacity = boundedCapacity;
		}

		/// <inheritdoc />
		public Action<T> ResetAction { get; }

		/// <inheritdoc />
		public Action<T> ReleaseAction { get; }

		/// <inheritdoc />
		public int BoundedCapacity { get; }
	}
}
