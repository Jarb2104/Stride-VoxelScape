using System.Collections.Generic;
using System.Diagnostics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Pact.Collections
{
	/// <summary>
	/// Provides contracts for the <see cref="IEqualityComparer{T}"/> interface.
	/// </summary>
	public static class IEqualityComparerContracts
	{
		/// <summary>
		/// Contracts for <see cref="IEqualityComparer{T}.GetHashCode(T)"/>.
		/// </summary>
		/// <typeparam name="T">The type of the values to compare.</typeparam>
		/// <param name="obj">The object to get the hash code for.</param>
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void GetHashCode<T>(T obj)
		{
			Contracts.Requires.That(obj != null);
		}
	}
}
