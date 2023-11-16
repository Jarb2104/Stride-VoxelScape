using System;
using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Disposables;

namespace Voxelscape.Stages.Management.Pact.Stages
{
	public interface IStage<TKey, TChunk> : IReadOnlyDictionary<TKey, TChunk>, IVisiblyDisposable
	{
		IObservable<KeyValuePair<TKey, TChunk>> Activated { get; }

		IObservable<KeyValuePair<TKey, TChunk>> Deactivated { get; }
	}
}
