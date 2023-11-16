using System.Collections.Concurrent;
using System.Diagnostics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Pact.Collections
{
	/// <summary>
	/// Provides contracts for the <see cref="IProducerConsumerCollection{T}"/> interface.
	/// </summary>
	public static class IProducerConsumerCollectionContracts
	{
		/// <summary>
		/// Contracts for <see cref="IProducerConsumerCollection{T}.CopyTo(T[], int)"/>.
		/// </summary>
		/// <typeparam name="T">The type of value stored in the collection.</typeparam>
		/// <param name="instance">The instance.</param>
		/// <param name="array">The array.</param>
		/// <param name="index">Index of the array.</param>
		/// <remarks>
		/// All implementations of <see cref="IProducerConsumerCollection{T}"/> must be threadsafe.
		/// As such, this contracts must be called in a threadsafe way.
		/// </remarks>
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void CopyTo<T>(IProducerConsumerCollection<T> instance, T[] array, int index)
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(array != null);
			Contracts.Requires.That(index >= 0);
			Contracts.Requires.That(index + instance.Count <= array.Length);
		}
	}
}
