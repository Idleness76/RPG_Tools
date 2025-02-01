namespace GameTools.DiceEngine.Contracts;

/// <summary>
/// IDiceBag defines the behaviours and features of the Dice Rolling component of the system.
/// </summary>


public interface IDiceBag
{
    /// <summary>
    /// Generate a random number by simulating the roll of a number of dice, each of which has a number of sides determined by the diceKind parameter.
    /// There is an optional resultModifier parameter that can be used to add or subtract from the result of the dice roll.
    /// </summary>
    /// <param name="numberOfDice">How many dice to roll?</param>
    /// <param name="diceKind">How many sides do these dice have?</param>
    /// <param name="resultModifier">Do we need to change the result before returning it?</param>
    /// <returns></returns>

    DiceTray Roll(int numberOfDice, MathRockKind diceKind, int? resultModifier = null);

    // implement rollModifier later
    // int Roll(int numberOfDice, MathRockKind diceKind, int? resultModifier = null, object? rollModifier = null);
}
