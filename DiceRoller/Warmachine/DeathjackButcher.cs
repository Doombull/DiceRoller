using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiceRoller
{
    class DeathjackButcher : IRunnable
    {
        protected int ButcherHealth = 20;
        protected int ButcherBaseArmour = 18;
        protected int ButcherBaseDefense = 14;

        protected int DeathjackMat = 9;
        protected int DeathjackBaseAttacks = 2;
        protected int DeathjackBaseHornAttacks = 1;
        protected int DeathjackBaseFocus = 5;
        protected int DeathjackBaseStrength = 18;
        protected int DeathjackHornStrength = 15;

        protected int ButcherFocusUsed = 4;
        protected bool UseDeathShroud = false;
        protected bool UseWarDog = true;
        protected bool BoostDamageRolls = true;
        protected bool UseIronFlesh = false;

        /// <summary>
        /// This runs the simulation
        /// </summary>
        /// <returns>True if the butcher dies</returns>
        public bool Run(out int damage)
        {
            bool success = false;
            damage = 0;

            int deathjackAttacks = DeathjackBaseAttacks;
            int deathjackHornAttacks = DeathjackBaseHornAttacks;
            int deathjackFocus = DeathjackBaseFocus;


            //Work out the butchers stats
            int butchersArmour = ButcherBaseArmour + 6 - ButcherFocusUsed;
            if (UseDeathShroud)
                butchersArmour -= 2;

            int butchersDefense = ButcherBaseDefense;
            if (UseIronFlesh)
            {
                butchersDefense += 3;
                butchersArmour -= 1;
            }
            if (UseWarDog)
            {
                butchersDefense += 2;
            }

            //Normal attacks
            while (deathjackAttacks > 0)
            {
                deathjackAttacks--;
                deathjackFocus--;

                //roll to hit
                int hitroll = DeathjackMat + Dice.Roll(3);
                if (hitroll >= butchersDefense)
                {
                    deathjackFocus--;
                    int damageroll = DeathjackBaseStrength + Dice.Roll(3);

                    if (damageroll > butchersArmour)
                        damage += damageroll - butchersArmour;
                }
            }

            //Horn attacks
            while (deathjackHornAttacks > 0)
            {
                deathjackHornAttacks--;
                deathjackFocus--;

                //roll to hit
                int hitroll = DeathjackMat + Dice.Roll(3);
                if (hitroll >= butchersDefense)
                {
                    deathjackFocus--;
                    int damageroll = DeathjackHornStrength + Dice.Roll(3);

                    if (damageroll > butchersArmour)
                        damage += damageroll - butchersArmour;
                }
            }

            //Bought attacks
            while (deathjackFocus > 0)
            {
                deathjackFocus--;

                //roll to hit
                int hitroll;
                if (deathjackFocus > 0)
                {
                    hitroll = DeathjackMat + Dice.Roll(3);
                    deathjackFocus--;
                }
                else
                    hitroll = DeathjackMat + Dice.Roll(2);
                
                //roll wound
                if (hitroll >= butchersDefense)
                {
                    int damageroll;
                    if (deathjackFocus > 0)
                    {
                        damageroll = DeathjackBaseStrength + Dice.Roll(3);
                        deathjackFocus--;
                    }
                    else
                        damageroll = DeathjackBaseStrength + Dice.Roll(2);

                    if (damageroll > butchersArmour)
                        damage += damageroll - butchersArmour;
                }
            }

            return damage >= ButcherHealth;
        }
    }
}
