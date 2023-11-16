using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Voxelscape.Stages.Management.Pact.Generation;
using Voxelscape.Utility.Common.Core.Types;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Concurrency.Pact.LifeCycle;

namespace Voxelscape.Stages.Management.Core.Generation
{
	/// <summary>
	///
	/// </summary>
	public class StageGenerator : AbstractAsyncCancelable, IStageGenerator
	{
		private static readonly IGenerationPhaseIdentity UnidentifiedPhase =
			new GenerationPhaseIdentity("Unidentified phase");

		private static readonly string ErrorMessage =
			"Exception generating stage. Generation phase did not identify itself in the exception.";

		private readonly Subject<FaultableValue<GenerationPhaseProgress, StageGenerationException>> progress =
			new Subject<FaultableValue<GenerationPhaseProgress, StageGenerationException>>();

		private readonly ActionBlock<IStageSeed> generator;

		public StageGenerator()
			: this(new GenerationOptions())
		{
		}

		public StageGenerator(GenerationOptions options)
			: base(options?.CancellationToken ?? CancellationToken.None)
		{
			Contracts.Requires.That(options != null);

			var dataflowOptions = new ExecutionDataflowBlockOptions()
			{
				MaxDegreeOfParallelism = options.MaxDegreeOfParallelism,
				CancellationToken = this.CancellationToken,
			};

			this.generator = new ActionBlock<IStageSeed>(this.HandleStageSeed, dataflowOptions);

			this.Progress = this.progress.AsObservable();
		}

		public IObservable<FaultableValue<GenerationPhaseProgress, StageGenerationException>> Progress { get; }

		/// <inheritdoc />
		public async Task GenerateStageAsync(IStageSeed stageSeed)
		{
			IStageGeneratorContracts.GenerateStageAsync(stageSeed);

			if (await this.generator.SendAsync(stageSeed, this.CancellationToken).DontMarshallContext())
			{
				await stageSeed.Completion.DontMarshallContext();
			}
			else
			{
				throw new InvalidOperationException("Stage generator has been completed or canceled.");
			}
		}

		/// <inheritdoc />
		protected override async Task CompleteAsync(CancellationToken cancellation)
		{
			try
			{
				this.generator.Complete();
				await this.generator.Completion.DontMarshallContext();
				this.progress.OnCompleted();
			}
			catch (Exception exception)
			{
				this.progress.OnError(exception);
				throw;
			}
			finally
			{
				this.progress.Dispose();
			}
		}

		private static StageGenerationException Wrap(IStageSeed stageSeed, Exception error) =>
			new StageGenerationException(stageSeed.StageIdentity, UnidentifiedPhase, ErrorMessage, error);

		private async Task HandleStageSeed(IStageSeed seed)
		{
			Contracts.Requires.That(seed != null);

			try
			{
				using (seed.LinkCancellation(this.CancellationToken))
				using (seed.Progress.WrapExceptions(error => Wrap(seed, error)).Subscribe(this.progress.OnNext))
				{
					await seed.CompleteAndAwaitAsync().DontMarshallContext();
				}
			}
			catch (Exception)
			{
				// if generation fails just end the task early in a successfully completed state so that the
				// action block keeps on processing additional stage generation requests instead of faulting
			}
		}
	}
}
