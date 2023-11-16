using System.Diagnostics;
using System.IO;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

/// <summary>
/// Provides extension methods for the <see cref="IBufferedArray"/> interface.
/// </summary>
public static class IBufferedArrayExtensions
{
	public static bool TryBufferNext(this IBufferedArray buffer, int count)
	{
		Contracts.Requires.That(buffer != null);
		Contracts.Requires.That(count >= 0);

		int countBuffered = buffer.BufferNext(count);
		return countBuffered == count;
	}

	#region TryNext methods

	public static bool TryNextBool(this IBufferedArray buffer, IByteConverter converter, out bool value)
	{
		SharedContracts(buffer, converter);

		if (buffer.TryBufferNext(ByteLength.Bool))
		{
			value = converter.ToBool(buffer.Buffer, 0);
			return true;
		}
		else
		{
			value = default(bool);
			return false;
		}
	}

	public static bool TryNextChar(this IBufferedArray buffer, IByteConverter converter, out char value)
	{
		SharedContracts(buffer, converter);

		if (buffer.TryBufferNext(ByteLength.Char))
		{
			value = converter.ToChar(buffer.Buffer, 0);
			return true;
		}
		else
		{
			value = default(char);
			return false;
		}
	}

	public static bool TryNextFloat(this IBufferedArray buffer, IByteConverter converter, out float value)
	{
		SharedContracts(buffer, converter);

		if (buffer.TryBufferNext(ByteLength.Float))
		{
			value = converter.ToFloat(buffer.Buffer, 0);
			return true;
		}
		else
		{
			value = default(float);
			return false;
		}
	}

	public static bool TryNextDouble(this IBufferedArray buffer, IByteConverter converter, out double value)
	{
		SharedContracts(buffer, converter);

		if (buffer.TryBufferNext(ByteLength.Double))
		{
			value = converter.ToDouble(buffer.Buffer, 0);
			return true;
		}
		else
		{
			value = default(double);
			return false;
		}
	}

	public static bool TryNextDecimal(this IBufferedArray buffer, IByteConverter converter, out decimal value)
	{
		SharedContracts(buffer, converter);

		if (buffer.TryBufferNext(ByteLength.Decimal))
		{
			value = converter.ToDecimal(buffer.Buffer, 0);
			return true;
		}
		else
		{
			value = default(decimal);
			return false;
		}
	}

	public static bool TryNextByte(this IBufferedArray buffer, IByteConverter converter, out byte value)
	{
		SharedContracts(buffer, converter);

		if (buffer.TryBufferNext(ByteLength.Byte))
		{
			value = converter.ToByte(buffer.Buffer, 0);
			return true;
		}
		else
		{
			value = default(byte);
			return false;
		}
	}

	public static bool TryNextSByte(this IBufferedArray buffer, IByteConverter converter, out sbyte value)
	{
		SharedContracts(buffer, converter);

		if (buffer.TryBufferNext(ByteLength.SByte))
		{
			value = converter.ToSByte(buffer.Buffer, 0);
			return true;
		}
		else
		{
			value = default(sbyte);
			return false;
		}
	}

	public static bool TryNextShort(this IBufferedArray buffer, IByteConverter converter, out short value)
	{
		SharedContracts(buffer, converter);

		if (buffer.TryBufferNext(ByteLength.Short))
		{
			value = converter.ToShort(buffer.Buffer, 0);
			return true;
		}
		else
		{
			value = default(short);
			return false;
		}
	}

	public static bool TryNextUShort(this IBufferedArray buffer, IByteConverter converter, out ushort value)
	{
		SharedContracts(buffer, converter);

		if (buffer.TryBufferNext(ByteLength.UShort))
		{
			value = converter.ToUShort(buffer.Buffer, 0);
			return true;
		}
		else
		{
			value = default(ushort);
			return false;
		}
	}

	public static bool TryNextInt(this IBufferedArray buffer, IByteConverter converter, out int value)
	{
		SharedContracts(buffer, converter);

		if (buffer.TryBufferNext(ByteLength.Int))
		{
			value = converter.ToInt(buffer.Buffer, 0);
			return true;
		}
		else
		{
			value = default(int);
			return false;
		}
	}

	public static bool TryNextUInt(this IBufferedArray buffer, IByteConverter converter, out uint value)
	{
		SharedContracts(buffer, converter);

		if (buffer.TryBufferNext(ByteLength.UInt))
		{
			value = converter.ToUInt(buffer.Buffer, 0);
			return true;
		}
		else
		{
			value = default(uint);
			return false;
		}
	}

	public static bool TryNextLong(this IBufferedArray buffer, IByteConverter converter, out long value)
	{
		SharedContracts(buffer, converter);

		if (buffer.TryBufferNext(ByteLength.Long))
		{
			value = converter.ToLong(buffer.Buffer, 0);
			return true;
		}
		else
		{
			value = default(long);
			return false;
		}
	}

	public static bool TryNextULong(this IBufferedArray buffer, IByteConverter converter, out ulong value)
	{
		SharedContracts(buffer, converter);

		if (buffer.TryBufferNext(ByteLength.ULong))
		{
			value = converter.ToULong(buffer.Buffer, 0);
			return true;
		}
		else
		{
			value = default(ulong);
			return false;
		}
	}

	#endregion

	#region Next methods

	public static bool NextBool(this IBufferedArray buffer, IByteConverter converter)
	{
		SharedContracts(buffer, converter);

		buffer.BufferNextExceptionChecking(ByteLength.Bool);
		return converter.ToBool(buffer.Buffer, 0);
	}

	public static char NextChar(this IBufferedArray buffer, IByteConverter converter)
	{
		SharedContracts(buffer, converter);

		buffer.BufferNextExceptionChecking(ByteLength.Char);
		return converter.ToChar(buffer.Buffer, 0);
	}

	public static float NextFloat(this IBufferedArray buffer, IByteConverter converter)
	{
		SharedContracts(buffer, converter);

		buffer.BufferNextExceptionChecking(ByteLength.Float);
		return converter.ToFloat(buffer.Buffer, 0);
	}

	public static double NextDouble(this IBufferedArray buffer, IByteConverter converter)
	{
		SharedContracts(buffer, converter);

		buffer.BufferNextExceptionChecking(ByteLength.Double);
		return converter.ToDouble(buffer.Buffer, 0);
	}

	public static decimal NextDecimal(this IBufferedArray buffer, IByteConverter converter)
	{
		SharedContracts(buffer, converter);

		buffer.BufferNextExceptionChecking(ByteLength.Decimal);
		return converter.ToDecimal(buffer.Buffer, 0);
	}

	public static byte NextByte(this IBufferedArray buffer, IByteConverter converter)
	{
		SharedContracts(buffer, converter);

		buffer.BufferNextExceptionChecking(ByteLength.Byte);
		return converter.ToByte(buffer.Buffer, 0);
	}

	public static sbyte NextSByte(this IBufferedArray buffer, IByteConverter converter)
	{
		SharedContracts(buffer, converter);

		buffer.BufferNextExceptionChecking(ByteLength.SByte);
		return converter.ToSByte(buffer.Buffer, 0);
	}

	public static short NextShort(this IBufferedArray buffer, IByteConverter converter)
	{
		SharedContracts(buffer, converter);

		buffer.BufferNextExceptionChecking(ByteLength.Short);
		return converter.ToShort(buffer.Buffer, 0);
	}

	public static ushort NextUShort(this IBufferedArray buffer, IByteConverter converter)
	{
		SharedContracts(buffer, converter);

		buffer.BufferNextExceptionChecking(ByteLength.UShort);
		return converter.ToUShort(buffer.Buffer, 0);
	}

	public static int NextInt(this IBufferedArray buffer, IByteConverter converter)
	{
		SharedContracts(buffer, converter);

		buffer.BufferNextExceptionChecking(ByteLength.Int);
		return converter.ToInt(buffer.Buffer, 0);
	}

	public static uint NextUInt(this IBufferedArray buffer, IByteConverter converter)
	{
		SharedContracts(buffer, converter);

		buffer.BufferNextExceptionChecking(ByteLength.UInt);
		return converter.ToUInt(buffer.Buffer, 0);
	}

	public static long NextLong(this IBufferedArray buffer, IByteConverter converter)
	{
		SharedContracts(buffer, converter);

		buffer.BufferNextExceptionChecking(ByteLength.Long);
		return converter.ToLong(buffer.Buffer, 0);
	}

	public static ulong NextULong(this IBufferedArray buffer, IByteConverter converter)
	{
		SharedContracts(buffer, converter);

		buffer.BufferNextExceptionChecking(ByteLength.ULong);
		return converter.ToULong(buffer.Buffer, 0);
	}

	#endregion

	[Conditional(Contracts.Requires.CompilationSymbol)]
	private static void SharedContracts(IBufferedArray buffer, IByteConverter converter)
	{
		Contracts.Requires.That(buffer != null);
		Contracts.Requires.That(converter != null);
	}

	private static void BufferNextExceptionChecking(this IBufferedArray buffer, int count)
	{
		Contracts.Requires.That(buffer != null);
		Contracts.Requires.That(count >= 0);

		if (!buffer.TryBufferNext(count))
		{
			throw new EndOfStreamException("End of buffered source reached.");
		}
	}
}
