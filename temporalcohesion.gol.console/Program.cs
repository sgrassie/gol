using System;
using System.Threading;
using Mono.Options;
using temporalcohesion.gol.core;

namespace temporalcohesion.gol.console
{
    class Program
    {
        private static int _seed;
        private static string _initialPattern;
        private static int _xSize;
        private static int _ySize;
        private static int _generations;

        static void Main(string[] args)
        {
            var optionSet = new OptionSet
            {
                {"s|seed=", "The random seed", (int v) => _seed = v},
                {"p|pattern=", "A starting pattern, e.g. 'glider'", v=> _initialPattern = v},
                {"x=", "The board x size", (int v) => _xSize= v},
                {"y=", "The board y size", (int v) => _ySize= v},
                {"g|generations=", "The number of generations", (int v) => _generations = v}
            };

            optionSet.Parse(args);

            var gridStrategy = SelectStrategy();

            var life = new Life(_xSize, _ySize, gridStrategy);

            for (var i = 0; i < _generations; i++)
            {
                life.Tick();
                Console.Clear();
                Console.WriteLine(life.ToString());
                Console.WriteLine();
                Console.WriteLine("{0}/{1}", i+1, _generations);
                Thread.Sleep(1000);
            }
        }

        private static IGridPopulationStrategy SelectStrategy()
        {
            if(_seed > 0 && string.IsNullOrEmpty(_initialPattern)) return new DefaultGridPopulationStrategy(_seed);

            return new DefaultGridPopulationStrategy(new Random(42).Next());
        }
    }
}
