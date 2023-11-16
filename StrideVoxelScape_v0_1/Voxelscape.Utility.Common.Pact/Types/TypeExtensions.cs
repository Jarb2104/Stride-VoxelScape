using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for the <see cref="Type"/> class.
/// </summary>
public static class TypeExtensions
{
	/// <summary>
	/// Determines whether the specified type can be assigned null.
	/// </summary>
	/// <param name="type">The type.</param>
	/// <returns>True if the type can be assigned null; otherwise false.</returns>
	public static bool IsNullAssignableTo(this Type type)
	{
		Contracts.Requires.That(type != null);

		return !type.IsValueType || (Nullable.GetUnderlyingType(type) != null);
	}

	/// <summary>
	/// Determines whether the specified type is a reference type.
	/// </summary>
	/// <param name="type">The type.</param>
	/// <returns>True if the type is a reference type; otherwise false.</returns>
	/// <remarks>
	/// This method just does the same this as IsClass, but it provides a name that is more consistent with IsValueType.
	/// </remarks>
	public static bool IsReferenceType(this Type type)
	{
		Contracts.Requires.That(type != null);

		return type.IsClass;
	}

	/// <summary>
	/// Determines whether the specified type is a user defined structure.
	/// </summary>
	/// <param name="type">The type.</param>
	/// <returns>True if the type is a structure; otherwise false.</returns>
	public static bool IsStruct(this Type type)
	{
		Contracts.Requires.That(type != null);

		return type.IsValueType && !type.IsEnum && !type.IsPrimitive;
	}

	/// <summary>
	/// Determines whether the specified type is a static class.
	/// </summary>
	/// <param name="type">The type.</param>
	/// <returns>True if the type is a static class; otherwise false.</returns>
	public static bool IsStaticClass(this Type type)
	{
		Contracts.Requires.That(type != null);

		return type.IsClass && type.IsAbstract && type.IsSealed;
	}

	/// <summary>
	/// Determines whether the type is an implementation of the specified type.
	/// </summary>
	/// <param name="type">The type.</param>
	/// <param name="typeDefinition">The type definition to check for.</param>
	/// <returns>True if the type is an implementation of the specified type; otherwise false.</returns>
	public static bool IsImplementationOfType(this Type type, Type typeDefinition)
	{
		Contracts.Requires.That(type != null);
		Contracts.Requires.That(typeDefinition != null);

		return type == typeDefinition
			|| type.GetInterfaces().Contains(typeDefinition)
			|| (type.BaseType != null && type.BaseType.IsImplementationOfType(typeDefinition));
	}

	/// <summary>
	/// Determines whether the specified type implements any version of the specified generic type definition.
	/// </summary>
	/// <param name="type">The type.</param>
	/// <param name="genericTypeDefinition">The generic type definition to check for.</param>
	/// <returns>True if the type is or implements the generic type definition; otherwise false.</returns>
	public static bool IsImplementationOfGenericType(this Type type, Type genericTypeDefinition)
	{
		Contracts.Requires.That(type != null);
		Contracts.Requires.That(genericTypeDefinition != null);
		Contracts.Requires.That(genericTypeDefinition.IsGenericTypeDefinition);

		// this conditional is necessary if type is an interface,
		// because an interface doesn't implement itself
		if (type.IsInterface &&
			type.IsGenericType &&
			type.GetGenericTypeDefinition() == genericTypeDefinition)
		{
			return true;
		}

		return type.GetInterfaces().Any(value =>
			value.IsGenericType && value.GetGenericTypeDefinition() == genericTypeDefinition);
	}

	/// <summary>
	/// Gets all generic types the specified type implements or extends that match
	/// any version of the specified generic type definition.
	/// </summary>
	/// <param name="type">The type.</param>
	/// <param name="genericTypeDefinition">The generic type definition to find.</param>
	/// <returns>
	/// An array of all the types that specified type implements or extends that match the generic type definition.
	/// </returns>
	public static Type[] GetImplementationsOfGenericType(this Type type, Type genericTypeDefinition)
	{
		Contracts.Requires.That(type != null);
		Contracts.Requires.That(genericTypeDefinition != null);
		Contracts.Requires.That(genericTypeDefinition.IsGenericTypeDefinition);

		IList<Type> results = new List<Type>();

		// this conditional is necessary if type is an interface,
		// because an interface doesn't implement itself
		if (type.IsInterface &&
			type.IsGenericType &&
			type.GetGenericTypeDefinition() == genericTypeDefinition)
		{
			results.Add(type);
		}

		results.AddMany(type.GetInterfaces().Where(value =>
			value.IsGenericType && value.GetGenericTypeDefinition() == genericTypeDefinition));

		return results.ToArray();
	}

	/// <summary>
	/// Determines whether the specified type is an enumerable type.
	/// </summary>
	/// <param name="type">The type.</param>
	/// <returns>True if the type is enumerable; otherwise false.</returns>
	public static bool IsEnumerableType(this Type type)
	{
		Contracts.Requires.That(type != null);

		return type.IsArray
			|| type.IsImplementationOfGenericType(typeof(IEnumerable<>))
			|| type.GetInterfaces().Any(value => value == typeof(IEnumerable));
	}

	/// <summary>
	/// Gets the types that the specified type can be enumerated for.
	/// </summary>
	/// <param name="type">The type.</param>
	/// <returns>An array of all the types that the specified type can be enumerated for.</returns>
	public static Type[] GetEnumerableTypes(this Type type)
	{
		Contracts.Requires.That(type != null);

		ISet<Type> results = new HashSet<Type>();

		if (type.IsArray)
		{
			results.Add(type.GetElementType());
		}

		// GetGenericArguments()[0] returns the first generic argument of IEnumerable<>, the type being enumerated
		results.AddMany(type.GetImplementationsOfGenericType(typeof(IEnumerable<>))
			.Select(value => value.GetGenericArguments()[0]));

		return results.ToArray();
	}

	/// <summary>
	/// Determines whether the specified type has a public parameterless constructor (a default constructor).
	/// </summary>
	/// <param name="type">The type.</param>
	/// <returns>True if the type has a public parameterless constructor; otherwise false.</returns>
	public static bool HasDefaultConstructor(this Type type)
	{
		Contracts.Requires.That(type != null);

		return type.GetConstructor(Type.EmptyTypes) != null;
	}

	/// <summary>
	/// Determines whether the specified type is instantiatable through a default constructor. To be instantiatable the type
	/// must not be abstract, must not have any open generic parameters, and must have a public parameterless constructor.
	/// </summary>
	/// <param name="type">The type.</param>
	/// <returns>
	/// True if the type is not abstract, does not have any open generic parameters, and has a public parameterless constructor;
	/// otherwise false.
	/// </returns>
	public static bool IsDefaultInstantiatable(this Type type)
	{
		Contracts.Requires.That(type != null);

		return !type.IsAbstract
			&& !type.ContainsGenericParameters
			&& type.HasDefaultConstructor();
	}
}
