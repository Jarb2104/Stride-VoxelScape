using System;
using Voxelscape.Common.Indexing.Core.Enums;
using Voxelscape.Common.Indexing.Pact.Indices;
using Voxelscape.Utility.Common.Core.Mathematics;
using Voxelscape.Utility.Common.Core.Types;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Exceptions;
using Voxelscape.Utility.Common.Pact.Mathematics;
using Range = Voxelscape.Utility.Common.Core.Mathematics.Range;

namespace Voxelscape.Common.Indexing.Core.Indices
{
	/// <summary>
	/// A three dimensional integer index for accessing arrays or grid based structures.
	/// </summary>
	public struct Index1D : IIndex, IEquatable<Index1D>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Index1D"/> struct.
		/// </summary>
		/// <param name="x">The x index.</param>
		public Index1D(int x)
		{
			this.X = x;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Index1D" /> struct.
		/// </summary>
		/// <param name="index">The index whose coordinates to copy.</param>
		public Index1D(Index1D index)
		{
			this.X = index.X;
		}

		#endregion

		#region Singleton Properties

		/// <summary>
		/// Gets the zero index.
		/// </summary>
		/// <value>
		/// The zero index.
		/// </value>
		public static Index1D Zero => new Index1D(0);

		/// <summary>
		/// Gets the minimum index.
		/// </summary>
		/// <value>
		/// The minimum index.
		/// </value>
		public static Index1D MinValue => new Index1D(int.MinValue);

		/// <summary>
		/// Gets the maximum index.
		/// </summary>
		/// <value>
		/// The maximum index.
		/// </value>
		public static Index1D MaxValue => new Index1D(int.MaxValue);

		#endregion

		#region Coordinate Properties

		/// <summary>
		/// Gets the X coordinate.
		/// </summary>
		/// <value>
		/// The X coordinate.
		/// </value>
		public int X { get; }

		#endregion

		#region IIndex Members

		/// <inheritdoc />
		public int Rank => 1;

		/// <inheritdoc />
		public int this[int dimension]
		{
			get
			{
				IIndexContracts.Indexer(this, dimension);

				switch (dimension)
				{
					case (int)Axis1D.X: return this.X;
					default:
						throw new UnreachableCodeException(IndexConstants.UnreachableCodeExceptionJustificationMessage);
				}
			}
		}

		#endregion

		/// <summary>
		/// Gets the coordinate value or index of a particular dimension.
		/// </summary>
		/// <param name="axis">The axis of the dimension whose coordinate needs to be determined.</param>
		/// <returns>The value of the dimensional coordinate.</returns>
		public int this[Axis1D axis] => this[(int)axis];

		#region Unary Operators

		public static Index1D operator +(Index1D value) => value;

		public static Index1D operator -(Index1D value) => new Index1D(-value.X);

		#endregion

		#region Binary Operators

		public static Index1D operator +(Index1D lhs, Index1D rhs) => new Index1D(lhs.X + rhs.X);

		public static Index1D operator -(Index1D lhs, Index1D rhs) => new Index1D(lhs.X - rhs.X);

		public static Index1D operator *(Index1D lhs, Index1D rhs) => new Index1D(lhs.X * rhs.X);

		public static Index1D operator *(Index1D index, int scalar) => new Index1D(index.X * scalar);

		public static Index1D operator /(Index1D lhs, Index1D rhs) => new Index1D(lhs.X / rhs.X);

		public static Index1D operator /(Index1D index, int scalar) => new Index1D(index.X / scalar);

		public static Index1D operator %(Index1D lhs, Index1D rhs) => new Index1D(lhs.X % rhs.X);

		public static Index1D operator <<(Index1D index, int shift) => new Index1D(index.X << shift);

		public static Index1D operator >>(Index1D index, int shift) => new Index1D(index.X >> shift);

		#endregion

		#region Equality Operators

		public static bool operator ==(Index1D lhs, Index1D rhs) => lhs.Equals(rhs);

		public static bool operator !=(Index1D lhs, Index1D rhs) => !lhs.Equals(rhs);

		#endregion

		#region IEquatable<Index1D> Members

		/// <inheritdoc />
		public bool Equals(Index1D other) => this.X == other.X;

		#endregion

		#region Object Overrides

		/// <inheritdoc />
		public override bool Equals(object obj) => StructUtilities.Equals(this, obj);

		/// <inheritdoc />
		public override int GetHashCode() => this.X.GetHashCode();

		/// <inheritdoc />
		public override string ToString() => this.X.ToString();

		#endregion

		#region Additional Methods

		/// <summary>
		/// Determines whether this index is within the specified range of indices.
		/// </summary>
		/// <param name="min">The minimum index.</param>
		/// <param name="max">The maximum index.</param>
		/// <param name="bounds">The inclusive/exclusive range options.</param>
		/// <returns>True if the value is within range, false otherwise.</returns>
		public bool IsIn(Index1D min, Index1D max, RangeClusivity bounds = RangeClusivity.Inclusive) =>
			this.X.IsIn(Range.New(min.X, max.X, bounds));

		/// <summary>
		/// Gets which orthant this index is located in with respect to the specified origin index.
		/// </summary>
		/// <param name="origin">The origin index.</param>
		/// <returns>The orthant of this index with respect to the origin index.</returns>
		public Orthant1D GetOrthant(Index1D origin = default(Index1D)) =>
			(Orthant1D)InternalIndexUtilities.GetOrthantOfAxis(this.X, origin.X, (int)Axis1D.X);

		/// <summary>
		/// Determines whether all coordinate values of this index are positive.
		/// </summary>
		/// <returns>True if all coordinates are positive, otherwise false.</returns>
		public bool IsAllPositive() => this.GetOrthant().IsAllGreater();

		/// <summary>
		/// Determines whether all coordinate values of this index are positive or zero.
		/// </summary>
		/// <returns>True if all coordinates are positive or zero, otherwise false.</returns>
		public bool IsAllPositiveOrZero() => this.GetOrthant().IsAllGreaterOrEqual();

		/// <summary>
		/// Determines whether all coordinate values of this index are negative.
		/// </summary>
		/// <returns>True if all coordinates are negative, otherwise false.</returns>
		public bool IsAllNegative() => this.GetOrthant().IsAllLess();

		/// <summary>
		/// Determines whether all coordinate values of this index are negative or zero.
		/// </summary>
		/// <returns>True if all coordinates are negative or zero, otherwise false.</returns>
		public bool IsAllNegativeOrZero() => this.GetOrthant().IsAllLessOrEqual();

		/// <summary>
		/// Sums all the coordinates of this index.
		/// </summary>
		/// <returns>The sum of all the coordinates of the index.</returns>
		public int SumCoordinates() => this.X;

		/// <summary>
		/// Sums all the coordinates of this index as a long value.
		/// </summary>
		/// <returns>The long sum of all the coordinates of the index.</returns>
		public long SumCoordinatesLong() => this.X;

		/// <summary>
		/// Multiplies all the coordinates of this index.
		/// </summary>
		/// <returns>The result of multiplying all the coordinates of the index.</returns>
		public int MultiplyCoordinates() => this.X;

		/// <summary>
		/// Multiplies all the coordinates of this index as a long value.
		/// </summary>
		/// <returns>The long result of multiplying all the coordinates of the index.</returns>
		public long MultiplyCoordinatesLong() => this.X;

		/// <summary>
		/// Divides this index by the divisor, rounding up to the next integral value instead of truncating.
		/// </summary>
		/// <param name="divisor">The divisor.</param>
		/// <returns>The result of the division, rounded up.</returns>
		public Index1D DivideByRoundUp(Index1D divisor) => new Index1D(this.X.DivideByRoundUp(divisor.X));

		/// <summary>
		/// Creates a new array whose dimensions are taken from this index.
		/// </summary>
		/// <typeparam name="T">The type of the value of the array.</typeparam>
		/// <returns>The new array.</returns>
		public T[] CreateArray<T>()
		{
			Contracts.Requires.That(this.IsAllPositiveOrZero());

			return new T[this.X];
		}

		#endregion
	}
}
