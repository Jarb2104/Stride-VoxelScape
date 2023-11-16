using System.Collections.Generic;
using Voxelscape.Common.Contouring.Pact.Textures;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Contouring.Core.Textures
{
	public class TextureAtlas<TKey> : ITextureAtlas<TKey>
	{
		private readonly IReadOnlyDictionary<TKey, ITextureSwatch> swatches;

		public TextureAtlas(IReadOnlyDictionary<TKey, ITextureSwatch> swatches)
		{
			Contracts.Requires.That(swatches != null);

			this.swatches = swatches;
		}

		public ITextureSwatch this[TKey textureSwatchKey]
		{
			get
			{
				ITextureAtlasContracts.Indexer(this, textureSwatchKey);

				return this.swatches[textureSwatchKey];
			}
		}

		public bool ContainsKey(TKey textureSwatchKey)
		{
			ITextureAtlasContracts.ContainsKey(textureSwatchKey);

			return this.swatches.ContainsKey(textureSwatchKey);
		}
	}
}
