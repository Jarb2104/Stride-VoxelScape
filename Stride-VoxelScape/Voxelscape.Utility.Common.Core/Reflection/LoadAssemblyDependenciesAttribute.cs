using System;
using System.Collections.Generic;
using System.Linq;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Utility.Common.Core.Reflection
{
	/// <summary>
	/// An attribute used to mark a method as an assembly dependency loading mechanism. This is often used to ensure
	/// assemblies are loaded before performing reflection to find types.
	/// </summary>
	/// <remarks>
	/// This attribute can only be applied to public static parameterless methods contained within a public static class.
	/// Use of this attribute on anything else will either result in errors or accomplish nothing.
	/// </remarks>
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	public class LoadAssemblyDependenciesAttribute : Attribute
	{
		/// <summary>
		/// The names of the categories of assemblies that the marked method will load.
		/// </summary>
		private readonly string[] categories;

		/// <summary>
		/// Initializes a new instance of the <see cref="LoadAssemblyDependenciesAttribute"/> class.
		/// </summary>
		/// <param name="categories">The category names of the types of assemblies the marked method will load.</param>
		public LoadAssemblyDependenciesAttribute(params string[] categories)
		{
			Contracts.Requires.That(categories != null);

			this.categories = categories;
		}

		/// <summary>
		/// Gets the category names of the types of assemblies that the marked method will load.
		/// </summary>
		/// <value>
		/// The category names.
		/// </value>
		public IEnumerable<string> Categories
		{
			get
			{
				if (this.categories == null)
				{
					return Enumerable.Empty<string>();
				}
				else
				{
					return this.categories;
				}
			}
		}
	}
}
