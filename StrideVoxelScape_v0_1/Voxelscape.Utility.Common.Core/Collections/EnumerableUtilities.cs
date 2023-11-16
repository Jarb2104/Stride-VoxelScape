using System;
using System.Collections.Generic;
using System.Diagnostics;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;
using Voxelscape.Utility.Common.Pact.Mathematics;

namespace Voxelscape.Utility.Common.Core.Collections
{
	/// <summary>
	/// Provides factory methods for creating enumerable sequences.
	/// </summary>
	public static class EnumerableUtilities
	{
		#region New

		/// <summary>
		/// Creates a new enumerable sequence by repeating values returned from a delegate.
		/// </summary>
		/// <typeparam name="T">The type of enumerable value.</typeparam>
		/// <param name="length">The length of the enumerable to generate.</param>
		/// <param name="values">The delegate used for creating values.</param>
		/// <returns>The resulting enumerable sequence.</returns>
		public static IEnumerable<T> New<T>(int length, Func<T> values)
		{
			Contracts.Requires.That(length >= 0);
			Contracts.Requires.That(values != null);

			return New(length, values.AsFactory());
		}

		/// <summary>
		/// Creates a new enumerable sequence by repeating values returned from a factory.
		/// </summary>
		/// <typeparam name="T">The type of enumerable value.</typeparam>
		/// <param name="length">The length of the enumerable to generate.</param>
		/// <param name="values">The factory used for creating values.</param>
		/// <returns>The resulting enumerable sequence.</returns>
		public static IEnumerable<T> New<T>(int length, IFactory<T> values)
		{
			Contracts.Requires.That(length >= 0);
			Contracts.Requires.That(values != null);

			for (int count = 0; count < length; count++)
			{
				yield return values.Create();
			}
		}

		/// <summary>
		/// Creates a new enumerable sequence by repeating values returned from a delegate.
		/// </summary>
		/// <typeparam name="TState">The type of the state.</typeparam>
		/// <typeparam name="T">The type of enumerable value.</typeparam>
		/// <param name="length">The length of the enumerable to generate.</param>
		/// <param name="values">The delegate used for creating values.</param>
		/// <param name="state">The state to pass to the delegate. This can be null.</param>
		/// <returns>The resulting enumerable sequence.</returns>
		public static IEnumerable<T> New<TState, T>(int length, Func<TState, T> values, TState state)
		{
			Contracts.Requires.That(length >= 0);
			Contracts.Requires.That(values != null);

			return New(length, values.AsFactory(), state);
		}

		/// <summary>
		/// Creates a new enumerable sequence by repeating values returned from a factory.
		/// </summary>
		/// <typeparam name="TState">The type of the state.</typeparam>
		/// <typeparam name="T">The type of enumerable value.</typeparam>
		/// <param name="length">The length of the enumerable to generate.</param>
		/// <param name="values">The factory used for creating values.</param>
		/// <param name="state">The state to pass to the factory. This can be null.</param>
		/// <returns>The resulting enumerable sequence.</returns>
		public static IEnumerable<T> New<TState, T>(int length, IFactory<TState, T> values, TState state)
		{
			Contracts.Requires.That(length >= 0);
			Contracts.Requires.That(values != null);

			for (int count = 0; count < length; count++)
			{
				yield return values.Create(state);
			}
		}

		#endregion

		#region NewRandom

		/// <summary>
		/// Creates a new random enumerable sequence by repeating values returned from a delegate passed
		/// a specified <see cref="Random"/> instance.
		/// </summary>
		/// <typeparam name="T">The type of enumerable value.</typeparam>
		/// <param name="length">The length of the enumerable to generate.</param>
		/// <param name="values">The delegate used for creating values given a <see cref="Random"/>.</param>
		/// <returns>The resulting enumerable sequence.</returns>
		/// <remarks>
		/// This overload will automatically pass <see cref="ThreadLocalRandom.Instance"/> to the delegate.
		/// </remarks>
		public static IEnumerable<T> NewRandom<T>(int length, Func<Random, T> values)
		{
			Contracts.Requires.That(length >= 0);
			Contracts.Requires.That(values != null);

			return New(length, values, ThreadLocalRandom.Instance);
		}

		/// <summary>
		/// Creates a new random enumerable sequence by repeating values returned from a factory passed
		/// a specified <see cref="Random"/> instance.
		/// </summary>
		/// <typeparam name="T">The type of enumerable value.</typeparam>
		/// <param name="length">The length of the enumerable to generate.</param>
		/// <param name="values">The factory used for creating values given a <see cref="Random"/>.</param>
		/// <returns>The resulting enumerable sequence.</returns>
		/// <remarks>
		/// This overload will automatically pass <see cref="ThreadLocalRandom.Instance"/> to the factory.
		/// </remarks>
		public static IEnumerable<T> NewRandom<T>(int length, IFactory<Random, T> values)
		{
			Contracts.Requires.That(length >= 0);
			Contracts.Requires.That(values != null);

			return New(length, values, ThreadLocalRandom.Instance);
		}

		/// <summary>
		/// Creates a new random enumerable sequence by repeating values returned from a delegate passed
		/// a specified <see cref="Random"/> instance.
		/// </summary>
		/// <typeparam name="T">The type of enumerable value.</typeparam>
		/// <param name="length">The length of the enumerable to generate.</param>
		/// <param name="values">The delegate used for creating values given a <see cref="Random"/>.</param>
		/// <param name="random">The <see cref="Random"/> to pass to the delegate.</param>
		/// <returns>The resulting enumerable sequence.</returns>
		public static IEnumerable<T> NewRandom<T>(int length, Func<Random, T> values, Random random)
		{
			Contracts.Requires.That(length >= 0);
			Contracts.Requires.That(values != null);
			Contracts.Requires.That(random != null);

			return New(length, values, random);
		}

		/// <summary>
		/// Creates a new random enumerable sequence by repeating values returned from a factory passed
		/// a specified <see cref="Random"/> instance.
		/// </summary>
		/// <typeparam name="T">The type of enumerable value.</typeparam>
		/// <param name="length">The length of the enumerable to generate.</param>
		/// <param name="values">The factory used for creating values given a <see cref="Random"/>.</param>
		/// <param name="random">The <see cref="Random"/> to pass to the factory.</param>
		/// <returns>The resulting enumerable sequence.</returns>
		public static IEnumerable<T> NewRandom<T>(int length, IFactory<Random, T> values, Random random)
		{
			Contracts.Requires.That(length >= 0);
			Contracts.Requires.That(values != null);
			Contracts.Requires.That(random != null);

			return New(length, values, random);
		}

		/// <summary>
		/// Creates a new random enumerable sequence by repeating values returned from a delegate passed
		/// a specified <see cref="Random"/> instance.
		/// </summary>
		/// <typeparam name="T">The type of enumerable value.</typeparam>
		/// <param name="minimumLength">The minimum length of the enumerable to generate.</param>
		/// <param name="maximumLength">The maximum length of the enumerable to generate.</param>
		/// <param name="values">The delegate used for creating values given a <see cref="Random"/>.</param>
		/// <returns>The resulting enumerable sequence.</returns>
		/// <remarks>
		/// This overload will automatically pass <see cref="ThreadLocalRandom.Instance"/> to the delegate.
		/// </remarks>
		public static IEnumerable<T> NewRandom<T>(int minimumLength, int maximumLength, Func<Random, T> values)
		{
			SharedRandomLengthContracts<T>(minimumLength, maximumLength);
			Contracts.Requires.That(values != null);

			return NewRandom(minimumLength, maximumLength, values.AsFactory(), ThreadLocalRandom.Instance);
		}

		/// <summary>
		/// Creates a new random enumerable sequence by repeating values returned from a factory passed
		/// a specified <see cref="Random"/> instance.
		/// </summary>
		/// <typeparam name="T">The type of enumerable value.</typeparam>
		/// <param name="minimumLength">The minimum length of the enumerable to generate.</param>
		/// <param name="maximumLength">The maximum length of the enumerable to generate.</param>
		/// <param name="values">The factory used for creating values given a <see cref="Random"/>.</param>
		/// <returns>The resulting enumerable sequence.</returns>
		/// <remarks>
		/// This overload will automatically pass <see cref="ThreadLocalRandom.Instance"/> to the factory.
		/// </remarks>
		public static IEnumerable<T> NewRandom<T>(int minimumLength, int maximumLength, IFactory<Random, T> values)
		{
			SharedRandomLengthContracts<T>(minimumLength, maximumLength);
			Contracts.Requires.That(values != null);

			return NewRandom(minimumLength, maximumLength, values, ThreadLocalRandom.Instance);
		}

		/// <summary>
		/// Creates a new random enumerable sequence by repeating values returned from a delegate passed
		/// a specified <see cref="Random"/> instance.
		/// </summary>
		/// <typeparam name="T">The type of enumerable value.</typeparam>
		/// <param name="minimumLength">The minimum length of the enumerable to generate.</param>
		/// <param name="maximumLength">The maximum length of the enumerable to generate.</param>
		/// <param name="values">The delegate used for creating values given a <see cref="Random"/>.</param>
		/// <param name="random">The <see cref="Random"/> to pass to the delegate.</param>
		/// <returns>The resulting enumerable sequence.</returns>
		public static IEnumerable<T> NewRandom<T>(
			int minimumLength, int maximumLength, Func<Random, T> values, Random random)
		{
			SharedRandomLengthContracts<T>(minimumLength, maximumLength);
			Contracts.Requires.That(values != null);
			Contracts.Requires.That(random != null);

			return NewRandom(minimumLength, maximumLength, values.AsFactory(), random);
		}

		/// <summary>
		/// Creates a new random enumerable sequence by repeating values returned from a factory passed
		/// a specified <see cref="Random"/> instance.
		/// </summary>
		/// <typeparam name="T">The type of enumerable value.</typeparam>
		/// <param name="minimumLength">The minimum length of the enumerable to generate.</param>
		/// <param name="maximumLength">The maximum length of the enumerable to generate.</param>
		/// <param name="values">The factory used for creating values given a <see cref="Random"/>.</param>
		/// <param name="random">The <see cref="Random"/> to pass to the factory.</param>
		/// <returns>The resulting enumerable sequence.</returns>
		public static IEnumerable<T> NewRandom<T>(
			int minimumLength, int maximumLength, IFactory<Random, T> values, Random random)
		{
			SharedRandomLengthContracts<T>(minimumLength, maximumLength);
			Contracts.Requires.That(values != null);
			Contracts.Requires.That(random != null);

			// + 1 because Random.Next has an inclusive minimum but an exclusive maximum
			return New(random.Next(minimumLength, maximumLength + 1), values, random);
		}

		#endregion

		/// <summary>
		/// The shared contracts for random length methods.
		/// </summary>
		/// <typeparam name="T">The type of the enumerable values.</typeparam>
		/// <param name="minimumLength">The minimum length of the enumerable to generate.</param>
		/// <param name="maximumLength">The maximum length of the enumerable to generate.</param>
		[Conditional(Contracts.Requires.CompilationSymbol)]
		private static void SharedRandomLengthContracts<T>(int minimumLength, int maximumLength)
		{
			Contracts.Requires.That(minimumLength >= 0);
			Contracts.Requires.That(maximumLength >= 0);
			Contracts.Requires.That(minimumLength <= maximumLength);
		}
	}
}
