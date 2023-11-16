namespace Voxelscape.Utility.Concurrency.Pact.Synchronization
{
	/// <summary>
	/// An mutable atmoic value that can be shared efficiently by multiple threads.
	/// </summary>
	/// <typeparam name="T">The type of the variable.</typeparam>
	/// <remarks>
	/// This interface provides an object oriented wrapping of the
	/// <see href="https://msdn.microsoft.com/en-us/library/system.threading.interlocked(v=vs.110).aspx">
	/// Interlocked Class</see>.
	/// </remarks>
	public interface IAtomicValue<T>
	{
		/// <summary>
		/// Adds a value to the current value of this instance and stores the sum, as an atomic operation.
		/// </summary>
		/// <param name="value">The value to be added to this instance.</param>
		/// <returns>The new value of this instance.</returns>
		/// <remarks>
		/// This method handles an overflow condition by wrapping. No exception is thrown.
		/// </remarks>
		T Add(T value);

		/// <summary>
		/// Compares a value to this instance for equality and, if they are equal,
		/// replaces the value in this instance, as an atomic operation.
		/// </summary>
		/// <param name="value">The value that replaces this instance's value if the comparison results in equality.</param>
		/// <param name="comparand">The value that is compared to the value in this instance.</param>
		/// <returns>The original value in this instance.</returns>
		/// <remarks>
		/// If <paramref name="comparand"/> and the value in this instance are equal, then <paramref name="value"/>
		/// is stored in this instance. Otherwise, no operation is performed. The compare and exchange operations
		/// are performed as an atomic operation. The return value is the original value in this instance, whether
		/// or not the exchange takes place.
		/// </remarks>
		T CompareExchange(T value, T comparand);

		/// <summary>
		/// Sets this instance to a specified value and returns this instance's original value, as an atomic operation.
		/// </summary>
		/// <param name="value">The value to which this instance is set.</param>
		/// <returns>The original value of this instance.</returns>
		T Exchange(T value);

		/// <summary>
		/// Decrements this instance by 1 and stores the result, as an atomic operation.
		/// </summary>
		/// <returns>The decremented value.</returns>
		/// <remarks>
		/// This method handles an overflow condition by wrapping. No exception is thrown.
		/// </remarks>
		T Decrement();

		/// <summary>
		/// Increments this instance by 1 and stores the result, as an atomic operation.
		/// </summary>
		/// <returns>The incremented value.</returns>
		/// <remarks>
		/// This method handles an overflow condition by wrapping. No exception is thrown.
		/// </remarks>
		T Increment();

		/// <summary>
		/// Returns the value currently in this instance.
		/// </summary>
		/// <returns>The value in this instance.</returns>
		/// <remarks>
		/// The value returned by this method is as current as possible. However, if multiple threads are
		/// modifying and accessing this instance, the value read by this method can change immediately
		/// after returning it.
		/// </remarks>
		T Read();

		/// <summary>
		/// Sets this instance to a specified value.
		/// </summary>
		/// <param name="value">The value to which this instance is set.</param>
		void Write(T value);
	}
}
