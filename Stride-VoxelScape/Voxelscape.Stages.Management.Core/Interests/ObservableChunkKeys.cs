using System;
using System.Reactive.Linq;
using Stride.Core.Mathematics;
using Voxelscape.Common.Indexing.Core.Indices;
using Voxelscape.Utility.Common.Pact.Diagnostics;

/// <summary>
/// Provides extension methods for converting an <see cref="IObservable{Vector3}"/> to <see cref="IObservable{Index3D}"/>.
/// </summary>
/// <remarks>
/// The observable sequence returned by these methods do more than just convert the vectors to indices and
/// filter out things like multiple duplicate values in a row. It handles throttling the index updates so
/// they only occur when the vector has moved across an entire cell's worth of distance. This means that
/// when a vector crosses the border of a cell, it returns a new index as expected, but then if the vector
/// immediately backs up into the previous cell, it does not immediately return that index and cause the
/// area of interest to snap back. Instead, the previous index is only returned once the vector travels all
/// the way back across the previous cell. This throttling prevents the constant snapping back and forth an
/// area of interest would incur if a vector is straddling the border between two adjacent cells.
/// </remarks>
public static class ObservableChunkKeys
{
	/// <summary>
	/// Converts an observable sequence of vectors to a filtered observable sequence of indices.
	/// </summary>
	/// <param name="observableVector">The observable vector to convert and filter.</param>
	/// <param name="cellDimensions">Size of the cells used in the interest map and area of interests.</param>
	/// <param name="rasterizedDimensions">The dimensions of the rasterized shape of the area of interest.</param>
	/// <returns>An observable sequence of filtered indices.</returns>
	public static IObservable<Index3D> FilterToChunkKeys(
		this IObservable<Vector3> observableVector, Vector3 cellDimensions, Index3D rasterizedDimensions)
	{
		Contracts.Requires.That(observableVector != null);
		Contracts.Requires.That(cellDimensions.X > 0);
		Contracts.Requires.That(cellDimensions.Y > 0);
		Contracts.Requires.That(cellDimensions.Z > 0);
		Contracts.Requires.That(rasterizedDimensions.IsAllPositive());

		var handleAxisX = GetHandleAxis(rasterizedDimensions.X);
		var handleAxisY = GetHandleAxis(rasterizedDimensions.Y);
		var handleAxisZ = GetHandleAxis(rasterizedDimensions.Z);

		// Accumulator below holds the index that defines the location of the 'active region'
		// - For even sized area of interst -
		// Defining Index: the current top right cell of the 'active region'
		// Active Region: the 2 by 2 region of cells that make up the center of the area of interest
		// - For odd sized area of interst -
		// Defining Index: the center cell of the 'active region'
		// Active Region: the 3 by 3 region of cells that make up the center of the area of interest
		return observableVector
			.Select(vector => ConvertVectorToIndex(vector, cellDimensions))
			.DistinctUntilChanged()
			.Scan((accumulator, index) => new Index3D(
				handleAxisX(accumulator.X, index.X),
				handleAxisY(accumulator.Y, index.Y),
				handleAxisZ(accumulator.Z, index.Z)))
			.DistinctUntilChanged();
	}

	#region Private Helpers

	/// <summary>
	/// Converts the vector to the index of the cell it is inside of.
	/// </summary>
	/// <param name="vector">The vector to convert.</param>
	/// <param name="cellDimensions">Size of the cells used in the interest map and area of interests.</param>
	/// <returns>The index of the cell that the vector is inside of.</returns>
	private static Index3D ConvertVectorToIndex(Vector3 vector, Vector3 cellDimensions) =>
		new Index3D(
			(int)Math.Floor(vector.X / cellDimensions.X),
			(int)Math.Floor(vector.Y / cellDimensions.Y),
			(int)Math.Floor(vector.Z / cellDimensions.Z));

	/// <summary>
	/// Gets the function for handling updating the index of the active region per axis.
	/// </summary>
	/// <param name="rasterizedLength">Length of the rasterized shape.</param>
	/// <returns>
	/// The function for handling the active region updates.
	/// </returns>
	private static Func<int, int, int> GetHandleAxis(int rasterizedLength)
	{
		if (rasterizedLength.IsEven())
		{
			return HandleAxisEven;
		}
		else
		{
			return HandleAxisOdd;
		}
	}

	/// <summary>
	/// Determines what the index value of the 'active region' is based off of the current active value and
	/// a possible new value. This method operates independently on each axis of the index.
	/// </summary>
	/// <param name="activeValue">The current value of the particular axis of the top right index of the 'active region'.</param>
	/// <param name="newValue">The possible new value for the 'active region' index.</param>
	/// <returns>
	/// The value determined to be the top right index of the 'active region' for the particular axis.
	/// This may be a new value or it could stay the same as what was passed into this method.
	/// </returns>
	private static int HandleAxisEven(int activeValue, int newValue)
	{
		if (newValue == activeValue - 1 || newValue == activeValue)
		{
			// if the index is in the 'active region' then don't change the active region index
			return activeValue;
		}
		else
		{
			if (newValue == activeValue - 2)
			{
				// if the index is adjacent to the left hand side
				// then move the active region index over 1 index to the left
				return activeValue - 1;
			}
			else
			{
				if (newValue == activeValue + 1)
				{
					// if the index is adjacent to the right hand side
					// then move the active region index over 1 index to the right
					return activeValue + 1;
				}
				else
				{
					// If the index is not in the active region nor adjacent to either of its sides
					// then completely resynchronize the active region index with the index (don't just
					// shift it over by 1 index). This occurs if the incoming index 'teleported' instead
					// of continuous smooth movement.
					return newValue;
				}
			}
		}
	}

	/// <summary>
	/// Determines what the index value of the 'active region' is based off of the current active value and
	/// a possible new value. This method operates independently on each axis of the index.
	/// </summary>
	/// <param name="activeValue">The current value of the particular axis of the center index of the 'active region'.</param>
	/// <param name="newValue">The possible new value for the 'active region' index.</param>
	/// <returns>
	/// The value determined to be the center index of the 'active region' for the particular axis.
	/// This may be a new value or it could stay the same as what was passed into this method.
	/// </returns>
	private static int HandleAxisOdd(int activeValue, int newValue)
	{
		if (newValue == activeValue + 1 || newValue == activeValue - 1 || newValue == activeValue)
		{
			// if the index is in the 'active region' then don't change the active region index
			return activeValue;
		}
		else
		{
			if (newValue == activeValue - 2)
			{
				// if the index is adjacent to the left hand side
				// then move the active region index over 1 index to the left
				return activeValue - 1;
			}
			else
			{
				if (newValue == activeValue + 2)
				{
					// if the index is adjacent to the right hand side
					// then move the active region index over 1 index to the right
					return activeValue + 1;
				}
				else
				{
					// If the index is not in the active region nor adjacent to either of its sides
					// then completely resynchronize the active region index with the index (don't just
					// shift it over by 1 index). This occurs if the incoming index 'teleported' instead
					// of continuous smooth movement.
					return newValue;
				}
			}
		}
	}

	#endregion
}
