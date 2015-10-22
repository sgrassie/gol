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

            for (var yy = 0; yy < y; yy++)
            {
                for (var xx = 0; xx < x; xx++)
                {
                    var cell = new Cell(xx, yy) { Alive = AliveOrDead(xx, yy) };
                    grid[xx, yy] = cell;
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

            for (var yy = 0; yy < y; yy++)
            {
                for (var xx = 0; xx < x; xx++)
                {
                    var cell = new Cell(xx, yy) { Alive = false };
                    grid[xx, yy] = cell;
                }
            }

            CreateGlider(grid);

            return grid;
        }

        private void CreateGlider(Cell[,] grid)
        {
            grid[2, 2].Alive = true;
            grid[3, 3].Alive = true;
            grid[1, 4].Alive = true;
            grid[2, 4].Alive = true;
            grid[3, 4].Alive = true;
        }
    }

    public class TenCellRowPopulationStrategy : IGridPopulationStrategy
    {
        public Cell[,] Populate(int x, int y)
        {
            var grid = new Cell[x, y];

            for (var yy = 0; yy < y; yy++)
            {
                for (var xx = 0; xx < x; xx++)
                {
                    var cell = new Cell(xx, yy) { Alive = false };
                    grid[xx, yy] = cell;
                }
            }

            CreateRow(grid);

            return grid;
        }

        private void CreateRow(Cell[,] grid)
        {
            grid[10,10].Alive = true;
            grid[11,10].Alive = true;
            grid[12,10].Alive = true;
            grid[13,10].Alive = true;
            grid[14,10].Alive = true;
            grid[15,10].Alive = true;
            grid[16,10].Alive = true;
            grid[17,10].Alive = true;
            grid[18,10].Alive = true;
            grid[19,10].Alive = true;
        }
    }

    public class GosperGliderGunPopulationStrategy : IGridPopulationStrategy
    {
        public Cell[,] Populate(int x, int y)
        {
            var grid = new Cell[x, y];

            for (var yy = 0; yy < y; yy++)
            {
                for (var xx = 0; xx < x; xx++)
                {
                    var cell = new Cell(xx, yy) { Alive = false };
                    grid[xx, yy] = cell;
                }
            }

            Create(grid);

            return grid;
        }

        private void Create(Cell[,] grid)
        {
            grid[2, 10].Alive = true;
            grid[2, 11].Alive = true;
            grid[3, 10].Alive = true;
            grid[3, 11].Alive = true;

            grid[11, 10].Alive = true;
            grid[12, 10].Alive = true;
            grid[10, 11].Alive = true;
            grid[10, 12].Alive = true;
            grid[12, 11].Alive = true;
            grid[11, 12].Alive = true;

            grid[18, 12].Alive = true;
            grid[18, 13].Alive = true;
            grid[18, 14].Alive = true;
            grid[19, 12].Alive = true;
            grid[20, 13].Alive = true;

            grid[24, 9].Alive = true;
            grid[24, 10].Alive = true;
            grid[25, 8].Alive = true;
            grid[25, 10].Alive = true;
            grid[26, 8].Alive = true;
            grid[26, 9].Alive = true;

            grid[26, 20].Alive = true;
            grid[26, 21].Alive = true;
            grid[27, 20].Alive = true;
            grid[27, 22].Alive = true;
            grid[28, 20].Alive = true;

            grid[36, 8].Alive = true;
            grid[36, 9].Alive = true;
            grid[37, 8].Alive = true;
            grid[37, 9].Alive = true;

            grid[37, 15].Alive = true;
            grid[37, 16].Alive = true;
            grid[37, 17].Alive = true;
            grid[38, 15].Alive = true;
            grid[39, 16].Alive = true;
        }
    }
}