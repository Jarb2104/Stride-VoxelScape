using System;
using System.Runtime.Serialization;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Pact.Disposables
{
	/// <summary>
	/// Exception that is thrown when a finalizer is ran.
	/// </summary>
	/// <remarks>
	/// <para>
	/// If <see cref="IDisposable.Dispose" /> is called there should be no need for a finalizer to run.
	/// This exception being thrown implies that either dispose was not called properly or it was not
	/// implemented properly. Any time a disposable resource is used its dispose method should be called,
	/// and implementations of dispose should call <c>GC.SuppressFinalize(this)</c>.
	/// </para>
	/// <para>
	/// This exception should be thrown as the first line in all finalizers to ensure they aren't run.
	/// The finalizers should be surronded by conditional compiler directives so only debug builds
	/// are affected.
	/// </para>
	/// </remarks>
	[Serializable]
	public class FinalizerRanException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="FinalizerRanException"/> class.
		/// </summary>
		public FinalizerRanException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FinalizerRanException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public FinalizerRanException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FinalizerRanException"/> class.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">
		/// The exception that is the cause of the current exception, or a null reference
		/// if no inner exception is specified.
		/// </param>
		public FinalizerRanException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FinalizerRanException"/> class.
		/// </summary>
		/// <param name="info">
		/// The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.
		/// </param>
		/// <param name="context">
		/// The <see cref="StreamingContext"/> that contains contextual information about the source or destination.
		/// </param>
		protected FinalizerRanException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public static FinalizerRanException CreateException(object instanceBeingFinalized)
		{
			Contracts.Requires.That(instanceBeingFinalized != null);

			return new FinalizerRanException(
				"Dispose was either not called or implemented wrong. Finalizers should not need to run. " +
				$"{nameof(instanceBeingFinalized)}: {instanceBeingFinalized}");
		}
	}
}
