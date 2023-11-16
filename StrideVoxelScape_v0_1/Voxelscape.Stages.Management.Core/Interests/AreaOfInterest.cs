using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using Stride.Core.Mathematics;
using Voxelscape.Common.Indexing.Core.Arrays;
using Voxelscape.Common.Indexing.Core.Enumerables;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Common.Indexing.Core.Utility;
using Voxelscape.Common.Indexing.Pact.Bounds;
using Voxelscape.Common.Indexing.Pact.Indices;
using Voxelscape.Common.Indexing.Pact.Rasterization;
using Voxelscape.Stages.Management.Pact.Interests;
using Voxelscape.Utility.Common.Core.Collections;
using Voxelscape.Utility.Common.Pact.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;

namespace Voxelscape.Stages.Management.Core.Interests
{
	/// <summary>
	///
	/// </summary>
	public static class AreaOfInterest
	{
		private static bool RoundUp => true;

		public static IVisiblyDisposable CreateCenteredSpiral<TInterest>(
			IInterestMap<Index3D, TInterest> map,
			TInterest interest,
			IObservable<Vector3> origin,
			IRasterizableMask<Index3D, bool> shape,
			float cellLength)
		{
			Contracts.Requires.That(map != null);
			Contracts.Requires.That(origin != null);
			Contracts.Requires.That(shape != null);
			Contracts.Requires.That(cellLength > 0);

			var rasterizedShape = shape.Rasterize(cellLength, true, false);

			return CreateCenteredSpiral(
				map,
				interest,
				origin.FilterToChunkKeys(new Vector3(cellLength), rasterizedShape.Dimensions),
				rasterizedShape);
		}

		public static IVisiblyDisposable CreateCenteredSpiral<TInterest>(
			IInterestMap<Index3D, TInterest> map,
			TInterest interest,
			IObservable<Index3D> origin,
			IBoundedIndexable<Index3D, bool> shape)
		{
			Contracts.Requires.That(map != null);
			Contracts.Requires.That(origin != null);
			Contracts.Requires.That(shape != null);

			return new AreaOfInterest3D<TInterest>(
				map, interest, origin, Keys.CreateCenteredSpiral(shape));
		}

		public static class Keys
		{
			public static Utility.Common.Pact.Collections.IReadOnlySet<TIndex> Create<TIndex>(
				IBoundedIndexable<TIndex, bool> shape,
				IEnumerable<TIndex> ordering = null,
				IEqualityComparer<TIndex> comparer = null)
				where TIndex : struct, IIndex
			{
				Contracts.Requires.That(shape != null);

				if (ordering != null)
				{
					ordering = ordering.Where(index =>
					{
						bool isInsideShape;
						return shape.TryGetValue(index, out isInsideShape) && isInsideShape;
					});
				}
				else
				{
					ordering = shape.Where(pair => pair.Value).Select(pair => pair.Key);
				}

				return ReadOnlySet.CreateOrdered(ordering, comparer);
			}

			public static Utility.Common.Pact.Collections.IReadOnlySet<Index2D> CreateCenteredSpiral(IBoundedIndexable<Index2D, bool> shape)
			{
				Contracts.Requires.That(shape != null);

				shape = OffsetArray.CenterOnZero(shape, RoundUp);
				return Create(shape, Spiral(shape.LowerBounds, shape.UpperBounds));
			}

			public static Utility.Common.Pact.Collections.IReadOnlySet<Index3D> CreateCenteredSpiral(IBoundedIndexable<Index3D, bool> shape)
			{
				Contracts.Requires.That(shape != null);

				shape = OffsetArray.CenterOnZero(shape, RoundUp);
				return Create(shape, SpiralColumns(shape.LowerBounds, shape.UpperBounds));
			}

			private static IEnumerable<Index2D> Spiral(Index2D lowerBounds, Index2D upperBounds)
			{
				Contracts.Requires.That(lowerBounds.X <= upperBounds.X);
				Contracts.Requires.That(lowerBounds.Y <= upperBounds.Y);
				Contracts.Requires.That(MiddleIndex.Get(lowerBounds, upperBounds, RoundUp) == Index2D.Zero);

				return new Spiral2D(new Spiral2D.Options()
				{
					Spirals = Math.Max(
						Math.Max(Math.Abs(lowerBounds.X), Math.Abs(lowerBounds.Y)),
						Math.Max(Math.Abs(upperBounds.X), Math.Abs(upperBounds.Y))),
				});
			}

			private static IEnumerable<Index3D> SpiralColumns(Index3D lowerBounds, Index3D upperBounds)
			{
				Contracts.Requires.That(lowerBounds.X <= upperBounds.X);
				Contracts.Requires.That(lowerBounds.Y <= upperBounds.Y);
				Contracts.Requires.That(lowerBounds.Z <= upperBounds.Z);
				Contracts.Requires.That(MiddleIndex.Get(lowerBounds, upperBounds, RoundUp) == Index3D.Zero);

				foreach (var index in Spiral(lowerBounds.ProjectDownYAxis(), upperBounds.ProjectDownYAxis()))
				{
					yield return new Index3D(index.X, 0, index.Y);
					int yUp = 1;
					int yDown = -1;
					bool continueLoop = true;
					while (continueLoop)
					{
						continueLoop = false;

						if (yUp <= upperBounds.Y)
						{
							yield return new Index3D(index.X, yUp, index.Y);
							yUp++;
							continueLoop = true;
						}

						if (yDown >= lowerBounds.Y)
						{
							yield return new Index3D(index.X, yDown, index.Y);
							yDown--;
							continueLoop = true;
						}
					}
				}
			}
		}
	}
}
