using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Core.Types;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for processing asynchronous enumerables.
/// </summary>
public static class AsyncIEnumerableExtensions
{
	public static async Task<T> SingleAsync<T>(this Task<IEnumerable<T>> values)
	{
		Contracts.Requires.That(values != null);

		return (await values.DontMarshallContext()).Single();
	}

	public static async Task<T> SingleOrDefaultAsync<T>(this Task<IEnumerable<T>> values)
	{
		Contracts.Requires.That(values != null);

		return (await values.DontMarshallContext()).SingleOrDefault();
	}

	public static async Task<TryValue<T>> SingleOrNoneAsync<T>(this Task<IEnumerable<T>> values)
	{
		Contracts.Requires.That(values != null);

		return (await values.DontMarshallContext()).SingleOrNone();
	}
}
