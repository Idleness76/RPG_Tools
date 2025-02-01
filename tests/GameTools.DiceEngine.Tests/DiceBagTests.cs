using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GameTools.DiceEngine.Contracts;

namespace GameTools.DiceEngine.Tests;
#pragma warning disable CA1859 // Don't care about performance in tests and prefer to use Interface

public class DiceBagUnmodifiedTestData : IEnumerable<object[]>
{
    private static readonly Random _random = new();
    private static readonly MathRockKind[] _mathRockKinds = (MathRockKind[])Enum.GetValues(typeof(MathRockKind));

    public IEnumerator<object[]> GetEnumerator()
    {
        var testCases = Enumerable.Range(0, 5).Select(_ => new object[] // 5 test cases
        {
                _random.Next(1, 8), // Random number of dice between 1 and 7
                _mathRockKinds[_random.Next(_mathRockKinds.Length)] // Random MathRockKind
        }).ToList();

        return testCases.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class DiceBagModifiedTestData : IEnumerable<object[]>
{

    private static readonly Random _random = new();
    private static readonly MathRockKind[] _mathRockKinds = (MathRockKind[])Enum.GetValues(typeof(MathRockKind));

    public IEnumerator<object[]> GetEnumerator()
    {
        var testCases = Enumerable.Range(0, 5).Select(_ =>
        {
            int resultModifier;
            do
            {
                resultModifier = _random.Next(-10, 11); // Random result modifier between -10 and 10, exluding 0
            } while (resultModifier == 0);

            return new object[]
            {
        _random.Next(1, 8), // Random number of dice between 1 and 7
        _mathRockKinds[_random.Next(_mathRockKinds.Length)], // Random MathRockKind
        resultModifier
            };
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

    // ==============================
    #region Basic Roll Tests

    /// <summary>
    /// The result of the unmodified Roll is a) an integer b) greater than or equal to: 1
    /// </summary>
    [Theory]
    [ClassData(typeof(DiceBagUnmodifiedTestData))]
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
    [ClassData(typeof(DiceBagUnmodifiedTestData))]
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
    [ClassData(typeof(DiceBagUnmodifiedTestData))]
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
    [ClassData(typeof(DiceBagUnmodifiedTestData))]
    public void DiceBag_Roll_Uses_CorrectNumberOfDice(int numberOfDice, MathRockKind mathRock)
    {
        IDiceBag testObject = new DiceBag();

        var diceTray = testObject.Roll(numberOfDice, mathRock);

        Assert.Equal(numberOfDice, diceTray.RollCount);
    }
    #endregion // Basic Roll Tests

    // ==============================
    #region Roll Modifier Tests

    /// <summary>
    /// The result of the Roll is modified by the resultModifier
    /// </summary>
    [Theory]
    [ClassData(typeof(DiceBagModifiedTestData))]
    public void DiceBag_Roll_WithNonZeroRollModifier_AppliesModifierToResult(int numberOfDice, MathRockKind mathRock, int resultModifier)
    {
        IDiceBag testObject = new DiceBag();

        DiceTray modifiedDiceRoll = testObject.Roll(numberOfDice, mathRock, resultModifier);

        // Assert.Equal(Math.Max(0, modifiedDiceRoll.UnadjustedResult + resultModifier), modifiedDiceRoll.Result);
        Assert.True(modifiedDiceRoll.Result != modifiedDiceRoll.UnadjustedResult);
    }

    /// <summary>
    /// The modified result, but never goes below 0, or above the maximum possible result
    /// </summary>
    [Theory]
    [ClassData(typeof(DiceBagModifiedTestData))]
    public void DiceBag_Roll_WithNonZeroRollModifier_NeverBelowZeroORAboveMax(int numberOfDice, MathRockKind mathRock, int resultModifier)
    {
        IDiceBag testObject = new DiceBag();

        int expectedMaximum = Math.Max(0, (numberOfDice * (int)mathRock) + resultModifier);

        DiceTray modifiedDiceRoll = testObject.Roll(numberOfDice, mathRock, resultModifier);

        Assert.True(modifiedDiceRoll.Result >= 0);
        Assert.InRange(modifiedDiceRoll.Result, 0, expectedMaximum);
    }





    #endregion // Roll Modifier Tests
}