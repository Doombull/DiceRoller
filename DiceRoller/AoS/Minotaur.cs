using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiceRoller
{
    class Minotaur : IRunnable
    {
        protected int Attacks;
        protected int Damage;
        protected int HitOn = 4;
        protected int WoundOn = 3;

        public Minotaur(bool greatWeapon)
        {
            if (greatWeapon)
            {
                Attacks = 6;
                Damage = 3;
            }
            else
            {
                Attacks = 9;
                Damage = 2;
            }
        }

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

                if (hitRoll >= HitOn)
                {
                    int damRoll = Dice.Roll() + 1;

                    if (damRoll >= WoundOn)
                        wounds += Damage;

                    if (damRoll >= 6)
                        remainingAttacks += 2;
                }
            }

            return false;
        }
    }
}
