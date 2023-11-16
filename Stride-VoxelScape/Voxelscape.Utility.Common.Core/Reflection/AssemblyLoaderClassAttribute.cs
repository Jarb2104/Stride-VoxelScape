using System;

namespace Voxelscape.Utility.Common.Core.Reflection
{
	/// <summary>
	/// An attribute used to mark a public static class as a provider of one or more assembly dependency loading mechanisms.
	/// These are often used to ensure assemblies are loaded before performing reflection to find types.
	/// </summary>
	/// <remarks>
	/// This attribute can only be applied to a public static class. Use of this attribute on anything else will either
	/// result in errors or accomplish nothing.
	/// </remarks>
	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
	public class AssemblyLoaderClassAttribute : Attribute
	{
	}
}
