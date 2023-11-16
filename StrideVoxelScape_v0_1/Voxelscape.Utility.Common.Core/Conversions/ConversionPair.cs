using System;
using Voxelscape.Utility.Common.Core.Hashing;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using HashCode = Voxelscape.Utility.Common.Core.Hashing.HashCode;

namespace Voxelscape.Utility.Common.Core.Conversions
{
	/// <summary>
	/// Represents a conversion from a source type to a result type.
	/// </summary>
	public class ConversionPair
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ConversionPair"/> class.
		/// </summary>
		/// <param name="sourceType">Type of the source.</param>
		/// <param name="resultType">Type of the result.</param>
		public ConversionPair(Type sourceType, Type resultType)
		{
			Contracts.Requires.That(sourceType != null);
			Contracts.Requires.That(resultType != null);

			this.SourceType = sourceType;
			this.ResultType = resultType;
		}

		#region Properties

		/// <summary>
		/// Gets the source type of the conversion.
		/// </summary>
		/// <value>
		/// The type of the source.
		/// </value>
		public Type SourceType { get; }

		/// <summary>
		/// Gets the result type of the conversion.
		/// </summary>
		/// <value>
		/// The type of the result.
		/// </value>
		public Type ResultType { get; }

		#endregion

		#region Equality Operators

		public static bool operator ==(ConversionPair lhs, ConversionPair rhs) => lhs.EqualsNullSafe(rhs);

		public static bool operator !=(ConversionPair lhs, ConversionPair rhs) => !lhs.EqualsNullSafe(rhs);

		#endregion

		#region Static Factory Methods

		/// <summary>
		/// Creates a new instance of the <see cref="ConversionPair"/> class.
		/// </summary>
		/// <typeparam name="TSource">The type of the source.</typeparam>
		/// <typeparam name="TResult">The type of the result.</typeparam>
		/// <returns>The new instance.</returns>
		public static ConversionPair CreateNew<TSource, TResult>() => new ConversionPair(typeof(TSource), typeof(TResult));

		/// <summary>
		/// Creates a new instance of the <see cref="ConversionPair"/> class.
		/// </summary>
		/// <param name="sourceType">Type of the source.</param>
		/// <param name="resultType">Type of the result.</param>
		/// <returns>The new instance.</returns>
		public static ConversionPair CreateNew(Type sourceType, Type resultType) => new ConversionPair(sourceType, resultType);

		#endregion

		#region Object Overrides

		/// <inheritdoc />
		public override bool Equals(object obj)
		{
			if (this.EqualsByReferenceNullSafe(obj))
			{
				return true;
			}

			var other = obj as ConversionPair;
			if (other == null)
			{
				return false;
			}

			// no need to use null safe equals because this class
			// doesn't allow the types to be null
			return this.SourceType.Equals(other.SourceType)
				&& this.ResultType.Equals(other.ResultType);
		}

		/// <inheritdoc />
		public override int GetHashCode() => HashCode.Start.And(this.SourceType).And(this.ResultType);

		/// <inheritdoc />
		public override string ToString() => $"({this.SourceType} converting to {this.ResultType})";

		#endregion
	}
}
