using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Voxelscape.Stages.Management.Pact.Interests;
using Voxelscape.Utility.Common.Core.Collections;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Conversions;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Concurrency.Core.Reactive;

namespace Voxelscape.Stages.Management.Core.Interests
{
	public class ConverterInterestMap<TSource, TResult, TInterest> : IInterestMap<TResult, TInterest>
	{
		private readonly IInterestMap<TSource, TInterest> map;

		private readonly ITwoWayConverter<TResult, TSource> converter;

		public ConverterInterestMap(
			IInterestMap<TSource, TInterest> map, ITwoWayConverter<TResult, TSource> converter)
		{
			Contracts.Requires.That(map != null);
			Contracts.Requires.That(converter != null);

			this.map = map;
			this.converter = converter;
		}

		/// <inheritdoc />
		public TInterest None => this.map.None;

		/// <inheritdoc />
		public IEqualityComparer<TInterest> Comparer => this.map.Comparer;

		/// <inheritdoc />
		public IObservable<KeyValuePair<TResult, SequentialPair<TInterest>>> InterestsChanged =>
			this.map.InterestsChanged.Select(pair => Utility.Common.Core.Collections.KeyValuePair.New(this.converter.Convert(pair.Key), pair.Value));

		/// <inheritdoc />
		public bool IsDisposed => this.map.IsDisposed;

		/// <inheritdoc />
		public TInterest this[TResult key] => this.map[this.converter.Convert(key)];

		/// <inheritdoc />
		public void AddInterest(TResult key, TInterest interest) =>
			this.map.AddInterest(this.converter.Convert(key), interest);

		/// <inheritdoc />
		public void Dispose() => this.map.Dispose();

		/// <inheritdoc />
		public void RemoveInterest(TResult key, TInterest interest) =>
			this.map.RemoveInterest(this.converter.Convert(key), interest);

		/// <inheritdoc />
		public void UpdateInterests(
			TInterest interest, Utility.Common.Pact.Collections.IReadOnlySet<TResult> addTo, Utility.Common.Pact.Collections.IReadOnlySet<TResult> removeFrom) =>
			this.map.UpdateInterests(
				interest,
				ReadOnlySet.Convert(addTo, this.converter),
				ReadOnlySet.Convert(removeFrom, this.converter));
	}
}
