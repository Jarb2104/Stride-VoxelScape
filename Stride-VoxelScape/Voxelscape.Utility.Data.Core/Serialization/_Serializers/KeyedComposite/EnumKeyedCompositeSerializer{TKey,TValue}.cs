using System;
using System.Collections.Generic;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Types;
using Voxelscape.Utility.Data.Pact.Serialization;

namespace Voxelscape.Utility.Data.Core.Serialization
{
	public class EnumKeyedCompositeSerializer<TKey, TValue> : KeyedCompositeSerializer<TKey, TValue>
		where TValue : IKeyed<TKey>
		where TKey : struct, IComparable, IFormattable, IConvertible
	{
		public EnumKeyedCompositeSerializer(
			ISerializerDeserializer<TKey> keySerializer,
			IReadOnlyDictionary<TKey, ISerializerDeserializer<TValue>> serializers)
			: base(keySerializer, serializers)
		{
			Contracts.Requires.That(typeof(TKey).IsEnum);
		}
	}
}
