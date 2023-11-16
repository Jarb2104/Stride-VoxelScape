using System;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Core.Factories;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods to easily wrap delegates in factory instances.
/// </summary>
public static class AsyncDelegateAsFactoryExtensions
{
	#region Async

	/// <summary>
	/// Wraps a task returning function in a <see cref="AsyncDelegateFactory{TResult}" />.
	/// </summary>
	/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
	/// <param name="func">The delegate to wrap in a factory.</param>
	/// <returns>The factory.</returns>
	public static AsyncDelegateFactory<TResult> AsAsyncFactory<TResult>(
		this Func<Task<TResult>> func)
	{
		Contracts.Requires.That(func != null);

		return new AsyncDelegateFactory<TResult>(func);
	}

	/// <summary>
	/// Wraps a task returning function in a <see cref="AsyncDelegateFactory{T, TResult}" />.
	/// </summary>
	/// <typeparam name="T">The type of the argument passed to the factory.</typeparam>
	/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
	/// <param name="func">The delegate to wrap in a factory.</param>
	/// <returns>The factory.</returns>
	public static AsyncDelegateFactory<T, TResult> AsAsyncFactory<T, TResult>(
		this Func<T, Task<TResult>> func)
	{
		Contracts.Requires.That(func != null);

		return new AsyncDelegateFactory<T, TResult>(func);
	}

	/// <summary>
	/// Wraps a task returning function in a <see cref="AsyncDelegateFactory{T1, T2, TResult}" />.
	/// </summary>
	/// <typeparam name="T1">The type of the first argument passed to the factory.</typeparam>
	/// <typeparam name="T2">The type of the second argument passed to the factory.</typeparam>
	/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
	/// <param name="func">The delegate to wrap in a factory.</param>
	/// <returns>The factory.</returns>
	public static AsyncDelegateFactory<T1, T2, TResult> AsAsyncFactory<T1, T2, TResult>(
		this Func<T1, T2, Task<TResult>> func)
	{
		Contracts.Requires.That(func != null);

		return new AsyncDelegateFactory<T1, T2, TResult>(func);
	}

	/// <summary>
	/// Wraps a task returning function in a <see cref="AsyncDelegateFactory{T1, T2, T3, TResult}" />.
	/// </summary>
	/// <typeparam name="T1">The type of the first argument passed to the factory.</typeparam>
	/// <typeparam name="T2">The type of the second argument passed to the factory.</typeparam>
	/// <typeparam name="T3">The type of the third argument passed to the factory.</typeparam>
	/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
	/// <param name="func">The delegate to wrap in a factory.</param>
	/// <returns>The factory.</returns>
	public static AsyncDelegateFactory<T1, T2, T3, TResult> AsAsyncFactory<T1, T2, T3, TResult>(
		this Func<T1, T2, T3, Task<TResult>> func)
	{
		Contracts.Requires.That(func != null);

		return new AsyncDelegateFactory<T1, T2, T3, TResult>(func);
	}

	/// <summary>
	/// Wraps a task returning function in a <see cref="AsyncDelegateFactory{T1, T2, T3, T4, TResult}" />.
	/// </summary>
	/// <typeparam name="T1">The type of the first argument passed to the factory.</typeparam>
	/// <typeparam name="T2">The type of the second argument passed to the factory.</typeparam>
	/// <typeparam name="T3">The type of the third argument passed to the factory.</typeparam>
	/// <typeparam name="T4">The type of the forth argument passed to the factory.</typeparam>
	/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
	/// <param name="func">The delegate to wrap in a factory.</param>
	/// <returns>The factory.</returns>
	public static AsyncDelegateFactory<T1, T2, T3, T4, TResult> AsAsyncFactory<T1, T2, T3, T4, TResult>(
		this Func<T1, T2, T3, T4, Task<TResult>> func)
	{
		Contracts.Requires.That(func != null);

		return new AsyncDelegateFactory<T1, T2, T3, T4, TResult>(func);
	}

	/// <summary>
	/// Wraps a task returning function in a <see cref="AsyncDelegateFactory{T1, T2, T3, T4, T5, TResult}" />.
	/// </summary>
	/// <typeparam name="T1">The type of the first argument passed to the factory.</typeparam>
	/// <typeparam name="T2">The type of the second argument passed to the factory.</typeparam>
	/// <typeparam name="T3">The type of the third argument passed to the factory.</typeparam>
	/// <typeparam name="T4">The type of the forth argument passed to the factory.</typeparam>
	/// <typeparam name="T5">The type of the fifth argument passed to the factory.</typeparam>
	/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
	/// <param name="func">The delegate to wrap in a factory.</param>
	/// <returns>The factory.</returns>
	public static AsyncDelegateFactory<T1, T2, T3, T4, T5, TResult> AsAsyncFactory<T1, T2, T3, T4, T5, TResult>(
		this Func<T1, T2, T3, T4, T5, Task<TResult>> func)
	{
		Contracts.Requires.That(func != null);

		return new AsyncDelegateFactory<T1, T2, T3, T4, T5, TResult>(func);
	}

	/// <summary>
	/// Wraps a task returning function in a <see cref="AsyncDelegateFactory{T1, T2, T3, T4, T5, T6, TResult}" />.
	/// </summary>
	/// <typeparam name="T1">The type of the first argument passed to the factory.</typeparam>
	/// <typeparam name="T2">The type of the second argument passed to the factory.</typeparam>
	/// <typeparam name="T3">The type of the third argument passed to the factory.</typeparam>
	/// <typeparam name="T4">The type of the forth argument passed to the factory.</typeparam>
	/// <typeparam name="T5">The type of the fifth argument passed to the factory.</typeparam>
	/// <typeparam name="T6">The type of the sixth argument passed to the factory.</typeparam>
	/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
	/// <param name="func">The delegate to wrap in a factory.</param>
	/// <returns>The factory.</returns>
	public static AsyncDelegateFactory<T1, T2, T3, T4, T5, T6, TResult> AsAsyncFactory<T1, T2, T3, T4, T5, T6, TResult>(
		this Func<T1, T2, T3, T4, T5, T6, Task<TResult>> func)
	{
		Contracts.Requires.That(func != null);

		return new AsyncDelegateFactory<T1, T2, T3, T4, T5, T6, TResult>(func);
	}

	/// <summary>
	/// Wraps a task returning function in a <see cref="AsyncDelegateFactory{T1, T2, T3, T4, T5, T6, T7, TResult}" />.
	/// </summary>
	/// <typeparam name="T1">The type of the first argument passed to the factory.</typeparam>
	/// <typeparam name="T2">The type of the second argument passed to the factory.</typeparam>
	/// <typeparam name="T3">The type of the third argument passed to the factory.</typeparam>
	/// <typeparam name="T4">The type of the forth argument passed to the factory.</typeparam>
	/// <typeparam name="T5">The type of the fifth argument passed to the factory.</typeparam>
	/// <typeparam name="T6">The type of the sixth argument passed to the factory.</typeparam>
	/// <typeparam name="T7">The type of the seventh argument passed to the factory.</typeparam>
	/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
	/// <param name="func">The delegate to wrap in a factory.</param>
	/// <returns>The factory.</returns>
	public static AsyncDelegateFactory<T1, T2, T3, T4, T5, T6, T7, TResult> AsAsyncFactory<T1, T2, T3, T4, T5, T6, T7, TResult>(
		this Func<T1, T2, T3, T4, T5, T6, T7, Task<TResult>> func)
	{
		Contracts.Requires.That(func != null);

		return new AsyncDelegateFactory<T1, T2, T3, T4, T5, T6, T7, TResult>(func);
	}

	/// <summary>
	/// Wraps a task returning function in a <see cref="AsyncDelegateFactory{T1, T2, T3, T4, T5, T6, T7, T8, TResult}" />.
	/// </summary>
	/// <typeparam name="T1">The type of the first argument passed to the factory.</typeparam>
	/// <typeparam name="T2">The type of the second argument passed to the factory.</typeparam>
	/// <typeparam name="T3">The type of the third argument passed to the factory.</typeparam>
	/// <typeparam name="T4">The type of the forth argument passed to the factory.</typeparam>
	/// <typeparam name="T5">The type of the fifth argument passed to the factory.</typeparam>
	/// <typeparam name="T6">The type of the sixth argument passed to the factory.</typeparam>
	/// <typeparam name="T7">The type of the seventh argument passed to the factory.</typeparam>
	/// <typeparam name="T8">The type of the eighth argument passed to the factory.</typeparam>
	/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
	/// <param name="func">The delegate to wrap in a factory.</param>
	/// <returns>The factory.</returns>
	public static AsyncDelegateFactory<T1, T2, T3, T4, T5, T6, T7, T8, TResult> AsAsyncFactory<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
		this Func<T1, T2, T3, T4, T5, T6, T7, T8, Task<TResult>> func)
	{
		Contracts.Requires.That(func != null);

		return new AsyncDelegateFactory<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(func);
	}

	#endregion

	#region Sync As Async

	/// <summary>
	/// Wraps a function in a <see cref="AsyncDelegateFactory{TResult}" />.
	/// </summary>
	/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
	/// <param name="func">The delegate to wrap in a factory.</param>
	/// <returns>The factory.</returns>
	public static AsyncDelegateFactory<TResult> AsAsyncFactory<TResult>(
		this Func<TResult> func)
	{
		Contracts.Requires.That(func != null);

		return new AsyncDelegateFactory<TResult>(() => Task.FromResult(func()));
	}

	/// <summary>
	/// Wraps a function in a <see cref="AsyncDelegateFactory{T, TResult}" />.
	/// </summary>
	/// <typeparam name="T">The type of the argument passed to the factory.</typeparam>
	/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
	/// <param name="func">The delegate to wrap in a factory.</param>
	/// <returns>The factory.</returns>
	public static AsyncDelegateFactory<T, TResult> AsAsyncFactory<T, TResult>(
		this Func<T, TResult> func)
	{
		Contracts.Requires.That(func != null);

		return new AsyncDelegateFactory<T, TResult>(arg => Task.FromResult(func(arg)));
	}

	/// <summary>
	/// Wraps a function in a <see cref="AsyncDelegateFactory{T1, T2, TResult}" />.
	/// </summary>
	/// <typeparam name="T1">The type of the first argument passed to the factory.</typeparam>
	/// <typeparam name="T2">The type of the second argument passed to the factory.</typeparam>
	/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
	/// <param name="func">The delegate to wrap in a factory.</param>
	/// <returns>The factory.</returns>
	public static AsyncDelegateFactory<T1, T2, TResult> AsAsyncFactory<T1, T2, TResult>(
		this Func<T1, T2, TResult> func)
	{
		Contracts.Requires.That(func != null);

		return new AsyncDelegateFactory<T1, T2, TResult>((arg1, arg2) => Task.FromResult(func(arg1, arg2)));
	}

	/// <summary>
	/// Wraps a function in a <see cref="AsyncDelegateFactory{T1, T2, T3, TResult}" />.
	/// </summary>
	/// <typeparam name="T1">The type of the first argument passed to the factory.</typeparam>
	/// <typeparam name="T2">The type of the second argument passed to the factory.</typeparam>
	/// <typeparam name="T3">The type of the third argument passed to the factory.</typeparam>
	/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
	/// <param name="func">The delegate to wrap in a factory.</param>
	/// <returns>The factory.</returns>
	public static AsyncDelegateFactory<T1, T2, T3, TResult> AsAsyncFactory<T1, T2, T3, TResult>(
		this Func<T1, T2, T3, TResult> func)
	{
		Contracts.Requires.That(func != null);

		return new AsyncDelegateFactory<T1, T2, T3, TResult>((arg1, arg2, arg3) => Task.FromResult(func(arg1, arg2, arg3)));
	}

	/// <summary>
	/// Wraps a function in a <see cref="AsyncDelegateFactory{T1, T2, T3, T4, TResult}" />.
	/// </summary>
	/// <typeparam name="T1">The type of the first argument passed to the factory.</typeparam>
	/// <typeparam name="T2">The type of the second argument passed to the factory.</typeparam>
	/// <typeparam name="T3">The type of the third argument passed to the factory.</typeparam>
	/// <typeparam name="T4">The type of the forth argument passed to the factory.</typeparam>
	/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
	/// <param name="func">The delegate to wrap in a factory.</param>
	/// <returns>The factory.</returns>
	public static AsyncDelegateFactory<T1, T2, T3, T4, TResult> AsAsyncFactory<T1, T2, T3, T4, TResult>(
		this Func<T1, T2, T3, T4, TResult> func)
	{
		Contracts.Requires.That(func != null);

		return new AsyncDelegateFactory<T1, T2, T3, T4, TResult>(
			(arg1, arg2, arg3, arg4) => Task.FromResult(func(arg1, arg2, arg3, arg4)));
	}

	/// <summary>
	/// Wraps a function in a <see cref="AsyncDelegateFactory{T1, T2, T3, T4, T5, TResult}" />.
	/// </summary>
	/// <typeparam name="T1">The type of the first argument passed to the factory.</typeparam>
	/// <typeparam name="T2">The type of the second argument passed to the factory.</typeparam>
	/// <typeparam name="T3">The type of the third argument passed to the factory.</typeparam>
	/// <typeparam name="T4">The type of the forth argument passed to the factory.</typeparam>
	/// <typeparam name="T5">The type of the fifth argument passed to the factory.</typeparam>
	/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
	/// <param name="func">The delegate to wrap in a factory.</param>
	/// <returns>The factory.</returns>
	public static AsyncDelegateFactory<T1, T2, T3, T4, T5, TResult> AsAsyncFactory<T1, T2, T3, T4, T5, TResult>(
		this Func<T1, T2, T3, T4, T5, TResult> func)
	{
		Contracts.Requires.That(func != null);

		return new AsyncDelegateFactory<T1, T2, T3, T4, T5, TResult>(
			(arg1, arg2, arg3, arg4, arg5) => Task.FromResult(func(arg1, arg2, arg3, arg4, arg5)));
	}

	/// <summary>
	/// Wraps a function in a <see cref="AsyncDelegateFactory{T1, T2, T3, T4, T5, T6, TResult}" />.
	/// </summary>
	/// <typeparam name="T1">The type of the first argument passed to the factory.</typeparam>
	/// <typeparam name="T2">The type of the second argument passed to the factory.</typeparam>
	/// <typeparam name="T3">The type of the third argument passed to the factory.</typeparam>
	/// <typeparam name="T4">The type of the forth argument passed to the factory.</typeparam>
	/// <typeparam name="T5">The type of the fifth argument passed to the factory.</typeparam>
	/// <typeparam name="T6">The type of the sixth argument passed to the factory.</typeparam>
	/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
	/// <param name="func">The delegate to wrap in a factory.</param>
	/// <returns>The factory.</returns>
	public static AsyncDelegateFactory<T1, T2, T3, T4, T5, T6, TResult> AsAsyncFactory<T1, T2, T3, T4, T5, T6, TResult>(
		this Func<T1, T2, T3, T4, T5, T6, TResult> func)
	{
		Contracts.Requires.That(func != null);

		return new AsyncDelegateFactory<T1, T2, T3, T4, T5, T6, TResult>(
			(arg1, arg2, arg3, arg4, arg5, arg6) => Task.FromResult(func(arg1, arg2, arg3, arg4, arg5, arg6)));
	}

	/// <summary>
	/// Wraps a function in a <see cref="AsyncDelegateFactory{T1, T2, T3, T4, T5, T6, T7, TResult}" />.
	/// </summary>
	/// <typeparam name="T1">The type of the first argument passed to the factory.</typeparam>
	/// <typeparam name="T2">The type of the second argument passed to the factory.</typeparam>
	/// <typeparam name="T3">The type of the third argument passed to the factory.</typeparam>
	/// <typeparam name="T4">The type of the forth argument passed to the factory.</typeparam>
	/// <typeparam name="T5">The type of the fifth argument passed to the factory.</typeparam>
	/// <typeparam name="T6">The type of the sixth argument passed to the factory.</typeparam>
	/// <typeparam name="T7">The type of the seventh argument passed to the factory.</typeparam>
	/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
	/// <param name="func">The delegate to wrap in a factory.</param>
	/// <returns>The factory.</returns>
	public static AsyncDelegateFactory<T1, T2, T3, T4, T5, T6, T7, TResult> AsAsyncFactory<T1, T2, T3, T4, T5, T6, T7, TResult>(
		this Func<T1, T2, T3, T4, T5, T6, T7, TResult> func)
	{
		Contracts.Requires.That(func != null);

		return new AsyncDelegateFactory<T1, T2, T3, T4, T5, T6, T7, TResult>(
			(arg1, arg2, arg3, arg4, arg5, arg6, arg7) => Task.FromResult(func(arg1, arg2, arg3, arg4, arg5, arg6, arg7)));
	}

	/// <summary>
	/// Wraps a function in a <see cref="AsyncDelegateFactory{T1, T2, T3, T4, T5, T6, T7, T8, TResult}" />.
	/// </summary>
	/// <typeparam name="T1">The type of the first argument passed to the factory.</typeparam>
	/// <typeparam name="T2">The type of the second argument passed to the factory.</typeparam>
	/// <typeparam name="T3">The type of the third argument passed to the factory.</typeparam>
	/// <typeparam name="T4">The type of the forth argument passed to the factory.</typeparam>
	/// <typeparam name="T5">The type of the fifth argument passed to the factory.</typeparam>
	/// <typeparam name="T6">The type of the sixth argument passed to the factory.</typeparam>
	/// <typeparam name="T7">The type of the seventh argument passed to the factory.</typeparam>
	/// <typeparam name="T8">The type of the eighth argument passed to the factory.</typeparam>
	/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
	/// <param name="func">The delegate to wrap in a factory.</param>
	/// <returns>The factory.</returns>
	public static AsyncDelegateFactory<T1, T2, T3, T4, T5, T6, T7, T8, TResult> AsAsyncFactory<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
		this Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func)
	{
		Contracts.Requires.That(func != null);

		return new AsyncDelegateFactory<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
			(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8) => Task.FromResult(func(arg1, arg2, arg3, arg4, arg5, arg6, arg7, arg8)));
	}

	#endregion
}
