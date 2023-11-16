using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Voxelscape.Utility.Concurrency.Pact.Tasks
{
	public class TaskCompletionSource
	{
		private readonly TaskCompletionSource<bool> completion;

		public TaskCompletionSource()
		{
			this.completion = new TaskCompletionSource<bool>();
		}

		public TaskCompletionSource(TaskCreationOptions creationOptions)
		{
			this.completion = new TaskCompletionSource<bool>(creationOptions);
		}

		public TaskCompletionSource(object state)
		{
			this.completion = new TaskCompletionSource<bool>(state);
		}

		public TaskCompletionSource(object state, TaskCreationOptions creationOptions)
		{
			this.completion = new TaskCompletionSource<bool>(state, creationOptions);
		}

		public Task Task => this.completion.Task;

		public void SetCanceled() => this.completion.SetCanceled();

		public void SetException(Exception exception) => this.completion.SetException(exception);

		public void SetException(IEnumerable<Exception> exceptions) => this.completion.SetException(exceptions);

		public void SetResult() => this.completion.SetResult(default);

		public bool TrySetCanceled() => this.completion.TrySetCanceled();

		public bool TrySetCanceled(CancellationToken cancellationToken) => this.completion.TrySetCanceled(cancellationToken);

		public bool TrySetException(Exception exception) => this.completion.TrySetException(exception);

		public bool TrySetException(IEnumerable<Exception> exceptions) => this.completion.TrySetException(exceptions);

		public bool TrySetResult() => this.completion.TrySetResult(default);
	}
}
