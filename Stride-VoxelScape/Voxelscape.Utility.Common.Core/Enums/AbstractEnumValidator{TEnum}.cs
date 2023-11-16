using System;
using System.ComponentModel;
using System.Linq;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Exceptions;

namespace Voxelscape.Utility.Common.Core.Enums
{
	/// <summary>
	/// A base class for determing whether an enum value being cast from another type is valid.
	/// </summary>
	/// <typeparam name="TEnum">The type of the enum.</typeparam>
	/// <remarks>
	/// This class provides a more efficient way to validate enum values than <see cref="Enum.IsDefined(Type, object)"/>,
	/// but it only works for enums that are contiguous. <see cref="EnumUtilities.IsContiguousEnum{TEnum}()"/> for
	/// more information on contiguous enums.
	/// </remarks>
	public abstract class AbstractEnumValidator<TEnum>
		where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AbstractEnumValidator{TEnum}"/> class.
		/// </summary>
		public AbstractEnumValidator()
		{
			Contracts.Requires.That(typeof(TEnum).IsEnum);
			Contracts.Requires.That(EnumUtilities.IsContiguousEnum<TEnum>());

			TEnum[] values = EnumUtilities.GetEnumValues<TEnum>();
			this.MinValue = values.Min();
			this.MaxValue = values.Max();
		}

		/// <summary>
		/// Gets the includsive minimum acceptable enum value in the contiguous range of enum values.
		/// </summary>
		/// <value>
		/// The includsive minimum value.
		/// </value>
		protected TEnum MinValue { get; }

		/// <summary>
		/// Gets the includsive maximum acceptable enum value in the contiguous range of enum values.
		/// </summary>
		/// <value>
		/// The includsive maximum value.
		/// </value>
		protected TEnum MaxValue { get; }

		/// <summary>
		/// Determines whether the specified value is a valid enum value.
		/// </summary>
		/// <param name="value">The value to validate.</param>
		/// <returns>True if the value is a valid value for the enum; otherwise false.</returns>
		public abstract bool IsValidEnumValue(TEnum value);

		/// <summary>
		/// Throws an <see cref="InvalidEnumArgumentException"/> if the value is invalid for the enum type.
		/// </summary>
		/// <param name="value">The value to validate.</param>
		/// <exception cref="InvalidEnumArgumentException">If the value is invalid for the enum type.</exception>
		public void ThrowIfInvalidEnumValue(TEnum value)
		{
			if (!this.IsValidEnumValue(value))
			{
				throw InvalidEnumArgument.CreateException(nameof(value), value);
			}
		}
	}
}
