using System.Diagnostics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Contouring.Pact.Textures
{
	public interface ITextureAtlas<TKey>
	{
		ITextureSwatch this[TKey textureSwatchKey] { get; }

		bool ContainsKey(TKey textureSwatchKey);
	}

	public static class ITextureAtlasContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Indexer<TKey>(ITextureAtlas<TKey> instance, TKey textureSwatchKey)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(textureSwatchKey != null);
			Contracts.Requires.That(instance.ContainsKey(textureSwatchKey));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void ContainsKey<TKey>(TKey textureSwatchKey)
		{
			Contracts.Requires.That(textureSwatchKey != null);
		}
	}
}
