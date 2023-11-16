using System.Collections;
using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Conversions;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Collections
{
	public class ReadOnlySetConverter<TSource, TResult> : Voxelscape.Utility.Common.Pact.Collections.IReadOnlySet<TResult>
	{
		private readonly Voxelscape.Utility.Common.Pact.Collections.IReadOnlySet<TSource> set;

		private readonly ITwoWayConverter<TSource, TResult> converter;

		public ReadOnlySetConverter(Voxelscape.Utility.Common.Pact.Collections.IReadOnlySet<TSource> set, ITwoWayConverter<TSource, TResult> converter)
		{
			Contracts.Requires.That(set != null);
			Contracts.Requires.That(converter != null);

			this.set = set;
			this.converter = converter;
		}

		/// <inheritdoc />
		public int Count => this.set.Count;

		/// <inheritdoc />
		public bool Contains(TResult value) => this.set.Contains(this.converter.Convert(value));

		/// <inheritdoc />
		public IEnumerator<TResult> GetEnumerator()
		{
			foreach (var value in this.set)
			{
				yield return this.converter.Convert(value);
			}
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
}
