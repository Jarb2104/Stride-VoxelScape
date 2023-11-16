using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;

/// <summary>
/// Provides extension methods for the factory interfaces.
/// </summary>
public static class IFactoryExtensions
{
	public static IEnumerable<TResult> CreateMany<TResult>(this IFactory<TResult> factory, int count)
	{
		Contracts.Requires.That(factory != null);
		Contracts.Requires.That(count >= 0);

		for (int created = 0; created < count; created++)
		{
			yield return factory.Create();
		}
	}
}
