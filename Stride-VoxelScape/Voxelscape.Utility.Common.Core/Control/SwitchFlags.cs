namespace Voxelscape.Utility.Common.Core.Control
{
	/// <summary>
	/// Provides factory methods for creating <see cref="SwitchFlag"/> values from evaluating series of conditions for use
	/// in switch statements instead of using deeply nested if statements.
	/// </summary>
	public static class SwitchFlags
	{
		/// <summary>
		/// Produces a <see cref="SwitchFlag"/> value from evaluating a series of conditions for use in a switch statement
		/// instead of using deeply nested if statements.
		/// </summary>
		/// <param name="firstCondition">The first condition to evaluate for a switch statement.</param>
		/// <param name="secondCondition">The second condition to evaluate for a switch statement.</param>
		/// <returns>The <see cref="SwitchFlag"/> value representing the evaluation of all the conditions.</returns>
		public static SwitchFlag From(
			bool firstCondition,
			bool secondCondition)
		{
			SwitchFlag result = SwitchFlag.AllFalse;

			if (firstCondition)
			{
				result |= SwitchFlag.FirstTrue;
			}

			if (secondCondition)
			{
				result |= SwitchFlag.SecondTrue;
			}

			return result;
		}

		/// <summary>
		/// Produces a <see cref="SwitchFlag"/> value from evaluating a series of conditions for use in a switch statement
		/// instead of using deeply nested if statements.
		/// </summary>
		/// <param name="firstCondition">The first condition to evaluate for a switch statement.</param>
		/// <param name="secondCondition">The second condition to evaluate for a switch statement.</param>
		/// <param name="thirdCondition">The third condition to evaluate for a switch statement.</param>
		/// <returns>The <see cref="SwitchFlag"/> value representing the evaluation of all the conditions.</returns>
		public static SwitchFlag From(
			bool firstCondition,
			bool secondCondition,
			bool thirdCondition)
		{
			SwitchFlag result = SwitchFlag.AllFalse;

			if (firstCondition)
			{
				result |= SwitchFlag.FirstTrue;
			}

			if (secondCondition)
			{
				result |= SwitchFlag.SecondTrue;
			}

			if (thirdCondition)
			{
				result |= SwitchFlag.ThirdTrue;
			}

			return result;
		}

		/// <summary>
		/// Produces a <see cref="SwitchFlag"/> value from evaluating a series of conditions for use in a switch statement
		/// instead of using deeply nested if statements.
		/// </summary>
		/// <param name="firstCondition">The first condition to evaluate for a switch statement.</param>
		/// <param name="secondCondition">The second condition to evaluate for a switch statement.</param>
		/// <param name="thirdCondition">The third condition to evaluate for a switch statement.</param>
		/// <param name="forthCondition">The forth condition to evaluate for a switch statement.</param>
		/// <returns>The <see cref="SwitchFlag"/> value representing the evaluation of all the conditions.</returns>
		public static SwitchFlag From(
			bool firstCondition,
			bool secondCondition,
			bool thirdCondition,
			bool forthCondition)
		{
			SwitchFlag result = SwitchFlag.AllFalse;

			if (firstCondition)
			{
				result |= SwitchFlag.FirstTrue;
			}

			if (secondCondition)
			{
				result |= SwitchFlag.SecondTrue;
			}

			if (thirdCondition)
			{
				result |= SwitchFlag.ThirdTrue;
			}

			if (forthCondition)
			{
				result |= SwitchFlag.ForthTrue;
			}

			return result;
		}

		/// <summary>
		/// Produces a <see cref="SwitchFlag"/> value from evaluating a series of conditions for use in a switch statement
		/// instead of using deeply nested if statements.
		/// </summary>
		/// <param name="firstCondition">The first condition to evaluate for a switch statement.</param>
		/// <param name="secondCondition">The second condition to evaluate for a switch statement.</param>
		/// <param name="thirdCondition">The third condition to evaluate for a switch statement.</param>
		/// <param name="forthCondition">The forth condition to evaluate for a switch statement.</param>
		/// <param name="fifthCondition">The fifth condition to evaluate for a switch statement.</param>
		/// <returns>The <see cref="SwitchFlag"/> value representing the evaluation of all the conditions.</returns>
		public static SwitchFlag From(
			bool firstCondition,
			bool secondCondition,
			bool thirdCondition,
			bool forthCondition,
			bool fifthCondition)
		{
			SwitchFlag result = SwitchFlag.AllFalse;

			if (firstCondition)
			{
				result |= SwitchFlag.FirstTrue;
			}

			if (secondCondition)
			{
				result |= SwitchFlag.SecondTrue;
			}

			if (thirdCondition)
			{
				result |= SwitchFlag.ThirdTrue;
			}

			if (forthCondition)
			{
				result |= SwitchFlag.ForthTrue;
			}

			if (fifthCondition)
			{
				result |= SwitchFlag.FifthTrue;
			}

			return result;
		}

		/// <summary>
		/// Produces a <see cref="SwitchFlag"/> value from evaluating a series of conditions for use in a switch statement
		/// instead of using deeply nested if statements.
		/// </summary>
		/// <param name="firstCondition">The first condition to evaluate for a switch statement.</param>
		/// <param name="secondCondition">The second condition to evaluate for a switch statement.</param>
		/// <param name="thirdCondition">The third condition to evaluate for a switch statement.</param>
		/// <param name="forthCondition">The forth condition to evaluate for a switch statement.</param>
		/// <param name="fifthCondition">The fifth condition to evaluate for a switch statement.</param>
		/// <param name="sixthCondition">The sixth condition to evaluate for a switch statement.</param>
		/// <returns>The <see cref="SwitchFlag"/> value representing the evaluation of all the conditions.</returns>
		public static SwitchFlag From(
			bool firstCondition,
			bool secondCondition,
			bool thirdCondition,
			bool forthCondition,
			bool fifthCondition,
			bool sixthCondition)
		{
			SwitchFlag result = SwitchFlag.AllFalse;

			if (firstCondition)
			{
				result |= SwitchFlag.FirstTrue;
			}

			if (secondCondition)
			{
				result |= SwitchFlag.SecondTrue;
			}

			if (thirdCondition)
			{
				result |= SwitchFlag.ThirdTrue;
			}

			if (forthCondition)
			{
				result |= SwitchFlag.ForthTrue;
			}

			if (fifthCondition)
			{
				result |= SwitchFlag.FifthTrue;
			}

			if (sixthCondition)
			{
				result |= SwitchFlag.SixthTrue;
			}

			return result;
		}

		/// <summary>
		/// Produces a <see cref="SwitchFlag"/> value from evaluating a series of conditions for use in a switch statement
		/// instead of using deeply nested if statements.
		/// </summary>
		/// <param name="firstCondition">The first condition to evaluate for a switch statement.</param>
		/// <param name="secondCondition">The second condition to evaluate for a switch statement.</param>
		/// <param name="thirdCondition">The third condition to evaluate for a switch statement.</param>
		/// <param name="forthCondition">The forth condition to evaluate for a switch statement.</param>
		/// <param name="fifthCondition">The fifth condition to evaluate for a switch statement.</param>
		/// <param name="sixthCondition">The sixth condition to evaluate for a switch statement.</param>
		/// <param name="seventhCondition">The seventh condition to evaluate for a switch statement.</param>
		/// <returns>The <see cref="SwitchFlag"/> value representing the evaluation of all the conditions.</returns>
		public static SwitchFlag From(
			bool firstCondition,
			bool secondCondition,
			bool thirdCondition,
			bool forthCondition,
			bool fifthCondition,
			bool sixthCondition,
			bool seventhCondition)
		{
			SwitchFlag result = SwitchFlag.AllFalse;

			if (firstCondition)
			{
				result |= SwitchFlag.FirstTrue;
			}

			if (secondCondition)
			{
				result |= SwitchFlag.SecondTrue;
			}

			if (thirdCondition)
			{
				result |= SwitchFlag.ThirdTrue;
			}

			if (forthCondition)
			{
				result |= SwitchFlag.ForthTrue;
			}

			if (fifthCondition)
			{
				result |= SwitchFlag.FifthTrue;
			}

			if (sixthCondition)
			{
				result |= SwitchFlag.SixthTrue;
			}

			if (seventhCondition)
			{
				result |= SwitchFlag.SeventhTrue;
			}

			return result;
		}

		/// <summary>
		/// Produces a <see cref="SwitchFlag"/> value from evaluating a series of conditions for use in a switch statement
		/// instead of using deeply nested if statements.
		/// </summary>
		/// <param name="firstCondition">The first condition to evaluate for a switch statement.</param>
		/// <param name="secondCondition">The second condition to evaluate for a switch statement.</param>
		/// <param name="thirdCondition">The third condition to evaluate for a switch statement.</param>
		/// <param name="forthCondition">The forth condition to evaluate for a switch statement.</param>
		/// <param name="fifthCondition">The fifth condition to evaluate for a switch statement.</param>
		/// <param name="sixthCondition">The sixth condition to evaluate for a switch statement.</param>
		/// <param name="seventhCondition">The seventh condition to evaluate for a switch statement.</param>
		/// <param name="eighthCondition">The eighth condition to evaluate for a switch statement.</param>
		/// <returns>The <see cref="SwitchFlag"/> value representing the evaluation of all the conditions.</returns>
		public static SwitchFlag From(
			bool firstCondition,
			bool secondCondition,
			bool thirdCondition,
			bool forthCondition,
			bool fifthCondition,
			bool sixthCondition,
			bool seventhCondition,
			bool eighthCondition)
		{
			SwitchFlag result = SwitchFlag.AllFalse;

			if (firstCondition)
			{
				result |= SwitchFlag.FirstTrue;
			}

			if (secondCondition)
			{
				result |= SwitchFlag.SecondTrue;
			}

			if (thirdCondition)
			{
				result |= SwitchFlag.ThirdTrue;
			}

			if (forthCondition)
			{
				result |= SwitchFlag.ForthTrue;
			}

			if (fifthCondition)
			{
				result |= SwitchFlag.FifthTrue;
			}

			if (sixthCondition)
			{
				result |= SwitchFlag.SixthTrue;
			}

			if (seventhCondition)
			{
				result |= SwitchFlag.SeventhTrue;
			}

			if (eighthCondition)
			{
				result |= SwitchFlag.EighthTrue;
			}

			return result;
		}
	}
}
