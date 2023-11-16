using System.Collections.Generic;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	public class CollectionConstantSerializer<T> : AbstractConstantEnumerableInlineSerializer<ICollection<T>, T>
	{
		public CollectionConstantSerializer(
			IConstantSerializerDeserializer<T> valueSerializer, IConstantSerializerDeserializer<int> countSerializer)
			: base(valueSerializer, countSerializer)
		{
		}

		/// <inheritdoc />
		protected override int GetCount(ICollection<T> value) => value.Count;

		/// <inheritdoc />
		protected override ICollection<T> Create(int countOfValues) => new List<T>(countOfValues);

		/// <inheritdoc />
		protected override void DeserializeValuesInline(
			int countOfValues, IBufferedArray buffer, ICollection<T> result)
		{
			for (int count = 0; count < countOfValues; count++)
			{
				result.Add(this.ValueSerializer.Deserialize(buffer));
			}
		}

		/// <inheritdoc />
		protected override void DeserializeValuesInline(
			int countOfValues, byte[] buffer, ref int index, ICollection<T> result)
		{
			for (int count = 0; count < countOfValues; count++)
			{
				result.Add(this.ValueSerializer.Deserialize(buffer, ref index));
			}
		}
	}
}
