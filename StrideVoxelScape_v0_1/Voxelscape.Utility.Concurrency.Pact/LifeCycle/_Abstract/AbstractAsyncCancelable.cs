using System.Threading;
using System.Threading.Tasks;
using Nito.AsyncEx;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Concurrency.Pact.LifeCycle
{
	public abstract class AbstractAsyncCancelable : IAsyncCancelable
	{
		private readonly AsyncCountdownEvent completing = new AsyncCountdownEvent(1);

		private readonly CancellationTokenSource cancellationSource;

		public AbstractAsyncCancelable()
			: this(new CancellationTokenSource())
		{
		}

		public AbstractAsyncCancelable(CancellationToken cancellation)
			: this(CancellationTokenSource.CreateLinkedTokenSource(cancellation, CancellationToken.None))
		{
		}

		public AbstractAsyncCancelable(CancellationToken cancellation1, CancellationToken cancellation2)
			: this(CancellationTokenSource.CreateLinkedTokenSource(cancellation1, cancellation2))
		{
		}

		public AbstractAsyncCancelable(params CancellationToken[] cancellations)
			: this(CancellationTokenSource.CreateLinkedTokenSource(cancellations))
		{
		}

		private AbstractAsyncCancelable(CancellationTokenSource cancellationSource)
		{
			Contracts.Requires.That(cancellationSource != null);

			this.cancellationSource = cancellationSource;
			this.Completion = this.HandleCompleteAsync();
		}

		/// <inheritdoc />
		public virtual CancellationToken CancellationToken => this.cancellationSource.Token;

		/// <inheritdoc />
		public virtual Task Completion { get; }

		protected virtual bool IsCompletionStarted => this.completing.CurrentCount == 0;

		/// <inheritdoc />
		public virtual void Complete() => this.completing.TrySignal();

		/// <inheritdoc />
		public virtual void Cancel()
		{
			this.cancellationSource.Cancel();
			this.Complete();
		}

		protected abstract Task CompleteAsync(CancellationToken cancellation);

		private async Task HandleCompleteAsync()
		{
			try
			{
				await this.completing.WaitAsync().DontMarshallContext();
				this.CancellationToken.ThrowIfCancellationRequested();
				await this.CompleteAsync(this.CancellationToken).DontMarshallContext();
			}
			finally
			{
				this.cancellationSource.Dispose();
			}
		}
	}
}
