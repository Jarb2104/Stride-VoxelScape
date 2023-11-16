using System;
using System.Collections;
using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Conversions
{
	/// <summary>
	/// A collection of delegates that can be used to convert from various source type to various result types
	/// which are only known at runtime.
	/// </summary>
	public class MultiTypeConverter : IEnumerable<KeyValuePair<ConversionPair, NonTypedConverter>>
	{
		/// <summary>
		/// The type converter delegates mapped to the source to result type pairs of values they convert.
		/// </summary>
		private readonly IDictionary<ConversionPair, NonTypedConverter> converters =
			new Dictionary<ConversionPair, NonTypedConverter>();

		#region Add

		/// <summary>
		/// Adds the type converter delegate to this collection if this collection doesn't
		/// already contain a converter for the source type to result type pairing.
		/// </summary>
		/// <typeparam name="TSource">The type of the source.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="converter">The converter to add.</param>
		/// <returns>True if the converter was added; otherwise false.</returns>
		public bool AddConverter<TSource, TResult>(Converter<TSource, TResult> converter)
		{
			Contracts.Requires.That(converter != null);

			return this.AddNonTypedConverter(converter);
		}

		/// <summary>
		/// Adds the function delegate to this collection if this collection doesn't
		/// already contain a converter for the source type to result type pairing.
		/// </summary>
		/// <typeparam name="TSource">The type of the source.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="converter">The converter to add.</param>
		/// <returns>True if the function was added; otherwise false.</returns>
		public bool AddFunction<TSource, TResult>(Func<TSource, TResult> converter)
		{
			Contracts.Requires.That(converter != null);

			return this.AddNonTypedConverter(converter);
		}

		/// <summary>
		/// Adds the not type safe type converter delegate to this collection if this collection doesn't
		/// already contain a converter for the source type to result type pairing.
		/// </summary>
		/// <param name="converter">The converter to add.</param>
		/// <returns>True if the converter was added; otherwise false.</returns>
		public bool AddNonTypedConverter(object converter)
		{
			Contracts.Requires.That(converter != null);
			Contracts.Requires.That(NonTypedConverter.IsTypeConverter(converter));

			return this.AddNonTypedConverter(new NonTypedConverter(converter));
		}

		/// <summary>
		/// Adds the <see cref="NonTypedConverter"/> to this collection if this collection doesn't
		/// already contain a converter for the source type to result type pairing.
		/// </summary>
		/// <param name="converter">The converter to add.</param>
		/// <returns>True if the converter was added; otherwise false.</returns>
		public bool AddNonTypedConverter(NonTypedConverter converter)
		{
			Contracts.Requires.That(converter != null);

			return this.converters.AddIfNewKey(converter.ConversionPair, converter);
		}

		#endregion

		#region Contains

		/// <summary>
		/// Determines whether this instance contains a converter delegate for converting from the source type to the result type.
		/// </summary>
		/// <typeparam name="TSource">The type of the source.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <returns>True if this instance contains an appropriate converter delegate; otherwise false.</returns>
		public bool ContainsConverter<TSource, TResult>()
		{
			return this.ContainsConverter(ConversionPair.CreateNew<TSource, TResult>());
		}

		/// <summary>
		/// Determines whether this instance contains a converter delegate matching the <see cref="ConversionPair"/>.
		/// </summary>
		/// <param name="conversionPair">The conversion pair to check for a matching converter delegate for.</param>
		/// <returns>True if this instance contains an appropriate converter delegate; otherwise false.</returns>
		public bool ContainsConverter(ConversionPair conversionPair)
		{
			Contracts.Requires.That(conversionPair != null);

			return this.converters.ContainsKey(conversionPair);
		}

		#endregion

		#region TryGetConverter

		/// <summary>
		/// Tries to get a <see cref="NonTypedConverter"/> from this collection matching the source and result types.
		/// </summary>
		/// <typeparam name="TSource">The type of the source.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="converter">The type converter if one was found; otherwise null.</param>
		/// <returns>True if an appropriate converter was found; otherwise false.</returns>
		public bool TryGetConverter<TSource, TResult>(out NonTypedConverter converter)
		{
			return this.TryGetConverter(ConversionPair.CreateNew<TSource, TResult>(), out converter);
		}

		/// <summary>
		/// Tries to get a <see cref="NonTypedConverter"/> from this collection matching the source and result types.
		/// </summary>
		/// <param name="conversionPair">The conversion pair to find a matching converter for.</param>
		/// <param name="converter">The type converter if one was found; otherwise null.</param>
		/// <returns>True if an appropriate converter was found; otherwise false.</returns>
		public bool TryGetConverter(ConversionPair conversionPair, out NonTypedConverter converter)
		{
			Contracts.Requires.That(conversionPair != null);

			return this.converters.TryGetValue(conversionPair, out converter);
		}

		#endregion

		#region TryConvert

		/// <summary>
		/// Tries to convert convert the value from the source type to the result type.
		/// </summary>
		/// <typeparam name="TSource">The type of the source.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <param name="value">The value to convert.</param>
		/// <param name="result">The result if the conversion was successful; otherwise the default of the result type.</param>
		/// <param name="exception">The exception if one was thrown during the conversion; otherwise null.</param>
		/// <returns>An enumeration value indicating the outcome of the conversion attempt.</returns>
		public ConversionStatus TryConvert<TSource, TResult>(TSource value, out TResult result, out Exception exception)
		{
			NonTypedConverter converter;
			if (this.TryGetConverter<TSource, TResult>(out converter))
			{
				try
				{
					result = (TResult)converter.AsTypeConverter(value);
					exception = null;
					return ConversionStatus.Success;
				}
				catch (Exception conversionException)
				{
					result = default(TResult);
					exception = conversionException;
					return ConversionStatus.Exception;
				}
			}
			else
			{
				result = default(TResult);
				exception = null;
				return ConversionStatus.NoConverterFound;
			}
		}

		/// <summary>
		/// Tries to convert convert the value from the source type to the result type.
		/// </summary>
		/// <param name="conversionPair">The conversion pair representing the source and result types to try converting.</param>
		/// <param name="value">The value to convert.</param>
		/// <param name="result">The result if the conversion was successful; otherwise null.</param>
		/// <param name="exception">The exception if one was thrown during the conversion; otherwise null.</param>
		/// <returns>An enumeration value indicating the outcome of the conversion attempt.</returns>
		public ConversionStatus TryConvert(ConversionPair conversionPair, object value, out object result, out Exception exception)
		{
			Contracts.Requires.That(conversionPair != null);
			Contracts.Requires.That(value != null ? value.GetType() == conversionPair.SourceType : true);

			NonTypedConverter converter;
			if (this.TryGetConverter(conversionPair, out converter))
			{
				try
				{
					result = converter.AsTypeConverter(value);
					exception = null;
					return ConversionStatus.Success;
				}
				catch (Exception conversionException)
				{
					result = null;
					exception = conversionException;
					return ConversionStatus.Exception;
				}
			}
			else
			{
				result = null;
				exception = null;
				return ConversionStatus.NoConverterFound;
			}
		}

		#endregion

		#region IEnumerable<KeyValuePair<ConversionPair, NonTypedConverter>> Members

		/// <inheritdoc />
		public IEnumerator<KeyValuePair<ConversionPair, NonTypedConverter>> GetEnumerator()
		{
			return this.converters.GetEnumerator();
		}

		#endregion

		#region IEnumerable Members

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		#endregion
	}
}
