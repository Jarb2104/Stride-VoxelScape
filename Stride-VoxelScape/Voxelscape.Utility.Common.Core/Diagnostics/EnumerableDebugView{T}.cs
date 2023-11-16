using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Diagnostics
{
	/// <summary>
	/// A debugger proxy for enumerable classes to use to customize the way they are displayed by default in the debugger.
	/// Using this will make the type show up as an array of their values in the order they are enumerated.
	/// </summary>
	/// <typeparam name="T">The type of values in the enumerable.</typeparam>
	public class EnumerableDebugView<T>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EnumerableDebugView{T}"/> class.
		/// </summary>
		/// <param name="source">The source enumerable to customize the debugger display of.</param>
		public EnumerableDebugView(IEnumerable<T> source)
		{
			Contracts.Requires.That(source != null);

			this.Items = source.ToArray();
		}

		/// <summary>
		/// Gets the items that will be displayed in the debugger.
		/// </summary>
		/// <value>
		/// The items to display in the debugger.
		/// </value>
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items { get; }
	}
}
