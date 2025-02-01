using GameTools.DiceEngine.Contracts;

namespace GameTools.DiceEngine.Tests;

public class DiceBagTests
{
    /// <summary>
    /// The DiceBag exists, and is ready to use
    /// </summary>
    [Fact]
    public void DiceBag_Exists()
    {
        IDiceBag? testObject = new DiceBag();

        Assert.NotNull(testObject);
    }

    /// <summary>
    /// The result of the unmodified Roll is a) an integer b) greater than or equal to: 1
    /// </summary>
    [Fact]
    public void Roll_Returns_Postive_Integer()
    {
        DiceBag testObject = new();

        int numberOfDice = 1;
        MathRockKind diceKind = MathRockKind.D6;
        var diceTray = testObject.Roll(numberOfDice, diceKind);

        Assert.IsType<int>(diceTray.Result);
        Assert.True(diceTray.Result >= 1);
    }

    /// <summary>
    /// The result of the unmodified Roll is greater than or equal to: NumberOfDice
    /// </summary>
    [Fact]
    public void Roll_Returns_AtLeastNumberOfDice()
    {
        DiceBag testObject = new();

        int numberOfDice = 2;
        MathRockKind diceKind = MathRockKind.D6;
        var diceTray = testObject.Roll(numberOfDice, diceKind);

        Assert.True(diceTray.Result >= numberOfDice);
    }

    /// <summary>
    /// The result of the unmodified Roll is less than or equal to: NumberOfDice x DiceKind
    /// </summary>
    [Fact]
    public void Roll_Returns_AtMostNumberOfDiceTimesDiceKind()
    {
        DiceBag testObject = new();

        int numberOfDice = 2;
        MathRockKind diceKind = MathRockKind.D6;
        var diceTray = testObject.Roll(numberOfDice, diceKind);

        Assert.True(diceTray.Result <= numberOfDice * (int)diceKind);
    }

    /// <summary>
    /// The correct number of dice were rolled
    /// </summary>
    [Fact]
    public void Roll_Returns_CorrectNumberOfDice()
    {
        DiceBag testObject = new();

        int numberOfDice = 3;
        MathRockKind diceKind = MathRockKind.D6;
        var diceTray = testObject.Roll(numberOfDice, diceKind);

        Assert.Equal(numberOfDice, diceTray.RollCount);
    }

}