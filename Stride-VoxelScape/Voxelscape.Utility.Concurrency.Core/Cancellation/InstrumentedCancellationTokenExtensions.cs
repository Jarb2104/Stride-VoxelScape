using System.Threading;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Events;
using Voxelscape.Utility.Concurrency.Core.Cancellation;

/// <summary>
/// Provides extension methods for creating <see cref="InstrumentedCancellationToken"/>.
/// </summary>
public static class InstrumentedCancellationTokenExtensions
{
	/// <summary>
	/// Wraps the specified <see cref="CancellationToken"/> in an <see cref="InstrumentedCancellationToken"/>.
	/// </summary>
	/// <param name="cancellation">The cancellation token to wrap.</param>
	/// <param name="cancellationPolledEventHandlers">
	/// The event handlers for the <see cref="InstrumentedCancellationToken.CancellationPolled"/> event.
	/// </param>
	/// <returns>Returns a new <see cref="InstrumentedCancellationToken"/>.</returns>
	public static InstrumentedCancellationToken ToInstrumentedToken(
		this CancellationToken cancellation,
		params TypeSafeEventHandler<InstrumentedCancellationToken, CancellationPolledEventArgs>[] cancellationPolledEventHandlers)
	{
		Contracts.Requires.That(cancellationPolledEventHandlers.AllAndSelfNotNull());

		InstrumentedCancellationToken result = new InstrumentedCancellationToken(cancellation);

		foreach (var handler in cancellationPolledEventHandlers)
		{
			result.CancellationPolled += handler;
		}

		return result;
	}

	/// <summary>
	/// Gets the <see cref="InstrumentedCancellationToken"/> associated with this <see cref="CancellationTokenSource"/>.
	/// </summary>
	/// <param name="cancellationSource">The cancellation token source.</param>
	/// <param name="cancellationPolledHandlers">
	/// The event handlers for the <see cref="InstrumentedCancellationToken.CancellationPolled"/> event.
	/// </param>
	/// <returns>Returns a new <see cref="InstrumentedCancellationToken"/>.</returns>
	public static InstrumentedCancellationToken TokenWithInstrumentation(
		this CancellationTokenSource cancellationSource,
		params TypeSafeEventHandler<InstrumentedCancellationToken, CancellationPolledEventArgs>[] cancellationPolledHandlers)
	{
		Contracts.Requires.That(cancellationSource != null);
		Contracts.Requires.That(cancellationPolledHandlers.AllAndSelfNotNull());

		return cancellationSource.Token.ToInstrumentedToken(cancellationPolledHandlers);
	}
}
