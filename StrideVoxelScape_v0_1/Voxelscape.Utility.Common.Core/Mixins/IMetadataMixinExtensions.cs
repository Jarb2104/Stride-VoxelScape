using System.Runtime.CompilerServices;
using Voxelscape.Utility.Common.Core.Mixins;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides the implementation for the <see cref="IMetadataMixin{T}"/> mixin through the use of extension methods.
/// </summary>
public static class IMetadataMixinExtensions
{
	/// <summary>
	/// Gets the metadata associated with an instance.
	/// </summary>
	/// <typeparam name="TMixin">The type of the mixin.</typeparam>
	/// <typeparam name="TMetadata">The type of the metadata.</typeparam>
	/// <param name="self">The instance to retrieve the metadata.</param>
	/// <returns>The metadata.</returns>
	public static TMetadata GetMetadata<TMixin, TMetadata>(this TMixin self)
		where TMixin : class, IMetadataMixin<TMetadata>
	{
		Contracts.Requires.That(self != null);

		return State<TMetadata>.Table.GetOrCreateValue(self).Metadata;
	}

	/// <summary>
	/// Sets the metadata associated with an instance.
	/// </summary>
	/// <typeparam name="TMixin">The type of the mixin.</typeparam>
	/// <typeparam name="TMetadata">The type of the metadata.</typeparam>
	/// <param name="self">The instance to retrieve the metadata from.</param>
	/// <param name="data">The metadata.</param>
	public static void SetMetadata<TMixin, TMetadata>(this TMixin self, TMetadata data)
		where TMixin : class, IMetadataMixin<TMetadata>
	{
		Contracts.Requires.That(self != null);

		State<TMetadata>.Table.GetOrCreateValue(self).Metadata = data;
	}

	/// <summary>
	/// Private class used to hold the state for the <see cref="IMetadataMixin{T}"/>.
	/// </summary>
	/// <typeparam name="T">The type of the metadata.</typeparam>
	private sealed class State<T>
	{
		/// <summary>
		/// The state table to associate state objects with instances of the <see cref="IMetadataMixin{T}"/>.
		/// </summary>
		public static readonly ConditionalWeakTable<IMetadataMixin<T>, State<T>>
			Table = new ConditionalWeakTable<IMetadataMixin<T>, State<T>>();

		/// <summary>
		/// Gets or sets the metadata associated with the instance.
		/// </summary>
		/// <value>
		/// The metadata.
		/// </value>
		public T Metadata
		{
			get;
			set;
		}
	}
}
