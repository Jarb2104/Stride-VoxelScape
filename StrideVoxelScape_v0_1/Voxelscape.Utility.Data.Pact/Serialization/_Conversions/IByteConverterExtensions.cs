using System;
using System.Diagnostics;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Serialization;

/// <summary>
/// Provides extension methods for <see cref="IByteConverter"/>.
/// </summary>
public static class IByteConverterExtensions
{
	[ThreadStatic]
	private static byte[] threadLocalBuffer = null;

	#region CopyBytes non-ref index methods

	public static void CopyBytes(this IByteConverter converter, bool value, byte[] buffer, int index)
	{
		Contracts.Requires.That(converter != null);

		converter.CopyBytes(value, buffer, ref index);
	}

	public static void CopyBytes(this IByteConverter converter, char value, byte[] buffer, int index)
	{
		Contracts.Requires.That(converter != null);

		converter.CopyBytes(value, buffer, ref index);
	}

	public static void CopyBytes(this IByteConverter converter, float value, byte[] buffer, int index)
	{
		Contracts.Requires.That(converter != null);

		converter.CopyBytes(value, buffer, ref index);
	}

	public static void CopyBytes(this IByteConverter converter, double value, byte[] buffer, int index)
	{
		Contracts.Requires.That(converter != null);

		converter.CopyBytes(value, buffer, ref index);
	}

	public static void CopyBytes(this IByteConverter converter, decimal value, byte[] buffer, int index)
	{
		Contracts.Requires.That(converter != null);

		converter.CopyBytes(value, buffer, ref index);
	}

	public static void CopyBytes(this IByteConverter converter, byte value, byte[] buffer, int index)
	{
		Contracts.Requires.That(converter != null);

		converter.CopyBytes(value, buffer, ref index);
	}

	public static void CopyBytes(this IByteConverter converter, sbyte value, byte[] buffer, int index)
	{
		Contracts.Requires.That(converter != null);

		converter.CopyBytes(value, buffer, ref index);
	}

	public static void CopyBytes(this IByteConverter converter, short value, byte[] buffer, int index)
	{
		Contracts.Requires.That(converter != null);

		converter.CopyBytes(value, buffer, ref index);
	}

	public static void CopyBytes(this IByteConverter converter, ushort value, byte[] buffer, int index)
	{
		Contracts.Requires.That(converter != null);

		converter.CopyBytes(value, buffer, ref index);
	}

	public static void CopyBytes(this IByteConverter converter, int value, byte[] buffer, int index)
	{
		Contracts.Requires.That(converter != null);

		converter.CopyBytes(value, buffer, ref index);
	}

	public static void CopyBytes(this IByteConverter converter, uint value, byte[] buffer, int index)
	{
		Contracts.Requires.That(converter != null);

		converter.CopyBytes(value, buffer, ref index);
	}

	public static void CopyBytes(this IByteConverter converter, long value, byte[] buffer, int index)
	{
		Contracts.Requires.That(converter != null);

		converter.CopyBytes(value, buffer, ref index);
	}

	public static void CopyBytes(this IByteConverter converter, ulong value, byte[] buffer, int index)
	{
		Contracts.Requires.That(converter != null);

		converter.CopyBytes(value, buffer, ref index);
	}

	#endregion

	#region To[Type] non-ref index methods

	public static bool ToBool(this IByteConverter converter, byte[] buffer, int index)
	{
		Contracts.Requires.That(converter != null);

		return converter.ToBool(buffer, ref index);
	}

	public static char ToChar(this IByteConverter converter, byte[] buffer, int index)
	{
		Contracts.Requires.That(converter != null);

		return converter.ToChar(buffer, ref index);
	}

	public static float ToFloat(this IByteConverter converter, byte[] buffer, int index)
	{
		Contracts.Requires.That(converter != null);

		return converter.ToFloat(buffer, ref index);
	}

	public static double ToDouble(this IByteConverter converter, byte[] buffer, int index)
	{
		Contracts.Requires.That(converter != null);

		return converter.ToDouble(buffer, ref index);
	}

	public static decimal ToDecimal(this IByteConverter converter, byte[] buffer, int index)
	{
		Contracts.Requires.That(converter != null);

		return converter.ToDecimal(buffer, ref index);
	}

	public static byte ToByte(this IByteConverter converter, byte[] buffer, int index)
	{
		Contracts.Requires.That(converter != null);

		return converter.ToByte(buffer, ref index);
	}

	public static sbyte ToSByte(this IByteConverter converter, byte[] buffer, int index)
	{
		Contracts.Requires.That(converter != null);

		return converter.ToSByte(buffer, ref index);
	}

	public static short ToShort(this IByteConverter converter, byte[] buffer, int index)
	{
		Contracts.Requires.That(converter != null);

		return converter.ToShort(buffer, ref index);
	}

	public static ushort ToUShort(this IByteConverter converter, byte[] buffer, int index)
	{
		Contracts.Requires.That(converter != null);

		return converter.ToUShort(buffer, ref index);
	}

	public static int ToInt(this IByteConverter converter, byte[] buffer, int index)
	{
		Contracts.Requires.That(converter != null);

		return converter.ToInt(buffer, ref index);
	}

	public static uint ToUInt(this IByteConverter converter, byte[] buffer, int index)
	{
		Contracts.Requires.That(converter != null);

		return converter.ToUInt(buffer, ref index);
	}

	public static long ToLong(this IByteConverter converter, byte[] buffer, int index)
	{
		Contracts.Requires.That(converter != null);

		return converter.ToLong(buffer, ref index);
	}

	public static ulong ToULong(this IByteConverter converter, byte[] buffer, int index)
	{
		Contracts.Requires.That(converter != null);

		return converter.ToULong(buffer, ref index);
	}

	#endregion

	#region WriteBytes

	public static void WriteBytes(this IByteConverter converter, bool value, Action<byte> writeByte)
	{
		SharedContracts(converter, writeByte);

		writeByte(converter.GetByte(value));
	}

	public static void WriteBytes(this IByteConverter converter, char value, Action<byte> writeByte)
	{
		SharedContracts(converter, writeByte);

		PrepareBuffer();
		converter.CopyBytes(value, threadLocalBuffer, 0);
		WriteBytes(writeByte, ByteLength.Char);
	}

	public static void WriteBytes(this IByteConverter converter, float value, Action<byte> writeByte)
	{
		SharedContracts(converter, writeByte);

		PrepareBuffer();
		converter.CopyBytes(value, threadLocalBuffer, 0);
		WriteBytes(writeByte, ByteLength.Float);
	}

	public static void WriteBytes(this IByteConverter converter, double value, Action<byte> writeByte)
	{
		SharedContracts(converter, writeByte);

		PrepareBuffer();
		converter.CopyBytes(value, threadLocalBuffer, 0);
		WriteBytes(writeByte, ByteLength.Double);
	}

	public static void WriteBytes(this IByteConverter converter, decimal value, Action<byte> writeByte)
	{
		SharedContracts(converter, writeByte);

		PrepareBuffer();
		converter.CopyBytes(value, threadLocalBuffer, 0);
		WriteBytes(writeByte, ByteLength.Decimal);
	}

	public static void WriteBytes(this IByteConverter converter, byte value, Action<byte> writeByte)
	{
		SharedContracts(converter, writeByte);

		writeByte(value);
	}

	public static void WriteBytes(this IByteConverter converter, sbyte value, Action<byte> writeByte)
	{
		SharedContracts(converter, writeByte);

		writeByte(converter.GetByte(value));
	}

	public static void WriteBytes(this IByteConverter converter, short value, Action<byte> writeByte)
	{
		SharedContracts(converter, writeByte);

		PrepareBuffer();
		converter.CopyBytes(value, threadLocalBuffer, 0);
		WriteBytes(writeByte, ByteLength.Short);
	}

	public static void WriteBytes(this IByteConverter converter, ushort value, Action<byte> writeByte)
	{
		SharedContracts(converter, writeByte);

		PrepareBuffer();
		converter.CopyBytes(value, threadLocalBuffer, 0);
		WriteBytes(writeByte, ByteLength.UShort);
	}

	public static void WriteBytes(this IByteConverter converter, int value, Action<byte> writeByte)
	{
		SharedContracts(converter, writeByte);

		PrepareBuffer();
		converter.CopyBytes(value, threadLocalBuffer, 0);
		WriteBytes(writeByte, ByteLength.Int);
	}

	public static void WriteBytes(this IByteConverter converter, uint value, Action<byte> writeByte)
	{
		SharedContracts(converter, writeByte);

		PrepareBuffer();
		converter.CopyBytes(value, threadLocalBuffer, 0);
		WriteBytes(writeByte, ByteLength.UInt);
	}

	public static void WriteBytes(this IByteConverter converter, long value, Action<byte> writeByte)
	{
		SharedContracts(converter, writeByte);

		PrepareBuffer();
		converter.CopyBytes(value, threadLocalBuffer, 0);
		WriteBytes(writeByte, ByteLength.Long);
	}

	public static void WriteBytes(this IByteConverter converter, ulong value, Action<byte> writeByte)
	{
		SharedContracts(converter, writeByte);

		PrepareBuffer();
		converter.CopyBytes(value, threadLocalBuffer, 0);
		WriteBytes(writeByte, ByteLength.ULong);
	}

	#endregion

	[Conditional(Contracts.Requires.CompilationSymbol)]
	private static void SharedContracts(IByteConverter converter, Action<byte> writeByte)
	{
		Contracts.Requires.That(converter != null);
		Contracts.Requires.That(writeByte != null);
	}

	private static void PrepareBuffer()
	{
		if (threadLocalBuffer == null)
		{
			// buffer must be setup to be the longest of all the primitive sizes
			threadLocalBuffer = new byte[ByteLength.Decimal];
		}
	}

	private static void WriteBytes(Action<byte> writeByte, int serializedLength)
	{
		Contracts.Requires.That(writeByte != null);
		Contracts.Requires.That(serializedLength >= 0);

		for (int index = 0; index < serializedLength; index++)
		{
			writeByte(threadLocalBuffer[index]);
		}
	}
}
