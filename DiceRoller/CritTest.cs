using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiceRoller
{
    class CritTest : IRunnable
    {
        protected int TargetDef = 10;


        /// <summary>
        /// This runs the simulation
        /// </summary>
        /// <returns>True if Irusk dies</returns>
        public bool Run(out int metric)
        {
            metric = 0;

            bool crit;
            int hitRoll = Dice.Roll(5, 2, true, out crit);

            if (crit)
                metric = 1;

            if (hitRoll >= TargetDef)
                return true;

            return false;
        }
    }
}
