using System;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;

namespace Voxelscape.Utility.Common.Core.Collections
{
	/// <summary>
	///
	/// </summary>
	public static class ArrayUtilities
	{
		public static T[] Empty<T>() => EmptyArray<T>.Instance;

		/// <summary>
		/// Creates a new one dimensional array filled in with the specified initial value.
		/// </summary>
		/// <typeparam name="T">The type of the values in the array.</typeparam>
		/// <param name="length">The length of array to create.</param>
		/// <param name="initialValue">The initial value to copy to every slot of the array.</param>
		/// <returns>The new array with values filled in.</returns>
		/// <remarks>
		/// If <typeparamref name="T"/> is a reference type then each slot of the array will contain a reference
		/// back to the same object (or null if null was passed in). If you want a unique instance of an object
		/// for every slot of the array use one of the other overloads.
		/// </remarks>
		public static T[] New<T>(int length, T initialValue)
		{
			Contracts.Requires.That(length >= 0);

			T[] result = new T[length];

			for (int index = 0; index < length; index++)
			{
				result[index] = initialValue;
			}

			return result;
		}

		/// <summary>
		/// Creates a new one dimensional array filled in with values returned by the specified delegate.
		/// </summary>
		/// <typeparam name="T">The type of the values in the array.</typeparam>
		/// <param name="length">The length of array to create.</param>
		/// <param name="initialValues">The delegate used to return the initial values.</param>
		/// <returns>The new array with values filled in.</returns>
		public static T[] New<T>(int length, Func<T> initialValues)
		{
			Contracts.Requires.That(length >= 0);
			Contracts.Requires.That(initialValues != null);

			T[] result = new T[length];

			for (int index = 0; index < length; index++)
			{
				result[index] = initialValues();
			}

			return result;
		}

		/// <summary>
		/// Creates a new one dimensional array filled in with values returned by the specified factory.
		/// </summary>
		/// <typeparam name="T">The type of the values in the array.</typeparam>
		/// <param name="length">The length of array to create.</param>
		/// <param name="initialValues">The factory used to return the initial values.</param>
		/// <returns>The new array with values filled in.</returns>
		public static T[] New<T>(int length, IFactory<T> initialValues)
		{
			Contracts.Requires.That(length >= 0);
			Contracts.Requires.That(initialValues != null);

			return New(length, initialValues.Create);
		}

		/// <summary>
		/// Creates a new one dimensional array filled in with values returned by the specified delegate.
		/// </summary>
		/// <typeparam name="T">The type of the values in the array.</typeparam>
		/// <param name="length">The length of array to create.</param>
		/// <param name="initialValues">The delegate used to return the initial values.</param>
		/// <returns>The new array with values filled in.</returns>
		/// <remarks>
		/// The delegate will be passed in the index of each value as they are created.
		/// </remarks>
		public static T[] New<T>(int length, Func<int, T> initialValues)
		{
			Contracts.Requires.That(length >= 0);
			Contracts.Requires.That(initialValues != null);

			T[] result = new T[length];

			for (int index = 0; index < length; index++)
			{
				result[index] = initialValues(index);
			}

			return result;
		}

		/// <summary>
		/// Creates a new one dimensional array filled in with values returned by the specified factory.
		/// </summary>
		/// <typeparam name="T">The type of the values in the array.</typeparam>
		/// <param name="length">The length of array to create.</param>
		/// <param name="initialValues">The factory used to return the initial values.</param>
		/// <returns>The new array with values filled in.</returns>
		/// <remarks>
		/// The factory will be passed in the index of each value as they are created.
		/// </remarks>
		public static T[] New<T>(int length, IFactory<int, T> initialValues)
		{
			Contracts.Requires.That(length >= 0);
			Contracts.Requires.That(initialValues != null);

			return New(length, initialValues.Create);
		}

		private static class EmptyArray<T>
		{
			public static T[] Instance { get; } = new T[0];
		}
	}
}
