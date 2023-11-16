using System;
using System.Collections.Generic;
using System.Diagnostics;
using Voxelscape.Utility.Common.Core.Diagnostics;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;

namespace Voxelscape.Utility.Common.Core.Disposables
{
	/// <summary>
	/// Represents one or more <see cref="IDisposable"/> objects that will be disposed of as a single group
	/// when this instance is disposed of.
	/// </summary>
	[DebuggerTypeProxy(typeof(EnumerableDebugView<>))]
	public class AggregateDisposable : AbstractDisposable
	{
		/// <summary>
		/// The disposables to dispose of when this instance is disposed.
		/// </summary>
		private readonly IEnumerable<IDisposable> disposables;

		/// <summary>
		/// Initializes a new instance of the <see cref="AggregateDisposable"/> class.
		/// </summary>
		/// <param name="disposables">The disposables to dispose of when this instance is disposed.</param>
		public AggregateDisposable(params IDisposable[] disposables)
			: this((IEnumerable<IDisposable>)disposables)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AggregateDisposable"/> class.
		/// </summary>
		/// <param name="disposables">The disposables to dispose of when this instance is disposed.</param>
		public AggregateDisposable(IEnumerable<IDisposable> disposables)
		{
			Contracts.Requires.That(disposables.AllAndSelfNotNull());

			this.disposables = disposables;
		}

		/// <inheritdoc />
		protected override void ManagedDisposal()
		{
			foreach (IDisposable disposable in this.disposables)
			{
				disposable.Dispose();
			}
		}
	}
}
