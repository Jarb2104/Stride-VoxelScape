using System;
using System.Collections.Generic;
using System.Linq;
using Voxelscape.Common.Contouring.Pact.Textures;
using Voxelscape.Common.Indexing.Core.Enumerables;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Utility.Common.Core.Collections;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Contouring.Core.Textures
{
	public class EnumKeyTextureAtlasBuilder<TEnumKey>
		where TEnumKey : struct, IComparable, IFormattable, IConvertible
	{
		private readonly IDictionary<TEnumKey, ITextureSwatch> swatches =
			EnumDictionary.Create<TEnumKey, ITextureSwatch>();

		private readonly bool[,] reservedSwatches;

		private readonly Index2D atlasPixelDimensions;

		private readonly Index2D swatchPixelDimensions;

		private readonly bool invertXAxis;

		private readonly bool invertYAxis;

		public EnumKeyTextureAtlasBuilder(
			Index2D atlasPixelDimensions,
			Index2D swatchPixelDimensions,
			bool invertXAxis = false,
			bool invertYAxis = false)
		{
			Contracts.Requires.That(typeof(TEnumKey).IsEnum);
			Contracts.Requires.That(atlasPixelDimensions.X.IsDivisibleBy(swatchPixelDimensions.X));
			Contracts.Requires.That(atlasPixelDimensions.Y.IsDivisibleBy(swatchPixelDimensions.Y));

			this.reservedSwatches = (atlasPixelDimensions / swatchPixelDimensions).CreateArray<bool>();
			this.atlasPixelDimensions = atlasPixelDimensions;
			this.swatchPixelDimensions = swatchPixelDimensions;
			this.invertXAxis = invertXAxis;
			this.invertYAxis = invertYAxis;
		}

		public bool ContainsKey(TEnumKey key)
		{
			return this.swatches.ContainsKey(key);
		}

		public bool IsSwatchPositionAvailable(Index2D position)
		{
			if (!this.reservedSwatches.IsIndexValid(position))
			{
				return false;
			}

			return !this.reservedSwatches[position.X, position.Y];
		}

		public void AddSwatch(TEnumKey key, Index2D position)
		{
			this.AddSwatch(key, position, new Index2D(1, 1));
		}

		public void AddSwatch(TEnumKey key, Index2D position, Index2D dimensions)
		{
			Contracts.Requires.That(!this.ContainsKey(key));
			Contracts.Requires.That(dimensions.IsAllPositive());
			Contracts.Requires.That(
                Indexing.Core.Enumerables.Index.Range(position, dimensions).All(index => this.IsSwatchPositionAvailable(index)));

			foreach (var index in Indexing.Core.Enumerables.Index.Range(position, dimensions))
			{
				this.reservedSwatches[index.X, index.Y] = true;
			}

			float left, right, top, bottom;

			if (this.invertXAxis)
			{
				CalculateAxis(
					position.X, dimensions.X, this.swatchPixelDimensions.X, this.atlasPixelDimensions.X, out right, out left);
			}
			else
			{
				CalculateAxis(
					position.X, dimensions.X, this.swatchPixelDimensions.X, this.atlasPixelDimensions.X, out left, out right);
			}

			if (this.invertYAxis)
			{
				CalculateAxis(
					position.Y, dimensions.Y, this.swatchPixelDimensions.Y, this.atlasPixelDimensions.Y, out bottom, out top);
			}
			else
			{
				CalculateAxis(
					position.Y, dimensions.Y, this.swatchPixelDimensions.Y, this.atlasPixelDimensions.Y, out top, out bottom);
			}

			this.swatches[key] = new RectangleTextureSwatch(left, right, top, bottom);
		}

		public ITextureAtlas<TEnumKey> Build()
		{
			EnumDictionary.ThrowIfMissingEnumKey(this.swatches);

			var swatches = EnumDictionary.Create<TEnumKey, ITextureSwatch>();

			foreach (var pair in this.swatches)
			{
				swatches[pair.Key] = pair.Value;
			}

			return new TextureAtlas<TEnumKey>(swatches);
		}

		private static void CalculateAxis(
			float position, float length, float swatchLength, float atlasLength, out float start, out float end)
		{
			start = (swatchLength * position) / atlasLength;
			end = (swatchLength * (position + length)) / atlasLength;
		}
	}
}
