using System.Diagnostics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Management.Pact.Persistence
{
	/// <summary>
	///
	/// </summary>
	/// <typeparam name="TChunk">The type of the chunk.</typeparam>
	/// <typeparam name="TPersistable">The type of the persistable chunk.</typeparam>
	public interface IChunkPersister<TChunk, TPersistable>
	{
		TPersistable ToPersistable(TChunk chunk);

		void FromPersistable(TPersistable persistable, TChunk chunk);
	}

	public static class IChunkPersisterContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void ToPersistable<TChunk>(TChunk chunk)
		{
			Contracts.Requires.That(chunk != null);
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void FromPersistable<TPersistable, TChunk>(TPersistable persistable, TChunk chunk)
		{
			Contracts.Requires.That(persistable != null);
			Contracts.Requires.That(chunk != null);
		}
	}
}
