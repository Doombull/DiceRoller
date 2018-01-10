using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiceRoller
{
    class CataclysmIrusk : IRunnable
    {
        protected int IruskHealth = 17;
        protected int IruskBaseArmour = 15;
        protected int IruskBaseDefense = 15;

        protected int CataclysmStr = 15;


        /// <summary>
        /// This runs the simulation
        /// </summary>
        /// <returns>True if Irusk dies</returns>
        public bool Run(out int damage)
        {
            damage = 0;

            //damage
            damage += CataclysmStr - IruskBaseArmour + Dice.Roll(3);
            damage += CataclysmStr - IruskBaseArmour + Dice.Roll(3);

            //tough
            if (damage >= IruskHealth && Dice.Roll(1) < 4)
                return true;

            return false;
        }


    }
}
