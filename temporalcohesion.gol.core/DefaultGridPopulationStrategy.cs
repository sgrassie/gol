using System;

namespace temporalcohesion.gol.core
{
    public interface IGridPopulationStrategy
    {
        bool[,] Populate(int x, int y);
    }

    public class DefaultGridPopulationStrategy : IGridPopulationStrategy
    {
        private readonly int _seed;

        public DefaultGridPopulationStrategy(int seed)
        {
            _seed = seed;
        }

        public bool[,] Populate(int x, int y)
        {
            var grid = new bool[x, y];

            for (var yy = 0; yy < y; yy++)
            {
                for (var xx = 0; xx < x; xx++)
                {
                    grid[xx, yy] = AliveOrDead(xx, yy);
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

    public class GliderPopulationStrategy : IGridPopulationStrategy
    {
        public bool[,] Populate(int x, int y)
        {
            var grid = new bool[x, y];

            for (var yy = 0; yy < y; yy++)
            {
                for (var xx = 0; xx < x; xx++)
                {
                    grid[xx, yy] = false;
                }
            }

            CreateGlider(grid);

            return grid;
        }

        private void CreateGlider(bool[,] grid)
        {
            grid[2, 2] = true;
            grid[3, 3] = true;
            grid[1, 4] = true;
            grid[2, 4] = true;
            grid[3, 4] = true;
        }
    }

    public class TenCellRowPopulationStrategy : IGridPopulationStrategy
    {
        public bool[,] Populate(int x, int y)
        {
            var grid = new bool[x, y];

            for (var yy = 0; yy < y; yy++)
            {
                for (var xx = 0; xx < x; xx++)
                {
                    grid[xx, yy] = false;
                }
            }

            CreateRow(grid);

            return grid;
        }

        private void CreateRow(bool[,] grid)
        {
            grid[10,10] = true;
            grid[11,10] = true;
            grid[12,10] = true;
            grid[13,10] = true;
            grid[14,10] = true;
            grid[15,10] = true;
            grid[16,10] = true;
            grid[17,10] = true;
            grid[18,10] = true;
            grid[19,10] = true;
        }
    }

    public class GosperGliderGunPopulationStrategy : IGridPopulationStrategy
    {
        public bool[,] Populate(int x, int y)
        {
            var grid = new bool[x, y];

            for (var yy = 0; yy < y; yy++)
            {
                for (var xx = 0; xx < x; xx++)
                {
                    grid[xx, yy] = false;
                }
            }

            Create(grid);

            return grid;
        }

        private void Create(bool[,] grid)
        {
            grid[2, 10] = true;
            grid[2, 11] = true;
            grid[3, 10] = true;
            grid[3, 11] = true;

            grid[11, 10] = true;
            grid[12, 10] = true;
            grid[10, 11] = true;
            grid[10, 12] = true;
            grid[12, 11] = true;
            grid[11, 12] = true;

            grid[18, 12] = true;
            grid[18, 13] = true;
            grid[18, 14] = true;
            grid[19, 12] = true;
            grid[20, 13] = true;

            grid[24, 9] = true;
            grid[24, 10] = true;
            grid[25, 8] = true;
            grid[25, 10] = true;
            grid[26, 8] = true;
            grid[26, 9] = true;

            grid[26, 20] = true;
            grid[26, 21] = true;
            grid[27, 20] = true;
            grid[27, 22] = true;
            grid[28, 20] = true;

            grid[36, 8] = true;
            grid[36, 9] = true;
            grid[37, 8] = true;
            grid[37, 9] = true;

            grid[37, 15] = true;
            grid[37, 16] = true;
            grid[37, 17] = true;
            grid[38, 15] = true;
            grid[39, 16] = true;
        }
    }
}