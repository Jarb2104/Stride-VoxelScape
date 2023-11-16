using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Reflection
{
	/// <summary>
	/// Providers useful methods for performing reflection.
	/// </summary>
	public static class ReflectionUtilities
	{
		/// <summary>
		/// Instantiates instances of all classes found implementing the specified interface, using their default constructors.
		/// </summary>
		/// <typeparam name="TInterface">The type of the interface.</typeparam>
		/// <returns>Instances of all classes implementing the specified interface.</returns>
		public static IEnumerable<TInterface> GetInstancesImplementing<TInterface>()
		{
			return GetInstancesImplementing(typeof(TInterface)).Cast<TInterface>();
		}

		/// <summary>
		/// Instantiates instances of all classes found implementing the specified interface, using their default constructors.
		/// </summary>
		/// <param name="interfaceType">Type of the interface.</param>
		/// <returns>Instances of all classes implementing the specified interface.</returns>
		public static IEnumerable<object> GetInstancesImplementing(Type interfaceType)
		{
			Contracts.Requires.That(interfaceType != null);
			Contracts.Requires.That(interfaceType.IsInterface);

			// find types that implement interface, aren't abstract, have no generic parameters, and a default constructor
			Type[] types = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(assembly => assembly.GetTypes())
				.Where(type => type.GetInterfaces().Contains(interfaceType))
				.ToArray();

			// create the non-generic instances using default constructor only
			List<object> instances = new List<object>(types.Length);
			foreach (Type type in types)
			{
				object createdInstance;
				if (TryCreateInstance(type, out createdInstance))
				{
					instances.Add(createdInstance);
				}
			}

			return instances.AsReadOnly();
		}

		/// <summary>
		/// Tries to the create instance of the specified type using a default constructor.
		/// </summary>
		/// <param name="type">The type to try and create an instance of.</param>
		/// <param name="instance">The instance if it was successfully created; otherwise false.</param>
		/// <returns>True if an instance of the type was successfully created; otherwise false.</returns>
		public static bool TryCreateInstance(Type type, out object instance)
		{
			Exception exception;
			return TryCreateInstance(type, out instance, out exception);
		}

		/// <summary>
		/// Tries to the create instance of the specified type using a default constructor.
		/// </summary>
		/// <param name="type">The type to try and create an instance of.</param>
		/// <param name="instance">The instance if it was successfully created; otherwise false.</param>
		/// <param name="exception">
		/// The exception thrown if the type's default constructor threw an exception; otherwise null.
		/// </param>
		/// <returns>True if an instance of the type was successfully created; otherwise false.</returns>
		public static bool TryCreateInstance(Type type, out object instance, out Exception exception)
		{
			Contracts.Requires.That(type != null);

			if (!type.IsDefaultInstantiatable())
			{
				instance = null;
				exception = null;
				return false;
			}

			try
			{
				instance = Activator.CreateInstance(type);
				exception = null;
				return true;
			}
			catch (Exception instantiationException)
			{
				// instance failed to create because of special environment requirements
				exception = instantiationException;
				instance = null;
				return false;
			}
		}

		/// <summary>
		/// Invokes the method or constructor represented by the current instance, using the specified parameters.
		/// </summary>
		/// <param name="method">The method to invoke.</param>
		/// <param name="instance">
		/// The object on which to invoke the method or constructor. If a method is static, this argument is ignored.
		/// If a constructor is static, this argument must be null or an instance of the class that defines the constructor.
		/// </param>
		/// <param name="parameters">
		/// <para>
		/// An argument list for the invoked method or constructor. This is an array of objects with the same number, order,
		/// and type as the parameters of the method or constructor to be invoked. If there are no parameters, parameters
		/// should be null.
		/// </para><para>
		/// If the method or constructor represented by this instance takes a ref parameter (ByRef in Visual Basic), no
		/// special attribute is required for that parameter in order to invoke the method or constructor using this function.
		/// Any object in this array that is not explicitly initialized with a value will contain the default value for that
		/// object type. For reference-type elements, this value is null. For value-type elements, this value is 0, 0.0, or
		/// false, depending on the specific element type.
		/// </para>
		/// </param>
		/// <returns>An object containing the return value of the invoked method, or null in the case of a constructor.</returns>
		/// <remarks>
		/// Use this method when you want to invoke a method through reflection, but would like exceptions thrown by the
		/// invoked method to appear as normal exceptions instead of them being wrapped in an <see cref="TargetInvocationException"/>
		/// like they normally are when using <see cref="MethodBase.Invoke(object, object[])"/>.
		/// </remarks>
		/// <seealso href="https://msdn.microsoft.com/en-us/library/a89hcwhh(v=vs.110).aspx"/>
		public static object InvokeMethod(MethodBase method, object instance, object[] parameters)
		{
			// both instance and parameters can be null
			Contracts.Requires.That(method != null);

			try
			{
				return method.Invoke(instance, parameters);
			}
			catch (TargetInvocationException exception)
			{
				if (exception.InnerException != null)
				{
					// if the invoked method threw an exception, rethrow that exception directly
					// don't wrap it in a TargetInvocationException
					throw exception.InnerException;
				}
				else
				{
					// if there is no inner exception then rethrow the TargetInvocationException
					// without losing the stack trace
					// (this might happen if type is a DynamicMethod with unverifiable code?)
					// see https://msdn.microsoft.com/en-us/library/a89hcwhh(v=vs.110).aspx
					throw;
				}
			}
		}

		/// <summary>
		/// Ensures all referenced assemblies are loaded.
		/// </summary>
		/// <remarks>
		/// This will use recursion to ensure that all referenced assemblies, including those referenced by a referenced assembly
		/// and so on are loaded. However, this cannot account for assemblies there were dereferenced by the compiler for not
		/// containing any dependent upon code.
		/// </remarks>
		public static void EnsureAllReferencedAssembliesAreLoaded()
		{
			foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
			{
				LoadReferencedAssemblies(assembly);
			}
		}

		/// <summary>
		/// Loads the referenced assembly and all assemblies referenced by it.
		/// </summary>
		/// <param name="assembly">The assembly to load.</param>
		private static void LoadReferencedAssemblies(Assembly assembly)
		{
			foreach (AssemblyName name in assembly.GetReferencedAssemblies())
			{
				if (!AppDomain.CurrentDomain.GetAssemblies().Any(a => a.FullName == name.FullName))
				{
					LoadReferencedAssemblies(Assembly.Load(name));
				}
			}
		}
	}
}
