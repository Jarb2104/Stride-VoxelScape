using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Enums
{
	/// <summary>
	/// The exception that is thrown when an operation is invalid because the instance is missing configuration information for one
	/// or more values of an enumeration type.
	/// </summary>
	/// <typeparam name="TEnum">The type of the enumeration.</typeparam>
	[Serializable]
	public class MissingEnumException<TEnum> : InvalidOperationException
		where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		/// <summary>
		/// The enum values missing from the instance.
		/// </summary>
		private readonly IEnumerable<TEnum> missingEnums;

		/// <summary>
		/// Initializes a new instance of the <see cref="MissingEnumException{TEnum}" /> class.
		/// </summary>
		/// <param name="missingEnums">The missing enum values.</param>
		public MissingEnumException(IEnumerable<TEnum> missingEnums)
			: base("Operation is not valid due to the object missing state for one or more enum values.")
		{
			Contracts.Requires.That(typeof(TEnum).IsEnum);
			Contracts.Requires.That(missingEnums != null);
			Contracts.Requires.That(!missingEnums.IsEmpty());

			this.missingEnums = missingEnums;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MissingEnumException{TEnum}" /> class.
		/// </summary>
		/// <param name="missingEnums">The missing enum values.</param>
		public MissingEnumException(params TEnum[] missingEnums)
			: base("Operation is not valid due to the object missing state for one or more enum values.")
		{
			Contracts.Requires.That(typeof(TEnum).IsEnum);
			Contracts.Requires.That(missingEnums != null);
			Contracts.Requires.That(missingEnums.Length >= 1);

			this.missingEnums = missingEnums;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="MissingEnumException{TEnum}"/> class.
		/// </summary>
		/// <param name="info">
		/// The <see cref="SerializationInfo"/> that holds the serialized object data about the exception being thrown.
		/// </param>
		/// <param name="context">
		/// The <see cref="StreamingContext"/> that contains contextual information about the source or destination.
		/// </param>
		protected MissingEnumException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		/// <summary>
		/// Gets the list of missing enum values.
		/// </summary>
		/// <value>
		/// The missing enum values.
		/// </value>
		public IEnumerable<TEnum> MissingEnums
		{
			get { return this.missingEnums; }
		}
	}
}
