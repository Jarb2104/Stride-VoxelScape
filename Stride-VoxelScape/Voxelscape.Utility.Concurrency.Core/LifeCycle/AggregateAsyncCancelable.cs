using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Concurrency.Core.Tasks;
using Voxelscape.Utility.Concurrency.Pact.LifeCycle;

namespace Voxelscape.Utility.Concurrency.Core.LifeCycle
{
	/// <summary>
	///
	/// </summary>
	public class AggregateAsyncCancelable : AbstractAsyncCancelable
	{
		private readonly bool inParallel;

		private readonly IEnumerable<IAsyncCancelable> cancelables;

		public AggregateAsyncCancelable(params IAsyncCancelable[] completables)
			: this(false, completables)
		{
		}

		public AggregateAsyncCancelable(bool inParallel, params IAsyncCancelable[] completables)
			: this(inParallel, (IEnumerable<IAsyncCancelable>)completables)
		{
		}

		public AggregateAsyncCancelable(IEnumerable<IAsyncCancelable> completables)
			: this(false, completables)
		{
		}

		public AggregateAsyncCancelable(bool inParallel, IEnumerable<IAsyncCancelable> completables)
		{
			Contracts.Requires.That(completables.AllAndSelfNotNull());

			this.inParallel = inParallel;
			this.cancelables = completables;
		}

		/// <inheritdoc />
		public override void Cancel()
		{
			base.Cancel();
			foreach (var cancelable in this.cancelables)
			{
				cancelable.Cancel();
			}
		}

		/// <inheritdoc />
		protected override async Task CompleteAsync(CancellationToken cancellation)
		{
			if (this.inParallel)
			{
				cancellation.ThrowIfCancellationRequested();
				var completions = new List<Task>();
				foreach (var cancelable in this.cancelables)
				{
					cancelable.Complete();
					completions.Add(cancelable.Completion);
				}

				await TaskUtilities.WhenAll(cancellation, completions).DontMarshallContext();
			}
			else
			{
				foreach (var cancelable in this.cancelables)
				{
					cancellation.ThrowIfCancellationRequested();
					await cancelable.CompleteAndAwaitAsync().DontMarshallContext();
				}
			}
		}
	}
}
