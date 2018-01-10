using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiceRoller
{
    class BaldurButcher : IRunnable
    {
        protected bool ButcherHasIF = false;
        protected int ButcherBaseHealth = 20;
        protected int ButcherBaseArmour = 18;
        protected int ButcherBaseDef = 14 - 2;
        protected int ButcherCampedFocus = 3;

        protected int BaldurMAT = 7 + 2;
        protected int BaldurStrength = 14 + 2;
        protected int BaldurBaseFocus = 7 - 2;


        protected bool BoostDamage = false;


        public BaldurButcher(int campedFocus, bool hasIronFlesh, bool boostDamage)
        {
            ButcherCampedFocus = campedFocus;
            ButcherHasIF = hasIronFlesh;
            BoostDamage = boostDamage;
        }

        public bool Run(out int metric)
        {
            metric = 0;

            int butcherCurrentHealth = ButcherBaseHealth;
            int baldurCurrentFocus = BaldurBaseFocus;

            int butcherCurrentArm = ButcherBaseArmour + ButcherCampedFocus;
            int butcherCurrentDef = ButcherBaseDef;
            if (ButcherHasIF)
                butcherCurrentDef += 3;

            bool hasWeightOfStone = false;

            while (baldurCurrentFocus > 0)
            {
                //Buy the attack
                baldurCurrentFocus--;

                //check if we need to boost
                int hitRoll;
                if (butcherCurrentDef > BaldurMAT + 7 || !hasWeightOfStone && (butcherCurrentDef > BaldurMAT + 6))
                {
                    baldurCurrentFocus--;
                    hitRoll = Dice.Roll(3);
                }
                else
                    hitRoll = Dice.Roll(2);

                //Did it hit
                if (hitRoll + BaldurMAT >= butcherCurrentDef)
                {
                    if (!hasWeightOfStone)
                    {
                        hasWeightOfStone = true;
                        butcherCurrentDef -= 3;
                    }

                    int damageRoll;
                    if (BoostDamage && baldurCurrentFocus > 0)
                    {
                        baldurCurrentFocus--;
                        damageRoll = Dice.Roll(3);
                    }
                    else
                        damageRoll = Dice.Roll(2);

                    int damage = damageRoll + BaldurStrength - butcherCurrentArm;
                    if (damage > 0)
                    {
                        metric += damage;
                        butcherCurrentHealth -= damage;
                    }
                }
            }

            return (butcherCurrentHealth <= 0);
        }
    }
}
