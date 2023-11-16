using System.Diagnostics;
using Voxelscape.Common.Indexing.Pact.Indexables;
using Voxelscape.Common.Indexing.Pact.Indices;
using Voxelscape.Utility.Common.Core.Mathematics;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Indexing.Pact.Bounds
{
	/// <summary>
	/// Interface to define the static bounds of a grouping of values accessed by an <see cref="IIndex"/>
	/// of an undefined number of dimensions. Implementations of this interface may define how many dimensions
	/// they support.
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
	public interface IIndexingBounds<TIndex>
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
		/// Gets a 32-bit integer that represents the total number of indexable slots in all the dimensions of the indexable.
		/// </summary>
		/// <value>
		/// The total number of indexable slots in all the dimensions of the indexable.
		/// </value>
		/// <remarks>
		/// Retrieving the value of this property is an O(1) operation.
		/// </remarks>
		int Length { get; }

		/// <summary>
		/// Gets a 64-bit integer that represents the total number of indexable slots in all the dimensions of the indexable.
		/// </summary>
		/// <value>
		/// The total number of indexable slots in all the dimensions of the indexable.
		/// </value>
		/// <remarks>
		/// Retrieving the value of this property is an O(1) operation.
		/// </remarks>
		long LongLength { get; }

		/// <summary>
		/// Gets the size of the dimensions of the indexable returned as the same value type used to index into the indexable.
		/// </summary>
		/// <value>
		/// The dimensions of the indexable.
		/// </value>
		TIndex Dimensions { get; }

		/// <summary>
		/// Gets the index of the first slot of each of the dimensions in the indexable.
		/// </summary>
		/// <value>
		/// The index of the first slot of each dimension in the indexable.
		/// </value>
		/// <remarks>
		/// <para>
		/// The LowerBounds property always returns a value that indicates the index of the lower
		/// bounds of the indexable, even if the indexable is empty. The value returned is based on the
		/// capacity of the indexable, not on what is currently stored.
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
		TIndex LowerBounds { get; }

		/// <summary>
		/// Gets the index of the last slot of each of the dimensions in the indexable.
		/// </summary>
		/// <value>
		/// The index of the last slot of each dimension in the indexable.
		/// </value>
		/// <remarks>
		/// <para>
		/// The UpperBounds property always returns a value that indicates the index of the upper
		/// bound of the indexable, even if the indexable is empty. The value returned is based on the
		/// capacity of the indexable, not on what is currently stored. If a dimension of this indexable
		/// has zero capacity then that dimension of the index returned by this method will be -1.
		/// </para>
		/// <para>
		/// This method is an O(1) operation.
		/// </para>
		/// </remarks>
		TIndex UpperBounds { get; }

		/// <summary>
		/// Gets a 32-bit integer that represents the number of slots in the specified dimension of the indexable.
		/// </summary>
		/// <param name="dimension">
		/// A zero-based dimension of the indexable whose length needs to be determined.
		/// </param>
		/// <returns>
		/// A 32-bit integer that represents the number of slots in the specified dimension.
		/// </returns>
		int GetLength(int dimension);

		/// <summary>
		/// Gets the index of the first slot of the specified dimension in the indexable.
		/// </summary>
		/// <param name="dimension">
		/// A zero-based dimension of the indexable whose starting index needs to be determined.
		/// </param>
		/// <returns>
		/// The index of the first slot of the specified dimension in the indexable.
		/// </returns>
		/// <remarks>
		/// <para>
		/// GetLowerBound(0) returns the starting index of the first dimension of the indexable,
		/// and GetLowerBound(Rank - 1) returns the starting index of the last dimension of the indexable.
		/// </para>
		/// <para>
		/// The GetLowerBound method always returns a value that indicates the index of the lower
		/// bound of the indexable, even if the indexable is empty. The value returned is based on the
		/// capacity of the indexable, not on what is currently stored.
		/// </para>
		/// <para>
		/// Note that, although most arrays in the .NET Framework are zero-based (that is, the GetLowerBound method
		/// returns zero for each dimension of an array), the .NET Framework does support arrays that are not zero-based.
		/// Such arrays can be created with the CreateInstance(Type, Int32[], Int32[]) method, and can also be returned
		/// from unmanaged code. These arrays can be wrapped in the <see cref="IIndexable{TIndex, TValue}"/> interface,
		/// hence this interface's GetLowerBound can return similar non zero-based values. Additionally, custom implementations
		/// of the interface may actively choose to be non zero-based, regardless of what they are backed by.
		/// </para>
		/// <para>
		/// This method is an O(1) operation.
		/// </para>
		/// </remarks>
		int GetLowerBound(int dimension);

		/// <summary>
		/// Gets the index of the last slot of the specified dimension in the indexable.
		/// </summary>
		/// <param name="dimension">
		/// A zero-based dimension of the indexable whose ending index needs to be determined.
		/// </param>
		/// <returns>
		/// The index of the last slot of the specified dimension in the indexable.
		/// </returns>
		/// <remarks>
		/// <para>
		/// GetUpperBound(0) returns the last index of the first dimension of the indexable,
		/// and GetUpperBound(Rank - 1) returns the last index of the last dimension of the indexable.
		/// </para>
		/// <para>
		/// The GetUpperBound method always returns a value that indicates the index of the upper
		/// bound of the indexable, even if the indexable is empty. The value returned is based on the
		/// capacity of the indexable, not on what is currently stored. If this indexable has zero capacity
		/// then this returns -1.
		/// </para>
		/// <para>
		/// This method is an O(1) operation.
		/// </para>
		/// </remarks>
		int GetUpperBound(int dimension);
	}

	public static class IIndexingBoundsContracts
	{
		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void GetLength<TIndex>(IIndexingBounds<TIndex> instance, int dimension)
			where TIndex : IIndex
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(dimension.IsIn(Range.FromLength(instance.Rank)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void GetLowerBound<TIndex>(IIndexingBounds<TIndex> instance, int dimension)
			where TIndex : IIndex
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(dimension.IsIn(Range.FromLength(instance.Rank)));
		}

		[Conditional(Contracts.Requires.CompilationSymbol)]
		public static void GetUpperBound<TIndex>(IIndexingBounds<TIndex> instance, int dimension)
			where TIndex : IIndex
		{
			Contracts.Requires.That(instance != null);
			Contracts.Requires.That(dimension.IsIn(Range.FromLength(instance.Rank)));
		}
	}
}
