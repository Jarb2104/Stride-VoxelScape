using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Collections
{
	internal class ReadOnlyListConverter<TSource, TResult> : IReadOnlyList<TResult>
	{
		private readonly IReadOnlyList<TSource> source;

		private readonly Converter<TSource, TResult> converter;

		public ReadOnlyListConverter(IReadOnlyList<TSource> source, Converter<TSource, TResult> converter)
		{
			Contracts.Requires.That(source != null);
			Contracts.Requires.That(converter != null);

			this.source = source;
			this.converter = converter;
		}

		/// <inheritdoc />
		public int Count => this.source.Count;

		/// <inheritdoc />
		public TResult this[int index]
		{
			get
			{
				IReadOnlyListContracts.Indexer(this, index);

				return this.converter(this.source[index]);
			}
		}

		/// <inheritdoc />
		public IEnumerator<TResult> GetEnumerator() =>
			this.source.Select(value => this.converter(value)).GetEnumerator();

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
	}
}
