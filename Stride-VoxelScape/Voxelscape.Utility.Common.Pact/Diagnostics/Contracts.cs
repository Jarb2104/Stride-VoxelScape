using System;
using System.Diagnostics;

namespace Voxelscape.Utility.Common.Pact.Diagnostics
{
	/// <summary>
	/// Contains static methods for representing program contracts such as preconditions.
	/// </summary>
	public static class Contracts
	{
		public static class Requires
		{
			public const string CompilationSymbol = "CONTRACTSREQUIRES";

			/// <summary>
			/// Specifies a contract that must be met and throws an exception with the given message if it is violated.
			/// </summary>
			/// <param name="condition">The conditional expression to test.</param>
			/// <param name="message">The message to display if the condition is false.</param>
			/// <exception cref="ContractException">Thrown if the condition is false.</exception>
			[Conditional(CompilationSymbol)]
			public static void That(bool condition, string message = null)
			{
				if (!condition)
				{
					throw new ContractException(message);
				}
			}

			/// <summary>
			/// Specifies a contract that must be met and throws an exception with the given message if it is violated.
			/// </summary>
			/// <param name="condition">The conditional expression to test.</param>
			/// <param name="message">
			/// The function used to generate a message to display if the condition is false.
			/// Only executed if the contract is violated.
			/// </param>
			/// <exception cref="ContractException">Thrown if the condition is false.</exception>
			[Conditional(CompilationSymbol)]
			public static void That(bool condition, Func<string> message)
			{
				That(message != null);

				if (!condition)
				{
					throw new ContractException(message());
				}
			}
		}

		public static class Assert
		{
			public const string CompilationSymbol = "CONTRACTSASSERT";

			/// <summary>
			/// Specifies a contract that must be met and throws an exception with the given message if it is violated.
			/// </summary>
			/// <param name="condition">The conditional expression to test.</param>
			/// <param name="message">The message to display if the condition is false.</param>
			/// <exception cref="ContractException">Thrown if the condition is false.</exception>
			[Conditional(CompilationSymbol)]
			public static void That(bool condition, string message = null)
			{
				if (!condition)
				{
					throw new ContractException(message);
				}
			}

			/// <summary>
			/// Specifies a contract that must be met and throws an exception with the given message if it is violated.
			/// </summary>
			/// <param name="condition">The conditional expression to test.</param>
			/// <param name="message">
			/// The function used to generate a message to display if the condition is false.
			/// Only executed if the contract is violated.
			/// </param>
			/// <exception cref="ContractException">Thrown if the condition is false.</exception>
			[Conditional(CompilationSymbol)]
			public static void That(bool condition, Func<string> message)
			{
				That(message != null);

				if (!condition)
				{
					throw new ContractException(message());
				}
			}

			/// <summary>
			/// Asserts that this method call is never reached.
			/// </summary>
			/// <param name="message">The message to display if the condition is false.</param>
			/// <exception cref="ContractException">Thrown if this method called.</exception>
			[Conditional(CompilationSymbol)]
			public static void NotReached(string message = null)
			{
				throw new ContractException(message);
			}
		}
	}
}
