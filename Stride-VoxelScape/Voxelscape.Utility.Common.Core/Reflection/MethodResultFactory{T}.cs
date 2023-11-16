using System.Reflection;
using Voxelscape.Utility.Common.Pact.Diagnostics;
using Voxelscape.Utility.Common.Pact.Factories;

namespace Voxelscape.Utility.Common.Core.Reflection
{
	/// <summary>
	/// A factory that creates values by invoking a specified method through reflection.
	/// </summary>
	/// <typeparam name="T">The type of the result value produced from invoking the method.</typeparam>
	public class MethodResultFactory<T> : IFactory<T>
	{
		/// <summary>
		/// The method to invoke to create the value to cache.
		/// </summary>
		private readonly MethodInfo method;

		/// <summary>
		/// The instance to invoke the method from.
		/// </summary>
		private readonly object instance;

		/// <summary>
		/// The arguments to pass to the method to create the value.
		/// </summary>
		private readonly object[] arguments;

		/// <summary>
		/// Initializes a new instance of the <see cref="MethodResultFactory{T}"/> class.
		/// </summary>
		/// <param name="method">The method invoked to create the value.</param>
		/// <param name="instance">The instance to invoke the method from.</param>
		/// <param name="arguments">The arguments to pass to the method.</param>
		public MethodResultFactory(MethodInfo method, object instance, object[] arguments)
		{
			// both instance and arguments can be null
			Contracts.Requires.That(method != null);

			this.method = method;
			this.instance = instance;
			this.arguments = arguments;
		}

		#region IFactory<T> Members

		/// <inheritdoc />
		public T Create()
		{
			return (T)ReflectionUtilities.InvokeMethod(this.method, this.instance, this.arguments);
		}

		#endregion
	}
}
