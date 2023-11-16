using System;
using Voxelscape.Stages.Management.Pact.Stages;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Management.Pact.Generation
{
	/// <summary>
	///
	/// </summary>
	public class StageGenerationException : Exception, IStageIdentifiable, IGenerationPhaseIdentifiable
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="StageGenerationException" /> class.
		/// </summary>
		/// <param name="phase">The phase.</param>
		public StageGenerationException(IGenerationPhaseIdentifiable phase)
			: this(phase?.StageIdentity, phase?.PhaseIdentity)
		{
			Contracts.Requires.That(phase != null);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="StageGenerationException"/> class.
		/// </summary>
		/// <param name="phase">The phase.</param>
		/// <param name="message">The message that describes the error.</param>
		public StageGenerationException(IGenerationPhaseIdentifiable phase, string message)
			: this(phase?.StageIdentity, phase?.PhaseIdentity, message)
		{
			Contracts.Requires.That(phase != null);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="StageGenerationException"/> class.
		/// </summary>
		/// <param name="phase">The phase.</param>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">
		/// The exception that is the cause of the current exception, or a null reference
		/// if no inner exception is specified.
		/// </param>
		public StageGenerationException(IGenerationPhaseIdentifiable phase, string message, Exception innerException)
			: this(phase?.StageIdentity, phase?.PhaseIdentity, message, innerException)
		{
			Contracts.Requires.That(phase != null);
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="StageGenerationException" /> class.
		/// </summary>
		/// <param name="stage">The stage.</param>
		/// <param name="phase">The phase.</param>
		public StageGenerationException(IStageIdentity stage, IGenerationPhaseIdentity phase)
		{
			Contracts.Requires.That(stage != null);
			Contracts.Requires.That(phase != null);

			this.StageIdentity = stage;
			this.PhaseIdentity = phase;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="StageGenerationException"/> class.
		/// </summary>
		/// /// <param name="stage">The stage.</param>
		/// <param name="phase">The phase.</param>
		/// <param name="message">The message that describes the error.</param>
		public StageGenerationException(IStageIdentity stage, IGenerationPhaseIdentity phase, string message)
			: base(message)
		{
			Contracts.Requires.That(stage != null);
			Contracts.Requires.That(phase != null);

			this.StageIdentity = stage;
			this.PhaseIdentity = phase;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="StageGenerationException"/> class.
		/// </summary>
		/// /// <param name="stage">The stage.</param>
		/// <param name="phase">The phase.</param>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">
		/// The exception that is the cause of the current exception, or a null reference
		/// if no inner exception is specified.
		/// </param>
		public StageGenerationException(
			IStageIdentity stage, IGenerationPhaseIdentity phase, string message, Exception innerException)
			: base(message, innerException)
		{
			Contracts.Requires.That(stage != null);
			Contracts.Requires.That(phase != null);

			this.StageIdentity = stage;
			this.PhaseIdentity = phase;
		}

		/// <inheritdoc />
		public IStageIdentity StageIdentity { get; }

		/// <inheritdoc />
		public IGenerationPhaseIdentity PhaseIdentity { get; }
	}
}
