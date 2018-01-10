using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiceRoller
{
    class ButcherCaine : IRunnable
    {
        protected int ButcherHealth = 20;
        protected int ButcherBaseArmour = 18;
        protected int ButcherBaseDefense = 14;

        protected int CaineRAT = 9;
        protected int CaineAttacks = 3;
        protected int CaineFocus = 6;
        protected int CaineBaseStrength = 12;

        protected int ButcherFocusUsed = 3;
        protected bool UseSquire = true;
        protected bool UseRanger = false;
        protected bool BoostAttackRolls = false;
        protected bool UseIronFlesh = true;
        protected bool HasConcealment = true;
        protected bool TrueSightUpkept = true;

        /// <summary>
        /// This runs the simulation
        /// </summary>
        /// <returns>True if the butcher dies</returns>
        public bool Run(out int damage)
        {
            bool success = false;
            damage = 0;

            //Work out Caines stats
            int caineStrengthBonus = 0;
            if (UseSquire)
                CaineFocus++;

            if (UseRanger)
                CaineRAT += 2;

            if (HasConcealment)
            {
                if (TrueSightUpkept)
                    CaineFocus -= 1;
                else
                    CaineFocus -= 2;
            }

            //Work out the butchers stats
            int butchersArmour = ButcherBaseArmour + 6 - ButcherFocusUsed;

            int butchersDefense = ButcherBaseDefense;
            if (UseIronFlesh)
            {
                butchersDefense += 3;
                butchersArmour -= 1;
            }

            //Check if Caine has a shot
            while (CaineAttacks > 0 || CaineFocus > 0)
            {
                //record this shot
                if (CaineAttacks > 0)
                    CaineAttacks--;
                else
                    CaineFocus--;

                //roll the attack
                int attackRoll;
                if (BoostAttackRolls)
                {
                    attackRoll = Dice.Roll(3);
                    CaineFocus--;
                }
                else
                    attackRoll = Dice.Roll(2);

                //see if the attack hits
                if (attackRoll + CaineRAT >= butchersDefense)
                {
                    //roll the damage
                    int damageRoll = Dice.Roll(2) + CaineBaseStrength + caineStrengthBonus;
                    if (damageRoll > butchersArmour)
                        damage += damageRoll - butchersArmour;

                    //Strength goes up from feat
                    caineStrengthBonus++;
                }

                //If the butcher is down, record kill
                if (damage >= ButcherHealth)
                    success = true;
            }

            return success;
        }
    }
}
