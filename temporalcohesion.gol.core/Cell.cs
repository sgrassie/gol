using System.Collections.Generic;

namespace temporalcohesion.gol.core
{
    public class Cell
    {
        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }

        public bool Alive { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public List<Cell> GetNeighbours(Cell[,] grid)
        {
            var list = new List<Cell>();

            for (var i = X - 1; i < X + 2; i++)
            {
                for (var j = Y - 1; j < Y + 2; j++)
                {
                    if (i < 0) continue;
                    if (j < 0) continue;
                    if (i == grid.Rank + 1) continue;
                    if (j == grid.Rank + 1) continue;

                    if (!(i == X && j == Y))
                    {
                        list.Add(grid[i, j]);
                    }
                }
            }

            return list;
        }

        public override string ToString()
        {
            return string.Format("X: {0}, Y: {1}, {2}", X, Y, Alive ? "Alive": "Dead");
        }
    }
}