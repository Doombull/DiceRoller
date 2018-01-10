using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiceRoller
{
    public static class Dice
    {
        public static Random random = new Random();

        public static int Roll()
        {
            return random.Next(6) + 1;
        }

        public static int Roll(int noOfDice)
        {
            int sum = 0;

            for (int i = 0; i < noOfDice; i++)
                sum += random.Next(6) + 1;

            return sum;
        }

        public static int Roll(int noOfDice, int noToDrop)
        {
            bool crit;
            return Roll(noOfDice, noToDrop, true, out crit);
        }

        public static int Roll(int noOfDice, int noToDrop, bool dropLowest, out bool crit)
        {
            crit = false;

            int sum = 0;
            int[] rolls = new int[noOfDice];

            for (int i = 0; i < noOfDice; i++)
                rolls[i] = random.Next(6) + 1;

            Array.Sort<int>(rolls);

            int startPos = 0 + noToDrop;
            int endpos = noOfDice;

            if (!dropLowest)
            {
                startPos = 0;
                endpos = noOfDice - noToDrop;
            }

            int prevRoll = 0;
            for (int i = startPos; i < endpos; i++)
            {
                if (rolls[i] == prevRoll)
                    crit = true;

                sum += rolls[i];
                prevRoll = rolls[i];
            }

            return sum;
        }

        public static int RollD3()
        {
            return random.Next(3) + 1;
        }
    }
}
