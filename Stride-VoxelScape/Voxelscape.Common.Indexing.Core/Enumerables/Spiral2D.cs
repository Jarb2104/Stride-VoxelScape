using System.Collections;
using System.Collections.Generic;
using Voxelscape.Common.Indexing.Core.Indices;

namespace Voxelscape.Common.Indexing.Core.Enumerables
{
	/// <summary>
	/// An enumerable sequence of indices that spiral in a square formation about an origin.
	/// </summary>
	public class Spiral2D : IEnumerable<Index2D>
	{
		private static readonly Options DefaultOptions = new Options();

		/// <summary>
		/// The origin of the spiral.
		/// </summary>
		private readonly Index2D origin;

		/// <summary>
		/// The number of spirals to perform, or negative to spiral infinitely.
		/// </summary>
		private readonly int spirals;

		private readonly bool isEven;

		/// <summary>
		/// Initializes a new instance of the <see cref="Spiral2D" /> class that will spiral a set number of times.
		/// </summary>
		/// <param name="options">The options for this spiral.</param>
		public Spiral2D(Options options = null)
		{
			options = options ?? DefaultOptions;

			this.origin = options.Origin;
			this.spirals = options.Spirals;
			this.isEven = options.IsEven;
		}

		/// <inheritdoc />
		public IEnumerator<Index2D> GetEnumerator()
		{
			yield return this.origin;

			// spiral control variables
			int spiralCount = 0;
			int stepCount = 1;
			int xCursor = 0;
			int yCursor = 0;

			while (true)
			{
				// moves the cursor right (deliberately uses < for condition, not <=)
				for (int iX = 1; iX < stepCount; iX++)
				{
					xCursor++;
					yield return new Index2D(this.origin.X + xCursor, this.origin.Y + yCursor);
				}

				// check if done spiraling (odd)
				if (spiralCount == this.spirals && !this.isEven)
				{
					yield break;
				}

				// move the cursor one more right (this starts the next ring)
				xCursor++;
				yield return new Index2D(this.origin.X + xCursor, this.origin.Y + yCursor);

				// move the cursor up
				for (int iY = 1; iY <= stepCount; iY++)
				{
					yCursor++;
					yield return new Index2D(this.origin.X + xCursor, this.origin.Y + yCursor);
				}

				// move the cursor left (deliberately uses < for condition, not <=)
				stepCount++;
				for (int iX = 1; iX < stepCount; iX++)
				{
					xCursor--;
					yield return new Index2D(this.origin.X + xCursor, this.origin.Y + yCursor);
				}

				// check if done spiraling (even)
				if (spiralCount == this.spirals && this.isEven)
				{
					yield break;
				}

				// move the cursor one more left
				xCursor--;
				yield return new Index2D(this.origin.X + xCursor, this.origin.Y + yCursor);

				// move the cursor down
				for (int iY = 1; iY <= stepCount; iY++)
				{
					yCursor--;
					yield return new Index2D(this.origin.X + xCursor, this.origin.Y + yCursor);
				}

				stepCount++;
				spiralCount++;
			}
		}

		/// <inheritdoc />
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

		public class Options
		{
			/// <summary>
			/// Gets or sets the origin around which the indices will spiral.
			/// </summary>
			/// <value>
			/// The origin.
			/// </value>
			public Index2D Origin { get; set; } = Index2D.Zero;

			/// <summary>
			/// Gets or sets the number of spirals to enumerate around the origin or -1 to spiral infinitely.
			/// </summary>
			/// <value>
			/// The number of times to spiral.
			/// </value>
			public int Spirals { get; set; } = -1;

			/// <summary>
			/// Gets or sets a value indicating whether the spiral will be an even number of indices wide.
			/// </summary>
			/// <value>
			///   <c>true</c> if the spiral is an even number of indices wide; otherwise, <c>false</c>.
			/// </value>
			public bool IsEven { get; set; } = false;
		}
	}
}
