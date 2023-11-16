using System;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;

namespace Voxelscape.Utility.Common.Core.Factories
{
	/// <summary>
	/// Allows a <see cref="Func{T, TResult}" /> to be wrapped
	/// in the <see cref="IFactory{T, TResult}" /> interface.
	/// </summary>
	/// <typeparam name="T">The type of the argument.</typeparam>
	/// <typeparam name="TResult">The type to create.</typeparam>
	public class DelegateFactory<T, TResult> : IFactory<T, TResult>
	{
		/// <summary>
		/// The instance creation delegate.
		/// </summary>
		private readonly Func<T, TResult> create;

		/// <summary>
		/// Initializes a new instance of the <see cref="DelegateFactory{T, TResult}"/> class.
		/// </summary>
		/// <param name="instanceCreator">The instance creator.</param>
		public DelegateFactory(Func<T, TResult> instanceCreator)
		{
			Contracts.Requires.That(instanceCreator != null);

			this.create = instanceCreator;
		}

		#region IFactory<T, TResult> Members

		/// <inheritdoc />
		public TResult Create(T arg)
		{
			return this.create(arg);
		}

		#endregion
	}
}
