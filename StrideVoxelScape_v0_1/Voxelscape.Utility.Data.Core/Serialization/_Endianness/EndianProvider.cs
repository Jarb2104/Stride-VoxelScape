using System;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	/// <summary>
	///
	/// </summary>
	public static class EndianProvider
	{
		public static EndianProvider<T> New<T>(T endian1, T endian2)
			where T : IEndianSpecific => new EndianProvider<T>(endian1, endian2);

		#region Primitive type overloads

		public static EndianProvider<TResult> New<TResult>(Func<ByteConverter, TResult> create)
			where TResult : IEndianSpecific => New(create, ByteConverter.Get);

		public static EndianProvider<TResult> New<TResult>(Func<BoolSerializer, TResult> create)
			where TResult : IEndianSpecific => New(create, Serializer.Bool);

		public static EndianProvider<TResult> New<TResult>(Func<CharSerializer, TResult> create)
			where TResult : IEndianSpecific => New(create, Serializer.Char);

		public static EndianProvider<TResult> New<TResult>(Func<FloatSerializer, TResult> create)
			where TResult : IEndianSpecific => New(create, Serializer.Float);

		public static EndianProvider<TResult> New<TResult>(Func<DoubleSerializer, TResult> create)
			where TResult : IEndianSpecific => New(create, Serializer.Double);

		public static EndianProvider<TResult> New<TResult>(Func<DecimalSerializer, TResult> create)
			where TResult : IEndianSpecific => New(create, Serializer.Decimal);

		public static EndianProvider<TResult> New<TResult>(Func<ByteSerializer, TResult> create)
			where TResult : IEndianSpecific => New(create, Serializer.Byte);

		public static EndianProvider<TResult> New<TResult>(Func<SByteSerializer, TResult> create)
			where TResult : IEndianSpecific => New(create, Serializer.SByte);

		public static EndianProvider<TResult> New<TResult>(Func<ShortSerializer, TResult> create)
			where TResult : IEndianSpecific => New(create, Serializer.Short);

		public static EndianProvider<TResult> New<TResult>(Func<UShortSerializer, TResult> create)
			where TResult : IEndianSpecific => New(create, Serializer.UShort);

		public static EndianProvider<TResult> New<TResult>(Func<IntSerializer, TResult> create)
			where TResult : IEndianSpecific => New(create, Serializer.Int);

		public static EndianProvider<TResult> New<TResult>(Func<UIntSerializer, TResult> create)
			where TResult : IEndianSpecific => New(create, Serializer.UInt);

		public static EndianProvider<TResult> New<TResult>(Func<LongSerializer, TResult> create)
			where TResult : IEndianSpecific => New(create, Serializer.Long);

		public static EndianProvider<TResult> New<TResult>(Func<ULongSerializer, TResult> create)
			where TResult : IEndianSpecific => New(create, Serializer.ULong);

		#endregion

		#region Composite overloads

		public static EndianProvider<TResult> New<TResult, T>(Func<T, TResult> create, IEndianProvider<T> part)
			where TResult : IEndianSpecific
			where T : IEndianSpecific
		{
			Contracts.Requires.That(create != null);
			Contracts.Requires.That(part != null);

			var bigEndian = create(part.BigEndian);
			var littleEndian = create(part.LittleEndian);
			return new EndianProvider<TResult>(bigEndian, littleEndian);
		}

		public static EndianProvider<TResult> New<TResult, T1, T2>(
			Func<T1, T2, TResult> create, IEndianProvider<T1> part1, IEndianProvider<T2> part2)
			where TResult : IEndianSpecific
			where T1 : IEndianSpecific
			where T2 : IEndianSpecific
		{
			Contracts.Requires.That(create != null);
			Contracts.Requires.That(part1 != null);
			Contracts.Requires.That(part2 != null);

			var bigEndian = create(part1.BigEndian, part2.BigEndian);
			var littleEndian = create(part1.LittleEndian, part2.LittleEndian);
			return new EndianProvider<TResult>(bigEndian, littleEndian);
		}

		public static EndianProvider<TResult> New<TResult, T1, T2, T3>(
			Func<T1, T2, T3, TResult> create,
			IEndianProvider<T1> part1,
			IEndianProvider<T2> part2,
			IEndianProvider<T3> part3)
			where TResult : IEndianSpecific
			where T1 : IEndianSpecific
			where T2 : IEndianSpecific
			where T3 : IEndianSpecific
		{
			Contracts.Requires.That(create != null);
			Contracts.Requires.That(part1 != null);
			Contracts.Requires.That(part2 != null);
			Contracts.Requires.That(part3 != null);

			var bigEndian = create(part1.BigEndian, part2.BigEndian, part3.BigEndian);
			var littleEndian = create(part1.LittleEndian, part2.LittleEndian, part3.LittleEndian);
			return new EndianProvider<TResult>(bigEndian, littleEndian);
		}

		public static EndianProvider<TResult> New<TResult, T1, T2, T3, T4>(
			Func<T1, T2, T3, T4, TResult> create,
			IEndianProvider<T1> part1,
			IEndianProvider<T2> part2,
			IEndianProvider<T3> part3,
			IEndianProvider<T4> part4)
			where TResult : IEndianSpecific
			where T1 : IEndianSpecific
			where T2 : IEndianSpecific
			where T3 : IEndianSpecific
			where T4 : IEndianSpecific
		{
			Contracts.Requires.That(create != null);
			Contracts.Requires.That(part1 != null);
			Contracts.Requires.That(part2 != null);
			Contracts.Requires.That(part3 != null);
			Contracts.Requires.That(part4 != null);

			var bigEndian = create(
				part1.BigEndian,
				part2.BigEndian,
				part3.BigEndian,
				part4.BigEndian);
			var littleEndian = create(
				part1.LittleEndian,
				part2.LittleEndian,
				part3.LittleEndian,
				part4.LittleEndian);
			return new EndianProvider<TResult>(bigEndian, littleEndian);
		}

		public static EndianProvider<TResult> New<TResult, T1, T2, T3, T4, T5>(
			Func<T1, T2, T3, T4, T5, TResult> create,
			IEndianProvider<T1> part1,
			IEndianProvider<T2> part2,
			IEndianProvider<T3> part3,
			IEndianProvider<T4> part4,
			IEndianProvider<T5> part5)
			where TResult : IEndianSpecific
			where T1 : IEndianSpecific
			where T2 : IEndianSpecific
			where T3 : IEndianSpecific
			where T4 : IEndianSpecific
			where T5 : IEndianSpecific
		{
			Contracts.Requires.That(create != null);
			Contracts.Requires.That(part1 != null);
			Contracts.Requires.That(part2 != null);
			Contracts.Requires.That(part3 != null);
			Contracts.Requires.That(part4 != null);
			Contracts.Requires.That(part5 != null);

			var bigEndian = create(
				part1.BigEndian,
				part2.BigEndian,
				part3.BigEndian,
				part4.BigEndian,
				part5.BigEndian);
			var littleEndian = create(
				part1.LittleEndian,
				part2.LittleEndian,
				part3.LittleEndian,
				part4.LittleEndian,
				part5.LittleEndian);
			return new EndianProvider<TResult>(bigEndian, littleEndian);
		}

		public static EndianProvider<TResult> New<TResult, T1, T2, T3, T4, T5, T6>(
			Func<T1, T2, T3, T4, T5, T6, TResult> create,
			IEndianProvider<T1> part1,
			IEndianProvider<T2> part2,
			IEndianProvider<T3> part3,
			IEndianProvider<T4> part4,
			IEndianProvider<T5> part5,
			IEndianProvider<T6> part6)
			where TResult : IEndianSpecific
			where T1 : IEndianSpecific
			where T2 : IEndianSpecific
			where T3 : IEndianSpecific
			where T4 : IEndianSpecific
			where T5 : IEndianSpecific
			where T6 : IEndianSpecific
		{
			Contracts.Requires.That(create != null);
			Contracts.Requires.That(part1 != null);
			Contracts.Requires.That(part2 != null);
			Contracts.Requires.That(part3 != null);
			Contracts.Requires.That(part4 != null);
			Contracts.Requires.That(part5 != null);
			Contracts.Requires.That(part6 != null);

			var bigEndian = create(
				part1.BigEndian,
				part2.BigEndian,
				part3.BigEndian,
				part4.BigEndian,
				part5.BigEndian,
				part6.BigEndian);
			var littleEndian = create(
				part1.LittleEndian,
				part2.LittleEndian,
				part3.LittleEndian,
				part4.LittleEndian,
				part5.LittleEndian,
				part6.LittleEndian);
			return new EndianProvider<TResult>(bigEndian, littleEndian);
		}

		public static EndianProvider<TResult> New<TResult, T1, T2, T3, T4, T5, T6, T7>(
			Func<T1, T2, T3, T4, T5, T6, T7, TResult> create,
			IEndianProvider<T1> part1,
			IEndianProvider<T2> part2,
			IEndianProvider<T3> part3,
			IEndianProvider<T4> part4,
			IEndianProvider<T5> part5,
			IEndianProvider<T6> part6,
			IEndianProvider<T7> part7)
			where TResult : IEndianSpecific
			where T1 : IEndianSpecific
			where T2 : IEndianSpecific
			where T3 : IEndianSpecific
			where T4 : IEndianSpecific
			where T5 : IEndianSpecific
			where T6 : IEndianSpecific
			where T7 : IEndianSpecific
		{
			Contracts.Requires.That(create != null);
			Contracts.Requires.That(part1 != null);
			Contracts.Requires.That(part2 != null);
			Contracts.Requires.That(part3 != null);
			Contracts.Requires.That(part4 != null);
			Contracts.Requires.That(part5 != null);
			Contracts.Requires.That(part6 != null);
			Contracts.Requires.That(part7 != null);

			var bigEndian = create(
				part1.BigEndian,
				part2.BigEndian,
				part3.BigEndian,
				part4.BigEndian,
				part5.BigEndian,
				part6.BigEndian,
				part7.BigEndian);
			var littleEndian = create(
				part1.LittleEndian,
				part2.LittleEndian,
				part3.LittleEndian,
				part4.LittleEndian,
				part5.LittleEndian,
				part6.LittleEndian,
				part7.LittleEndian);
			return new EndianProvider<TResult>(bigEndian, littleEndian);
		}

		public static EndianProvider<TResult> New<TResult, T1, T2, T3, T4, T5, T6, T7, T8>(
			Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> create,
			IEndianProvider<T1> part1,
			IEndianProvider<T2> part2,
			IEndianProvider<T3> part3,
			IEndianProvider<T4> part4,
			IEndianProvider<T5> part5,
			IEndianProvider<T6> part6,
			IEndianProvider<T7> part7,
			IEndianProvider<T8> part8)
			where TResult : IEndianSpecific
			where T1 : IEndianSpecific
			where T2 : IEndianSpecific
			where T3 : IEndianSpecific
			where T4 : IEndianSpecific
			where T5 : IEndianSpecific
			where T6 : IEndianSpecific
			where T7 : IEndianSpecific
			where T8 : IEndianSpecific
		{
			Contracts.Requires.That(create != null);
			Contracts.Requires.That(part1 != null);
			Contracts.Requires.That(part2 != null);
			Contracts.Requires.That(part3 != null);
			Contracts.Requires.That(part4 != null);
			Contracts.Requires.That(part5 != null);
			Contracts.Requires.That(part6 != null);
			Contracts.Requires.That(part7 != null);
			Contracts.Requires.That(part8 != null);

			var bigEndian = create(
				part1.BigEndian,
				part2.BigEndian,
				part3.BigEndian,
				part4.BigEndian,
				part5.BigEndian,
				part6.BigEndian,
				part7.BigEndian,
				part8.BigEndian);
			var littleEndian = create(
				part1.LittleEndian,
				part2.LittleEndian,
				part3.LittleEndian,
				part4.LittleEndian,
				part5.LittleEndian,
				part6.LittleEndian,
				part7.LittleEndian,
				part8.LittleEndian);
			return new EndianProvider<TResult>(bigEndian, littleEndian);
		}

		#endregion
	}
}
