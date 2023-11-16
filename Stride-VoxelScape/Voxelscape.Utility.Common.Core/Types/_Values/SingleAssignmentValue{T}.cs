using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Types
{
	/// <summary>
	/// A value that starts not assigned and can be assigned once and only once.
	/// </summary>
	/// <typeparam name="T">The type of the value.</typeparam>
	public class SingleAssignmentValue<T>
	{
		/// <summary>
		/// The value.
		/// </summary>
		private TryValue<T> value = TryValue.None<T>();

		/// <summary>
		/// Gets or sets the value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		/// <remarks>
		/// In order to retrieve the value there must already be a value assigned.
		/// The value can only be assigned once.
		/// </remarks>
		public T Value
		{
			get
			{
				Contracts.Requires.That(this.HasValue);

				return this.value.Value;
			}

			set
			{
				Contracts.Requires.That(!this.HasValue);

				this.value = TryValue.New(value);
			}
		}

		/// <summary>
		/// Gets a value indicating whether this instance has a value assigned.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has a value; otherwise, <c>false</c>.
		/// </value>
		public bool HasValue => this.value.HasValue;
	}
}
