using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DiceRoller
{
    class Program
    {
        static void Main(string[] args)
        {
            IRunnable test;

            //Console.WriteLine("Minotaurs Charge");
            //test = new KoW.MinotaurPeasants(true);
            //PerformRun(test);

            //Console.WriteLine();

            //Console.WriteLine("Spearmen Charge");
            //test = new KoW.MinotaurPeasants(false);
            //PerformRun(test);

            Console.WriteLine("Minotaurs Charge");
            test = new KoW.StegadonMinotaur(true);
            PerformRun(test);

            Console.WriteLine();

            Console.WriteLine("Steg Charge");
            test = new KoW.StegadonMinotaur(false);
            PerformRun(test);

            //Wait to exit
            Console.ReadLine();
        }

        static void PerformRun(IRunnable test)
        {
            const int noOfRuns = 10000000;
            double successes = 0;
            double cumulativeResult = 0;

            for (int i = 0; i < noOfRuns; i++)
            {
                int metric;
                bool result = test.Run(out metric);

                cumulativeResult += metric;
                if (result)
                    successes++;
            }

            Console.WriteLine("Success: " + successes);
            Console.WriteLine("Runs: " + noOfRuns);

            string message = "Test completed. Result occured {0}%";
            Console.WriteLine(String.Format(message, (successes / noOfRuns * 100).ToString("0.00")));

            message = "Metric: {0}";
            Console.WriteLine(String.Format(message, (cumulativeResult / noOfRuns).ToString("0.00")));
            Console.WriteLine();
        }
    }
}
