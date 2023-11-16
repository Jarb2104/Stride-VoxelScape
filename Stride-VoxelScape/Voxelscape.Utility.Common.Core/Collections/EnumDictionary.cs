using System;
using System.Collections.Generic;
using Voxelscape.Utility.Common.Core.Enums;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Collections
{
	/// <summary>
	/// Provides utility methods for working with <see cref="IDictionary{TKey, TValue}"/> and
	/// <see cref="IReadOnlyDictionary{TKey, TValue}"/> using enum values as the keys.
	/// </summary>
	public static class EnumDictionary
	{
		/// <summary>
		/// Creates a new dictionary with the specified enum as its keys.
		/// </summary>
		/// <typeparam name="TEnum">The type of the enum used as the key.</typeparam>
		/// <typeparam name="TValue">The type of the value stored in the dictionary.</typeparam>
		/// <returns>A new dictionary.</returns>
		public static Dictionary<TEnum, TValue> Create<TEnum, TValue>()
			where TEnum : struct, IComparable, IFormattable, IConvertible
		{
			Contracts.Requires.That(typeof(TEnum).IsEnum);

			return new Dictionary<TEnum, TValue>(EnumEqualityComparer<TEnum>.Instance);
		}

		public static IEnumerable<TEnum> GetMissingEnumKeys<TEnum, TValue>(IDictionary<TEnum, TValue> dictionary)
			where TEnum : struct, IComparable, IFormattable, IConvertible =>
			ReadOnly.GetMissingEnumKeys(dictionary.AsReadOnlyDictionary());

		public static bool HasKeyForEachEnum<TEnum, TValue>(IDictionary<TEnum, TValue> dictionary)
			where TEnum : struct, IComparable, IFormattable, IConvertible =>
			ReadOnly.HasKeyForEachEnum(dictionary.AsReadOnlyDictionary());

		public static void ThrowIfMissingEnumKey<TEnum, TValue>(IDictionary<TEnum, TValue> dictionary)
			where TEnum : struct, IComparable, IFormattable, IConvertible =>
			ReadOnly.ThrowIfMissingEnumKey(dictionary.AsReadOnlyDictionary());

		public static class ReadOnly
		{
			public static IEnumerable<TEnum> GetMissingEnumKeys<TEnum, TValue>(
				IReadOnlyDictionary<TEnum, TValue> dictionary)
				where TEnum : struct, IComparable, IFormattable, IConvertible
			{
				Contracts.Requires.That(typeof(TEnum).IsEnum);
				Contracts.Requires.That(dictionary != null);

				IList<TEnum> result = new List<TEnum>();
				foreach (TEnum enumValue in EnumUtilities.GetEnumValues<TEnum>())
				{
					if (!dictionary.ContainsKey(enumValue))
					{
						result.Add(enumValue);
					}
				}

				return result;
			}

			public static bool HasKeyForEachEnum<TEnum, TValue>(IReadOnlyDictionary<TEnum, TValue> dictionary)
				where TEnum : struct, IComparable, IFormattable, IConvertible
			{
				Contracts.Requires.That(typeof(TEnum).IsEnum);
				Contracts.Requires.That(dictionary != null);

				return GetMissingEnumKeys(dictionary).IsEmpty();
			}

			public static void ThrowIfMissingEnumKey<TEnum, TValue>(IReadOnlyDictionary<TEnum, TValue> dictionary)
				where TEnum : struct, IComparable, IFormattable, IConvertible
			{
				Contracts.Requires.That(typeof(TEnum).IsEnum);
				Contracts.Requires.That(dictionary != null);

				IEnumerable<TEnum> missingEnums = GetMissingEnumKeys(dictionary);
				if (!missingEnums.IsEmpty())
				{
					throw new MissingEnumException<TEnum>(missingEnums);
				}
			}
		}
	}
}