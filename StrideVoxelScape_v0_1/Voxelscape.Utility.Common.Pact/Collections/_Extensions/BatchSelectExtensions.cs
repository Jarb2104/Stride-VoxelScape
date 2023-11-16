using System;
using System.Collections.Generic;
using System.Linq;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for batching <see cref="IEnumerable{T}"/>.
/// </summary>
public static class BatchSelectExtensions
{
	public static IEnumerable<TResult> BatchSelectExact<TSource, TResult>(
		this IEnumerable<TSource> source, Func<TSource, TSource, TResult> batch)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(batch != null);
		Contracts.Requires.That(source.Count().IsDivisibleBy(2));

		return source.BatchSelect(batch);
	}

	public static IEnumerable<TResult> BatchSelectExact<TSource, TResult>(
		this IEnumerable<TSource> source, Func<TSource, TSource, TSource, TResult> batch)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(batch != null);
		Contracts.Requires.That(source.Count().IsDivisibleBy(3));

		return source.BatchSelect(batch);
	}

	public static IEnumerable<TResult> BatchSelectExact<TSource, TResult>(
		this IEnumerable<TSource> source, Func<TSource, TSource, TSource, TSource, TResult> batch)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(batch != null);
		Contracts.Requires.That(source.Count().IsDivisibleBy(4));

		return source.BatchSelect(batch);
	}

	public static IEnumerable<TResult> BatchSelect<TSource, TResult>(
		this IEnumerable<TSource> source, Func<TSource, TSource, TResult> batch)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(batch != null);

		using (var enumerator = source.GetEnumerator())
		{
			while (true)
			{
				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value1 = enumerator.Current;

				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value2 = enumerator.Current;

				yield return batch(value1, value2);
			}
		}
	}

	public static IEnumerable<TResult> BatchSelect<TSource, TResult>(
		this IEnumerable<TSource> source, Func<TSource, TSource, TSource, TResult> batch)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(batch != null);

		using (var enumerator = source.GetEnumerator())
		{
			while (true)
			{
				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value1 = enumerator.Current;

				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value2 = enumerator.Current;

				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value3 = enumerator.Current;

				yield return batch(value1, value2, value3);
			}
		}
	}

	public static IEnumerable<TResult> BatchSelect<TSource, TResult>(
		this IEnumerable<TSource> source, Func<TSource, TSource, TSource, TSource, TResult> batch)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(batch != null);

		using (var enumerator = source.GetEnumerator())
		{
			while (true)
			{
				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value1 = enumerator.Current;

				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value2 = enumerator.Current;

				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value3 = enumerator.Current;

				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value4 = enumerator.Current;

				yield return batch(value1, value2, value3, value4);
			}
		}
	}

	public static IEnumerable<TResult> BatchSelectExact<TSource, TResult>(
		this IEnumerable<TSource> source,
		Func<TSource, TSource, TSource, TSource, TSource, TResult> batch)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(batch != null);
		Contracts.Requires.That(source.Count().IsDivisibleBy(5));

		return source.BatchSelect(batch);
	}

	public static IEnumerable<TResult> BatchSelectExact<TSource, TResult>(
		this IEnumerable<TSource> source,
		Func<TSource, TSource, TSource, TSource, TSource, TSource, TResult> batch)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(batch != null);
		Contracts.Requires.That(source.Count().IsDivisibleBy(6));

		return source.BatchSelect(batch);
	}

	public static IEnumerable<TResult> BatchSelectExact<TSource, TResult>(
		this IEnumerable<TSource> source,
		Func<TSource, TSource, TSource, TSource, TSource, TSource, TSource, TResult> batch)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(batch != null);
		Contracts.Requires.That(source.Count().IsDivisibleBy(7));

		return source.BatchSelect(batch);
	}

	public static IEnumerable<TResult> BatchSelectExact<TSource, TResult>(
		this IEnumerable<TSource> source,
		Func<TSource, TSource, TSource, TSource, TSource, TSource, TSource, TSource, TResult> batch)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(batch != null);
		Contracts.Requires.That(source.Count().IsDivisibleBy(8));

		return source.BatchSelect(batch);
	}

	public static IEnumerable<TResult> BatchSelect<TSource, TResult>(
		this IEnumerable<TSource> source,
		Func<TSource, TSource, TSource, TSource, TSource, TResult> batch)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(batch != null);

		using (var enumerator = source.GetEnumerator())
		{
			while (true)
			{
				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value1 = enumerator.Current;

				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value2 = enumerator.Current;

				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value3 = enumerator.Current;

				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value4 = enumerator.Current;

				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value5 = enumerator.Current;

				yield return batch(value1, value2, value3, value4, value5);
			}
		}
	}

	public static IEnumerable<TResult> BatchSelect<TSource, TResult>(
		this IEnumerable<TSource> source,
		Func<TSource, TSource, TSource, TSource, TSource, TSource, TResult> batch)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(batch != null);

		using (var enumerator = source.GetEnumerator())
		{
			while (true)
			{
				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value1 = enumerator.Current;

				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value2 = enumerator.Current;

				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value3 = enumerator.Current;

				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value4 = enumerator.Current;

				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value5 = enumerator.Current;

				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value6 = enumerator.Current;

				yield return batch(value1, value2, value3, value4, value5, value6);
			}
		}
	}

	public static IEnumerable<TResult> BatchSelect<TSource, TResult>(
		this IEnumerable<TSource> source,
		Func<TSource, TSource, TSource, TSource, TSource, TSource, TSource, TResult> batch)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(batch != null);

		using (var enumerator = source.GetEnumerator())
		{
			while (true)
			{
				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value1 = enumerator.Current;

				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value2 = enumerator.Current;

				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value3 = enumerator.Current;

				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value4 = enumerator.Current;

				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value5 = enumerator.Current;

				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value6 = enumerator.Current;

				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value7 = enumerator.Current;

				yield return batch(value1, value2, value3, value4, value5, value6, value7);
			}
		}
	}

	public static IEnumerable<TResult> BatchSelect<TSource, TResult>(
		this IEnumerable<TSource> source,
		Func<TSource, TSource, TSource, TSource, TSource, TSource, TSource, TSource, TResult> batch)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(batch != null);

		using (var enumerator = source.GetEnumerator())
		{
			while (true)
			{
				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value1 = enumerator.Current;

				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value2 = enumerator.Current;

				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value3 = enumerator.Current;

				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value4 = enumerator.Current;

				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value5 = enumerator.Current;

				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value6 = enumerator.Current;

				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value7 = enumerator.Current;

				if (!enumerator.MoveNext())
				{
					yield break;
				}

				TSource value8 = enumerator.Current;

				yield return batch(value1, value2, value3, value4, value5, value6, value7, value8);
			}
		}
	}
}
