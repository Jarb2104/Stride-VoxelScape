using System;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;

namespace Voxelscape.Utility.Common.Core.Factories
{
	/// <summary>
	/// Allows a task returning function to be wrapped in the <see cref="IAsyncFactory{T1, T2, TResult}" /> interface.
	/// </summary>
	/// <typeparam name="T1">The type of the first argument.</typeparam>
	/// <typeparam name="T2">The type of the second argument.</typeparam>
	/// <typeparam name="TResult">The type to create.</typeparam>
	public class AsyncDelegateFactory<T1, T2, TResult> : IAsyncFactory<T1, T2, TResult>
	{
		/// <summary>
		/// The instance creation delegate.
		/// </summary>
		private readonly Func<T1, T2, Task<TResult>> create;

		/// <summary>
		/// Initializes a new instance of the <see cref="AsyncDelegateFactory{T1, T2, TResult}"/> class.
		/// </summary>
		/// <param name="instanceCreator">The instance creator.</param>
		public AsyncDelegateFactory(Func<T1, T2, Task<TResult>> instanceCreator)
		{
			Contracts.Requires.That(instanceCreator != null);

			this.create = instanceCreator;
		}

		#region IAsyncFactory<T1, T2, TResult> Members

		/// <inheritdoc />
		public async Task<TResult> CreateAsync(T1 arg1, T2 arg2)
		{
			return await this.create(arg1, arg2).ConfigureAwait(false);
		}

		#endregion
	}
}
