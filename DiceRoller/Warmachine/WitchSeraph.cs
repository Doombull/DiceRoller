using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiceRoller
{
    class WitchSeraph : IRunnable
    {
        protected int WitchArmour = 14;
        protected int WitchDefence = 15;
        protected int WitchHealth = 16;

        protected int SeraphRAT = 5;
        protected int SeraphStrength = 12;
        protected int SeraphFury = 4;

        protected bool AlwaysBoostDamage = true;

        public bool Run(out int damage)
        {
            bool success = false;
            damage = 0;

            int seraphCurrentFury = SeraphFury;


            int shots = Dice.RollD3() + 1;

            for (int i = 0; i < shots; i++)
            {
                int hitRoll = Dice.Roll(2) + SeraphRAT;

                if (seraphCurrentFury > 0)
                {
                    seraphCurrentFury--;
                    hitRoll += Dice.Roll();
                }

                if (hitRoll >= WitchDefence)
                {
                    int damageRoll = Dice.Roll(2) + SeraphStrength;

                    if (seraphCurrentFury > 0 && (AlwaysBoostDamage || i + seraphCurrentFury >= shots))
                    {
                        seraphCurrentFury--;
                        damageRoll += Dice.Roll();
                    }

                    if (damageRoll > WitchArmour)
                        damage += (damageRoll - WitchArmour);

                    if (damage >= WitchHealth)
                        success = true;
                }
            }

            return success;
        }
    }
}
