using System;

namespace Voxelscape.Utility.Common.Pact.Events
{
	/// <summary>
	/// An event handler delegate that provides a type-safe sender argument instead
	/// of having to cast the sender object to the correct type when using the
	/// normal event handler.
	/// </summary>
	/// <typeparam name="TSender">The type of the sender.</typeparam>
	/// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
	/// <param name="sender">The sender.</param>
	/// <param name="eventArgs">The TEventArgs instance containing the event data.</param>
	public delegate void TypeSafeEventHandler<TSender, TEventArgs>(TSender sender, TEventArgs eventArgs)
		where TEventArgs : EventArgs;

	/*
	If you came here seeking to copy pasta the suppression, here it is ready to go;
	[SuppressMessage("Microsoft.Design", "CA1009", Justification = "Type-safe event handler pattern.")]
	*/
}
