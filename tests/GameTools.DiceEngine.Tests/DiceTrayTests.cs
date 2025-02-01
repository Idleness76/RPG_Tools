using GameTools.DiceEngine.Contracts;
namespace GameTools.DiceEngine.Tests;

public class DiceTrayTests
{
    /// <summary>
    /// The DiceTray exists, and is ready to use
    /// </summary>
    [Fact]
    public void DiceTray_Exists()
    {
        var testObject = new DiceTray();

        Assert.NotNull(testObject);
        Assert.True(testObject.Result == 0);
        Assert.True(testObject.RollCount == 0);
    }

    /// <summary>
    /// The DiceTray can add a MathRock result to its collection
    /// </summary>
    [Fact]
    public void DiceTray_CanAddMathRock()
    {
        DiceTray testObject = new();

        testObject.AddRoll(MathRockKind.D6, 4);

        int expectedRockCount = 1;

        Assert.Equal(expectedRockCount, testObject.RollCount);
    }

    /// <summary>
    /// The DiceTray can add multiple MathRock results to its collection, and calculate the total
    /// </summary>
    [Fact]
    public void DiceTray_CanAddMathRocksAndCalculateResult()
    {
        DiceTray testObject = new();

        testObject.AddRoll(MathRockKind.D6, 4);
        testObject.AddRoll(MathRockKind.D6, 3);
        testObject.AddRoll(MathRockKind.D6, 2);

        int expectedRockCount = 3;
        int expectedTotal = 9;

        Assert.Equal(expectedRockCount, testObject.RollCount);
        Assert.Equal(expectedTotal, testObject.Result);
    }
}
