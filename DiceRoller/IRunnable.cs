using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiceRoller
{
    public interface IRunnable
    {
        bool Run(out int metric);
    }
}
