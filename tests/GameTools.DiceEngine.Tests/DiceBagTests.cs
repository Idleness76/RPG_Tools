using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameTools.DiceEngine.Contracts;

namespace GameTools.DiceEngine.Tests;
#pragma warning disable CA1859 // Don't care about performance in tests and prefer to use Interface

public class DiceBagTestData : IEnumerable<object[]>
{
    private static readonly Random _random = new();
    private static readonly MathRockKind[] _mathRockKinds = (MathRockKind[])Enum.GetValues(typeof(MathRockKind));

    public IEnumerator<object[]> GetEnumerator()
    {
        var testCases = Enumerable.Range(0, 5).Select(_ => new object[] // 5 test cases
        {
                _random.Next(1, 8), // Random number between 1 and 7
                _mathRockKinds[_random.Next(_mathRockKinds.Length)] // Random MathRockKind
        }).ToList();

        return testCases.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

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
    [Theory]
    [ClassData(typeof(DiceBagTestData))]
    public void DiceBag_Roll_Returns_PostiveInteger(int numberOfDice, MathRockKind mathRock)
    {
        IDiceBag testObject = new DiceBag();

        var diceTray = testObject.Roll(numberOfDice, mathRock);

        // Ensure the summed result is a positive integer
        Assert.True(diceTray.Result >= 1);

        // Ensure every single roll within the result is a positive integer
        foreach (var roll in diceTray.Rolls)
        {
            Assert.True(roll >= 1, "Each roll should be greater than or equal to 1");
        }
    }

    /// <summary>
    /// The result of the unmodified Roll is greater than or equal to: NumberOfDice
    /// </summary>
    [Theory]
    [ClassData(typeof(DiceBagTestData))]
    public void DiceBag_Roll_Returns_AtLeast_NumberOfDice(int numberOfDice, MathRockKind mathRock)
    {
        IDiceBag testObject = new DiceBag();

        var diceTray = testObject.Roll(numberOfDice, mathRock);

        Assert.True(diceTray.Result >= numberOfDice);
    }

    /// <summary>
    /// The result of the unmodified Roll is less than or equal to: NumberOfDice x DiceKind
    /// </summary>
    [Theory]
    [ClassData(typeof(DiceBagTestData))]
    public void DiceBag_Roll_Returns_AtMost_NumberOfDiceTimesDiceKind(int numberOfDice, MathRockKind mathRock)
    {
        IDiceBag testObject = new DiceBag();

        var diceTray = testObject.Roll(numberOfDice, mathRock);

        Assert.True(diceTray.Result <= numberOfDice * (int)mathRock);
    }

    /// <summary>
    /// The correct number of dice were rolled
    /// </summary>
    [Theory]
    [ClassData(typeof(DiceBagTestData))]
    public void DiceBag_Roll_Uses_CorrectNumberOfDice(int numberOfDice, MathRockKind mathRock)
    {
        IDiceBag testObject = new DiceBag();

        var diceTray = testObject.Roll(numberOfDice, mathRock);

        Assert.Equal(numberOfDice, diceTray.RollCount);
    }

}