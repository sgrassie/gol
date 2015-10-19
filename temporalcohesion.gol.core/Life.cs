using System;

namespace temporalcohesion.gol.core
{
    public class Life
    {
        private readonly int _seed;

        public Life(int seed, int size)
        {
            _seed = seed;
            PopulateGrid(size);
        }

        public Cell[,] Grid { get; set; }

        private void PopulateGrid(int size)
        {
            Grid = new Cell[size, size];

            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    var cell = new Cell(i, j, Grid);
                    cell.Alive = AliveOrDead(i, j);
                    Grid[i, j] = cell;
                }
            }
        }

        private bool AliveOrDead(int x, int y)
        {
            var random = new Random(_seed);

            return ((x*y + random.Next())%2) == 0;
        }
    }
}