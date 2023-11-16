using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Conversions;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Collections
{
	/// <summary>
	///
	/// </summary>
	public static class ReadOnlySet
	{
		public static Pact.Collections.IReadOnlySet<TResult> Convert<TSource, TResult>(
            Pact.Collections.IReadOnlySet<TSource> set, ITwoWayConverter<TSource, TResult> converter) =>
			new ReadOnlySetConverter<TSource, TResult>(set, converter);

		public static Pact.Collections.IReadOnlySet<T> CreateUnordered<T>(IEnumerable<T> values, IEqualityComparer<T> comparer = null)
		{
			Contracts.Requires.That(values != null);

			return new ReadOnlySet<T>(new HashSet<T>(values, comparer));
		}

		public static Pact.Collections.IReadOnlySet<T> CreateOrdered<T>(IEnumerable<T> values, IEqualityComparer<T> comparer = null)
		{
			Contracts.Requires.That(values != null);

			return new ReadOnlySet<T>(new OrderedHashSet<T>(values, comparer));
		}

		public static Pact.Collections.IReadOnlySet<T> Empty<T>() => EmptySet<T>.Instance;

		private class EmptySet<T> : Pact.Collections.IReadOnlySet<T>
        {
			private EmptySet()
			{
			}

			public static Pact.Collections.IReadOnlySet<T> Instance { get; } = new EmptySet<T>();

			/// <inheritdoc />
			public int Count => 0;

			/// <inheritdoc />
			public bool Contains(T value) => false;

			/// <inheritdoc />
			public IEnumerator<T> GetEnumerator() => Enumerable.Empty<T>().GetEnumerator();

			/// <inheritdoc />
			IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
		}
	}
}
