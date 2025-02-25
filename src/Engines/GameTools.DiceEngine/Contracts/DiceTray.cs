using System;
using System.Collections.Generic;
using System.Linq;

namespace GameTools.DiceEngine.Contracts
{
    /// <summary>
    /// DiceTray carries the result of the Roll operation back to the component who uses the DiceBag.
    /// </summary>
    public class DiceTray
    {
        private List<RolledMathRock> _rocks;

        public DiceTray() : this(resultModifier: 0) { }

        public DiceTray(int resultModifier)
        {
            _rocks = new List<RolledMathRock>();
            ResultModifier = resultModifier;
        }

        public int ResultModifier { get; private set; }

        public int[] Rolls => _rocks.Select(r => r.Value).ToArray();

        public int UnadjustedResult => _rocks.Sum(r => r.Value);

        public int Result => Math.Max(0, UnadjustedResult + ResultModifier);

        public int RollCount => _rocks.Count;

        public void AddRoll(MathRockKind kind, int result)
        {
            var roll = new RolledMathRock(kind, result);
            _rocks.Add(roll);
        }
    }
}