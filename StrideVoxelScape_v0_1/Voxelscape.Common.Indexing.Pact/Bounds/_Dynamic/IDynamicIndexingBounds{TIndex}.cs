using System.Diagnostics;
using Voxelscape.Common.Indexing.Pact.Indexables;
using Voxelscape.Common.Indexing.Pact.Indices;
using Voxelscape.Utility.Common.Core.Mathematics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Pact.Bounds
{
	/// <summary>
	/// Interface to define the dynamically sizing bounds of a grouping of values accessed by an <see cref="IIndex"/>
	/// of an undefined number of dimensions. As values are added or removed from the corresponding group the bounds
	/// will grow or shrink to accommodate. Implementations of this interface may define how many dimensions they support.
	/// </summary>
	/// <typeparam name="TIndex">
	/// The type of the index used to access values.
	/// This determines the number of dimensions that can be indexed.
	/// </typeparam>
	/// <remarks>
	/// <para>
	/// This interface provides no mechanism for retrieving values, nor does it even define what the values are.
	/// This interface is only for providing the bounds of an indexable collection. Use the <see cref="IIndexable{TIndex, TValue}"/>
	/// interface for providing retrieval of values. Any time an 'indexable' is referred to in this interface it is
	/// referring to the <see cref="IIndexable{TIndex, TValue}"/> this instance is associated with.
	/// </para>
	/// <para>
	/// Implementations must be non-jagged, i.e. they must be rectangular, rigidly multidimensional, and/or uniform.
	/// Each dimension must also be capable of being indexed. This means the length of each dimension must be greater
	/// than or equal to one. A non indexable dimension would disallow the entire indexable from storing any values
	/// and would needlessly complicate the bounds methods.
	/// </para>
	/// </remarks>
	public interface IDynamicIndexingBounds<TIndex>
		where TIndex : IIndex
	{
		/// <summary>
		/// Gets the rank (number of dimensions) of the indexable. For example, a one-dimensional indexable returns 1,
		/// a two-dimensional indexable returns 2, and so on.
		/// </summary>
		/// <value>
		/// The number of dimensions.
		/// </value>
		int Rank { get; }

		/// <summary>
		/// Gets a 32-bit integer that represents the current total number of indexable slots in all the dimensions of the indexable.
		/// </summary>
		/// <value>
		/// The current total number of indexable slots in all the dimensions of the indexable.
		/// </value>
		/// <remarks>
		/// Retrieving the value of this property is an O(1) operation.
		/// </remarks>
		int CurrentLength { get; }

		/// <summary>
		/// Gets a 64-bit integer that represents the current total number of indexable slots in all the dimensions of the indexable.
		/// </summary>
		/// <value>
		/// The current total number of indexable slots in all the dimensions of the indexable.
		/// </value>
		/// <remarks>
		/// Retrieving the value of this property is an O(1) operation.
		/// </remarks>
		long CurrentLongLength { get; }

		/// <summary>
		/// Gets the current size of the dimensions of the indexable returned as the same value type used to index into the indexable.
		/// </summary>
		/// <value>
		/// The current dimensions of the indexable.
		/// </value>
		TIndex CurrentDimensions { get; }

		/// <summary>
		/// Gets the current index of the first slot of each of the dimensions in the indexable.
		/// </summary>
		/// <value>
		/// The current index of the first slot of each dimension in the indexable.
		/// </value>
		/// <remarks>
		/// <para>
		/// The CurrentLowerBounds property always returns a value that indicates the current index of the lower
		/// bounds of the indexable, even if the indexable is empty. The value returned is based on the
		/// current capacity of the indexable, not on what is currently stored.
		/// </para>
		/// <para>
		/// Note that, although most arrays in the .NET Framework are zero-based (that is, the GetLowerBound method
		/// returns zero for each dimension of an array), the .NET Framework does support arrays that are not zero-based.
		/// Such arrays can be created with the CreateInstance(Type, Int32[], Int32[]) method, and can also be returned
		/// from unmanaged code. These arrays can be wrapped in the <see cref="IIndexable{TIndex, TValue}"/> interface,
		/// hence this interface's LowerBounds can return similar non zero-based values. Additionally, custom implementations
		/// of the interface may actively choose to be non zero-based, regardless of what they are backed by.
		/// </para>
		/// <para>
		/// This method is an O(1) operation.
		/// </para>
		/// </remarks>
		TIndex CurrentLowerBounds { get; }

		/// <summary>
		/// Gets the current index of the last slot of each of the dimensions in the indexable.
		/// </summary>
		/// <value>
		/// The current index of the last slot of each dimension in the indexable.
		/// </value>
		/// <remarks>
		/// <para>
		/// The CurrentUpperBounds property always returns a value that indicates the current index of the upper
		/// bound of the indexable, even if the indexable is empty. The value returned is based on the
		/// current capacity of the indexable, not on what is currently stored. If a dimension of this indexable
		/// has zero capacity then that dimension of the index returned by this method will be -1.
		/// </para>
		/// <para>
		/// This method is an O(1) operation.
		/// </para>
		/// </remarks>
		TIndex CurrentUpperBounds { get; }

		/// <summary>
		/// Gets a 32-bit integer that represents the current number of slots in the specified dimension of the indexable.
		/// </summary>
		/// <param name="dimension">
		/// A zero-based dimension of the indexable whose current length needs to be determined.
		/// </param>
		/// <returns>
		/// A 32-bit integer that represents the current number of slots in the specified dimension.
		/// </returns>
		int GetCurrentLength(int dimension);

		/// <summary>
		/// Gets the current index of the first slot of the specified dimension in the indexable.
		/// </summary>
		/// <param name="dimension">
		/// A zero-based dimension of the indexable whose current starting index needs to be determined.
		/// </param>
		/// <returns>
		/// The current index of the first slot of the specified dimension in the indexable.
		/// </returns>
		/// <remarks>
		/// <para>
		/// GetCurrentLowerBound(0) returns the current starting index of the first dimension of the indexable,
		/// and GetCurrentLowerBound(Rank - 1) returns the current starting index of the last dimension of the indexable.
		/// </para>
		/// <para>
		/// The GetCurrentLowerBound method always returns a value that indicates the current index of the lower
		/// bound of the indexable, even if the indexable is empty. The value returned is based on the
		/// current capacity of the indexable, not on what is currently stored.
		/// </para>
		/// <para>
		/// Note that, although most arrays in the .NET Framework are zero-based (that is, the GetLowerBound method
		/// returns zero for each dimension of an array), the .NET Framework does support arrays that are not zero-based.
		/// Such arrays can be created with the CreateInstance(Type, Int32[], Int32[]) method, and can also be returned
		/// from unmanaged code. These arrays can be wrapped in the <see cref="IIndexable{TIndex, TValue}"/> interface,
		/// hence this interface's GetCurrentLowerBound can return similar non zero-based values. Additionally, custom
		/// implementations of the interface may actively choose to be non zero-based, regardless of what they are backed by.
		/// </para>
		/// <para>
		/// This method is an O(1) operation.
		/// </para>
		/// </remarks>
		int GetCurrentLowerBound(int dimension);

		/// <summary>
		/// Gets the current index of the last slot of the specified dimension in the indexable.
		/// </summary>
		/// <param name="dimension">
		/// A zero-based dimension of the indexable whose current ending index needs to be determined.
		/// </param>
		/// <returns>
		/// The current index of the last slot of the specified dimension in the indexable.
		/// </returns>
		/// <remarks>
		/// <para>
		/// GetCurrentUpperBound(0) returns the last index of the current first dimension of the indexable,
		/// and GetCurrentUpperBound(Rank - 1) returns the current last index of the last dimension of the indexable.
		/// </para>
		/// <para>
		/// The GetCurrentUpperBound method always returns a value that indicates the index of the upper
		/// bound of the indexable, even if the indexable is empty. The value returned is based on the
		/// current capacity of the indexable, not on what is currently stored. If this indexable has zero
		/// capacity then this returns -1.
		/// </para>
		/// <para>
		/// This method is an O(1) operation.
		/// </para>
		/// </remarks>
		int GetCurrentUpperBound(int dimension);
	}

	public static class IDynamicIndexingBoundsContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void GetCurrentLength<TIndex>(IDynamicIndexingBounds<TIndex> instance, int dimension)
			where TIndex : IIndex
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(dimension.IsIn(Range.FromLength(instance.Rank)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void GetCurrentLowerBound<TIndex>(IDynamicIndexingBounds<TIndex> instance, int dimension)
			where TIndex : IIndex
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(dimension.IsIn(Range.FromLength(instance.Rank)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void GetCurrentUpperBound<TIndex>(IDynamicIndexingBounds<TIndex> instance, int dimension)
			where TIndex : IIndex
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(dimension.IsIn(Range.FromLength(instance.Rank)));
		}
	}
}
