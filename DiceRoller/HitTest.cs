using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiceRoller
{
    class HitTest : IRunnable
    {
        protected int TargetDef = 12;
        protected int TargetArm = 20;
        protected int TargetHealth = 32;
        protected int Focus = 3;
        protected int Strength = 13;
        protected int MAT = 10;
        protected bool IsWeaponMaster = true;

        protected bool IsBoostHit;
        protected bool IsBoostDam;


        public HitTest(bool boostHit, bool boostDam)
        {
            IsBoostHit = boostHit;
            IsBoostDam = boostDam;
        }

        public HitTest(bool boostHit, bool boostDam, int focus) : this(boostHit, boostDam)
        {
            Focus = focus;
        }


        /// <summary>
        /// This runs the simulation
        /// </summary>
        /// <returns>True if the caster dies</returns>
        public bool Run(out int damage)
        {
            damage = 0;
            //int currentFocus = Focus; Dice.RollD3() + Dice.Roll() + 2;

            int currentFocus = Dice.RollD3() + Dice.RollD3() + 2;

            while (currentFocus > 0)
            {
                currentFocus--;
                int hitRoll = MAT + Dice.Roll(2);

                if (IsBoostHit)
                {
                    currentFocus--;
                    hitRoll += Dice.Roll(1);
                }

                if (hitRoll >= TargetDef)
                {
                    int damRoll = Dice.Roll(2) + Strength;

                    if (IsBoostDam)
                    {
                        currentFocus--;
                        damRoll += Dice.Roll(1);
                    }

                    if (IsWeaponMaster)
                        damRoll += Dice.Roll(1);

                    if (damRoll > TargetArm)
                        damage += damRoll - TargetArm;                    
                }
            } 

            if (damage >= TargetHealth)
                return true;

            return false;
        }
    }
}
