using System;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;

namespace Voxelscape.Utility.Common.Core.Factories
{
	/// <summary>
	/// Allows a <see cref="Func{TResult}"/> to be wrapped
	/// in the <see cref="IFactory{TResult}"/> interface.
	/// </summary>
	/// <typeparam name="TResult">The type to create.</typeparam>
	public class DelegateFactory<TResult> : IFactory<TResult>
	{
		/// <summary>
		/// The instance creation delegate.
		/// </summary>
		private readonly Func<TResult> create;

		/// <summary>
		/// Initializes a new instance of the <see cref="DelegateFactory{TResult}"/> class.
		/// </summary>
		/// <param name="instanceCreator">The instance creator.</param>
		public DelegateFactory(Func<TResult> instanceCreator)
		{
			Contracts.Requires.That(instanceCreator != null);

			this.create = instanceCreator;
		}

		#region IFactory<TResult> Members

		/// <inheritdoc />
		public TResult Create()
		{
			return this.create();
		}

		#endregion
	}
}
