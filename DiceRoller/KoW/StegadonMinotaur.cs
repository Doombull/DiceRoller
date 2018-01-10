using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiceRoller.KoW
{
    class StegadonMinotaur : GenericCombat
    {
        public StegadonMinotaur(bool minoCharge)
        {
            var Minotaur = new Unit()
                {
                    Hit = 4,
                    Def = 4,
                    Attacks = 24,
                    Nerve = 18,
                    CrushingStr = 1,
                    ThunderingCharge = 2
                };
            var Stegadon = new Unit()
            {
                Hit = 4,
                Def = 6,
                Attacks = 10,
                Nerve = 16,
                CrushingStr = 4
            };

            if (minoCharge)
            {
                FirstUnit = Minotaur;
                SecondUnit = Stegadon;
            }
            else
            {
                FirstUnit = Stegadon;
                SecondUnit = Minotaur;
            }
        }            
    }
}
