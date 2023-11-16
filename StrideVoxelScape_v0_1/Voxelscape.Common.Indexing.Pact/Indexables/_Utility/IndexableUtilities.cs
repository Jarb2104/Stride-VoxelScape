using Voxelscape.Common.Indexing.Pact.Indices;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Pact.Indexables
{
	/// <summary>
	///
	/// </summary>
	public static class IndexableUtilities
	{
		public static bool TrySetValue<TIndex, TValue>(IIndexable<TIndex, TValue> instance, TIndex index, TValue value)
			where TIndex : IIndex
		{
			Contracts.Requires.That(instance != null);
			IIndexableContracts.TrySetValue(instance, index);

			if (instance.IsIndexValid(index))
			{
				instance[index] = value;
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
