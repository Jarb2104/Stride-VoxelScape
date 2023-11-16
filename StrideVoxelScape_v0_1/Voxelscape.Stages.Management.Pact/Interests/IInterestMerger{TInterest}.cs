using System.Collections.Generic;

namespace Voxelscape.Stages.Management.Pact.Interests
{
	public interface IInterestMerger<TInterest>
	{
		IEqualityComparer<TInterest> Comparer { get; }

		TInterest None { get; }

		TInterest GetInterestByAdding(TInterest current, TInterest add);

		TInterest GetInterestByRemoving(TInterest current, TInterest remove);
	}
}
