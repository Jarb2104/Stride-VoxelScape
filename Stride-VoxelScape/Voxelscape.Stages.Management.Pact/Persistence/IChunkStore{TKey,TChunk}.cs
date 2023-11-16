using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Core.Types;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Stages.Management.Pact.Persistence
{
	/// <summary>
	///
	/// </summary>
	/// <typeparam name="TKey">The type of the key.</typeparam>
	/// <typeparam name="TChunk">The type of the chunk.</typeparam>
	public interface IChunkStore<TKey, TChunk>
	{
		Task AddOrUpdateAllAsync(
			IEnumerable<TChunk> chunks, CancellationToken cancellation = default(CancellationToken));

		Task<TryValue<TChunk>> TryGetAsync(
			TKey key, CancellationToken cancellation = default(CancellationToken));
	}

	public static class IChunkStoreContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void AddOrUpdateAllAsync<TChunk>(IEnumerable<TChunk> chunks)
		{
			Contracts.Requires.That(chunks.AllAndSelfNotNull());
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void TryGetAsync<TKey>(TKey key)
		{
			Contracts.Requires.That(key != null);
		}
	}
}
