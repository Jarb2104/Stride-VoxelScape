using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Voxelscape.Utility.Common.Core.Reflection
{
	/// <summary>
	/// Provides methods for loading categories of assemblies given the presence of public static classes marked with
	/// the <see cref="AssemblyLoaderClassAttribute"/>.
	/// </summary>
	public static class LoadAssemblies
	{
		/// <summary>
		/// Loads the assemblies for the specified category names.
		/// </summary>
		/// <param name="categories">The category names of assemblies to load.</param>
		/// <exception cref="AssemblyLoaderException">
		/// No public static classes marked with AssemblyLoaderAttribute were found.
		/// or
		/// No public static parameterless methods marked with LoadAssemblyDependenciesAttribute were found.
		/// </exception>
		public static void ForCategories(params string[] categories)
		{
			foreach (MethodInfo loaderMethod in LoadDependenciesMethods().Where(method =>
				method.GetCustomAttribute<LoadAssemblyDependenciesAttribute>().Categories.ContainsAny(categories)))
			{
				loaderMethod.Invoke(null, null);
			}
		}

		/// <summary>
		/// Loads the assemblies of all category types.
		/// </summary>
		/// <exception cref="Voxelscape.Utility.Common.Core.Reflection.AssemblyLoaderException">
		/// No public static classes marked with AssemblyLoaderAttribute were found.
		/// or
		/// No public static parameterless methods marked with LoadAssemblyDependenciesAttribute were found.
		/// </exception>
		public static void AnyCategory()
		{
			foreach (MethodInfo loaderMethod in LoadDependenciesMethods())
			{
				loaderMethod.Invoke(null, null);
			}
		}

		/// <summary>
		/// An enumeration of the load dependencies methods found in the currently accessible assemblies.
		/// </summary>
		/// <returns>The load dependencies methods found.</returns>
		private static IEnumerable<MethodInfo> LoadDependenciesMethods()
		{
			// find all public static types marker with AssemblyLoaderAttribute
			Type[] assemblyLoaders = AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(assembly => assembly.GetTypes())
				.Where(type =>
					type.GetCustomAttribute<AssemblyLoaderClassAttribute>() != null &&
					type.IsPublic &&
					type.IsStaticClass()).ToArray();

			// if none found, error
			if (assemblyLoaders.Length == 0)
			{
				throw new AssemblyLoaderException(
					"No public static classes marked with AssemblyLoaderAttribute were found.");
			}

			foreach (Type loaderClass in assemblyLoaders)
			{
				// find all public static parameterless methods marked with LoadAssemblyDependenciesAttribute
				MethodInfo[] loadDependenciesMethods = loaderClass.GetMethods(BindingFlags.Static | BindingFlags.Public)
					.Where(method =>
						method.GetCustomAttribute<LoadAssemblyDependenciesAttribute>() != null &&
						method.GetParameters().Length == 0).ToArray();

				// if none found, error
				if (loadDependenciesMethods.Length == 0)
				{
					throw new AssemblyLoaderException(
						"No public static parameterless methods marked with LoadAssemblyDependenciesAttribute were found.");
				}

				foreach (MethodInfo method in loadDependenciesMethods)
				{
					yield return method;
				}
			}
		}
	}
}
