using System;
using System.Threading.Tasks;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;

namespace Voxelscape.Utility.Common.Core.Factories
{
	/// <summary>
	/// Provides static factory methods to easily wrap delegates in <see cref="IFactory{T}"/> instances
	/// (or the matching interface for the number of input parameters / asynchronous).
	/// </summary>
	public static class Factory
	{
		#region Synchronous

		/// <summary>
		/// Wraps a <see cref="Func{TResult}" /> in a <see cref="DelegateFactory{TResult}" />.
		/// </summary>
		/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
		/// <param name="func">The delegate to wrap in a factory.</param>
		/// <returns>The factory.</returns>
		public static DelegateFactory<TResult> From<TResult>(
			Func<TResult> func)
		{
			Contracts.Requires.That(func != null);

			return func.AsFactory();
		}

		/// <summary>
		/// Wraps a <see cref="Func{T, TResult}" /> in a <see cref="DelegateFactory{T, TResult}" />.
		/// </summary>
		/// <typeparam name="T">The type of the argument passed to the factory.</typeparam>
		/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
		/// <param name="func">The delegate to wrap in a factory.</param>
		/// <returns>The factory.</returns>
		public static DelegateFactory<T, TResult> From<T, TResult>(
			Func<T, TResult> func)
		{
			Contracts.Requires.That(func != null);

			return func.AsFactory();
		}

		/// <summary>
		/// Wraps a <see cref="Func{T1, T2, TResult}" /> in a <see cref="DelegateFactory{T1, T2, TResult}" />.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument passed to the factory.</typeparam>
		/// <typeparam name="T2">The type of the second argument passed to the factory.</typeparam>
		/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
		/// <param name="func">The delegate to wrap in a factory.</param>
		/// <returns>The factory.</returns>
		public static DelegateFactory<T1, T2, TResult> From<T1, T2, TResult>(
			Func<T1, T2, TResult> func)
		{
			Contracts.Requires.That(func != null);

			return func.AsFactory();
		}

		/// <summary>
		/// Wraps a <see cref="Func{T1, T2, T3, TResult}" /> in a <see cref="DelegateFactory{T1, T2, T3, TResult}" />.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument passed to the factory.</typeparam>
		/// <typeparam name="T2">The type of the second argument passed to the factory.</typeparam>
		/// <typeparam name="T3">The type of the third argument passed to the factory.</typeparam>
		/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
		/// <param name="func">The delegate to wrap in a factory.</param>
		/// <returns>The factory.</returns>
		public static DelegateFactory<T1, T2, T3, TResult> From<T1, T2, T3, TResult>(
			Func<T1, T2, T3, TResult> func)
		{
			Contracts.Requires.That(func != null);

			return func.AsFactory();
		}

		/// <summary>
		/// Wraps a <see cref="Func{T1, T2, T3, T4, TResult}" /> in a <see cref="DelegateFactory{T1, T2, T3, T4, TResult}" />.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument passed to the factory.</typeparam>
		/// <typeparam name="T2">The type of the second argument passed to the factory.</typeparam>
		/// <typeparam name="T3">The type of the third argument passed to the factory.</typeparam>
		/// <typeparam name="T4">The type of the forth argument passed to the factory.</typeparam>
		/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
		/// <param name="func">The delegate to wrap in a factory.</param>
		/// <returns>The factory.</returns>
		public static DelegateFactory<T1, T2, T3, T4, TResult> From<T1, T2, T3, T4, TResult>(
			Func<T1, T2, T3, T4, TResult> func)
		{
			Contracts.Requires.That(func != null);

			return func.AsFactory();
		}

		/// <summary>
		/// Wraps a <see cref="Func{T1, T2, T3, T4, T5, TResult}" /> in a
		/// <see cref="DelegateFactory{T1, T2, T3, T4, T5, TResult}" />.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument passed to the factory.</typeparam>
		/// <typeparam name="T2">The type of the second argument passed to the factory.</typeparam>
		/// <typeparam name="T3">The type of the third argument passed to the factory.</typeparam>
		/// <typeparam name="T4">The type of the forth argument passed to the factory.</typeparam>
		/// <typeparam name="T5">The type of the fifth argument passed to the factory.</typeparam>
		/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
		/// <param name="func">The delegate to wrap in a factory.</param>
		/// <returns>The factory.</returns>
		public static DelegateFactory<T1, T2, T3, T4, T5, TResult> From<T1, T2, T3, T4, T5, TResult>(
			Func<T1, T2, T3, T4, T5, TResult> func)
		{
			Contracts.Requires.That(func != null);

			return func.AsFactory();
		}

		/// <summary>
		/// Wraps a <see cref="Func{T1, T2, T3, T4, T5, T6, TResult}" /> in a
		/// <see cref="DelegateFactory{T1, T2, T3, T4, T5, T6, TResult}" />.
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
		public static DelegateFactory<T1, T2, T3, T4, T5, T6, TResult> From<T1, T2, T3, T4, T5, T6, TResult>(
			Func<T1, T2, T3, T4, T5, T6, TResult> func)
		{
			Contracts.Requires.That(func != null);

			return func.AsFactory();
		}

		/// <summary>
		/// Wraps a <see cref="Func{T1, T2, T3, T4, T5, T6, T7, TResult}" /> in a
		/// <see cref="DelegateFactory{T1, T2, T3, T4, T5, T6, T7, TResult}" />.
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
		public static DelegateFactory<T1, T2, T3, T4, T5, T6, T7, TResult> From<T1, T2, T3, T4, T5, T6, T7, TResult>(
			Func<T1, T2, T3, T4, T5, T6, T7, TResult> func)
		{
			Contracts.Requires.That(func != null);

			return func.AsFactory();
		}

		/// <summary>
		/// Wraps a <see cref="Func{T1, T2, T3, T4, T5, T6, T7, T8, TResult}" /> in a
		/// <see cref="DelegateFactory{T1, T2, T3, T4, T5, T6, T7, T8, TResult}" />.
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
		public static DelegateFactory<T1, T2, T3, T4, T5, T6, T7, T8, TResult> From<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
			Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func)
		{
			Contracts.Requires.That(func != null);

			return func.AsFactory();
		}

		#endregion

		#region Asynchronous

		/// <summary>
		/// Wraps a task returning function in a <see cref="AsyncDelegateFactory{TResult}" />.
		/// </summary>
		/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
		/// <param name="func">The delegate to wrap in a factory.</param>
		/// <returns>The factory.</returns>
		public static AsyncDelegateFactory<TResult> FromAsync<TResult>(
			Func<Task<TResult>> func)
		{
			Contracts.Requires.That(func != null);

			return func.AsAsyncFactory();
		}

		/// <summary>
		/// Wraps a task returning function in a <see cref="AsyncDelegateFactory{T, TResult}" />.
		/// </summary>
		/// <typeparam name="T">The type of the argument passed to the factory.</typeparam>
		/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
		/// <param name="func">The delegate to wrap in a factory.</param>
		/// <returns>The factory.</returns>
		public static AsyncDelegateFactory<T, TResult> FromAsync<T, TResult>(
			Func<T, Task<TResult>> func)
		{
			Contracts.Requires.That(func != null);

			return func.AsAsyncFactory();
		}

		/// <summary>
		/// Wraps a task returning function in a <see cref="AsyncDelegateFactory{T1, T2, TResult}" />.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument passed to the factory.</typeparam>
		/// <typeparam name="T2">The type of the second argument passed to the factory.</typeparam>
		/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
		/// <param name="func">The delegate to wrap in a factory.</param>
		/// <returns>The factory.</returns>
		public static AsyncDelegateFactory<T1, T2, TResult> FromAsync<T1, T2, TResult>(
			Func<T1, T2, Task<TResult>> func)
		{
			Contracts.Requires.That(func != null);

			return func.AsAsyncFactory();
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
		public static AsyncDelegateFactory<T1, T2, T3, TResult> FromAsync<T1, T2, T3, TResult>(
			Func<T1, T2, T3, Task<TResult>> func)
		{
			Contracts.Requires.That(func != null);

			return func.AsAsyncFactory();
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
		public static AsyncDelegateFactory<T1, T2, T3, T4, TResult> FromAsync<T1, T2, T3, T4, TResult>(
			Func<T1, T2, T3, T4, Task<TResult>> func)
		{
			Contracts.Requires.That(func != null);

			return func.AsAsyncFactory();
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
		public static AsyncDelegateFactory<T1, T2, T3, T4, T5, TResult> FromAsync<T1, T2, T3, T4, T5, TResult>(
			Func<T1, T2, T3, T4, T5, Task<TResult>> func)
		{
			Contracts.Requires.That(func != null);

			return func.AsAsyncFactory();
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
		public static AsyncDelegateFactory<T1, T2, T3, T4, T5, T6, TResult> FromAsync<T1, T2, T3, T4, T5, T6, TResult>(
			Func<T1, T2, T3, T4, T5, T6, Task<TResult>> func)
		{
			Contracts.Requires.That(func != null);

			return func.AsAsyncFactory();
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
		public static AsyncDelegateFactory<T1, T2, T3, T4, T5, T6, T7, TResult> FromAsync<T1, T2, T3, T4, T5, T6, T7, TResult>(
			Func<T1, T2, T3, T4, T5, T6, T7, Task<TResult>> func)
		{
			Contracts.Requires.That(func != null);

			return func.AsAsyncFactory();
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
		public static AsyncDelegateFactory<T1, T2, T3, T4, T5, T6, T7, T8, TResult> FromAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
			Func<T1, T2, T3, T4, T5, T6, T7, T8, Task<TResult>> func)
		{
			Contracts.Requires.That(func != null);

			return func.AsAsyncFactory();
		}

		#endregion

		#region Asynchronous From Result

		/// <summary>
		/// Wraps a task returning function in a <see cref="AsyncDelegateFactory{TResult}" />.
		/// </summary>
		/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
		/// <param name="func">The delegate to wrap in a factory.</param>
		/// <returns>The factory.</returns>
		public static AsyncDelegateFactory<TResult> FromAsync<TResult>(
			Func<TResult> func)
		{
			Contracts.Requires.That(func != null);

			return func.AsAsyncFactory();
		}

		/// <summary>
		/// Wraps a task returning function in a <see cref="AsyncDelegateFactory{T, TResult}" />.
		/// </summary>
		/// <typeparam name="T">The type of the argument passed to the factory.</typeparam>
		/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
		/// <param name="func">The delegate to wrap in a factory.</param>
		/// <returns>The factory.</returns>
		public static AsyncDelegateFactory<T, TResult> FromAsync<T, TResult>(
			Func<T, TResult> func)
		{
			Contracts.Requires.That(func != null);

			return func.AsAsyncFactory();
		}

		/// <summary>
		/// Wraps a task returning function in a <see cref="AsyncDelegateFactory{T1, T2, TResult}" />.
		/// </summary>
		/// <typeparam name="T1">The type of the first argument passed to the factory.</typeparam>
		/// <typeparam name="T2">The type of the second argument passed to the factory.</typeparam>
		/// <typeparam name="TResult">The result type returned by the factory.</typeparam>
		/// <param name="func">The delegate to wrap in a factory.</param>
		/// <returns>The factory.</returns>
		public static AsyncDelegateFactory<T1, T2, TResult> FromAsync<T1, T2, TResult>(
			Func<T1, T2, TResult> func)
		{
			Contracts.Requires.That(func != null);

			return func.AsAsyncFactory();
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
		public static AsyncDelegateFactory<T1, T2, T3, TResult> FromAsync<T1, T2, T3, TResult>(
			Func<T1, T2, T3, TResult> func)
		{
			Contracts.Requires.That(func != null);

			return func.AsAsyncFactory();
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
		public static AsyncDelegateFactory<T1, T2, T3, T4, TResult> FromAsync<T1, T2, T3, T4, TResult>(
			Func<T1, T2, T3, T4, TResult> func)
		{
			Contracts.Requires.That(func != null);

			return func.AsAsyncFactory();
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
		public static AsyncDelegateFactory<T1, T2, T3, T4, T5, TResult> FromAsync<T1, T2, T3, T4, T5, TResult>(
			Func<T1, T2, T3, T4, T5, TResult> func)
		{
			Contracts.Requires.That(func != null);

			return func.AsAsyncFactory();
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
		public static AsyncDelegateFactory<T1, T2, T3, T4, T5, T6, TResult> FromAsync<T1, T2, T3, T4, T5, T6, TResult>(
			Func<T1, T2, T3, T4, T5, T6, TResult> func)
		{
			Contracts.Requires.That(func != null);

			return func.AsAsyncFactory();
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
		public static AsyncDelegateFactory<T1, T2, T3, T4, T5, T6, T7, TResult> FromAsync<T1, T2, T3, T4, T5, T6, T7, TResult>(
			Func<T1, T2, T3, T4, T5, T6, T7, TResult> func)
		{
			Contracts.Requires.That(func != null);

			return func.AsAsyncFactory();
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
		public static AsyncDelegateFactory<T1, T2, T3, T4, T5, T6, T7, T8, TResult> FromAsync<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
			Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult> func)
		{
			Contracts.Requires.That(func != null);

			return func.AsAsyncFactory();
		}

		#endregion
	}
}
