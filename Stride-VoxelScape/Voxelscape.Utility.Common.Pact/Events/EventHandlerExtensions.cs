using System;
using System.Runtime.CompilerServices;

/// <summary>
/// Provides extension methods for the <see cref="EventHandler{TEventArgs}"/> class.
/// </summary>
public static class EventHandlerExtensions
{
	/// <summary>
	/// Raises the event following the standard thread safe check for no subscribers (the handler being null).
	/// </summary>
	/// <typeparam name="TEventArgs">The type of the event arguments.</typeparam>
	/// <param name="handler">The handler whose event should be raised.</param>
	/// <param name="sender">The sender of the event.</param>
	/// <param name="eventArgs">The <typeparamref name="TEventArgs"/> instance containing the event data.</param>
	/// <remarks>
	/// This overload has no <c>where TEventArgs : EventArgs</c> generic constraint because <see cref="EventHandler"/>
	/// itself does not have the same generic constraint.
	/// </remarks>
	[MethodImpl(MethodImplOptions.NoInlining)]
	public static void RaiseEventNullSafe<TEventArgs>(
		this EventHandler<TEventArgs> handler,
		object sender,
		TEventArgs eventArgs)
		where TEventArgs : EventArgs
	{
		// this method is not allowed to be inlined by the compiler
		// this is to ensure we get a parameter variable to hold the handler in
		// checking for null on this local copy and raising from it ensures thread safety
		if (handler != null)
		{
			handler(sender, eventArgs);
		}
	}
}
