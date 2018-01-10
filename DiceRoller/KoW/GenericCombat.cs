using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiceRoller.KoW
{
    public class GenericCombat : IRunnable
    {
        protected Unit FirstUnit;
        protected Unit SecondUnit;

        protected GenericCombat()
        { } 

        public GenericCombat(Unit firstUnit, Unit secondUnit)
        {
            FirstUnit = firstUnit;
            SecondUnit = secondUnit;
        }            

        /// <summary>
        /// This runs the simulation
        /// </summary>
        /// <returns>True if attacker wins</returns>
        public bool Run(out int firstUnitWounds)
        {
            firstUnitWounds = 0;
            var secondUnitWounds = CombatRound(FirstUnit, SecondUnit, true);
            if (RoutCheck(SecondUnit, secondUnitWounds))
                return true;

            while (true)
            {
                firstUnitWounds += CombatRound(SecondUnit, FirstUnit, false);
                if (RoutCheck(FirstUnit, firstUnitWounds))
                    return false;

                secondUnitWounds += CombatRound(FirstUnit, SecondUnit, false);
                if (RoutCheck(SecondUnit, secondUnitWounds))
                    return true;
            }
        }

        protected int CombatRound(Unit attacker, Unit defender, bool isCharge)
        {
            var wounds = 0;

            for (int i = 0; i < attacker.Attacks; i++)
            {
                var hitRoll = Dice.Roll();
                if (hitRoll < attacker.Hit)
                    continue;

                var damageRoll = Dice.Roll();
                if (damageRoll == 1)
                    continue;

                damageRoll += attacker.CrushingStr;
                if (isCharge)
                    damageRoll += attacker.ThunderingCharge;

                if (damageRoll < defender.Def)
                    continue;

                wounds++;
            }

            return wounds;
        }

        protected bool RoutCheck(Unit unit, int wounds)
        {
            if (Dice.Roll(2) + wounds >= unit.Nerve)
            {
                if (!unit.Inspired)
                    return true;

                if (Dice.Roll(2) + wounds >= unit.Nerve)
                    return true;
            }

            return false;
        }
    }
}
