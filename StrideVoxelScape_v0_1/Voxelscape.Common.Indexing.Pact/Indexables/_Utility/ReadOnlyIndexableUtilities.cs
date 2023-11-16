using Voxelscape.Common.Indexing.Pact.Indices;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Pact.Indexables
{
	/// <summary>
	///
	/// </summary>
	public static class ReadOnlyIndexableUtilities
	{
		public static bool TryGetValue<TIndex, TValue>(IReadOnlyIndexable<TIndex, TValue> instance, TIndex index, out TValue value)
			where TIndex : IIndex
		{
			Contracts.Requires.That(instance != null);
			IReadOnlyIndexableContracts.TryGetValue(instance, index);

			if (instance.IsIndexValid(index))
			{
				value = instance[index];
				return true;
			}
			else
			{
				value = default(TValue);
				return false;
			}
		}
	}
}
