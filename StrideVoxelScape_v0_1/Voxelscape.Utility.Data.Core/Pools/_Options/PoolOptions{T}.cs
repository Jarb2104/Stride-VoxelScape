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
	public class PoolOptions<T> : IPoolOptions<T>
	{
		private Action<T> resetAction = PoolOptions.Default<T>().ResetAction;

		private Action<T> releaseAction = PoolOptions.Default<T>().ReleaseAction;

		private int boundedCapacity = PoolOptions.Default<T>().BoundedCapacity;

		/// <inheritdoc />
		public virtual Action<T> ResetAction
		{
			get
			{
				return this.resetAction;
			}

			set
			{
				Contracts.Requires.That(value != null);

				this.resetAction = value;
			}
		}

		/// <inheritdoc />
		public virtual Action<T> ReleaseAction
		{
			get
			{
				return this.releaseAction;
			}

			set
			{
				Contracts.Requires.That(value != null);

				this.releaseAction = value;
			}
		}

		/// <inheritdoc />
		public virtual int BoundedCapacity
		{
			get
			{
				return this.boundedCapacity;
			}

			set
			{
				Contracts.Requires.That(value > 0 || value == Capacity.Unbounded);

				this.boundedCapacity = value;
			}
		}
	}
}
