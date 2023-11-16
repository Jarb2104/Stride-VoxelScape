using System;
using System.Runtime.CompilerServices;
using Nito.AsyncEx;

/// <summary>
/// Provides extension methods for the <see cref="AwaitableDisposableExtensions"/> class.
/// </summary>
public static class AwaitableDisposableExtensions
{
	/// <summary>
	/// Configures the await to not attempt to marshal the continuation back to the original context captured.
	/// </summary>
	/// <typeparam name="T">The type of the result of the underlying task.</typeparam>
	/// <param name="awaitable">The awaitable to configure the await for.</param>
	/// <returns>
	/// An object used to await this awaitable.
	/// </returns>
	/// <remarks>
	/// This method makes it so that the code after the await can be ran on any available thread pool thread
	/// instead of being marshalled back to the same thread that the code was running on before the await.
	/// Avoiding this marshalling helps to slightly improve performance and helps avoid potential deadlocks. See
	/// <see href="http://blog.ciber.no/2014/05/19/using-task-configureawaitfalse-to-prevent-deadlocks-in-async-code/">
	/// this link</see> for more information.
	/// </remarks>
	public static ConfiguredTaskAwaitable<T> DontMarshallContext<T>(this AwaitableDisposable<T> awaitable)
		where T : IDisposable
		=> awaitable.ConfigureAwait(false);
}
