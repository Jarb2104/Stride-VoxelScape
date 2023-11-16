using System;
using System.Collections.Generic;
using Voxelscape.Utility.Common.Core.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;

namespace Voxelscape.Utility.Common.Core.Conversions
{
	/// <summary>
	/// A wrapper around a <see cref="MultiTypeConverter"/> for pooling the results of previous conversions and
	/// returning those values if the same conversion is requested later.
	/// </summary>
	public class PooledValueConverter
	{
		/// <summary>
		/// The pools of converted values.
		/// </summary>
		/// <remarks>
		/// Each source to result type pair has its own pool of converted values. The pools themselves are
		/// <see cref="DefaultDictionary{TKey, TValue}"/> where the first object (the key) is the value to convert and
		/// the second object (the value) is the converted value. When a value is requested from the DefaultDictionary
		/// that is doesn't have, it will invoke the type converter to converted the value and store the result. If a value
		/// is requested that it does have, then it just returns the stored value. This way each conversion is performed
		/// only once and object identity of the results are maintained (that is that the exact same value is returned for the
		/// same input every time).
		/// </remarks>
		private readonly Dictionary<ConversionPair, DefaultDictionary<object, object>> valuePools =
			new Dictionary<ConversionPair, DefaultDictionary<object, object>>();

		/// <summary>
		/// Initializes a new instance of the <see cref="PooledValueConverter"/> class.
		/// </summary>
		/// <param name="valueConverters">The collection of value converters to use for conversions.</param>
		public PooledValueConverter(MultiTypeConverter valueConverters)
		{
			Contracts.Requires.That(valueConverters != null);

			foreach (KeyValuePair<ConversionPair, NonTypedConverter> entry in valueConverters)
			{
				// see remarks on the valuePools about how the use of DefaultDictionary works here
				this.valuePools.Add(
					entry.Key,
					new DefaultDictionary<object, object>(entry.Value.AsFunction));
			}
		}

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
			DefaultDictionary<object, object> valuePool;
			if (this.valuePools.TryGetValue(ConversionPair.CreateNew<TSource, TResult>(), out valuePool))
			{
				try
				{
					result = (TResult)valuePool[value];
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

			DefaultDictionary<object, object> valuePool;
			if (this.valuePools.TryGetValue(conversionPair, out valuePool))
			{
				try
				{
					result = valuePool[value];
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

		/// <summary>
		/// Clears this instance of all pooled values.
		/// </summary>
		public void ClearValues()
		{
			foreach (DefaultDictionary<object, object> valuePool in this.valuePools.Values)
			{
				valuePool.Clear();
			}
		}

		/// <summary>
		/// Clears this instance of all pooled values, disposing them if they are disposable.
		/// </summary>
		public void ClearAndDisposeValues()
		{
			foreach (DefaultDictionary<object, object> valuePool in this.valuePools.Values)
			{
				foreach (object value in valuePool.Values)
				{
					Disposable.DisposeObjectIfAble(value);
				}

				valuePool.Clear();
			}
		}
	}
}
