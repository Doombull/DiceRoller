using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiceRoller.KoW
{
    class MinotaurPeasants : GenericCombat
    {
        public MinotaurPeasants(bool minoCharge)
        {
            var Minotaur = new Unit()
                {
                    Hit = 4,
                    Def = 4,
                    Attacks = 12,
                    Nerve = 15,
                    CrushingStr = 1,
                    ThunderingCharge = 0
                };
            var Spearmen = new Unit()
            {
                Hit = 4,
                Def = 4,
                Attacks = 15,
                Nerve = 15,
                Inspired = true
            };

            if (minoCharge)
            {
                FirstUnit = Minotaur;
                SecondUnit = Spearmen;
            }
            else
            {
                FirstUnit = Spearmen;
                SecondUnit = Minotaur;
            }
        }            
    }
}
