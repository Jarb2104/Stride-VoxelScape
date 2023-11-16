using System;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Stages.Management.Pact.Interests;
using Voxelscape.Utility.Common.Pact.Collections;

namespace Voxelscape.Stages.Management.Core.Interests
{
	/// <summary>
	/// A four dimensional implementation of <see cref="AbstractAreaOfInterest{TKey, TInterest}"/>.
	/// </summary>
	/// <typeparam name="TInterest">The type of the interest.</typeparam>
	public class AreaOfInterest4D<TInterest> : AbstractAreaOfInterest<Index4D, TInterest>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AreaOfInterest4D{TInterest}" /> class.
		/// </summary>
		/// <param name="map">The interest map to add and remove interests from.</param>
		/// <param name="interest">The interest to add and remove.</param>
		/// <param name="origin">The origin of the area of interest to follow.</param>
		/// <param name="indices">The indices of the cells to apply interests to.</param>
		public AreaOfInterest4D(
			IInterestMap<Index4D, TInterest> map,
			TInterest interest,
			IObservable<Index4D> origin,
			IReadOnlySet<Index4D> indices)
			: base(map, interest, origin, indices)
		{
		}

		/// <inheritdoc />
		protected override Index4D Add(Index4D lhs, Index4D rhs) => lhs + rhs;

		/// <inheritdoc />
		protected override Index4D Subtract(Index4D lhs, Index4D rhs) => lhs - rhs;
	}
}
