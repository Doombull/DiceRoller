using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiceRoller
{
    class ButcherSorscha : IRunnable
    {
        protected static int SorschaHealth = 17;
        protected static int SorschaBaseArmour = 15;
        protected static int SorschaBaseDefense = 19;

        protected static int ButcherMAT = 9;
        protected static int ButcherFocus = 4;
        protected static int ButcherStrength = 16;

        protected bool BoostDamageRolls = false;
        protected int BoostThreshold;

        public ButcherSorscha(bool boostDamage)
            : this(boostDamage, SorschaHealth)
        {}

        public ButcherSorscha(bool boostDamage, int boostThreshold)
        {
            BoostThreshold = boostThreshold;
            BoostDamageRolls = boostDamage;
        }

        /// <summary>
        /// This runs the simulation
        /// </summary>
        /// <returns>True if the butcher dies</returns>
        public bool Run(out int damage)
        {
            bool success = false;
            damage = 0;

            int butcherCurrFocus = ButcherFocus;

            //Make an attack
            while (butcherCurrFocus > 0)
            {
                //record this attack
                butcherCurrFocus--;

                //roll the attack
                int attackRoll;

                if (butcherCurrFocus == 0)
                    attackRoll = Dice.Roll(2);
                else
                {
                    attackRoll = Dice.Roll(3);
                    butcherCurrFocus--;
                }

                //see if the attack hits
                if (attackRoll + ButcherMAT >= SorschaBaseDefense)
                {
                    //roll the damage
                    int damageRoll;

                    if (BoostDamageRolls && BoostThreshold >= SorschaHealth - damage)
                        damageRoll = Dice.Roll(4) + ButcherStrength;
                    else
                        damageRoll = Dice.Roll(3) + ButcherStrength;

                    if (damageRoll > SorschaBaseArmour)
                        damage += damageRoll - SorschaBaseArmour;
                }

                //If the caster is down, record kill
                if (damage >= SorschaHealth)
                    success = true;
            }

            return success;
        }
    }
}
