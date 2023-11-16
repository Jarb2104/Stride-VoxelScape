using System.Collections.Generic;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Concurrency.Pact.LifeCycle;

namespace Voxelscape.Utility.Concurrency.Core.LifeCycle
{
	/// <summary>
	///
	/// </summary>
	public class AggregateAsyncCompletable : AbstractAsyncCompletable
	{
		private readonly bool inParallel;

		private readonly IEnumerable<IAsyncCompletable> completables;

		public AggregateAsyncCompletable(params IAsyncCompletable[] completables)
			: this(false, completables)
		{
		}

		public AggregateAsyncCompletable(bool inParallel, params IAsyncCompletable[] completables)
			: this(inParallel, (IEnumerable<IAsyncCompletable>)completables)
		{
		}

		public AggregateAsyncCompletable(IEnumerable<IAsyncCompletable> completables)
			: this(false, completables)
		{
		}

		public AggregateAsyncCompletable(bool inParallel, IEnumerable<IAsyncCompletable> completables)
		{
			Contracts.Requires.That(completables.AllAndSelfNotNull());

			this.inParallel = inParallel;
			this.completables = completables;
		}

		/// <inheritdoc />
		protected override async Task CompleteAsync()
		{
			if (this.inParallel)
			{
				var completions = new List<Task>();
				foreach (var completable in this.completables)
				{
					completable.Complete();
					completions.Add(completable.Completion);
				}

				await Task.WhenAll(completions).DontMarshallContext();
			}
			else
			{
				foreach (var completable in this.completables)
				{
					await completable.CompleteAndAwaitAsync().DontMarshallContext();
				}
			}
		}
	}
}
