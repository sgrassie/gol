using System.Linq;
using System.Text;

namespace temporalcohesion.gol.core
{
    public class Life
    {
        public Life(int x, int y, IGridPopulationStrategy gridPopulationStrategy)
        {
            Grid = gridPopulationStrategy.Populate(x, y);
        }

        public Cell[,] Grid { get; set; }

        public void Tick()
        {
            var nextGrid = new Cell[Grid.GetUpperBound(0)+1, Grid.GetUpperBound(1)+1];

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

        private Cell[,] CreateCopy(Cell[,] grid)
        {
            var copy= new Cell[Grid.GetUpperBound(0)+1, Grid.GetUpperBound(1)+1];

            foreach (var cell in grid)
            {
                copy[cell.X, cell.Y] = new Cell(cell.X, cell.Y) { Alive = cell.Alive };
            }

            return copy;
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