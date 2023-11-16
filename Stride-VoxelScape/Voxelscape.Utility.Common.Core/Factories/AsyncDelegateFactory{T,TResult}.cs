using System;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;

namespace Voxelscape.Utility.Common.Core.Factories
{
	/// <summary>
	/// Allows a task returning function to be wrapped in the <see cref="IAsyncFactory{T, TResult}" /> interface.
	/// </summary>
	/// <typeparam name="T">The type of the argument.</typeparam>
	/// <typeparam name="TResult">The type to create.</typeparam>
	public class AsyncDelegateFactory<T, TResult> : IAsyncFactory<T, TResult>
	{
		/// <summary>
		/// The instance creation delegate.
		/// </summary>
		private readonly Func<T, Task<TResult>> create;

		/// <summary>
		/// Initializes a new instance of the <see cref="AsyncDelegateFactory{T, TResult}"/> class.
		/// </summary>
		/// <param name="instanceCreator">The instance creator.</param>
		public AsyncDelegateFactory(Func<T, Task<TResult>> instanceCreator)
		{
			Contracts.Requires.That(instanceCreator != null);

			this.create = instanceCreator;
		}

		#region IAsyncFactory<T, TResult> Members

		/// <inheritdoc />
		public async Task<TResult> CreateAsync(T arg)
		{
			return await this.create(arg).ConfigureAwait(false);
		}

		#endregion
	}
}
