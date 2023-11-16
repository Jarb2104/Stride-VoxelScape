using System.Diagnostics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Pact.Disposables
{
	/// <summary>
	/// Defines a disposable wrapper for a value where the value can only be accessed
	/// until the wrapper is disposed of.
	/// </summary>
	/// <typeparam name="T">The type of the value.</typeparam>
	public interface ITemporaryValue<out T> : IDisposed
	{
		/// <summary>
		/// Gets the value.
		/// </summary>
		/// <value>
		/// The value.
		/// </value>
		/// <remarks>
		/// This can only be accessed if the wrapper is not disposed. Do not store the value returned by
		/// this property. Always retrieve it through this property whenever needed. Otherwise using
		/// a disposable value wrapper is pointless.
		/// </remarks>
		T Value { get; }
	}

	public static class ITemporaryValueContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void Value<T>(ITemporaryValue<T> instance)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(!instance.IsDisposed);
		}
	}
}
