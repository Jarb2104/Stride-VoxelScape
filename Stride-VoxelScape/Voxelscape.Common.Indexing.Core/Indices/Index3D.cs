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
	public struct Index3D : IIndex, IEquatable<Index3D>
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="Index3D"/> struct.
		/// </summary>
		/// <param name="number">The number to assign to each index.</param>
		public Index3D(int number)
		{
			this.X = number;
			this.Y = number;
			this.Z = number;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Index3D"/> struct.
		/// </summary>
		/// <param name="x">The x index.</param>
		/// <param name="y">The y index.</param>
		/// <param name="z">The z index.</param>
		public Index3D(int x, int y, int z)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Index3D" /> struct.
		/// </summary>
		/// <param name="index">The index whose coordinates to copy.</param>
		public Index3D(Index3D index)
		{
			this.X = index.X;
			this.Y = index.Y;
			this.Z = index.Z;
		}

		#endregion

		#region Singleton Properties

		/// <summary>
		/// Gets the zero index.
		/// </summary>
		/// <value>
		/// The zero index.
		/// </value>
		public static Index3D Zero => new Index3D(0);

		/// <summary>
		/// Gets the minimum index.
		/// </summary>
		/// <value>
		/// The minimum index.
		/// </value>
		public static Index3D MinValue => new Index3D(int.MinValue);

		/// <summary>
		/// Gets the maximum index.
		/// </summary>
		/// <value>
		/// The maximum index.
		/// </value>
		public static Index3D MaxValue => new Index3D(int.MaxValue);

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

		#endregion

		#region IIndex Members

		/// <inheritdoc />
		public int Rank => 3;

		/// <inheritdoc />
		public int this[int dimension]
		{
			get
			{
				IIndexContracts.Indexer(this, dimension);

				switch (dimension)
				{
					case (int)Axis3D.X: return this.X;
					case (int)Axis3D.Y: return this.Y;
					case (int)Axis3D.Z: return this.Z;
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
		public int this[Axis3D axis] => this[(int)axis];

		#region Unary Operators

		public static Index3D operator +(Index3D value) => value;

		public static Index3D operator -(Index3D value) => new Index3D(-value.X, -value.Y, -value.Z);

		#endregion

		#region Binary Operators

		public static Index3D operator +(Index3D lhs, Index3D rhs) => new Index3D(lhs.X + rhs.X, lhs.Y + rhs.Y, lhs.Z + rhs.Z);

		public static Index3D operator -(Index3D lhs, Index3D rhs) => new Index3D(lhs.X - rhs.X, lhs.Y - rhs.Y, lhs.Z - rhs.Z);

		public static Index3D operator *(Index3D lhs, Index3D rhs) => new Index3D(lhs.X * rhs.X, lhs.Y * rhs.Y, lhs.Z * rhs.Z);

		public static Index3D operator *(Index3D index, int scalar) => new Index3D(index.X * scalar, index.Y * scalar, index.Z * scalar);

		public static Index3D operator /(Index3D lhs, Index3D rhs) => new Index3D(lhs.X / rhs.X, lhs.Y / rhs.Y, lhs.Z / rhs.Z);

		public static Index3D operator /(Index3D index, int scalar) => new Index3D(index.X / scalar, index.Y / scalar, index.Z / scalar);

		public static Index3D operator %(Index3D lhs, Index3D rhs) => new Index3D(lhs.X % rhs.X, lhs.Y % rhs.Y, lhs.Z % rhs.Z);

		public static Index3D operator <<(Index3D index, int shift) => new Index3D(index.X << shift, index.Y << shift, index.Z << shift);

		public static Index3D operator >>(Index3D index, int shift) => new Index3D(index.X >> shift, index.Y >> shift, index.Z >> shift);

		#endregion

		#region Equality Operators

		public static bool operator ==(Index3D lhs, Index3D rhs) => lhs.Equals(rhs);

		public static bool operator !=(Index3D lhs, Index3D rhs) => !lhs.Equals(rhs);

		#endregion

		#region IEquatable<Index3D> Members

		/// <inheritdoc />
		public bool Equals(Index3D other) => this.X == other.X && this.Y == other.Y && this.Z == other.Z;

		#endregion

		#region Object Overrides

		/// <inheritdoc />
		public override bool Equals(object obj) => StructUtilities.Equals(this, obj);

		/// <inheritdoc />
		public override int GetHashCode() => HashCode.Start.And(this.X).And(this.Y).And(this.Z);

		/// <inheritdoc />
		public override string ToString() => $"{this.X}, {this.Y}, {this.Z}";

		#endregion

		#region Additional Methods

		/// <summary>
		/// Determines whether this index is within the specified range of indices.
		/// </summary>
		/// <param name="min">The minimum index.</param>
		/// <param name="max">The maximum index.</param>
		/// <param name="bounds">The inclusive/exclusive range options.</param>
		/// <returns>True if the value is within range, false otherwise.</returns>
		public bool IsIn(Index3D min, Index3D max, RangeClusivity bounds = RangeClusivity.Inclusive) =>
			this.X.IsIn(Range.New(min.X, max.X, bounds)) &&
			this.Y.IsIn(Range.New(min.Y, max.Y, bounds)) &&
			this.Z.IsIn(Range.New(min.Z, max.Z, bounds));

		/// <summary>
		/// Gets which orthant this index is located in with respect to the specified origin index.
		/// </summary>
		/// <param name="origin">The origin index.</param>
		/// <returns>The orthant of this index with respect to the origin index.</returns>
		public Orthant3D GetOrthant(Index3D origin = default(Index3D)) =>
			(Orthant3D)InternalIndexUtilities.GetOrthantOfAxis(this.X, origin.X, (int)Axis3D.X) |
			(Orthant3D)InternalIndexUtilities.GetOrthantOfAxis(this.Y, origin.Y, (int)Axis3D.Y) |
			(Orthant3D)InternalIndexUtilities.GetOrthantOfAxis(this.Z, origin.Z, (int)Axis3D.Z);

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
		public int SumCoordinates() => this.X + this.Y + this.Z;

		/// <summary>
		/// Sums all the coordinates of this index as a long value.
		/// </summary>
		/// <returns>The long sum of all the coordinates of the index.</returns>
		public long SumCoordinatesLong()
		{
			long result = this.X;
			result += this.Y;
			result += this.Z;
			return result;
		}

		/// <summary>
		/// Multiplies all the coordinates of this index.
		/// </summary>
		/// <returns>The result of multiplying all the coordinates of the index.</returns>
		public int MultiplyCoordinates() => this.X * this.Y * this.Z;

		/// <summary>
		/// Multiplies all the coordinates of this index as a long value.
		/// </summary>
		/// <returns>The long result of multiplying all the coordinates of the index.</returns>
		public long MultiplyCoordinatesLong()
		{
			long result = this.X;
			result *= this.Y;
			result *= this.Z;
			return result;
		}

		/// <summary>
		/// Divides this index by the divisor, rounding up to the next integral value instead of truncating.
		/// </summary>
		/// <param name="divisor">The divisor.</param>
		/// <returns>The result of the division, rounded up.</returns>
		public Index3D DivideByRoundUp(Index3D divisor) => new Index3D(
			this.X.DivideByRoundUp(divisor.X),
			this.Y.DivideByRoundUp(divisor.Y),
			this.Z.DivideByRoundUp(divisor.Z));

		/// <summary>
		/// Creates a new array whose dimensions are taken from this index.
		/// </summary>
		/// <typeparam name="T">The type of the value of the array.</typeparam>
		/// <returns>The new array.</returns>
		public T[,,] CreateArray<T>()
		{
			Contracts.Requires.That(this.IsAllPositiveOrZero());

			return new T[this.X, this.Y, this.Z];
		}

		#endregion
	}
}
