using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Core.Disposables;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Concurrency.Core.Tasks;
using Voxelscape.Utility.Concurrency.Pact.LifeCycle;

namespace Voxelscape.Utility.Concurrency.Core.LifeCycle
{
	/// <summary>
	///
	/// </summary>
	public class DisposableAsyncCompletable : AbstractAsyncCompletable
	{
		private readonly IDisposable disposable;

		public DisposableAsyncCompletable(IDisposable disposable)
		{
			Contracts.Requires.That(disposable != null);

			this.disposable = disposable;
		}

		public DisposableAsyncCompletable(params IDisposable[] disposables)
			: this(new AggregateDisposable(disposables))
		{
		}

		public DisposableAsyncCompletable(IEnumerable<IDisposable> disposables)
			: this(new AggregateDisposable(disposables))
		{
		}

		/// <inheritdoc />
		protected override Task CompleteAsync()
		{
			this.disposable.Dispose();
			return TaskUtilities.CompletedTask;
		}
	}
}
