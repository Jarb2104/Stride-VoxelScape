using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Voxelscape.Stages.Management.Pact.Interests;
using Voxelscape.Utility.Common.Core.Collections;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for <see cref="IInterestMap{TKey, TInterest}"/>.
/// </summary>
public static class IInterestMapExtensions
{
	public static bool HasAnyInterests<TKey, TInterest>(
		this IInterestMap<TKey, TInterest> map, TKey index)
	{
		Contracts.Requires.That(map != null);

		return !map.Comparer.Equals(map[index], map.None);
	}

	public static void AddInterests<TKey, TInterest>(
		this IInterestMap<TKey, TInterest> map, TInterest interest, Voxelscape.Utility.Common.Pact.Collections.IReadOnlySet<TKey> addTo)
	{
		Contracts.Requires.That(map != null);
		Contracts.Requires.That(addTo.AllAndSelfNotNull());

		map.UpdateInterests(interest, addTo, ReadOnlySet.Empty<TKey>());
	}

	public static void RemoveInterests<TKey, TInterest>(
		this IInterestMap<TKey, TInterest> map, TInterest interest, Voxelscape.Utility.Common.Pact.Collections.IReadOnlySet<TKey> removeFrom)
	{
		Contracts.Requires.That(map != null);
		Contracts.Requires.That(removeFrom.AllAndSelfNotNull());

		map.UpdateInterests(interest, ReadOnlySet.Empty<TKey>(), removeFrom);
	}

	public static IObservable<TKey> GetInterestOpened<TKey, TInterest>(this IInterestMap<TKey, TInterest> map)
	{
		Contracts.Requires.That(map != null);

		return map.InterestsChanged.Where(changed =>
			map.Comparer.Equals(changed.Value.Previous, map.None) &&
			!map.Comparer.Equals(changed.Value.Next, map.None))
			.Select(changed => changed.Key);
	}

	public static IObservable<TKey> GetInterestClosed<TKey, TInterest>(this IInterestMap<TKey, TInterest> map)
	{
		Contracts.Requires.That(map != null);

		return map.InterestsChanged.Where(changed =>
			!map.Comparer.Equals(changed.Value.Previous, map.None) &&
			map.Comparer.Equals(changed.Value.Next, map.None))
			.Select(changed => changed.Key);
	}

	public static IObservable<KeyValuePair<TKey, bool>> GetInterestActivity<TKey, TInterest>(
		this IInterestMap<TKey, TInterest> map)
	{
		Contracts.Requires.That(map != null);

		return map.GetInterestOpened().Select(key => Voxelscape.Utility.Common.Core.Collections.KeyValuePair.New(key, true))
			.Merge(map.GetInterestClosed().Select(key => Voxelscape.Utility.Common.Core.Collections.KeyValuePair.New(key, false)));
	}
}
