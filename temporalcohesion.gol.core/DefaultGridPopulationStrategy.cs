using System;

namespace temporalcohesion.gol.core
{
    public interface IGridPopulationStrategy
    {
        Cell[,] Populate(int x, int y);
    }

    public class DefaultGridPopulationStrategy : IGridPopulationStrategy
    {
        private readonly int _seed;

        public DefaultGridPopulationStrategy(int seed)
        {
            _seed = seed;
        }

        public Cell[,] Populate(int x, int y)
        {
            var grid = new Cell[x, y];

            for (var i = 0; i < x; i++)
            {
                for (var j = 0; j < y; j++)
                {
                    var cell = new Cell(j, i) { Alive = AliveOrDead(j, i) };
                    grid[j, i] = cell;
                }
            }

            return grid;
        }

        private bool AliveOrDead(int x, int y)
        {
            var random = new Random(_seed);

            return (((x ^ y) + random.Next()) % 5) == 0;
        }

    }
}