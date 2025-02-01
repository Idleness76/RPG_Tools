namespace GameTools.DiceEngine.Contracts;

/// <summary>
/// IDiceBag defines the behaviours and features of the Dice Rolling component of the system.
/// </summary>


public interface IDiceBag
{
    /// <summary>
    /// Generate a random number by simulating the roll of a number of dice, each of which has a number of sides determined by the diceKind parameter.
    /// There is an optional rollAdjustment parameter that can be used to add or subtract from the result of the dice roll.
    /// </summary>
    /// <param name="numberOfDice">How many dice to roll?</param>
    /// <param name="diceKind">How many sides do these dice have?</param>
    /// <param name="rollAdjustment">(Optional) Do we need to change the result before returning it?</param>
    /// <param name="rollModifier">(Optional) Do we have advantage or disadvantage on this roll?</param>
    /// <returns></returns>

    DiceTray Roll(int numberOfDice, MathRockKind diceKind, int rollAdjustment = 0, RollModifier rollModifier = RollModifier.None);

}
