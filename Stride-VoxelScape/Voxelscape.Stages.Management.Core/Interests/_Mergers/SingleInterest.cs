using System.Collections.Generic;
using Voxelscape.Stages.Management.Pact.Interests;

namespace Voxelscape.Stages.Management.Core.Interests
{
	/// <summary>
	///
	/// </summary>
	public class SingleInterest : IInterestMerger<bool>
	{
		private SingleInterest()
		{
		}

		public static SingleInterest Merger { get; } = new SingleInterest();

		/// <inheritdoc />
		public IEqualityComparer<bool> Comparer => EqualityComparer<bool>.Default;

		/// <inheritdoc />
		public bool None => false;

		/// <inheritdoc />
		public bool GetInterestByAdding(bool current, bool add) => current | add;

		/// <inheritdoc />
		public bool GetInterestByRemoving(bool current, bool remove) => remove ? false : current;
	}
}
