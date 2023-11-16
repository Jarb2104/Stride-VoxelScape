using System;
using Voxelscape.Utility.Common.Core.Mathematics;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Types;
using Range = Voxelscape.Utility.Common.Core.Mathematics.Range;

/// <summary>
/// Provides extension methods for converting types.
/// </summary>
public static class CommonConversionExtensions
{
	/// <summary>
	/// Determines whether the specified source type is implicitly convertible to the specified result type.
	/// </summary>
	/// <param name="source">The source type.</param>
	/// <param name="result">The result type.</param>
	/// <returns>
	/// True if the source type can be implicitly converted to the result type, otherwise false.
	/// </returns>
	public static bool IsImplicitlyConvertibleTo(this Type source, Type result)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(result != null);

		if (source.IsConvertibleToPrimitiveType() && result.IsConvertibleToPrimitiveType())
		{
			return source.ToPrimitiveType().IsImplicitlyConvertibleTo(result.ToPrimitiveType());
		}
		else
		{
			return false;
		}
	}

	/// <summary>
	/// Determines whether the specified source type is implicitly convertible to the specified result type.
	/// </summary>
	/// <param name="sourceType">Type of the source.</param>
	/// <param name="resultType">Type of the result.</param>
	/// <returns>True if the source type can be implicitly converted to the result type, otherwise false.</returns>
	public static bool IsImplicitlyConvertibleTo(this PrimitiveType sourceType, PrimitiveType resultType)
	{
		// all types can implicitly convert to themselves
		if (sourceType == resultType)
		{
			return true;
		}

		// special handling for non-integral source types; bool, decimal, char, float, double
		switch (sourceType)
		{
			// bool and decimal don't implicitly convert to any other type
			case PrimitiveType.Bool:
			case PrimitiveType.Decimal:
				return false;

			// char implicitly converts to anything large enough to hold a ushort
			case PrimitiveType.Char:
				return resultType != PrimitiveType.Bool
					&& resultType != PrimitiveType.Byte
					&& resultType != PrimitiveType.SByte
					&& resultType != PrimitiveType.Short;

			// float and double implicitly convert to double
			case PrimitiveType.Float:
			case PrimitiveType.Double:
				return resultType == PrimitiveType.Double;
		}

		// source types beyond this point are integral only; byte, sbyte, short, ushort, int, uint, long, ulong

		// special handling for the non-integral result types
		switch (resultType)
		{
			// all remaining types don't implicitly convert to a bool or char
			case PrimitiveType.Bool:
			case PrimitiveType.Char:
				return false;

			// all remaining types implicitly convert to float, double, or decimal
			case PrimitiveType.Float:
			case PrimitiveType.Double:
			case PrimitiveType.Decimal: // decimal can be handle by the range function below, but handling it here is faster
				return true;
		}

		// for all the remaining integral types, the type implicitly converts if the
		// resulting type can contain the range of the source type
		return Range.New(resultType.MinValue(), resultType.MaxValue())
			.Contains(Range.New(sourceType.MinValue(), sourceType.MaxValue()));
	}
}
