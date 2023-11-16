using System;
using System.Diagnostics;
using System.Linq.Expressions;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Enums
{
	/// <summary>
	/// Provides <see cref="Converter{TSource, TResult}"/> delegates that convert enums
	/// to their underlying primitive types.
	/// </summary>
	public static class EnumConverter
	{
		#region Converter Methods

		/// <summary>
		/// Gets a type converter for an enum backed by an underlying byte.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <returns>The type converter for the enum.</returns>
		public static Converter<TEnum, byte> ToByte<TEnum>()
			where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			ConverterContracts<TEnum, byte>();

			return GeneratedConverter<TEnum, byte>.Instance;
		}

		/// <summary>
		/// Gets a type converter for an enum backed by an underlying sbyte.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <returns>The type converter for the enum.</returns>
		public static Converter<TEnum, sbyte> ToSByte<TEnum>()
			where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			ConverterContracts<TEnum, sbyte>();

			return GeneratedConverter<TEnum, sbyte>.Instance;
		}

		/// <summary>
		/// Gets a type converter for an enum backed by an underlying short.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <returns>The type converter for the enum.</returns>
		public static Converter<TEnum, short> ToShort<TEnum>()
			where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			ConverterContracts<TEnum, short>();

			return GeneratedConverter<TEnum, short>.Instance;
		}

		/// <summary>
		/// Gets a type converter for an enum backed by an underlying ushort.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <returns>The type converter for the enum.</returns>
		public static Converter<TEnum, ushort> ToUShort<TEnum>()
			where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			ConverterContracts<TEnum, ushort>();

			return GeneratedConverter<TEnum, ushort>.Instance;
		}

		/// <summary>
		/// Gets a type converter for an enum backed by an underlying int.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <returns>The type converter for the enum.</returns>
		public static Converter<TEnum, int> ToInt<TEnum>()
			where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			ConverterContracts<TEnum, int>();

			return GeneratedConverter<TEnum, int>.Instance;
		}

		/// <summary>
		/// Gets a type converter for an enum backed by an underlying uint.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <returns>The type converter for the enum.</returns>
		public static Converter<TEnum, uint> ToUInt<TEnum>()
			where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			ConverterContracts<TEnum, uint>();

			return GeneratedConverter<TEnum, uint>.Instance;
		}

		/// <summary>
		/// Gets a type converter for an enum backed by an underlying long.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <returns>The type converter for the enum.</returns>
		public static Converter<TEnum, long> ToLong<TEnum>()
			where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			ConverterContracts<TEnum, long>();

			return GeneratedConverter<TEnum, long>.Instance;
		}

		/// <summary>
		/// Gets a type converter for an enum backed by an underlying ulong.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <returns>The type converter for the enum.</returns>
		public static Converter<TEnum, ulong> ToULong<TEnum>()
			where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			ConverterContracts<TEnum, ulong>();

			return GeneratedConverter<TEnum, ulong>.Instance;
		}

		/// <summary>
		/// Provides the contracts for the enum converter methods.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <typeparam name="TResult">The underlying type to convert the enum to.</typeparam>
		[Conditional(Contracts.Requires.CompilationSymbol)]
		private static void ConverterContracts<TEnum, TResult>()
			where TEnum : struct, IComparable, IFormattable, IConvertible
			where TResult : struct, IComparable, IFormattable, IConvertible, IComparable<TResult>, IEquatable<TResult>
		{
			Contracts.Requires.That(typeof(TEnum).IsEnum);
			Contracts.Requires.That(EnumUtilities.IsValidUnderlyingTypeForEnum<TResult>());
			Contracts.Requires.That(EnumUtilities.IsEnumConvertibleTo<TEnum, TResult>());
		}

		#endregion

		#region Private Nested Converter Classes

		/// <summary>
		/// Provides singleton instances of <see cref="Converter{TSource, TResult}"/> delegates
		/// that convert enums to their underlying primitive types.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum.</typeparam>
		/// <typeparam name="TResult">The underlying type to convert the enum to.</typeparam>
		private class GeneratedConverter<TEnum, TResult>
			where TEnum : struct, IComparable, IFormattable, IConvertible
			where TResult : struct, IComparable, IFormattable, IConvertible, IComparable<TResult>, IEquatable<TResult>
		{
			/// <summary>
			/// The singleton delegate instance used for converting enums to their underlying types.
			/// </summary>
			public static readonly Converter<TEnum, TResult> Instance = GenerateConverter();

			/// <summary>
			/// Generates a <see cref="Converter{TSource, TResult}" /> that will cast generic enum values
			/// to their underlying primitive values.
			/// </summary>
			/// <returns>
			/// The type converter delegate.
			/// </returns>
			/// <remarks>
			/// The code generated is similar to the following.
			/// <code>
			/// int Convert(TEnum value) => (int)value;
			/// </code>
			/// Except that int will be replaced by whatever the underlying type of TEnum is.
			/// </remarks>
			private static Converter<TEnum, TResult> GenerateConverter()
			{
				ConverterContracts<TEnum, TResult>();

				var valueParameter = Expression.Parameter(typeof(TEnum), "value");
				var convertExpression = Expression.Convert(valueParameter, typeof(TResult));
				return Expression.Lambda<Converter<TEnum, TResult>>(convertExpression, new[] { valueParameter }).Compile();
			}
		}

		#endregion
	}
}
