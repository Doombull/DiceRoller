using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiceRoller
{
    class Ghorgon : IRunnable
    {
        protected int Attacks = 5;
        protected int BladeHitOn = 3;
        protected int BladeWoundOn = 3;
        protected int BladeDamage = 3;
        protected int MawHitOn = 4;
        protected int MawWoundOn = 2;

        /// <summary>
        /// This runs the simulation
        /// </summary>
        /// <returns>Always true. Metric is the wounds done before saves</returns>
        public bool Run(out int wounds)
        {
            wounds = 0;
            int remainingAttacks = Attacks;

            while (remainingAttacks > 0)
            {
                remainingAttacks--;
                int hitRoll = Dice.Roll();

                if (hitRoll >= BladeHitOn)
                {
                    int damRoll = Dice.Roll() + 1;

                    if (damRoll >= BladeWoundOn)
                        wounds += BladeDamage;

                    if (damRoll >= 5)
                        remainingAttacks += 2;
                }
            }

            remainingAttacks = 1;

            while (remainingAttacks > 0)
            {
                remainingAttacks--;
                int hitRoll = Dice.Roll();

                if (hitRoll >= MawHitOn)
                {
                    int damRoll = Dice.Roll() + 1;

                    if (damRoll >= MawWoundOn)
                        wounds += Dice.Roll();

                    if (damRoll >= 5)
                        remainingAttacks += 2;
                }
            }

            return false;
        }
    }
}
