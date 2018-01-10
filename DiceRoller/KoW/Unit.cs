using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiceRoller.KoW
{
    public class Unit
    {
        public int Hit { get; set; }
        public int Def { get; set; }
        public int Attacks { get; set; }
        public int Nerve { get; set; }
        public int CrushingStr { get; set; }
        public int ThunderingCharge { get; set; }
        public bool Inspired { get; set; }
    }
}
