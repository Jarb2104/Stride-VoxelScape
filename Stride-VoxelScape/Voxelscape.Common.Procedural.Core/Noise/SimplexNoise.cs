using System;
using System.Diagnostics.CodeAnalysis;
using Voxelscape.Common.Procedural.Pact.Noise;
using Voxelscape.Utility.Common.Pact.Diagnostics;

namespace Voxelscape.Common.Procedural.Core.Noise
{
	/// <summary>
	/// Simplex noise generates pseudo random density points which are continuous with their neighbors.
	/// </summary>
	/// <remarks>
	/// Implementation copied and modified from
	/// <see href="http://webstaff.itn.liu.se/~stegu/simplexnoise/simplexnoise.pdf">this link</see>.
	/// </remarks>
	[SuppressMessage(
		"StyleCop.CSharp.SpacingRules",
		"SA1025:CodeMustNotContainMultipleWhitespaceInARow",
		Justification = "Extra spacing used to line up array constants.")]
	[SuppressMessage(
		"StyleCop.CSharp.SpacingRules",
		"SA1137:ElementsShouldHaveTheSameIndentation",
		Justification = "Extra spacing used to line up array constants.")]
	[SuppressMessage(
		"StyleCop.CSharp.NamingRules",
		"SA1312:Variable names must begin with lower-case letter",
		Justification = "Implementation taken from a third party. No idea what the variable names mean.")]
	public class SimplexNoise : INoise4D
	{
		#region Private static resources

		/// <summary>
		/// The grad3 array.
		/// </summary>
		private static readonly int[][] Grad3 = new int[][]
		{
			new int[] { 1, 1, 0 }, new int[] { -1, 1, 0 }, new int[] { 1, -1, 0 }, new int[] { -1, -1, 0 },
			new int[] { 1, 0, 1 }, new int[] { -1, 0, 1 }, new int[] { 1, 0, -1 }, new int[] { -1, 0, -1 },
			new int[] { 0, 1, 1 }, new int[] { 0, -1, 1 }, new int[] { 0, 1, -1 }, new int[] { 0, -1, -1 },
		};

		/// <summary>
		/// The grad4 array.
		/// </summary>
		private static readonly int[][] Grad4 = new int[][]
		{
			new int[] {  0,  1, 1, 1 }, new int[] {  0,  1,  1, -1 }, new int[] {  0,  1, -1, 1 }, new int[] {  0,  1, -1, -1 },
			new int[] {  0, -1, 1, 1 }, new int[] {  0, -1,  1, -1 }, new int[] {  0, -1, -1, 1 }, new int[] {  0, -1, -1, -1 },
			new int[] {  1,  0, 1, 1 }, new int[] {  1,  0,  1, -1 }, new int[] {  1,  0, -1, 1 }, new int[] {  1,  0, -1, -1 },
			new int[] { -1,  0, 1, 1 }, new int[] { -1,  0,  1, -1 }, new int[] { -1,  0, -1, 1 }, new int[] { -1,  0, -1, -1 },
			new int[] {  1,  1, 0, 1 }, new int[] {  1,  1,  0, -1 }, new int[] {  1, -1,  0, 1 }, new int[] {  1, -1,  0, -1 },
			new int[] { -1,  1, 0, 1 }, new int[] { -1,  1,  0, -1 }, new int[] { -1, -1,  0, 1 }, new int[] { -1, -1,  0, -1 },
			new int[] {  1,  1, 1, 0 }, new int[] {  1,  1, -1,  0 }, new int[] {  1, -1,  1, 0 }, new int[] {  1, -1, -1,  0 },
			new int[] { -1,  1, 1, 0 }, new int[] { -1,  1, -1,  0 }, new int[] { -1, -1,  1, 0 }, new int[] { -1, -1, -1,  0 },
		};

		/// <summary>
		/// A lookup table to traverse the simplex around a given point in 4D.
		/// Details can be found where this table is used, in the 4D noise method.
		/// </summary>
		private static readonly int[][] Simplex = new int[][]
		{
			new int[] { 0, 1, 2, 3 }, new int[] { 0, 1, 3, 2 }, new int[] { 0, 0, 0, 0 }, new int[] { 0, 2, 3, 1 },
			new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0 }, new int[] { 1, 2, 3, 0 },
			new int[] { 0, 2, 1, 3 }, new int[] { 0, 0, 0, 0 }, new int[] { 0, 3, 1, 2 }, new int[] { 0, 3, 2, 1 },
			new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0 }, new int[] { 1, 3, 2, 0 },
			new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0 },
			new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0 },
			new int[] { 1, 2, 0, 3 }, new int[] { 0, 0, 0, 0 }, new int[] { 1, 3, 0, 2 }, new int[] { 0, 0, 0, 0 },
			new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0 }, new int[] { 2, 3, 0, 1 }, new int[] { 2, 3, 1, 0 },
			new int[] { 1, 0, 2, 3 }, new int[] { 1, 0, 3, 2 }, new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0 },
			new int[] { 0, 0, 0, 0 }, new int[] { 2, 0, 3, 1 }, new int[] { 0, 0, 0, 0 }, new int[] { 2, 1, 3, 0 },
			new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0 },
			new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0 },
			new int[] { 2, 0, 1, 3 }, new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0 },
			new int[] { 3, 0, 1, 2 }, new int[] { 3, 0, 2, 1 }, new int[] { 0, 0, 0, 0 }, new int[] { 3, 1, 2, 0 },
			new int[] { 2, 1, 0, 3 }, new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0 }, new int[] { 0, 0, 0, 0 },
			new int[] { 3, 1, 0, 2 }, new int[] { 0, 0, 0, 0 }, new int[] { 3, 2, 0, 1 }, new int[] { 3, 2, 1, 0 },
		};

		/// <summary>
		/// The lazy singleton magic numbers permutation table.
		/// </summary>
		private static readonly Lazy<int[]> MagicNumbersPermutationTable =
			new Lazy<int[]>(() => CreateNonwrappingPermutationTable(CreateMagicNumbersPermutationTable()));

		#endregion

		/// <summary>
		/// The permutation table used to generate noise values from.
		/// </summary>
		private readonly int[] perm;

		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="SimplexNoise"/> class.
		/// </summary>
		public SimplexNoise()
		{
			this.perm = MagicNumbersPermutationTable.Value;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SimplexNoise"/> class.
		/// </summary>
		/// <param name="seed">The seed to generate the noise from.</param>
		public SimplexNoise(int seed)
			: this(new Random(seed))
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="SimplexNoise"/> class.
		/// </summary>
		/// <param name="random">The <see cref="Random"/> used to generate the permutation table from.</param>
		public SimplexNoise(Random random)
		{
			Contracts.Requires.That(random != null);

			this.perm = CreateNonwrappingPermutationTable(CreateRandomPermutationTable(random));
		}

		#endregion

		#region Implementing INoise Interfaces

		/// <inheritdoc />
		public double Noise(double x)
		{
			return this.Noise(x, 0);
		}

		/// <inheritdoc />
		public double Noise(double x, double y)
		{
			double n0, n1, n2; // Noise contributions from the three corners

			// Skew the input space to determine which simplex cell we're in
			double F2 = 0.5 * (System.Math.Sqrt(3.0f) - 1.0);

			double s = (x + y) * F2; // Hairy factor for 2D

			int i = FastFloor(x + s);

			int j = FastFloor(y + s);

			double G2 = (3.0 - System.Math.Sqrt(3.0f)) / 6.0;

			double t = (i + j) * G2;

			double X0 = i - t; // Unskew the cell origin back to (x,y) space

			double Y0 = j - t;

			double x0 = x - X0; // The x,y distances from the cell origin

			double y0 = y - Y0;

			// For the 2D case, the simplex shape is an equilateral triangle.
			// Determine which simplex we are in.
			int i1, j1; // Offsets for second (middle) corner of simplex in (i,j) coords

			if (x0 > y0)
			{
				// lower triangle, XY order: (0,0)->(1,0)->(1,1)
				i1 = 1;
				j1 = 0;
			}
			else
			{
				// upper triangle, YX order: (0,0)->(0,1)->(1,1)
				i1 = 0;
				j1 = 1;
			}

			// A step of (1,0) in (i,j) means a step of (1-c,-c) in (x,y), and
			// a step of (0,1) in (i,j) means a step of (-c,1-c) in (x,y), where

			// c = (3-Sqrt(3))/6
			double x1 = x0 - i1 + G2; // Offsets for middle corner in (x,y) unskewed coords

			double y1 = y0 - j1 + G2;

			double x2 = x0 - 1.0 + (2.0 * G2); // Offsets for last corner in (x,y) unskewed coords

			double y2 = y0 - 1.0 + (2.0 * G2);

			// Work out the hashed gradient indices of the three simplex corners
			int ii = i & 255;

			int jj = j & 255;

			int gi0 = this.perm[ii + this.perm[jj]] % 12;

			int gi1 = this.perm[ii + i1 + this.perm[jj + j1]] % 12;

			int gi2 = this.perm[ii + 1 + this.perm[jj + 1]] % 12;

			// Calculate the contribution from the three corners
			double t0 = 0.5 - (x0 * x0) - (y0 * y0);

			if (t0 < 0)
			{
				n0 = 0.0;
			}
			else
			{
				t0 *= t0;
				n0 = t0 * t0 * Dot(Grad3[gi0], x0, y0);  // (x,y) of grad3 used for 2D gradient
			}

			double t1 = 0.5 - (x1 * x1) - (y1 * y1);

			if (t1 < 0)
			{
				n1 = 0.0;
			}
			else
			{
				t1 *= t1;
				n1 = t1 * t1 * Dot(Grad3[gi1], x1, y1);
			}

			double t2 = 0.5 - (x2 * x2) - (y2 * y2);

			if (t2 < 0)
			{
				n2 = 0.0;
			}
			else
			{
				t2 *= t2;
				n2 = t2 * t2 * Dot(Grad3[gi2], x2, y2);
			}

			// Add contributions from each corner to get the final noise value.
			// The result is scaled to return values in the interval [-1,1].
			return 70.0 * (n0 + n1 + n2);
		}

		/// <inheritdoc />
		public double Noise(double x, double y, double z)
		{
			double n0, n1, n2, n3; // Noise contributions from the four corners

			// Skew the input space to determine which simplex cell we're in
			double F3 = 1.0 / 3.0;

			double s = (x + y + z) * F3; // Very nice and simple skew factor for 3D

			int i = FastFloor(x + s);

			int j = FastFloor(y + s);

			int k = FastFloor(z + s);

			double G3 = 1.0 / 6.0; // Very nice and simple unskew factor, too

			double t = (i + j + k) * G3;

			double X0 = i - t; // Unskew the cell origin back to (x,y,z) space

			double Y0 = j - t;

			double Z0 = k - t;

			double x0 = x - X0; // The x,y,z distances from the cell origin

			double y0 = y - Y0;

			double z0 = z - Z0;

			// For the 3D case, the simplex shape is a slightly irregular tetrahedron.
			// Determine which simplex we are in.
			int i1, j1, k1; // Offsets for second corner of simplex in (i,j,k) coords

			int i2, j2, k2; // Offsets for third corner of simplex in (i,j,k) coords

			if (x0 >= y0)
			{
				if (y0 >= z0)
				{
					// X Y Z order
					i1 = 1;
					j1 = 0;
					k1 = 0;
					i2 = 1;
					j2 = 1;
					k2 = 0;
				}
				else if (x0 >= z0)
				{
					// X Z Y order
					i1 = 1;
					j1 = 0;
					k1 = 0;
					i2 = 1;
					j2 = 0;
					k2 = 1;
				}
				else
				{
					// Z X Y order
					i1 = 0;
					j1 = 0;
					k1 = 1;
					i2 = 1;
					j2 = 0;
					k2 = 1;
				}
			}
			else
			{
				// x0 < y0 (from the outer conditional (not the below one))
				if (y0 < z0)
				{
					// Z Y X order
					i1 = 0;
					j1 = 0;
					k1 = 1;
					i2 = 0;
					j2 = 1;
					k2 = 1;
				}
				else if (x0 < z0)
				{
					// Y Z X order
					i1 = 0;
					j1 = 1;
					k1 = 0;
					i2 = 0;
					j2 = 1;
					k2 = 1;
				}
				else
				{
					// Y X Z order
					i1 = 0;
					j1 = 1;
					k1 = 0;
					i2 = 1;
					j2 = 1;
					k2 = 0;
				}
			}

			// A step of (1,0,0) in (i,j,k) means a step of (1-c,-c,-c) in (x,y,z),

			// a step of (0,1,0) in (i,j,k) means a step of (-c,1-c,-c) in (x,y,z), and
			// a step of (0,0,1) in (i,j,k) means a step of (-c,-c,1-c) in (x,y,z), where

			// c = 1/6.
			double x1 = x0 - i1 + G3; // Offsets for second corner in (x,y,z) coords

			double y1 = y0 - j1 + G3;

			double z1 = z0 - k1 + G3;

			double x2 = x0 - i2 + (2.0 * G3); // Offsets for third corner in (x,y,z) coords

			double y2 = y0 - j2 + (2.0 * G3);

			double z2 = z0 - k2 + (2.0 * G3);

			double x3 = x0 - 1.0 + (3.0 * G3); // Offsets for last corner in (x,y,z) coords

			double y3 = y0 - 1.0 + (3.0 * G3);

			double z3 = z0 - 1.0 + (3.0 * G3);

			// Work out the hashed gradient indices of the four simplex corners
			int ii = i & 255;

			int jj = j & 255;

			int kk = k & 255;

			int gi0 = this.perm[ii + this.perm[jj + this.perm[kk]]] % 12;

			int gi1 = this.perm[ii + i1 + this.perm[jj + j1 + this.perm[kk + k1]]] % 12;

			int gi2 = this.perm[ii + i2 + this.perm[jj + j2 + this.perm[kk + k2]]] % 12;

			int gi3 = this.perm[ii + 1 + this.perm[jj + 1 + this.perm[kk + 1]]] % 12;

			// Calculate the contribution from the four corners
			double t0 = 0.6 - (x0 * x0) - (y0 * y0) - (z0 * z0);

			if (t0 < 0)
			{
				n0 = 0.0;
			}
			else
			{
				t0 *= t0;
				n0 = t0 * t0 * Dot(Grad3[gi0], x0, y0, z0);
			}

			double t1 = 0.6 - (x1 * x1) - (y1 * y1) - (z1 * z1);

			if (t1 < 0)
			{
				n1 = 0.0;
			}
			else
			{
				t1 *= t1;
				n1 = t1 * t1 * Dot(Grad3[gi1], x1, y1, z1);
			}

			double t2 = 0.6 - (x2 * x2) - (y2 * y2) - (z2 * z2);

			if (t2 < 0)
			{
				n2 = 0.0;
			}
			else
			{
				t2 *= t2;
				n2 = t2 * t2 * Dot(Grad3[gi2], x2, y2, z2);
			}

			double t3 = 0.6 - (x3 * x3) - (y3 * y3) - (z3 * z3);

			if (t3 < 0)
			{
				n3 = 0.0;
			}
			else
			{
				t3 *= t3;
				n3 = t3 * t3 * Dot(Grad3[gi3], x3, y3, z3);
			}

			// Add contributions from each corner to get the final noise value.
			// The result is scaled to stay just inside [-1,1]
			return 32.0 * (n0 + n1 + n2 + n3);
		}

		/// <inheritdoc />
		public double Noise(double x, double y, double z, double w)
		{
			// The skewing and unskewing factors are hairy again for the 4D case
			double F4 = (System.Math.Sqrt(5.0f) - 1.0) / 4.0;

			double G4 = (5.0 - System.Math.Sqrt(5.0f)) / 20.0;

			double n0, n1, n2, n3, n4; // Noise contributions from the five corners

			// Skew the (x,y,z,w) space to determine which cell of 24 simplices we're in
			double s = (x + y + z + w) * F4; // Factor for 4D skewing

			int i = FastFloor(x + s);

			int j = FastFloor(y + s);

			int k = FastFloor(z + s);

			int l = FastFloor(w + s);

			double t = (i + j + k + l) * G4; // Factor for 4D unskewing

			double X0 = i - t; // Unskew the cell origin back to (x,y,z,w) space

			double Y0 = j - t;

			double Z0 = k - t;

			double W0 = l - t;

			double x0 = x - X0;  // The x,y,z,w distances from the cell origin

			double y0 = y - Y0;

			double z0 = z - Z0;

			double w0 = w - W0;

			// For the 4D case, the simplex is a 4D shape I won't even try to describe.
			// To find out which of the 24 possible simplices we're in, we need to

			// determine the magnitude ordering of x0, y0, z0 and w0.
			// The method below is a good way of finding the ordering of x,y,z,w and

			// then find the correct traversal order for the simplex we’re in.
			// First, six pair-wise comparisons are performed between each possible pair

			// of the four coordinates, and the results are used to add up binary bits
			// for an integer index.
			int c1 = (x0 > y0) ? 32 : 0;

			int c2 = (x0 > z0) ? 16 : 0;

			int c3 = (y0 > z0) ? 8 : 0;

			int c4 = (x0 > w0) ? 4 : 0;

			int c5 = (y0 > w0) ? 2 : 0;

			int c6 = (z0 > w0) ? 1 : 0;

			int c = c1 + c2 + c3 + c4 + c5 + c6;

			int i1, j1, k1, l1; // The integer offsets for the second simplex corner

			int i2, j2, k2, l2; // The integer offsets for the third simplex corner

			int i3, j3, k3, l3; // The integer offsets for the fourth simplex corner

			// simplex[c] is a 4-vector with the numbers 0, 1, 2 and 3 in some order.
			// Many values of c will never occur, since e.g. x>y>z>w makes x<z, y<w and x<w

			// impossible. Only the 24 indices which have non-zero entries make any sense.
			// We use a thresholding to set the coordinates in turn from the largest magnitude.

			// The number 3 in the "simplex" array is at the position of the largest coordinate.
			i1 = Simplex[c][0] >= 3 ? 1 : 0;

			j1 = Simplex[c][1] >= 3 ? 1 : 0;

			k1 = Simplex[c][2] >= 3 ? 1 : 0;

			l1 = Simplex[c][3] >= 3 ? 1 : 0;

			// The number 2 in the "simplex" array is at the second largest coordinate.
			i2 = Simplex[c][0] >= 2 ? 1 : 0;

			j2 = Simplex[c][1] >= 2 ? 1 : 0;

			k2 = Simplex[c][2] >= 2 ? 1 : 0;

			l2 = Simplex[c][3] >= 2 ? 1 : 0;

			// The number 1 in the "simplex" array is at the second smallest coordinate.
			i3 = Simplex[c][0] >= 1 ? 1 : 0;

			j3 = Simplex[c][1] >= 1 ? 1 : 0;

			k3 = Simplex[c][2] >= 1 ? 1 : 0;

			l3 = Simplex[c][3] >= 1 ? 1 : 0;

			// The fifth corner has all coordinate offsets = 1, so no need to look that up.
			double x1 = x0 - i1 + G4; // Offsets for second corner in (x,y,z,w) coords

			double y1 = y0 - j1 + G4;

			double z1 = z0 - k1 + G4;

			double w1 = w0 - l1 + G4;

			double x2 = x0 - i2 + (2.0 * G4); // Offsets for third corner in (x,y,z,w) coords

			double y2 = y0 - j2 + (2.0 * G4);

			double z2 = z0 - k2 + (2.0 * G4);

			double w2 = w0 - l2 + (2.0 * G4);

			double x3 = x0 - i3 + (3.0 * G4); // Offsets for fourth corner in (x,y,z,w) coords

			double y3 = y0 - j3 + (3.0 * G4);

			double z3 = z0 - k3 + (3.0 * G4);

			double w3 = w0 - l3 + (3.0 * G4);

			double x4 = x0 - 1.0 + (4.0 * G4); // Offsets for last corner in (x,y,z,w) coords

			double y4 = y0 - 1.0 + (4.0 * G4);

			double z4 = z0 - 1.0 + (4.0 * G4);

			double w4 = w0 - 1.0 + (4.0 * G4);

			// Work out the hashed gradient indices of the five simplex corners
			int ii = i & 255;

			int jj = j & 255;

			int kk = k & 255;

			int ll = l & 255;

			int gi0 = this.perm[ii + this.perm[jj + this.perm[kk + this.perm[ll]]]] % 32;

			int gi1 = this.perm[ii + i1 + this.perm[jj + j1 + this.perm[kk + k1 + this.perm[ll + l1]]]] % 32;

			int gi2 = this.perm[ii + i2 + this.perm[jj + j2 + this.perm[kk + k2 + this.perm[ll + l2]]]] % 32;

			int gi3 = this.perm[ii + i3 + this.perm[jj + j3 + this.perm[kk + k3 + this.perm[ll + l3]]]] % 32;

			int gi4 = this.perm[ii + 1 + this.perm[jj + 1 + this.perm[kk + 1 + this.perm[ll + 1]]]] % 32;

			// Calculate the contribution from the five corners
			double t0 = 0.6 - (x0 * x0) - (y0 * y0) - (z0 * z0) - (w0 * w0);

			if (t0 < 0)
			{
				n0 = 0.0;
			}
			else
			{
				t0 *= t0;
				n0 = t0 * t0 * Dot(Grad4[gi0], x0, y0, z0, w0);
			}

			double t1 = 0.6 - (x1 * x1) - (y1 * y1) - (z1 * z1) - (w1 * w1);

			if (t1 < 0)
			{
				n1 = 0.0;
			}
			else
			{
				t1 *= t1;
				n1 = t1 * t1 * Dot(Grad4[gi1], x1, y1, z1, w1);
			}

			double t2 = 0.6 - (x2 * x2) - (y2 * y2) - (z2 * z2) - (w2 * w2);

			if (t2 < 0)
			{
				n2 = 0.0;
			}
			else
			{
				t2 *= t2;
				n2 = t2 * t2 * Dot(Grad4[gi2], x2, y2, z2, w2);
			}

			double t3 = 0.6 - (x3 * x3) - (y3 * y3) - (z3 * z3) - (w3 * w3);

			if (t3 < 0)
			{
				n3 = 0.0;
			}
			else
			{
				t3 *= t3;
				n3 = t3 * t3 * Dot(Grad4[gi3], x3, y3, z3, w3);
			}

			double t4 = 0.6 - (x4 * x4) - (y4 * y4) - (z4 * z4) - (w4 * w4);

			if (t4 < 0)
			{
				n4 = 0.0;
			}
			else
			{
				t4 *= t4;
				n4 = t4 * t4 * Dot(Grad4[gi4], x4, y4, z4, w4);
			}

			// Sum up and scale the result to cover the range [-1,1]
			return 27.0 * (n0 + n1 + n2 + n3 + n4);
		}

		#endregion

		#region Private static permutation table helpers

		/// <summary>
		/// Creates the non-wrapping permutation table.
		/// </summary>
		/// <param name="permutationTable">The original permutation table.</param>
		/// <returns>The non-wrapping version of the original permutation table.</returns>
		private static int[] CreateNonwrappingPermutationTable(int[] permutationTable)
		{
			Contracts.Requires.That(permutationTable != null);

			int[] result = new int[512];

			for (int i = 0; i < 256; i++)
			{
				result[i] = permutationTable[i];
				result[i + 256] = permutationTable[i];
			}

			return result;
		}

		/// <summary>
		/// Creates the magic numbers permutation table for a non-seeded Simplex noise object.
		/// </summary>
		/// <returns>The permutation table.</returns>
		private static int[] CreateMagicNumbersPermutationTable() => new int[]
		{
			151, 160, 137,  91,  90,  15, 131,  13, 201,  95,  96,  53, 194, 233,   7, 225,
			140,  36, 103,  30,  69, 142,   8,  99,  37, 240,  21,  10,  23, 190,   6, 148,
			247, 120, 234,  75,   0,  26, 197,  62,  94, 252, 219, 203, 117,  35,  11,  32,
			 57, 177,  33,  88, 237, 149,  56,  87, 174,  20, 125, 136, 171, 168,  68, 175,
			 74, 165,  71, 134, 139,  48,  27, 166,  77, 146, 158, 231,  83, 111, 229, 122,
			 60, 211, 133, 230, 220, 105,  92,  41,  55,  46, 245,  40, 244, 102, 143,  54,
			 65,  25,  63, 161,   1, 216,  80,  73, 209,  76, 132, 187, 208,  89,  18, 169,
			200, 196, 135, 130, 116, 188, 159,  86, 164, 100, 109, 198, 173, 186,   3,  64,
			 52, 217, 226, 250, 124, 123,   5, 202,  38, 147, 118, 126, 255,  82,  85, 212,
			207, 206,  59, 227,  47,  16,  58,  17, 182, 189,  28,  42, 223, 183, 170, 213,
			119, 248, 152,   2,  44, 154, 163,  70, 221, 153, 101, 155, 167,  43, 172,   9,
			129,  22,  39, 253,  19,  98, 108, 110,  79, 113, 224, 232, 178, 185, 112, 104,
			218, 246,  97, 228, 251,  34, 242, 193, 238, 210, 144,  12, 191, 179, 162, 241,
			 81,  51, 145, 235, 249,  14, 239, 107,  49, 192, 214,  31, 181, 199, 106, 157,
			184,  84, 204, 176, 115, 121,  50,  45, 127,   4, 150, 254, 138, 236, 205,  93,
			222, 114,  67,  29,  24,  72, 243, 141, 128, 195,  78,  66, 215,  61, 156, 180,
		};

		/// <summary>
		/// Generates a new random permutation of 256 values with no repeats.
		/// </summary>
		/// <param name="random">The <see cref="Random"/> used to generate the permutation table from.</param>
		/// <returns>An array containing the random permutation.</returns>
		private static int[] CreateRandomPermutationTable(Random random)
		{
			Contracts.Requires.That(random != null);

			int[] result = new int[256];
			for (int count = 0; count < 256; count++)
			{
				result[count] = count;
			}

			result.Shuffle(random);
			return result;
		}

		#endregion

		#region Private static helpers

		/// <summary>
		/// A faster version of the (int)Math.floor(x) method.
		/// </summary>
		/// <param name="x">The value to floor.</param>
		/// <returns>The value with the decimal portion truncated.</returns>
		private static int FastFloor(double x)
		{
			return x > 0 ? (int)x : (int)x - 1;
		}

		/// <summary>
		/// Two dimensional dot product function.
		/// </summary>
		/// <param name="g">The g vector (two-dimensional).</param>
		/// <param name="x">The x.</param>
		/// <param name="y">The y.</param>
		/// <returns>The result of the dot product.</returns>
		private static double Dot(int[] g, double x, double y)
		{
			return (g[0] * x) + (g[1] * y);
		}

		/// <summary>
		/// Three dimensional dot product function.
		/// </summary>
		/// <param name="g">The g vector (three-dimensional).</param>
		/// <param name="x">The x.</param>
		/// <param name="y">The y.</param>
		/// <param name="z">The z.</param>
		/// <returns>The result of the dot product.</returns>
		private static double Dot(int[] g, double x, double y, double z)
		{
			return (g[0] * x) + (g[1] * y) + (g[2] * z);
		}

		/// <summary>
		/// Four dimensional dot product function.
		/// </summary>
		/// <param name="g">The g vector (four-dimensional).</param>
		/// <param name="x">The x.</param>
		/// <param name="y">The y.</param>
		/// <param name="z">The z.</param>
		/// <param name="w">The w.</param>
		/// <returns>The result of the dot product.</returns>
		private static double Dot(int[] g, double x, double y, double z, double w)
		{
			return (g[0] * x) + (g[1] * y) + (g[2] * z) + (g[3] * w);
		}

		#endregion
	}
}
