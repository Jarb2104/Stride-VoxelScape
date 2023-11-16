namespace Voxelscape.Common.Contouring.Pact.Meshing
{
	/// <summary>
	///
	/// </summary>
	public enum QuadDiagonal
	{
		/// <summary>
		/// The diagonal between the triangles runs from the bottom left corner to the top right corner.
		/// </summary>
		/// <remarks>
		/// The two triangles in clockwise order are;
		/// (top left, top right, bottom left) and (bottom left, top right, bottom right).
		/// The two triangles in counterclockwise order are;
		/// (top left, bottom left, top right) and (bottom left, bottom right, top right).
		/// </remarks>
		Ascending,

		/// <summary>
		/// The diagonal between the triangles runs from the top left corner to the bottom right corner.
		/// </summary>
		/// <remarks>
		/// The two triangles int clockwise order are;
		/// (top left, bottom right, bottom left) and (top left, top right, bottom right).
		/// The two triangles int counterclockwise order are;
		/// (top left, bottom left, bottom right) and (top left, bottom right, top right).
		/// </remarks>
		Descending,
	}
}
