using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Data.Pact.Stores;

namespace Voxelscape.Utility.Data.Core.Stores
{
	public abstract class AbstractValueKey<T> : IValueKey<T>
	{
		public AbstractValueKey(string key)
		{
			Contracts.Requires.That(!key.IsNullOrWhiteSpace());

			this.Key = key;
		}

		/// <inheritdoc />
		public string Key { get; }

		/// <inheritdoc />
		public abstract string Serialize(T value);

		/// <inheritdoc />
		public abstract bool TryDeserialize(string value, out T result);
	}
}
