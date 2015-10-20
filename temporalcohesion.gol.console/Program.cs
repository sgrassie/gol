using System;
using System.Threading;
using Mono.Options;
using temporalcohesion.gol.core;

namespace temporalcohesion.gol.console
{
    class Program
    {
        private static int seed;
        private static int boardSize;
        private static int generations;

        static void Main(string[] args)
        {
            var optionSet = new OptionSet
            {
                {"s|seed=", "The random seed", (int v) => seed = v},
                {"b|boardSize=", "The board size", (int v) => boardSize = v},
                {"g|generations=", "The number of generations", (int v) => generations = v}
            };

            optionSet.Parse(args);

            var life = new Life(seed, boardSize);


            for (var i = 0; i < generations; i++)
            {
                life.Tick();
                Console.Clear();
                Console.WriteLine(life.ToString());
                Console.WriteLine();
                Console.WriteLine("{0}/{1}", i+1, generations);
                Thread.Sleep(1000);
            }
        }
    }
}
