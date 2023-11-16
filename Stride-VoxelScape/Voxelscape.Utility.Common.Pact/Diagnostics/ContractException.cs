using System;
using System.Runtime.Serialization;

namespace Voxelscape.Utility.Common.Pact.Diagnostics
{
	/// <summary>
	/// Exception that is thrown when a contract is violated.
	/// </summary>
	/// <remarks>
	/// Contract violations indicate a bug in the code which should always be fixed.
	/// Therefore, this exception type should never be caught by production code.
	/// </remarks>
	public class ContractException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ContractException"/> class.
		/// </summary>
		public ContractException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ContractException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public ContractException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ContractException"/> class.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">
		/// The exception that is the cause of the current exception, or a null reference
		/// if no inner exception is specified.
		/// </param>
		public ContractException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ContractException"/> class.
		/// </summary>
		/// <param name="info">
		/// The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.
		/// </param>
		/// <param name="context">
		/// The <see cref="StreamingContext"/> that contains contextual information about the source or destination.
		/// </param>
		protected ContractException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
