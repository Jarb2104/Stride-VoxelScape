using System;
using System.Runtime.Serialization;

namespace Voxelscape.Utility.Common.Pact.Exceptions
{
	/// <summary>
	/// The exception that is thrown when one of the generic type parameters provided to a method is not valid.
	/// </summary>
	[Serializable]
	public class TypeParameterException : ArgumentException
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="TypeParameterException"/> class.
		/// </summary>
		public TypeParameterException()
			: base()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TypeParameterException"/> class.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		public TypeParameterException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TypeParameterException"/> class.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">
		/// The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter
		/// is not a null reference, the current exception is raised in a catch block that handles the inner exception.
		/// </param>
		public TypeParameterException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TypeParameterException"/> class.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="paramName">The name of the parameter that caused the current exception.</param>
		public TypeParameterException(string message, string paramName)
			: base(message, paramName)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TypeParameterException"/> class.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="paramName">The name of the parameter that caused the current exception.</param>
		/// <param name="innerException">
		/// The exception that is the cause of the current exception. If the <paramref name="innerException" /> parameter
		/// is not a null reference, the current exception is raised in a catch block that handles the inner exception.
		/// </param>
		public TypeParameterException(string message, string paramName, Exception innerException)
			: base(message, paramName, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="TypeParameterException"/> class.
		/// </summary>
		/// <param name="info">
		/// The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.
		/// </param>
		/// <param name="context">
		/// The <see cref="StreamingContext"/> that contains contextual information about the source or destination.
		/// </param>
		protected TypeParameterException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
