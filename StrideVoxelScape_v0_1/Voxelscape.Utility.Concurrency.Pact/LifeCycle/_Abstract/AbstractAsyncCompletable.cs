using System.Threading.Tasks;
using Nito.AsyncEx;

namespace Voxelscape.Utility.Concurrency.Pact.LifeCycle
{
	public abstract class AbstractAsyncCompletable : IAsyncCompletable
	{
		private readonly AsyncCountdownEvent completing = new AsyncCountdownEvent(1);

		public AbstractAsyncCompletable()
		{
			this.Completion = this.HandleCompleteAsync();
		}

		/// <inheritdoc />
		public virtual Task Completion { get; }

		protected virtual bool IsCompletionStarted => this.completing.CurrentCount == 0;

		/// <inheritdoc />
		public virtual void Complete() => this.completing.TrySignal();

		protected abstract Task CompleteAsync();

		private async Task HandleCompleteAsync()
		{
			await this.completing.WaitAsync().DontMarshallContext();
			await this.CompleteAsync().DontMarshallContext();
		}
	}
}
