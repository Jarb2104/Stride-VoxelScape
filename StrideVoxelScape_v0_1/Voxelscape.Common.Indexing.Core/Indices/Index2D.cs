using System;
using Voxelscape.Common.Indexing.Core.Enums;
using Voxelscape.Common.Indexing.Pact.Indices;
using Voxelscape.Utility.Common.Core.Hashing;
using Voxelscape.Utility.Common.Core.Mathematics;
using Voxelscape.Utility.Common.Core.Types;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Exceptions;
using HashCode = Voxelscape.Utility.Common.Core.Hashing.HashCode;
using Range = Voxelscape.Utility.Common.Core.Mathematics.Range;

namespace Voxelscape.Common.Indexing.Core.Indices
{
	/// <summary>
	/// A three dimensional integer index for accessing arrays or grid based structures.
	/// </summary>
	public struct Index2D : IIndex, IEquatable<Index2D>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Index2D"/> struct.
		/// </summary>
		/// <param name="number">The number to assign to each index.</param>
		public Index2D(int number)
		{
			this.X = number;
			this.Y = number;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Index2D"/> struct.
		/// </summary>
		/// <param name="x">The x index.</param>
		/// <param name="y">The y index.</param>
		public Index2D(int x, int y)
		{
			this.X = x;
			this.Y = y;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Index2D" /> struct.
		/// </summary>
		/// <param name="index">The index whose coordinates to copy.</param>
		public Index2D(Index2D index)
		{
			this.X = index.X;
			this.Y = index.Y;
		}

		#endregion

		#region Singleton Properties

		/// <summary>
		/// Gets the zero index.
		/// </summary>
		/// <value>
		/// The zero index.
		/// </value>
		public static Index2D Zero => new Index2D(0);

		/// <summary>
		/// Gets the minimum index.
		/// </summary>
		/// <value>
		/// The minimum index.
		/// </value>
		public static Index2D MinValue => new Index2D(int.MinValue);

		/// <summary>
		/// Gets the maximum index.
		/// </summary>
		/// <value>
		/// The maximum index.
		/// </value>
		public static Index2D MaxValue => new Index2D(int.MaxValue);

		#endregion

		#region Coordinate Properties

		/// <summary>
		/// Gets the X coordinate.
		/// </summary>
		/// <value>
		/// The X coordinate.
		/// </value>
		public int X { get; }

		/// <summary>
		/// Gets the Y coordinate.
		/// </summary>
		/// <value>
		/// The Y coordinate.
		/// </value>
		public int Y { get; }

		#endregion

		#region IIndex Members

		/// <inheritdoc />
		public int Rank => 2;

		/// <inheritdoc />
		public int this[int dimension]
		{
			get
			{
				IIndexContracts.Indexer(this, dimension);

				switch (dimension)
				{
					case (int)Axis2D.X: return this.X;
					case (int)Axis2D.Y: return this.Y;
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
		public int this[Axis2D axis] => this[(int)axis];

		#region Unary Operators

		public static Index2D operator +(Index2D value) => value;

		public static Index2D operator -(Index2D value) => new Index2D(-value.X, -value.Y);

		#endregion

		#region Binary Operators

		public static Index2D operator +(Index2D lhs, Index2D rhs) => new Index2D(lhs.X + rhs.X, lhs.Y + rhs.Y);

		public static Index2D operator -(Index2D lhs, Index2D rhs) => new Index2D(lhs.X - rhs.X, lhs.Y - rhs.Y);

		public static Index2D operator *(Index2D lhs, Index2D rhs) => new Index2D(lhs.X * rhs.X, lhs.Y * rhs.Y);

		public static Index2D operator *(Index2D index, int scalar) => new Index2D(index.X * scalar, index.Y * scalar);

		public static Index2D operator /(Index2D lhs, Index2D rhs) => new Index2D(lhs.X / rhs.X, lhs.Y / rhs.Y);

		public static Index2D operator /(Index2D index, int scalar) => new Index2D(index.X / scalar, index.Y / scalar);

		public static Index2D operator %(Index2D lhs, Index2D rhs) => new Index2D(lhs.X % rhs.X, lhs.Y % rhs.Y);

		public static Index2D operator <<(Index2D index, int shift) => new Index2D(index.X << shift, index.Y << shift);

		public static Index2D operator >>(Index2D index, int shift) => new Index2D(index.X >> shift, index.Y >> shift);

		#endregion

		#region Equality Operators

		public static bool operator ==(Index2D lhs, Index2D rhs) => lhs.Equals(rhs);

		public static bool operator !=(Index2D lhs, Index2D rhs) => !lhs.Equals(rhs);

		#endregion

		#region IEquatable<Index2D> Members

		/// <inheritdoc />
		public bool Equals(Index2D other) => this.X == other.X && this.Y == other.Y;

		#endregion

		#region Object Overrides

		/// <inheritdoc />
		public override bool Equals(object obj) => StructUtilities.Equals(this, obj);

		/// <inheritdoc />
		public override int GetHashCode() => HashCode.Start.And(this.X).And(this.Y);

		/// <inheritdoc />
		public override string ToString() => $"{this.X}, {this.Y}";

		#endregion

		#region Additional Methods

		/// <summary>
		/// Determines whether this index is within the specified range of indices.
		/// </summary>
		/// <param name="min">The minimum index.</param>
		/// <param name="max">The maximum index.</param>
		/// <param name="bounds">The inclusive/exclusive range options.</param>
		/// <returns>True if the value is within range, false otherwise.</returns>
		public bool IsIn(Index2D min, Index2D max, RangeClusivity bounds = RangeClusivity.Inclusive) =>
			this.X.IsIn(Range.New(min.X, max.X, bounds)) && this.Y.IsIn(Range.New(min.Y, max.Y, bounds));

		/// <summary>
		/// Gets which orthant this index is located in with respect to the specified origin index.
		/// </summary>
		/// <param name="origin">The origin index.</param>
		/// <returns>The orthant of this index with respect to the origin index.</returns>
		public Orthant2D GetOrthant(Index2D origin = default(Index2D)) =>
			(Orthant2D)InternalIndexUtilities.GetOrthantOfAxis(this.X, origin.X, (int)Axis2D.X) |
			(Orthant2D)InternalIndexUtilities.GetOrthantOfAxis(this.Y, origin.Y, (int)Axis2D.Y);

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
		public int SumCoordinates() => this.X + this.Y;

		/// <summary>
		/// Sums all the coordinates of this index as a long value.
		/// </summary>
		/// <returns>The long sum of all the coordinates of the index.</returns>
		public long SumCoordinatesLong()
		{
			long result = this.X;
			result += this.Y;
			return result;
		}

		/// <summary>
		/// Multiplies all the coordinates of this index.
		/// </summary>
		/// <returns>The result of multiplying all the coordinates of the index.</returns>
		public int MultiplyCoordinates() => this.X * this.Y;

		/// <summary>
		/// Multiplies all the coordinates of this index as a long value.
		/// </summary>
		/// <returns>The long result of multiplying all the coordinates of the index.</returns>
		public long MultiplyCoordinatesLong()
		{
			long result = this.X;
			result *= this.Y;
			return result;
		}

		/// <summary>
		/// Divides this index by the divisor, rounding up to the next integral value instead of truncating.
		/// </summary>
		/// <param name="divisor">The divisor.</param>
		/// <returns>The result of the division, rounded up.</returns>
		public Index2D DivideByRoundUp(Index2D divisor) => new Index2D(
			this.X.DivideByRoundUp(divisor.X), this.Y.DivideByRoundUp(divisor.Y));

		/// <summary>
		/// Creates a new array whose dimensions are taken from this index.
		/// </summary>
		/// <typeparam name="T">The type of the value of the array.</typeparam>
		/// <returns>The new array.</returns>
		public T[,] CreateArray<T>()
		{
			Contracts.Requires.That(this.IsAllPositiveOrZero());

			return new T[this.X, this.Y];
		}

		#endregion
	}
}
