using System;
using System.Reactive.Linq;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Concurrency.Core.Reactive;

/// <summary>
/// Provides extension methods for the <see cref="IObservable{T}"/> interface.
/// </summary>
public static class IObservableSequentialPairExtensions
{
	#region PairWithPrevious

	/// <summary>
	/// Pairs each value in an observable sequence with the value that came before it.
	/// </summary>
	/// <typeparam name="TSource">The type of the source.</typeparam>
	/// <param name="source">The sequence to observe as pairs of values.</param>
	/// <returns>
	/// An observable sequence that contains the elements of the input sequence paired with their previous values.
	/// </returns>
	/// <remarks>
	/// This method supplies the default value of type TSource to serve as the previous value of the first observed
	/// pair of values. This is necessary as there is otherwise no previous value to pair with the first value of the
	/// input sequence. There is an overload available to allow you to provide your own initial seed value to serve
	/// as the previous value of the first pair if the default value of type TSource does not meet your requirements.
	/// </remarks>
	public static IObservable<SequentialPair<TSource>> PairWithPrevious<TSource>(
		this IObservable<TSource> source)
	{
		Contracts.Requires.That(source != null);

		return source.Scan(
			new SequentialPair<TSource>(default(TSource), default(TSource)),
			(accumulator, current) => new SequentialPair<TSource>(accumulator.Next, current));
	}

	/// <summary>
	/// Pairs each value in an observable sequence with the value that came before it.
	/// </summary>
	/// <typeparam name="TSource">The type of the source.</typeparam>
	/// <param name="source">The sequence to observe as pairs of values.</param>
	/// <param name="previousOfFirst">
	/// The value to use as the previous value of the first observed pair of values. This is necessary as there is
	/// otherwise no previous value to pair with the first value of the input sequence.
	/// </param>
	/// <returns>
	/// An observable sequence that contains the elements of the input sequence paired with their previous values.
	/// </returns>
	public static IObservable<SequentialPair<TSource>> PairWithPrevious<TSource>(
		this IObservable<TSource> source,
		TSource previousOfFirst)
	{
		Contracts.Requires.That(source != null);

		return source.Scan(
			new SequentialPair<TSource>(default(TSource), previousOfFirst),
			(accumulator, current) => new SequentialPair<TSource>(accumulator.Next, current));
	}

	/// <summary>
	/// Pairs each value in an observable sequence with the value that came before it.
	/// </summary>
	/// <typeparam name="TSource">The type of the source.</typeparam>
	/// <param name="source">The sequence to observe as pairs of values.</param>
	/// <param name="first">
	/// An observable sequence that will return just the first value of the input sequence. This is provided because
	/// the first value of the sequence won't yet have a previous value to pair with it. As such it will be observed
	/// only via the <paramref name="first"/> observable sequence, while the observable sequence returned by this method
	/// will skip it, thus only ever returning pairs of both next and previous values.
	/// </param>
	/// <returns>
	/// An observable sequence that contains the elements of the input sequence paired with their previous values.
	/// </returns>
	public static IObservable<SequentialPair<TSource>> PairWithPrevious<TSource>(
		this IObservable<TSource> source,
		out IObservable<TSource> first)
	{
		Contracts.Requires.That(source != null);

		// first output parameter takes just the first value while the rest are generated as pairs except for the
		// first value which is skipped because it has no previous value
		first = source.Take(1);
		return source.PairWithPrevious().Skip(1);
	}

	/// <summary>
	/// Pairs each value in an observable sequence with the value that came before it.
	/// </summary>
	/// <typeparam name="TSource">The type of the source.</typeparam>
	/// <param name="source">The sequence to observe as pairs of values.</param>
	/// <param name="first">
	/// An observable sequence that will return just the first value of the input sequence. This is provided because
	/// the first value of the sequence won't yet have a previous value to pair with it. As such it will be observed
	/// only via the <paramref name="first" /> observable sequence, while the observable sequence returned by this method
	/// will skip it, thus only ever returning pairs of both next and previous values.</param>
	/// <param name="last">An observable sequence that will return just the last value of the input sequence.</param>
	/// <returns>
	/// An observable sequence that contains the elements of the input sequence paired with their previous values.
	/// </returns>
	public static IObservable<SequentialPair<TSource>> PairWithPrevious<TSource>(
		this IObservable<TSource> source,
		out IObservable<TSource> first,
		out IObservable<TSource> last)
	{
		Contracts.Requires.That(source != null);

		// first output parameter takes just the first value while the rest are generated as pairs except for the
		// first value which is skipped because it has no previous value
		first = source.Take(1);
		last = source.TakeLast(1);
		return source.PairWithPrevious().Skip(1);
	}

	#endregion

	#region CombineWithPrevious

	/// <summary>
	/// Combines each value in an observable sequence with the value that came before it using the specified delegate.
	/// </summary>
	/// <typeparam name="TSource">The type of the source.</typeparam>
	/// <typeparam name="TResult">The type of the result.</typeparam>
	/// <param name="source">The sequence to combine pairs of values into a new sequence from.</param>
	/// <param name="resultSelector">
	/// The delegate used to take the current and previous values and produce a result value from them.
	/// </param>
	/// <returns>
	/// An observable sequence that contains the paired elements of the input sequence combined into new values
	/// using the specified delegate.
	/// </returns>
	/// <remarks>
	/// This method supplies the default value of type TSource to serve as the previous value of the first observed
	/// pair of values. This is necessary as there is otherwise no previous value to pair with the first value of the
	/// input sequence. There is an overload available to allow you to provide your own initial seed value to serve
	/// as the previous value of the first pair if the default value of type TSource does not meet your requirements.
	/// </remarks>
	public static IObservable<TResult> CombineWithPrevious<TSource, TResult>(
		this IObservable<TSource> source,
		Func<TSource, TSource, TResult> resultSelector)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(resultSelector != null);

		return source.Scan(
			new SequentialPair<TSource>(default(TSource), default(TSource)),
			(accumulator, current) => new SequentialPair<TSource>(accumulator.Next, current)).Select(
				pair => resultSelector(pair.Previous, pair.Next));
	}

	/// <summary>
	/// Combines each value in an observable sequence with the value that came before it using the specified delegate.
	/// </summary>
	/// <typeparam name="TSource">The type of the source.</typeparam>
	/// <typeparam name="TResult">The type of the result.</typeparam>
	/// <param name="source">The sequence to combine pairs of values into a new sequence from.</param>
	/// <param name="previousOfFirst">
	/// The value to use as the previous value of the first observed pair of values. This is necessary as there is
	/// otherwise no previous value to pair with the first value of the input sequence.
	/// </param>
	/// <param name="resultSelector">
	/// The delegate used to take the current and previous values and produce a result value from them.
	/// </param>
	/// <returns>
	/// An observable sequence that contains the paired elements of the input sequence combined into new values
	/// using the specified delegate.
	/// </returns>
	public static IObservable<TResult> CombineWithPrevious<TSource, TResult>(
		this IObservable<TSource> source,
		TSource previousOfFirst,
		Func<TSource, TSource, TResult> resultSelector)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(resultSelector != null);

		return source.Scan(
			new SequentialPair<TSource>(default(TSource), previousOfFirst),
			(accumulator, current) => new SequentialPair<TSource>(accumulator.Next, current)).Select(
				pair => resultSelector(pair.Previous, pair.Next));
	}

	/// <summary>
	/// Combines each value in an observable sequence with the value that came before it using the specified delegate.
	/// </summary>
	/// <typeparam name="TSource">The type of the source.</typeparam>
	/// <typeparam name="TResult">The type of the result.</typeparam>
	/// <param name="source">The sequence to combine pairs of values into a new sequence from.</param>
	/// <param name="first">
	/// An observable sequence that will return just the first value of the input sequence. This is provided because
	/// the first value of the sequence won't yet have a previous value to pair with it. As such it will be observed
	/// only via the <paramref name="first"/> observable sequence, while the observable sequence returned by this method
	/// will skip it, thus only ever returning pairs of both next and previous values.
	/// </param>
	/// <param name="resultSelector">
	/// The delegate used to take the current and previous values and produce a result value from them.
	/// </param>
	/// <returns>
	/// An observable sequence that contains the paired elements of the input sequence combined into new values
	/// using the specified delegate.
	/// </returns>
	public static IObservable<TResult> CombineWithPrevious<TSource, TResult>(
		this IObservable<TSource> source,
		out IObservable<TSource> first,
		Func<TSource, TSource, TResult> resultSelector)
	{
		Contracts.Requires.That(source != null);
		Contracts.Requires.That(resultSelector != null);

		// first output parameter takes just the first value while the rest are generated as pairs except for the
		// first value which is skipped because it has no previous value
		first = source.Take(1);
		return source.CombineWithPrevious(resultSelector).Skip(1);
	}

	#endregion
}
