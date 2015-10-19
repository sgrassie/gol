using System.Collections.Generic;

namespace temporalcohesion.gol.core
{
    public class Cell
    {
        private readonly Cell[,] _grid ;

        public Cell(int x, int y, Cell[,] grid)
        {
            X = x;
            Y = y;
            _grid = grid;
        }

        public bool Alive { get; set; }
        public int X { get; set; }
        public int Y { get; set; }

        public List<Cell> Neighbours
        {
            get
            {
                var list = new List<Cell>();

                for (var i = X-1; i < X+2; i++)
                {
                    for (var j = Y-1; j < Y+2; j++)
                    {
                        if (i < 0) continue;
                        if (j < 0) continue;
                        if (i == _grid.Rank + 1) continue;
                        if (j == _grid.Rank + 1) continue;

                        if (!(i == X && j == Y))
                        {
                            list.Add(_grid[i, j]);
                        }
                    }
                }

                return list;
            }
        }
    }
}