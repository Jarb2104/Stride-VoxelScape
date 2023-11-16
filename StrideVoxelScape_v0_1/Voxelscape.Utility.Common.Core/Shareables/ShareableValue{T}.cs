using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Shareables;

namespace Voxelscape.Utility.Common.Core.Shareables
{
	/// <summary>
	///
	/// </summary>
	/// <typeparam name="T">The type of value.</typeparam>
	/// <threadsafety static="true" instance="true" />
	public class ShareableValue<T> : Shareable, IShareableValue<T>
	{
		public ShareableValue(T value)
			: this(value, 0)
		{
		}

		public ShareableValue(T value, int count)
			: base(count)
		{
			Contracts.Requires.That(count >= 0);

			this.Value = value;
		}

		/// <inheritdoc />
		public T Value { get; }
	}
}
