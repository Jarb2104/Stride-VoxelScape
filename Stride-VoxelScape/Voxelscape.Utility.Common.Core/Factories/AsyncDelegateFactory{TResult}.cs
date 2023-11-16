using System;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;

namespace Voxelscape.Utility.Common.Core.Factories
{
	/// <summary>
	/// Allows a task returning function to be wrapped in the <see cref="IAsyncFactory{TResult}" /> interface.
	/// </summary>
	/// <typeparam name="TResult">The type to create.</typeparam>
	public class AsyncDelegateFactory<TResult> : IAsyncFactory<TResult>
	{
		/// <summary>
		/// The instance creation delegate.
		/// </summary>
		private readonly Func<Task<TResult>> create;

		/// <summary>
		/// Initializes a new instance of the <see cref="AsyncDelegateFactory{TResult}"/> class.
		/// </summary>
		/// <param name="instanceCreator">The instance creator.</param>
		public AsyncDelegateFactory(Func<Task<TResult>> instanceCreator)
		{
			Contracts.Requires.That(instanceCreator != null);

			this.create = instanceCreator;
		}

		#region IAsyncFactory<TResult> Members

		/// <inheritdoc />
		public async Task<TResult> CreateAsync()
		{
			return await this.create().ConfigureAwait(false);
		}

		#endregion
	}
}
