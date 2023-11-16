using Voxelscape.Utility.Common.Core.Types;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Stores;

/// <summary>
/// Provides extension methods for <see cref="IValueKey{T}"/>.
/// </summary>
public static class IValueKeyExtensions
{
	public static TryValue<T> TryDeserialize<T>(this IValueKey<T> key, string value)
	{
		Contracts.Requires.That(key != null);

		T result;
		if (key.TryDeserialize(value, out result))
		{
			return TryValue.New(result);
		}
		else
		{
			return TryValue.None<T>();
		}
	}
}
