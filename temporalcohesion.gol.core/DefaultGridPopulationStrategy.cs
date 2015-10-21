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
                    var cell = new Cell(i, j) { Alive = AliveOrDead(i, j) };
                    grid[i, j] = cell;
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
        public Cell[,] Populate(int x, int y)
        {
            var grid = new Cell[x, y];

            for (var i = 0; i < x; i++)
            {
                for (var j = 0; j < y; j++)
                {
                    var cell = new Cell(i, j) { Alive = false };
                    grid[i, j] = cell;
                }
            }

            CreateGlider(grid);

            return grid;
        }

        private void CreateGlider(Cell[,] grid)
        {
            grid[2, 1].Alive = true;
            grid[1, 3].Alive = true;
            grid[2, 3].Alive = true;
            grid[3, 3].Alive = true;
            grid[3, 2].Alive = true;
        }
    }

    public class TenCellRowPopulationStrategy : IGridPopulationStrategy
    {
        public Cell[,] Populate(int x, int y)
        {
            var grid = new Cell[x, y];

            for (var i = 0; i < x; i++)
            {
                for (var j = 0; j < y; j++)
                {
                    var cell = new Cell(i, j) { Alive = false };
                    grid[i, j] = cell;
                }
            }

            CreateRow(grid);

            return grid;
        }

        private void CreateRow(Cell[,] grid)
        {
            grid[10, 10].Alive = true;
            grid[10, 11].Alive = true;
            grid[10, 12].Alive = true;
            grid[10, 13].Alive = true;
            grid[10, 14].Alive = true;
            grid[10, 15].Alive = true;
            grid[10, 16].Alive = true;
            grid[10, 17].Alive = true;
            grid[10, 18].Alive = true;
            grid[10, 19].Alive = true;
        }
    }

    public class GosperGliderGunPopulationStrategy : IGridPopulationStrategy
    {
        public Cell[,] Populate(int x, int y)
        {
            var grid = new Cell[x, y];

            for (var i = 0; i < x; i++)
            {
                for (var j = 0; j < y; j++)
                {
                    var cell = new Cell(i, j) { Alive = false };
                    grid[i, j] = cell;
                }
            }

            Create(grid);

            return grid;
        }

        private void Create(Cell[,] grid)
        {
            grid[9, 1].Alive = true;
            grid[9, 2].Alive = true;
            grid[10, 1].Alive = true;
            grid[10, 2].Alive = true;

            grid[9, 10].Alive = true;
            grid[9, 11].Alive = true;
            grid[10, 9].Alive = true;
            grid[10, 11].Alive = true;
            grid[11, 9].Alive = true;
            grid[11, 10].Alive = true;

            grid[11, 17].Alive = true;
            grid[11, 18].Alive = true;
            grid[12, 17].Alive = true;
            grid[12, 19].Alive = true;
            grid[13, 17].Alive = true;

            grid[7, 24].Alive = true;
            grid[7, 25].Alive = true;
            grid[8, 23].Alive = true;
            grid[8, 25].Alive = true;
            grid[9, 23].Alive = true;
            grid[9, 24].Alive = true;

            grid[19, 25].Alive = true;
            grid[19, 26].Alive = true;
            grid[19, 27].Alive = true;
            grid[20, 25].Alive = true;
            grid[21, 26].Alive = true;

            grid[7, 35].Alive = true;
            grid[7, 36].Alive = true;
            grid[8, 35].Alive = true;
            grid[8, 36].Alive = true;

            grid[14, 36].Alive = true;
            grid[14, 37].Alive = true;
            grid[15, 36].Alive = true;
            grid[15, 38].Alive = true;
            grid[16, 36].Alive = true;
        }
    }
}