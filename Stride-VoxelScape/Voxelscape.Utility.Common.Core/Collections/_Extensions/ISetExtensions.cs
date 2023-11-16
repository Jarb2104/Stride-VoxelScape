using System.Collections.Generic;
using Voxelscape.Utility.Common.Core.Collections;
using Voxelscape.Utility.Common.Pact.Collections;

/// <summary>
/// Provides extension methods for <see cref="ISet{T}"/>.
/// </summary>
public static class ISetExtensions
{
	public static Voxelscape.Utility.Common.Pact.Collections.IReadOnlySet<T> AsReadOnlySet<T>(this ISet<T> set) => new ReadOnlySet<T>(set);
}
