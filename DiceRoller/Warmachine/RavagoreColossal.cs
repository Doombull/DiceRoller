using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiceRoller
{
    class RavagoreColossal : IRunnable
    {
        protected int NoOfRavagoreShots = 6;
        protected int RavagoreStrength = 15;
        protected bool RavagoreBoostDamage = true;

        protected int ColossalArmour;
        protected int ColossalHealth;

        protected int NoOfBowShots = 6;
        protected int BowStrength = 12;
        protected bool BowBoostDamage = true;

        public RavagoreColossal(int armour, int health)
        {
            ColossalArmour = armour;
            ColossalHealth = health;
        }

        public bool Run(out int damage)
        {
            damage = 0;

            for (int i = 0; i < NoOfRavagoreShots; i++)
            {
                if (RavagoreBoostDamage)
                    damage += CalculateDamage(Dice.Roll(4, 1), RavagoreStrength);
                else
                    damage += CalculateDamage(Dice.Roll(3), RavagoreStrength);
            }

            for (int i = 0; i < NoOfBowShots; i++)
            {
                if (BowBoostDamage)
                    damage += CalculateDamage(Dice.Roll(4, 1), BowStrength);
                else
                    damage += CalculateDamage(Dice.Roll(3), BowStrength);
            }

            return (ColossalHealth - damage <= 0);
        }

        protected int CalculateDamage(int damageRoll, int strength)
        {
            return damageRoll + strength - ColossalArmour;
        }
    }
}
