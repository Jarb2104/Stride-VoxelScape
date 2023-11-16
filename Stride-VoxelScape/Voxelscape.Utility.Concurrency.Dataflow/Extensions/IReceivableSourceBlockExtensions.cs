using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for the <see cref="IReceivableSourceBlock{T}"/> interface.
/// </summary>
public static class IReceivableSourceBlockExtensions
{
	/// <summary>
	/// Attempts to synchronously receive all available items from the <see cref="IReceivableSourceBlock{T}" />.
	/// </summary>
	/// <typeparam name="T">The type of items stored in the <see cref="IReceivableSourceBlock{T}" />.</typeparam>
	/// <param name="buffer">The source where the items should be extracted from.</param>
	/// <param name="items">The items received from the source.</param>
	/// <returns>
	/// True if one or more items could be received; otherwise, false.
	/// </returns>
	/// <remarks>
	/// <para>
	/// This method does not block waiting for the source to provide an item.
	/// It will return after checking for elements, whether or not an element was available.
	/// </para><para>
	/// Microsoft TPL Dataflow version 4.5.24 contains a bug in TryReceiveAll,
	/// hence this function uses TryReceive until nothing is available anymore.
	/// </para>
	/// </remarks>
	/// <seealso href="http://stackoverflow.com/questions/25339029/bufferblock-deadlock-with-outputavailableasync-after-tryreceiveall"/>
	public static bool TryReceiveAllBugFixed<T>(this IReceivableSourceBlock<T> buffer, out IList<T> items)
	{
		Contracts.Requires.That(buffer != null);

		items = new List<T>();
		T receivedItem = default(T);
		while (buffer.TryReceive(out receivedItem))
		{
			items.Add(receivedItem);
		}

		return items.Count > 0;
	}
}
