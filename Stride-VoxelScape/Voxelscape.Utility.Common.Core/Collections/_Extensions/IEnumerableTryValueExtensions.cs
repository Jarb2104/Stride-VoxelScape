using System;
using System.Collections.Generic;
using System.Linq;
using Voxelscape.Utility.Common.Core.Types;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for converting the results of some <see cref="Enumerable"/> methods
/// into <see cref="TryValue{T}"/>.
/// </summary>
public static class IEnumerableTryValueExtensions
{
	public static TryValue<T> ElementAtOrNone<T>(this IEnumerable<T> values, int index)
	{
		Contracts.Requires.That(values != null);
		Contracts.Requires.That(index >= 0);

		var readOnlyList = values as IReadOnlyList<T>;
		if (readOnlyList != null)
		{
			return index.IsIn(readOnlyList.GetIndexRange()) ? TryValue.New(readOnlyList[index]) : TryValue.None<T>();
		}

		var list = values as IList<T>;
		if (list != null)
		{
			return index.IsIn(list.GetListIndexRange()) ? TryValue.New(list[index]) : TryValue.None<T>();
		}

		int count = 0;
		foreach (var value in values)
		{
			if (count == index)
			{
				return TryValue.New(value);
			}

			count++;
		}

		return TryValue.None<T>();
	}

	public static TryValue<T> FirstOrNone<T>(this IEnumerable<T> values)
	{
		Contracts.Requires.That(values != null);

		return values.IsEmpty() ? TryValue.None<T>() : TryValue.New(values.First());
	}

	public static TryValue<T> FirstOrNone<T>(this IEnumerable<T> values, Func<T, bool> predicate)
	{
		Contracts.Requires.That(values != null);
		Contracts.Requires.That(predicate != null);

		return values.Select(value => TryValue.New(value)).FirstOrDefault(value => predicate(value.Value));
	}

	public static TryValue<T> LastOrNone<T>(this IEnumerable<T> values)
	{
		Contracts.Requires.That(values != null);

		return values.IsEmpty() ? TryValue.None<T>() : TryValue.New(values.Last());
	}

	public static TryValue<T> LastOrNone<T>(this IEnumerable<T> values, Func<T, bool> predicate)
	{
		Contracts.Requires.That(values != null);
		Contracts.Requires.That(predicate != null);

		return values.Select(value => TryValue.New(value)).LastOrDefault(value => predicate(value.Value));
	}

	public static TryValue<T> SingleOrNone<T>(this IEnumerable<T> values)
	{
		Contracts.Requires.That(values != null);

		return values.IsEmpty() ? TryValue.None<T>() : TryValue.New(values.Single());
	}

	public static TryValue<T> SingleOrNone<T>(this IEnumerable<T> values, Func<T, bool> predicate)
	{
		Contracts.Requires.That(values != null);
		Contracts.Requires.That(predicate != null);

		return values.Select(value => TryValue.New(value)).SingleOrDefault(value => predicate(value.Value));
	}
}
