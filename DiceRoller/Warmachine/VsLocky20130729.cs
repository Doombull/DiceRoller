using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiceRoller
{
    class VsLocky20130729 : IRunnable
    {
        protected int ButcherHealth = 20;
        protected int ButcherBaseArmour = 20;
        protected int ButcherBaseDefense = 5;

        protected int CaineBaseFocus = 2;
        protected int CaineShotStr = 12;
        protected int SlamStr = 14;
        protected int NyssCraStr = 17;
        protected int OutriderStr = 10;


        /// <summary>
        /// This runs the simulation
        /// </summary>
        /// <returns>True if Irusk dies</returns>
        public bool Run(out int damage)
        {
            damage = 0;
            int attackStr = 0;

            //caine
            damage += SlamStr - ButcherBaseArmour + Dice.Roll(2);

            int caineCurrentFocus = CaineBaseFocus;
            for (int i = 4; i >= 1; i--)
            {
                if (Dice.Roll(2) == 2)
                    continue;

                if (caineCurrentFocus >= 1)
                {
                    attackStr = CaineShotStr + Dice.Roll(3);
                    caineCurrentFocus--;
                }
                else
                    attackStr = CaineShotStr + Dice.Roll(2);

                if (attackStr > ButcherBaseArmour)
                    damage += attackStr - ButcherBaseArmour;
            }

            //outriders
            for (int i = 3; i >= 1; i--)
            {
                if (Dice.Roll(2) == 2)
                    continue;

                attackStr = OutriderStr + Dice.Roll(3);

                if (attackStr > ButcherBaseArmour)
                    damage += attackStr - ButcherBaseArmour;
            }

            //nyss
            if (Dice.Roll(2) > 2)
            {
                attackStr = NyssCraStr + Dice.Roll(2);

                if (attackStr > ButcherBaseArmour)
                    damage += attackStr - ButcherBaseArmour;                
            }

            //result
            if (damage >= ButcherHealth)
                return true;

            return false;
        }


    }
}
