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

        public DiceTray() : this(rollAdjustment: 0, RollModifier.None) { }

        public DiceTray(int rollAdjustment, RollModifier rollModifier)
        {
            _rocks = new List<RolledMathRock>();
            RollAdjustment = rollAdjustment;
            RollModifier = rollModifier;
        }

        public int RollAdjustment { get; private set; }

        public RollModifier RollModifier { get; private set; }

        private bool _rollIsModified { get; set; }

        public RolledMathRock[] Rolls
        {
            get
            {
                HandleRollModifier();
                return _rocks.ToArray(); // Return the array of RolledMathRock objects
            }
        }

        public int UnadjustedResult
        {
            get
            {
                HandleRollModifier();
                return _rocks.Where(r => !r.IsDiscarded).Sum(r => r.Value);
            }
        }

        public int Result
        {
            get
            {
                return Math.Max(0, UnadjustedResult + RollAdjustment);
            }
        }

        public int RollCount => _rocks.Count;

        public void AddRoll(MathRockKind kind, int result)
        {
            var roll = new RolledMathRock(kind, result);
            _rocks.Add(roll);
            _rollIsModified = false;
        }

        private void HandleRollModifier()
        {
            if (_rollIsModified) return;

            _rocks.ForEach(r => r.ResetDiscard());

            if (RollModifier != RollModifier.None)
            {
                _rocks = _rocks.OrderByDescending(r => r.Value).ToList();
            }

            if (RollModifier == RollModifier.Advantage)
            {
                _rocks.Last().Discard();
                _rollIsModified = true;
            }
            else if (RollModifier == RollModifier.Disadvantage)
            {
                _rocks.First().Discard();
                _rollIsModified = true;
            }
        }
    }
}