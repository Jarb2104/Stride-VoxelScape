using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Enums
{
	/// <summary>
	/// Provides helper methods for Enum types.
	/// </summary>
	public static class EnumUtilities
	{
		#region Misc Methods

		/// <summary>
		/// Determines whether the specified enum type is convertible to the specified resulting type.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <returns>True if the enum is convertible to the result type, otherwise false.</returns>
		public static bool IsEnumConvertibleTo<TEnum, TResult>()
			where TEnum : struct, IComparable, IFormattable, IConvertible
			where TResult : struct, IComparable, IFormattable, IConvertible, IComparable<TResult>, IEquatable<TResult>
		{
			Contracts.Requires.That(typeof(TEnum).IsEnum);

			return Enum.GetUnderlyingType(typeof(TEnum)).IsImplicitlyConvertibleTo(typeof(TResult));
		}

		/// <summary>
		/// Determines whether the type is a valid underlying type for an enum.
		/// </summary>
		/// <typeparam name="T">The type.</typeparam>
		/// <returns>True if the type is valid for underlying an enum, otherwise false.</returns>
		public static bool IsValidUnderlyingTypeForEnum<T>() => typeof(T).IsValidUnderlyingTypeForEnum();

		/// <summary>
		/// Gets the values of an enum as a type-safe array.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <returns>The values of that enum.</returns>
		/// <remarks>
		/// The values are returned in ascending unsigned binary order. For more information see
		/// <see href="https://msdn.microsoft.com/en-us/library/system.enum.getvalues%28v=vs.110%29.aspx">this link</see>.
		/// </remarks>
		public static TEnum[] GetEnumValues<TEnum>()
			where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Contracts.Requires.That(typeof(TEnum).IsEnum);

			return (TEnum[])Enum.GetValues(typeof(TEnum));
		}

		#endregion

		#region GetContiguousLength

		/// <summary>
		/// Gets the number of unique values in a contiguous enum.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <returns>The number of unique values.</returns>
		/// <seealso cref="IsContiguousEnum{TEnum}()"/>
		public static int GetContiguousEnumLength<TEnum>()
			where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Contracts.Requires.That(typeof(TEnum).IsEnum);
			Contracts.Requires.That(IsEnumConvertibleTo<TEnum, int>());
			Contracts.Requires.That(IsContiguousEnum<TEnum>());

			return GetContiguousEnumLength(EnumConverter.ToInt<TEnum>());
		}

		/// <summary>
		/// Gets the number of unique values in a contiguous enum.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <param name="enumConverter">The delegate used to convert the enum to an integer.</param>
		/// <returns>The number of unique values.</returns>
		/// <seealso cref="IsContiguousEnum{TEnum}()"/>
		public static int GetContiguousEnumLength<TEnum>(Converter<TEnum, int> enumConverter)
			where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Contracts.Requires.That(typeof(TEnum).IsEnum);
			Contracts.Requires.That(enumConverter != null);
			Contracts.Requires.That(IsContiguousEnum(enumConverter));

			return GetContiguousEnumLengthPrivateMethod(enumConverter);
		}

		#endregion

		#region IsContiguous

		/// <summary>
		/// Determines whether the specified enum is contiguous and contains no duplicate values.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <returns>True if the enum is contiguous, otherwise false.</returns>
		/// <remarks>
		/// An enum is contiguous if; it contains only values that convert to non negative integers, has no missing gaps between
		/// zero and the max value contained in the enum, and includes 0. The order in which the values occur does not matter.
		/// </remarks>
		public static bool IsContiguousEnumNoDuplicates<TEnum>()
			where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Contracts.Requires.That(typeof(TEnum).IsEnum);

			if (IsEnumConvertibleTo<TEnum, int>())
			{
				return IsContiguousEnumNoDuplicates(EnumConverter.ToInt<TEnum>());
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Determines whether the specified enum is contiguous and contains no duplicate values.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <param name="enumConverter">The delegate used to convert the enum to an integer.</param>
		/// <returns>True if the enum is contiguous, otherwise false.</returns>
		/// <remarks>
		/// An enum is contiguous if; it contains only values that convert to non negative integers, has no missing gaps between
		/// zero and the max value contained in the enum, and includes 0. The order in which the values occur does not matter.
		/// </remarks>
		public static bool IsContiguousEnumNoDuplicates<TEnum>(Converter<TEnum, int> enumConverter)
			where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			IsContiguousEnumContracts(enumConverter);

			// get values returns in ascending unsigned binary order, so the enum values
			// are effectively sorted already so this algorithm works
			int nextValue = 0;
			foreach (TEnum enumValue in GetEnumValues<TEnum>())
			{
				if (enumConverter(enumValue) != nextValue)
				{
					return false;
				}
				else
				{
					nextValue++;
				}
			}

			// sanity check that if enum is contiguous with no duplicates
			// then it must also be just plain contiguous
			Contracts.Assert.That(IsContiguousEnum(enumConverter));
			return true;
		}

		/// <summary>
		/// Determines whether the specified enum is contiguous.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <returns>True if the enum is contiguous, otherwise false.</returns>
		/// <remarks>
		/// An enum is contiguous if; it contains only values that convert to non negative integers, has no missing gaps between
		/// zero and the max value contained in the enum, and includes 0. The order in which the values occur does not matter.
		/// </remarks>
		public static bool IsContiguousEnum<TEnum>()
			where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Contracts.Requires.That(typeof(TEnum).IsEnum);

			if (IsEnumConvertibleTo<TEnum, int>())
			{
				return IsContiguousEnum(EnumConverter.ToInt<TEnum>());
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Determines whether the specified enum is contiguous.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <param name="enumConverter">The delegate used to convert the enum to an integer.</param>
		/// <returns>True if the enum is contiguous, otherwise false.</returns>
		/// <remarks>
		/// An enum is contiguous if; it contains only values that convert to non negative integers, has no missing gaps between
		/// zero and the max value contained in the enum, and includes 0. The order in which the values occur does not matter.
		/// </remarks>
		public static bool IsContiguousEnum<TEnum>(Converter<TEnum, int> enumConverter)
			where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			IsContiguousEnumContracts(enumConverter);

			int contiguousLength = GetContiguousEnumLengthPrivateMethod(enumConverter);
			if (contiguousLength < 0)
			{
				// Because the private version of GetContiguousLength is used (to avoid recursion) we don't actually
				// know if the enum is contiguous, therefor we guard against nonsensical results from the private method.
				// For example, a negative length could happen if all values in the enum are declared as negatives.
				return false;
			}

			bool[] isMapped = new bool[contiguousLength];
			foreach (TEnum enumValue in GetEnumValues<TEnum>())
			{
				int index = enumConverter(enumValue);
				if (index < 0 || index >= isMapped.Length)
				{
					// if the index isn't within the mapped range then it isn't contiguous
					return false;
				}
				else
				{
					// mark the index as mapped to
					isMapped[index] = true;
				}
			}

			// if every index is mapped to then the enum is contiguous
			return !isMapped.Contains(false);
		}

		/// <summary>
		/// Determines whether the specified enum is contiguous.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <param name="allowDuplicates">If set to <c>true</c> duplicate enum values will be allowed.</param>
		/// <returns>
		/// True if the enum is contiguous, otherwise false.
		/// </returns>
		/// <remarks>
		/// An enum is contiguous if; it contains only values that convert to non negative integers, has no missing gaps between
		/// zero and the max value contained in the enum, and includes 0. The order in which the values occur does not matter.
		/// </remarks>
		public static bool IsContiguousEnum<TEnum>(bool allowDuplicates)
			where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Contracts.Requires.That(typeof(TEnum).IsEnum);

			if (IsEnumConvertibleTo<TEnum, int>())
			{
				return IsContiguousEnum(EnumConverter.ToInt<TEnum>(), allowDuplicates);
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// Determines whether the specified enum is contiguous.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <param name="enumConverter">The delegate used to convert the enum to an integer.</param>
		/// <param name="allowDuplicates">If set to <c>true</c> duplicate enum values will be allowed.</param>
		/// <returns>
		/// True if the enum is contiguous, otherwise false.
		/// </returns>
		/// <remarks>
		/// An enum is contiguous if; it contains only values that convert to non negative integers, has no missing gaps between
		/// zero and the max value contained in the enum, and includes 0. The order in which the values occur does not matter.
		/// </remarks>
		public static bool IsContiguousEnum<TEnum>(Converter<TEnum, int> enumConverter, bool allowDuplicates)
			where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			IsContiguousEnumContracts<TEnum>(enumConverter);

			if (allowDuplicates)
			{
				return IsContiguousEnum(enumConverter);
			}
			else
			{
				return IsContiguousEnumNoDuplicates<TEnum>(enumConverter);
			}
		}

		#endregion

		#region Descriptions

		/// <summary>
		/// Gets the description from an enum value that has a <see cref="System.ComponentModel.DescriptionAttribute" />
		/// or that enum value's ToString if it doesn't have a DescriptionAttribute. This allows you to provide arbitrary
		/// string versions of the values of an enumeration.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <param name="value">The value to get the description from.</param>
		/// <returns>
		/// The description text of the enum value or its ToString value if it doesn't have a DescriptionAttribute.
		/// </returns>
		/// <remarks>
		/// For more information see
		/// <see href="http://blog.spontaneouspublicity.com/associating-strings-with-enums-in-c">this link</see>.
		/// </remarks>
		public static string GetEnumDescription<TEnum>(TEnum value)
			where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Contracts.Requires.That(typeof(TEnum).IsEnum);

			FieldInfo info = value.GetType().GetField(value.ToString());

			DescriptionAttribute[] attributes =
				(DescriptionAttribute[])info.GetCustomAttributes(typeof(DescriptionAttribute), false);

			if (attributes != null && attributes.Length > 0)
			{
				return attributes[0].Description;
			}
			else
			{
				return value.ToString();
			}
		}

		/// <summary>
		/// Gets the descriptions of an enum using the <see cref="GetEnumDescription"/> method.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <returns>An enumerable collection of the enum value's descriptions.</returns>
		public static IEnumerable<string> GetEnumValueDescriptions<TEnum>()
			where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Contracts.Requires.That(typeof(TEnum).IsEnum);

			TEnum[] values = GetEnumValues<TEnum>();
			string[] result = new string[values.Length];

			for (int index = 0; index < values.Length; index++)
			{
				result[index] = GetEnumDescription(values[index]);
			}

			return result;
		}

		#endregion

		#region IsValidEnumValue

		/// <summary>
		/// Determines whether the specified value would convert to a valid enum value.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <param name="value">The value to check.</param>
		/// <returns>True if the value would convert to a valid enum value; otherwise false.</returns>
		/// <remarks>
		/// <para>
		/// This method does not consider combinations of flag enum values.
		/// To include flag enum values use <see cref="IsValidFlagsEnumValue"/> instead.
		/// </para><para>
		/// This method results in boxing and may negatively impact performance critical loops.
		/// </para>
		/// </remarks>
		public static bool IsValidEnumValue<TEnum>(TEnum value)
			where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Contracts.Requires.That(typeof(TEnum).IsEnum);

			// Enum.IsDefined causes boxing anyway so share implementation by passing as object to internal helper method
			return IsValidEnumValueInternalMethod(value);
		}

		/// <summary>
		/// Determines whether the specified value would convert to a valid enum value or a valid combination
		/// of flag enum values. This is slower than just <see cref="IsValidEnumValue"/> so only use if checking
		/// a flags enum.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <param name="value">The value to check.</param>
		/// <returns>True if the value would convert to a valid enum value; otherwise false.</returns>
		/// <remarks>
		/// This method results in boxing and may negatively impact performance critical loops.
		/// </remarks>
		public static bool IsValidFlagsEnumValue<TEnum>(TEnum value)
			where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Contracts.Requires.That(typeof(TEnum).IsEnum);

			// Calling ToString on an enum causes boxing anyway so share
			// implementation by passing as object to internal helper method.
			return IsValidFlagsEnumValueInternalMethod(value);
		}

		#endregion

		#region Helpers

		/// <summary>
		/// Determines whether the specified value would convert to a valid enum value.
		/// </summary>
		/// <param name="value">The value to check.</param>
		/// <returns>True if the value would convert to a valid enum value; otherwise false.</returns>
		/// <remarks>
		/// This is the shared implementation of the method. Because boxing would have occurred anyway given
		/// it's implementation, there's no lose in passing the value is as an object and by doing so this
		/// implementation can be shared by both the TEnum generic method and the Enum extension method.
		/// </remarks>
		internal static bool IsValidEnumValueInternalMethod(object value)
		{
			Contracts.Requires.That(value != null);

			// Enum.IsDefined would cause boxing, but value is alredy passed in as object
			return Enum.IsDefined(value.GetType(), value);
		}

		/// <summary>
		/// Determines whether the specified value would convert to a valid enum value or a valid combination
		/// of flag enum values. This is slower than just <see cref="IsValidEnumValue"/> so only use if checking
		/// a flags enum.
		/// </summary>
		/// <param name="value">The value to check.</param>
		/// <returns>True if the value would convert to a valid enum value; otherwise false.</returns>
		/// <remarks>
		/// This is the shared implementation of the method. Because boxing would have occurred anyway given
		/// it's implementation, there's no lose in passing the value is as an object and by doing so this
		/// implementation can be shared by both the TEnum generic method and the Enum extension method.
		/// </remarks>
		internal static bool IsValidFlagsEnumValueInternalMethod(object value)
		{
			Contracts.Requires.That(value != null);

			// Calling ToString on an enum would cause boxing, but value is alredy passed in as object
			char firstChar = value.ToString()[0];
			return firstChar < '0' || firstChar > '9';

			// for more on this implementation see (scroll down to "The above solutions do not deal with [Flags] situations.")
			// http://stackoverflow.com/questions/2674730/is-there-a-way-to-check-if-int-is-legal-enum-in-c
			// for a less hacky but far more complex implementation see
			// http://www.codeproject.com/Tips/194233/See-if-a-Flags-enum-is-valid
		}

		/// <summary>
		/// Private helper method to actually perform the work of GetContiguousLength.
		/// This is required to avoid infinite recursion when IsContiguous calls this method.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <param name="enumConverter">The delegate used to convert the enum to an integer.</param>
		/// <returns>The number of unique values.</returns>
		private static int GetContiguousEnumLengthPrivateMethod<TEnum>(Converter<TEnum, int> enumConverter)
			where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			TEnum[] values = GetEnumValues<TEnum>();

			if (values.Length == 0)
			{
				return 0;
			}

			// Because a 'contiguous enum' has been defined as starting at 0 and containing no gaps the number
			// of contiguous values will be equal to the maximum value plus 1. Duplicates can be ignored.
			// For example; Enum = 2, 0, 1, 0		ContiguousLength = 3 (max of 2 + 1)
			return values.Max(value => enumConverter(value)) + 1;
		}

		/// <summary>
		/// Provides the contracts for the is contiguous methods.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <param name="enumConverter">The delegate used to convert the enum to an integer.</param>
		[Conditional(Contracts.Requires.CompilationSymbol)]
		private static void IsContiguousEnumContracts<TEnum>(Converter<TEnum, int> enumConverter)
			where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Contracts.Requires.That(typeof(TEnum).IsEnum);
			Contracts.Requires.That(enumConverter != null);
		}

		#endregion
	}
}
