using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Collections
{
	/// <summary>
	/// A fast and efficient implementation of <see cref="IEqualityComparer{T}"/> for Enum types.
	/// Useful for dictionaries that use Enums as their keys.
	/// </summary>
	/// <typeparam name="TEnum">The type of the enum.</typeparam>
	/// <seealso href="http://www.codeproject.com/Articles/33528/Accelerating-Enum-Based-Dictionaries-with-Generic"/>
	public class EnumEqualityComparer<TEnum> : EqualityComparer<TEnum>
		where TEnum : struct, IComparable, IFormattable, IConvertible
	{
		/// <summary>
		/// The generated equals method for the enum type.
		/// </summary>
		private readonly Func<TEnum, TEnum, bool> equals;

		/// <summary>
		/// The generated get hash code method for the enum type.
		/// </summary>
		private readonly Func<TEnum, int> getHashCode;

		/// <summary>
		/// Prevents a default instance of the <see cref="EnumEqualityComparer{TEnum}"/> class from being created.
		/// </summary>
		private EnumEqualityComparer()
		{
			Contracts.Requires.That(typeof(TEnum).IsEnum);

			this.getHashCode = GenerateGetHashCode();
			this.equals = GenerateEquals();
		}

		/// <summary>
		/// Gets the singleton instance of the <see cref="EnumEqualityComparer{TEnum}"/> comparer.
		/// </summary>
		/// <value>
		/// The singleton comparer instance.
		/// </value>
		public static EnumEqualityComparer<TEnum> Instance { get; } = new EnumEqualityComparer<TEnum>();

		#region Overriding EqualityComparer<TEnum> Members

		/// <inheritdoc />
		public override bool Equals(TEnum x, TEnum y)
		{
			return this.equals(x, y);
		}

		/// <inheritdoc />
		public override int GetHashCode(TEnum obj)
		{
			IEqualityComparerContracts.GetHashCode(obj);

			return this.getHashCode(obj);
		}

		#endregion

		/// <summary>
		/// Generates the equality comparison method.
		/// </summary>
		/// <returns>The generated method.</returns>
		/// <remarks>
		/// The code generated is similar to the following.
		/// <code>
		/// public override bool Equals(TEnum x, TEnum y) => x == y;
		/// </code>
		/// </remarks>
		private static Func<TEnum, TEnum, bool> GenerateEquals()
		{
			var xParam = Expression.Parameter(typeof(TEnum), "x");
			var yParam = Expression.Parameter(typeof(TEnum), "y");
			var equalExpression = Expression.Equal(xParam, yParam);
			return Expression.Lambda<Func<TEnum, TEnum, bool>>(equalExpression, new[] { xParam, yParam }).Compile();
		}

		/// <summary>
		/// Generates the GetHashCode method.
		/// </summary>
		/// <returns>The generated method.</returns>
		/// <remarks>
		/// The code generated is similar to the following.
		/// <code>
		/// public override int GetHashCode(TEnum obj) => ((int)obj).GetHashCode();
		/// </code>
		/// Except that (int) will be replaced by whatever the underlying type of TEnum is.
		/// </remarks>
		private static Func<TEnum, int> GenerateGetHashCode()
		{
			var objParam = Expression.Parameter(typeof(TEnum), "obj");
			var underlyingType = Enum.GetUnderlyingType(typeof(TEnum));
			var convertExpression = Expression.Convert(objParam, underlyingType);
			var getHashCodeMethod = underlyingType.GetMethod("GetHashCode");
			var getHashCodeExpression = Expression.Call(convertExpression, getHashCodeMethod);
			return Expression.Lambda<Func<TEnum, int>>(getHashCodeExpression, new[] { objParam }).Compile();
		}
	}
}
