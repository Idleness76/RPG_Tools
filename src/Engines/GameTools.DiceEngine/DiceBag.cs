using GameTools.DiceEngine.Contracts;
namespace GameTools.DiceEngine;

public class DiceBag : IDiceBag
{
    public DiceTray Roll(int numberOfDice, MathRockKind mathRockKind, int rollAdjustment = 0, RollModifier rollModifier = RollModifier.None)
    {
        DiceTray tray = new DiceTray(rollAdjustment, rollModifier);

        if (rollModifier != RollModifier.None && numberOfDice != 1)
        {
            throw new InvalidOperationException("When using Advantage or Disadvantage you can only ask to roll a single math rock!");
        }

        if (rollModifier != RollModifier.None)
        {
            numberOfDice += 1;

        }

        for (int roll = 0; roll < numberOfDice; roll++)
        {
            var rollResult = new Random();

            int value = rollResult.Next(1, (int)mathRockKind + 1);
            tray.AddRoll(mathRockKind, value);
        }

        return tray;
    }
}
