using System.Diagnostics;
using Voxelscape.Utility.Common.Core.Mathematics;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Disposables;

namespace Voxelscape.Common.Contouring.Pact.Meshing
{
	// vertices should always be specified in clockwise winding order for this platform independent format
	public interface ITriangleGroupBuilder<TVertex> : IVisiblyDisposable
		where TVertex : struct
	{
		TriangleGroup Required { get; }

		TriangleGroup Current { get; }

		ITriangleGroupBuilder<TVertex> AddTriangleOffsets(byte offsetA, byte offsetB, byte offsetC);

		// returns the offset for that vertex
		byte AddVertex(TVertex vertex);
	}

	public static class ITriangleGroupBuilderContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void AddTriangleOffsets<TVertex>(
			ITriangleGroupBuilder<TVertex> instance, byte offsetA, byte offsetB, byte offsetC)
			where TVertex : struct
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(instance.Current.Triangles < instance.Required.Triangles);
			Contracts.Requires.That(offsetA.IsIn(Range.FromLength(instance.Required.Vertices)));
			Contracts.Requires.That(offsetB.IsIn(Range.FromLength(instance.Required.Vertices)));
			Contracts.Requires.That(offsetC.IsIn(Range.FromLength(instance.Required.Vertices)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void AddVertex<TVertex>(ITriangleGroupBuilder<TVertex> instance, TVertex vertex)
			where TVertex : struct
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
			Contracts.Requires.That(instance.Current.Vertices < instance.Required.Vertices);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Dispose<TVertex>(ITriangleGroupBuilder<TVertex> instance)
			where TVertex : struct
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(instance.Current == instance.Required);
		}
	}
}
