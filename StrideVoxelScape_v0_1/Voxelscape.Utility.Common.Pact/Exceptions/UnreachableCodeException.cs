using System;
using System.Runtime.Serialization;

namespace Voxelscape.Utility.Common.Pact.Exceptions
{
	/// <summary>
	/// The exception that is thrown when the flow of execution has managed to reach what is supposed to be an impossible
	/// area in code. An example of this could be a default clause in a switch statement if you have properly
	/// checked input so that one of the cases should always execute.
	/// </summary>
	[Serializable]
	public class UnreachableCodeException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UnreachableCodeException"/> class.
		/// </summary>
		public UnreachableCodeException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UnreachableCodeException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public UnreachableCodeException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UnreachableCodeException"/> class.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">
		/// The exception that is the cause of the current exception, or a null reference
		/// if no inner exception is specified.
		/// </param>
		public UnreachableCodeException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="UnreachableCodeException"/> class.
		/// </summary>
		/// <param name="info">
		/// The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.
		/// </param>
		/// <param name="context">
		/// The <see cref="StreamingContext"/> that contains contextual information about the source or destination.
		/// </param>
		protected UnreachableCodeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
