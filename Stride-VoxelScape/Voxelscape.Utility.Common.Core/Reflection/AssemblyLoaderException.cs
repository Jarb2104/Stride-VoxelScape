using System;
using System.Runtime.Serialization;

namespace Voxelscape.Utility.Common.Core.Reflection
{
	/// <summary>
	/// The exception that is thrown when attempting to load assembly dependencies through use of the
	/// <see cref="AssemblyLoaderClassAttribute"/> or <see cref="LoadAssemblyDependenciesAttribute"/> fails.
	/// </summary>
	[Serializable]
	public class AssemblyLoaderException : Exception
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AssemblyLoaderException"/> class.
		/// </summary>
		public AssemblyLoaderException()
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AssemblyLoaderException"/> class.
		/// </summary>
		/// <param name="message">The message that describes the error.</param>
		public AssemblyLoaderException(string message)
			: base(message)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AssemblyLoaderException"/> class.
		/// </summary>
		/// <param name="message">The error message that explains the reason for the exception.</param>
		/// <param name="innerException">
		/// The exception that is the cause of the current exception, or a null reference
		/// if no inner exception is specified.
		/// </param>
		public AssemblyLoaderException(string message, Exception innerException)
			: base(message, innerException)
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="AssemblyLoaderException"/> class.
		/// </summary>
		/// <param name="info">The SerializationInfo that holds the serialized object data about the exception being thrown.</param>
		/// <param name="context">The StreamingContext that contains contextual information about the source or destination.</param>
		protected AssemblyLoaderException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}
	}
}
