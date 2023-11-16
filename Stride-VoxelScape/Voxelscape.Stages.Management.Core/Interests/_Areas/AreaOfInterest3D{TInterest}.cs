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
	public class AreaOfInterest3D<TInterest> : AbstractAreaOfInterest<Index3D, TInterest>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="AreaOfInterest3D{TInterest}" /> class.
		/// </summary>
		/// <param name="map">The interest map to add and remove interests from.</param>
		/// <param name="interest">The interest to add and remove.</param>
		/// <param name="origin">The origin of the area of interest to follow.</param>
		/// <param name="indices">The indices of the cells to apply interests to.</param>
		public AreaOfInterest3D(
			IInterestMap<Index3D, TInterest> map,
			TInterest interest,
			IObservable<Index3D> origin,
			IReadOnlySet<Index3D> indices)
			: base(map, interest, origin, indices)
		{
		}

		/// <inheritdoc />
		protected override Index3D Add(Index3D lhs, Index3D rhs) => lhs + rhs;

		/// <inheritdoc />
		protected override Index3D Subtract(Index3D lhs, Index3D rhs) => lhs - rhs;
	}
}
