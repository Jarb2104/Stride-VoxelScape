using System;
using Voxelscape.Common.Indexing.Core.Enums;
using Voxelscape.Common.Indexing.Pact.Indices;
using Voxelscape.Utility.Common.Core.Hashing;
using Voxelscape.Utility.Common.Core.Mathematics;
using Voxelscape.Utility.Common.Core.Types;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Exceptions;
using Voxelscape.Utility.Common.Pact.Mathematics;
using HashCode = Voxelscape.Utility.Common.Core.Hashing.HashCode;
using Range = Voxelscape.Utility.Common.Core.Mathematics.Range;

namespace Voxelscape.Common.Indexing.Core.Indices
{
	/// <summary>
	/// A four dimensional integer index for accessing arrays or grid based structures.
	/// </summary>
	public struct Index4D : IIndex, IEquatable<Index4D>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Index4D"/> struct.
		/// </summary>
		/// <param name="number">The number to assign to each index.</param>
		public Index4D(int number)
		{
			this.X = number;
			this.Y = number;
			this.Z = number;
			this.W = number;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Index4D"/> struct.
		/// </summary>
		/// <param name="x">The x index.</param>
		/// <param name="y">The y index.</param>
		/// <param name="z">The z index.</param>
		/// <param name="w">The w index.</param>
		public Index4D(int x, int y, int z, int w)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.W = w;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Index4D" /> struct.
		/// </summary>
		/// <param name="index">The index whose coordinates to copy.</param>
		public Index4D(Index4D index)
		{
			this.X = index.X;
			this.Y = index.Y;
			this.Z = index.Z;
			this.W = index.W;
		}

		#endregion

		#region Singleton Properties

		/// <summary>
		/// Gets the zero index.
		/// </summary>
		/// <value>
		/// The zero index.
		/// </value>
		public static Index4D Zero => new Index4D(0);

		/// <summary>
		/// Gets the minimum index.
		/// </summary>
		/// <value>
		/// The minimum index.
		/// </value>
		public static Index4D MinValue => new Index4D(int.MinValue);

		/// <summary>
		/// Gets the maximum index.
		/// </summary>
		/// <value>
		/// The maximum index.
		/// </value>
		public static Index4D MaxValue => new Index4D(int.MaxValue);

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

		/// <summary>
		/// Gets the Z coordinate.
		/// </summary>
		/// <value>
		/// The Z coordinate.
		/// </value>
		public int Z { get; }

		/// <summary>
		/// Gets the W coordinate.
		/// </summary>
		/// <value>
		/// The W coordinate.
		/// </value>
		public int W { get; }

		#endregion

		#region IIndex Members

		/// <inheritdoc />
		public int Rank => 4;

		/// <inheritdoc />
		public int this[int dimension]
		{
			get
			{
				IIndexContracts.Indexer(this, dimension);

				switch (dimension)
				{
					case (int)Axis4D.X: return this.X;
					case (int)Axis4D.Y: return this.Y;
					case (int)Axis4D.Z: return this.Z;
					case (int)Axis4D.W: return this.W;
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
		public int this[Axis4D axis] => this[(int)axis];

		#region Unary Operators

		public static Index4D operator +(Index4D value) => value;

		public static Index4D operator -(Index4D value) => new Index4D(-value.X, -value.Y, -value.Z, -value.W);

		#endregion

		#region Binary Operators

		public static Index4D operator +(Index4D lhs, Index4D rhs) =>
			new Index4D(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z, lhs.W + rhs.W);

		public static Index4D operator -(Index4D lhs, Index4D rhs) =>
			new Index4D(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z, lhs.W - rhs.W);

		public static Index4D operator *(Index4D lhs, Index4D rhs) =>
			new Index4D(lhs.X * rhs.X, lhs.Y * rhs.Y, lhs.Z * rhs.Z, lhs.W * rhs.W);

		public static Index4D operator *(Index4D index, int scalar) =>
			new Index4D(index.X * scalar, index.Y * scalar, index.Z * scalar, index.W * scalar);

		public static Index4D operator /(Index4D lhs, Index4D rhs) =>
			new Index4D(lhs.X / rhs.X, lhs.Y / rhs.Y, lhs.Z / rhs.Z, lhs.W / rhs.W);

		public static Index4D operator /(Index4D index, int scalar) =>
			new Index4D(index.X / scalar, index.Y / scalar, index.Z / scalar, index.W / scalar);

		public static Index4D operator %(Index4D lhs, Index4D rhs) =>
			new Index4D(lhs.X % rhs.X, lhs.Y % rhs.Y, lhs.Z % rhs.Z, lhs.W % rhs.W);

		public static Index4D operator <<(Index4D index, int shift) =>
			new Index4D(index.X << shift, index.Y << shift, index.Z << shift, index.W << shift);

		public static Index4D operator >>(Index4D index, int shift) =>
			new Index4D(index.X >> shift, index.Y >> shift, index.Z >> shift, index.W >> shift);

		#endregion

		#region Equality Operators

		public static bool operator ==(Index4D lhs, Index4D rhs) => lhs.Equals(rhs);

		public static bool operator !=(Index4D lhs, Index4D rhs) => !lhs.Equals(rhs);

		#endregion

		#region IEquatable<Index4D> Members

		/// <inheritdoc />
		public bool Equals(Index4D other) =>
			this.X == other.X && this.Y == other.Y && this.Z == other.Z && this.W == other.W;

		#endregion

		#region Object Overrides

		/// <inheritdoc />
		public override bool Equals(object obj) => StructUtilities.Equals(this, obj);

		/// <inheritdoc />
		public override int GetHashCode() => HashCode.Start.And(this.X).And(this.Y).And(this.Z).And(this.W);

		/// <inheritdoc />
		public override string ToString() => $"{this.X}, {this.Y}, {this.Z}, {this.W}";

		#endregion

		#region Additional Methods

		/// <summary>
		/// Determines whether this index is within the specified range of indices.
		/// </summary>
		/// <param name="min">The minimum index.</param>
		/// <param name="max">The maximum index.</param>
		/// <param name="bounds">The inclusive/exclusive range options.</param>
		/// <returns>True if the value is within range, false otherwise.</returns>
		public bool IsIn(Index4D min, Index4D max, RangeClusivity bounds = RangeClusivity.Inclusive) =>
			this.X.IsIn(Range.New(min.X, max.X, bounds)) &&
			this.Y.IsIn(Range.New(min.Y, max.Y, bounds)) &&
			this.Z.IsIn(Range.New(min.Z, max.Z, bounds)) &&
			this.W.IsIn(Range.New(min.W, max.W, bounds));

		/// <summary>
		/// Gets which orthant this index is located in with respect to the specified origin index.
		/// </summary>
		/// <param name="origin">The origin index.</param>
		/// <returns>The orthant of this index with respect to the origin index.</returns>
		public Orthant4D GetOrthant(Index4D origin = default(Index4D)) =>
			(Orthant4D)InternalIndexUtilities.GetOrthantOfAxis(this.X, origin.X, (int)Axis4D.X) |
			(Orthant4D)InternalIndexUtilities.GetOrthantOfAxis(this.Y, origin.Y, (int)Axis4D.Y) |
			(Orthant4D)InternalIndexUtilities.GetOrthantOfAxis(this.Z, origin.Z, (int)Axis4D.Z) |
			(Orthant4D)InternalIndexUtilities.GetOrthantOfAxis(this.W, origin.W, (int)Axis4D.W);

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
		public int SumCoordinates() => this.X + this.Y + this.Z + this.W;

		/// <summary>
		/// Sums all the coordinates of this index as a long value.
		/// </summary>
		/// <returns>The long sum of all the coordinates of the index.</returns>
		public long SumCoordinatesLong()
		{
			long result = this.X;
			result += this.Y;
			result += this.Z;
			result += this.W;
			return result;
		}

		/// <summary>
		/// Multiplies all the coordinates of this index.
		/// </summary>
		/// <returns>The result of multiplying all the coordinates of the index.</returns>
		public int MultiplyCoordinates() => this.X * this.Y * this.Z * this.W;

		/// <summary>
		/// Multiplies all the coordinates of this index as a long value.
		/// </summary>
		/// <returns>The long result of multiplying all the coordinates of the index.</returns>
		public long MultiplyCoordinatesLong()
		{
			long result = this.X;
			result *= this.Y;
			result *= this.Z;
			result *= this.W;
			return result;
		}

		/// <summary>
		/// Divides this index by the divisor, rounding up to the next integral value instead of truncating.
		/// </summary>
		/// <param name="divisor">The divisor.</param>
		/// <returns>The result of the division, rounded up.</returns>
		public Index4D DivideByRoundUp(Index4D divisor) => new Index4D(
			this.X.DivideByRoundUp(divisor.X),
			this.Y.DivideByRoundUp(divisor.Y),
			this.Z.DivideByRoundUp(divisor.Z),
			this.W.DivideByRoundUp(divisor.W));

		/// <summary>
		/// Creates a new array whose dimensions are taken from this index.
		/// </summary>
		/// <typeparam name="T">The type of the value of the array.</typeparam>
		/// <returns>The new array.</returns>
		public T[,,,] CreateArray<T>()
		{
			Contracts.Requires.That(this.IsAllPositiveOrZero());

			return new T[this.X, this.Y, this.Z, this.W];
		}

		#endregion
	}
}
