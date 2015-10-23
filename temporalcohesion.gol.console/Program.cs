using System;
using System.Threading;
using Mono.Options;
using temporalcohesion.gol.core;

namespace temporalcohesion.gol.console
{
    class Program
    {
        private const ConsoleColor DeadColor = ConsoleColor.Black;
        private const ConsoleColor LiveColor = ConsoleColor.Green;
        private const ConsoleColor BackgroundColor = ConsoleColor.DarkGray;

        private static string _initialPattern;
        private static int _xSize;
        private static int _ySize;
        private static int _generations;
        private static int _delay;

        static void Main(string[] args)
        {
            var optionSet = new OptionSet
            {
                {"p|pattern=", "A starting pattern, e.g. 'glider'", v=> _initialPattern = v},
                {"x=", "The board x size", (int v) => _xSize= v},
                {"y=", "The board y size", (int v) => _ySize= v},
                {"g|generations=", "The number of generations", (int v) => _generations = v},
                {"d|delay=", "The delay in milliseconds", (int v) => _delay = v}
            };

            optionSet.Parse(args);

            InitialiseConsole();

            var gridStrategy = SelectStrategy();

            var life = new Life(_xSize, _ySize, gridStrategy);

            for (var i = 0; i < _generations; i++)
            {
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape) break;
                life.Tick();
                Console.SetCursorPosition(0, 0);
                Console.Write(life.ToString());
                Console.WriteLine();
                Console.WriteLine("{0}/{1}", i + 1, _generations);
                if (_delay > 0) Thread.Sleep(_delay);
            }

            ResetConsole();
        }

        private static void InitialiseConsole()
        {
            Console.BackgroundColor = BackgroundColor;
            Console.Clear();
            Console.CursorVisible = false;
            var consoleWidth = Console.BufferWidth;
            var consoleHeight = Console.BufferHeight;
            Console.SetBufferSize(consoleWidth, consoleHeight);
            Console.BackgroundColor = DeadColor;
            Console.ForegroundColor = LiveColor;
        }

        private static void ResetConsole()
        {
            Console.ResetColor();
            Console.CursorVisible = true;
        }

        private static IGridPopulationStrategy SelectStrategy()
        {
            switch (_initialPattern)
            {
                case "glider" : return new GliderPopulationStrategy();
                case "tencellrow": return new TenCellRowPopulationStrategy();
                case "gospergun": return new GosperGliderGunPopulationStrategy();
                default: return new DefaultGridPopulationStrategy();
            }
        }
    }
}
