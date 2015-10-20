using System;
using System.Linq;
using System.Text;

namespace temporalcohesion.gol.core
{
    public class Life
    {
        private readonly int _seed;
        private readonly int _size;

        public Life(int seed, int size)
        {
            _seed = seed;
            _size = size;
            PopulateGrid(size);
        }

        public Cell[,] Grid { get; set; }

        public void Tick()
        {
            var nextGrid = new Cell[_size, _size];

            foreach (var cell in Grid)
            {
                var copy = CreateCopy(Grid);
                var cellCopy = copy[cell.X, cell.Y];
                var neighbours = copy[cellCopy.X, cellCopy.Y].GetNeighbours(copy);

                if (neighbours.Count(x => x.Alive) < 2)
                {
                    cellCopy.Alive = false;
                }
                else if (neighbours.Count(x => x.Alive) > 3)
                {
                    cellCopy.Alive = false;
                }
                else if (neighbours.Count(x => x.Alive) == 3)
                {
                    cellCopy.Alive = true;
                }

                nextGrid[cellCopy.X, cellCopy.Y] = cellCopy;
            }

            Grid = nextGrid;
        }

        private void PopulateGrid(int size)
        {
            Grid = new Cell[size, size];

            for (var i = 0; i < size; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    var cell = new Cell(j, i) {Alive = AliveOrDead(j, i)};
                    Grid[j, i] = cell;
                }
            }
        }

        private Cell[,] CreateCopy(Cell[,] grid)
        {
            var copy = new Cell[_size, _size];

            foreach (var cell in grid)
            {
                copy[cell.X, cell.Y] = new Cell(cell.X, cell.Y) { Alive = cell.Alive };
            }

            return copy;
        }

        private bool AliveOrDead(int x, int y)
        {
            var random = new Random(_seed);

            return (((x ^ y) + random.Next()) % 5) == 0;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            var xBounds = Grid.GetUpperBound(0);
            var yBounds = Grid.GetUpperBound(1);

            for (var i = 0; i <= xBounds; i++)
            {
                for (var j = 0; j <= yBounds; j++)
                {
                    var cell = Grid[j, i];
                    sb.AppendFormat("{0} ", cell.Alive ? "+" : ".");
                    //sb.AppendFormat("|{0},{1}", cell.X, cell.Y);
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}