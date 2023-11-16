using System;
using Voxelscape.Utility.Common.Pact.Conversions;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Conversions
{
	/// <summary>
	/// A two way type converter that uses delegates passed into the constructor to perform the type conversions.
	/// </summary>
	/// <typeparam name="TSource">The type of the source.</typeparam>
	/// <typeparam name="TResult">The type of the result.</typeparam>
	public class TwoWayTypeConverter<TSource, TResult> : ITwoWayConverter<TSource, TResult>
	{
		/// <summary>
		/// The converter used to convert from the source type to the result type.
		/// </summary>
		private readonly Converter<TSource, TResult> toResult;

		/// <summary>
		/// The converter used to convert from the result type to the source type.
		/// </summary>
		private readonly Converter<TResult, TSource> toSource;

		/// <summary>
		/// Initializes a new instance of the <see cref="TwoWayTypeConverter{TSource, TResult}"/> class.
		/// </summary>
		/// <param name="toResult">The converter used to convert from the source type to the result type.</param>
		/// <param name="toSource">The converter used to convert from the result type to the source type.</param>
		public TwoWayTypeConverter(
			Converter<TSource, TResult> toResult,
			Converter<TResult, TSource> toSource)
		{
			Contracts.Requires.That(toResult != null);
			Contracts.Requires.That(toSource != null);

			this.toResult = toResult;
			this.toSource = toSource;
		}

		#region ITwoWayTypeConverter<TSource,TResult> Members

		/// <inheritdoc />
		public TResult Convert(TSource value)
		{
			return this.toResult(value);
		}

		/// <inheritdoc />
		public TSource Convert(TResult value)
		{
			return this.toSource(value);
		}

		#endregion
	}
}
