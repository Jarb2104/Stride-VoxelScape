using System.Collections.Generic;
using Voxelscape.Stages.Management.Core.Interests;
using Voxelscape.Stages.Management.Pact.Interests;
using Voxelscape.Stages.Management.Pact.Stages;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;
using Voxelscape.Utility.Common.Pact.Factories;

namespace Voxelscape.Stages.Management.Core.Stages
{
	public class InterestStage<TKey, TInterest, TChunk> : AbstractDisposable
	{
		public InterestStage(
			IFactory<TKey, TChunk> factory, IInterestMerger<TInterest> merger)
			: this(factory, merger, null)
		{
		}

		public InterestStage(
			IFactory<TKey, TChunk> factory, IInterestMerger<TInterest> merger, IEqualityComparer<TKey> comparer)
		{
			Contracts.Requires.That(factory != null);
			Contracts.Requires.That(merger != null);

			this.Interests = new InterestMap<TKey, TInterest>(merger, comparer);
			this.Chunks = new Stage<TKey, TChunk>(this.Interests.GetInterestActivity(), factory, comparer);
		}

		public InterestStage(
			IFactory<TKey, TChunk> factory,
			IInterestMerger<TInterest> merger,
			IDictionary<TKey, TInterest> map,
			IDictionary<TKey, TChunk> stage)
		{
			Contracts.Requires.That(factory != null);
			Contracts.Requires.That(merger != null);
			Contracts.Requires.That(map != null);
			Contracts.Requires.That(stage != null);

			this.Interests = new InterestMap<TKey, TInterest>(merger, map);
			this.Chunks = new Stage<TKey, TChunk>(this.Interests.GetInterestActivity(), factory, stage);
		}

		public IInterestMap<TKey, TInterest> Interests { get; }

		public IStage<TKey, TChunk> Chunks { get; }

		/// <inheritdoc />
		protected override void ManagedDisposal()
		{
			this.Interests.Dispose();
			this.Chunks.Dispose();
		}
	}
}
