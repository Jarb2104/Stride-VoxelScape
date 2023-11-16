using System;
using System.Reflection;
using Voxelscape.Utility.Common.Core.Reflection;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Conversions
{
	/// <summary>
	/// Providers a non type safe wrapper for delegates that convert types. This can be used for converting types
	/// when the types involved are only known at runtime.
	/// </summary>
	public class NonTypedConverter
	{
		/// <summary>
		/// The delegate used to perform the conversion. Its actual type is only known at runtime.
		/// </summary>
		private readonly object converter;

		/// <summary>
		/// Initializes a new instance of the <see cref="NonTypedConverter"/> class.
		/// </summary>
		/// <param name="converter">The converter to wrap.</param>
		public NonTypedConverter(object converter)
		{
			Contracts.Requires.That(converter != null);
			Contracts.Requires.That(IsTypeConverter(converter));

			Type sourceType = converter.GetType().GetGenericArguments()[0];
			Type resultType = converter.GetType().GetGenericArguments()[1];

			this.ConversionPair = ConversionPair.CreateNew(sourceType, resultType);
			this.converter = converter;
		}

		#region Public Properties

		/// <summary>
		/// Gets the source type to result type conversion pair of this converter.
		/// </summary>
		/// <value>
		/// The conversion pair.
		/// </value>
		public ConversionPair ConversionPair { get; }

		/// <summary>
		/// Gets this converter as a <see cref="Converter{TSource, TResult}"/> delegate.
		/// </summary>
		/// <value>
		/// This converter as a <see cref="Converter{TSource, TResult}"/> delegate.
		/// </value>
		public Converter<object, object> AsTypeConverter => value => this.InvokeConverter(value);

		/// <summary>
		/// Gets this converter as a <see cref="Func{TSource, TResult}"/> delegate.
		/// </summary>
		/// <value>
		/// This converter as a <see cref="Func{TSource, TResult}"/> delegate.
		/// </value>
		public Func<object, object> AsFunction => value => this.InvokeConverter(value);

		#endregion

		#region Equality Operators

		public static bool operator ==(NonTypedConverter lhs, NonTypedConverter rhs) => lhs.EqualsNullSafe(rhs);

		public static bool operator !=(NonTypedConverter lhs, NonTypedConverter rhs) => !lhs.EqualsNullSafe(rhs);

		#endregion

		/// <summary>
		/// Determines whether the specified object is type converter delegate.
		/// </summary>
		/// <param name="converter">The object to check for being a type converter.</param>
		/// <returns>True if the object is a type converter delegate; otherwise false.</returns>
		/// <remarks>
		/// An object is a type converter if it is either a <see cref="Converter{TSource, TResult}"/> or a
		/// <see cref="Func{T, TResult}"/> delegate. The source and result types do not matter.
		/// </remarks>
		public static bool IsTypeConverter(object converter)
		{
			Contracts.Requires.That(converter != null);

			Type converterType = converter.GetType();

			return converterType.IsGenericType && (
				converterType.GetGenericTypeDefinition() == typeof(Func<,>) ||
				converterType.GetGenericTypeDefinition() == typeof(Converter<,>));
		}

		#region Object Overrides

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if (this.EqualsByReferenceNullSafe(obj))
			{
				return true;
			}

			var other = obj as NonTypedConverter;
			if (other == null)
			{
				return false;
			}

			return this.ConversionPair == other.ConversionPair;
		}

		/// <inheritdoc />
		public override int GetHashCode() => this.ConversionPair.GetHashCode();

		/// <inheritdoc />
		public override string ToString() => this.ConversionPair.ToString();

		#endregion

		/// <summary>
		/// Invokes the wrapped converter to attempt converting the value.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <returns>The converted value.</returns>
		private object InvokeConverter(object value)
		{
			Contracts.Requires.That(value == null || value.GetType() == this.ConversionPair.SourceType);

			MethodInfo invokeMethod = this.converter.GetType().GetMethod("Invoke");

			// this utility with unwrap exceptions thrown by the invoked method
			return ReflectionUtilities.InvokeMethod(invokeMethod, this.converter, new object[] { value });
		}
	}
}
