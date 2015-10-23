using System.Text;

namespace temporalcohesion.gol.core
{
    public class Life
    {
        private static char EMPTY_BLOCK_CHAR = ' ';
        private static char FULL_BLOCK_CHAR = '\u2588';

        public Life(int x, int y, IGridPopulationStrategy gridPopulationStrategy)
        {
            Grid = gridPopulationStrategy.Populate(x, y);
        }

        public bool[,] Grid { get; set; }

        public void Tick()
        {
            var nextGrid = new bool[Grid.GetUpperBound(0)+1, Grid.GetUpperBound(1)+1];

            var xBounds = Grid.GetUpperBound(0);
            var yBounds = Grid.GetUpperBound(1);

            for (var y = 0; y <= yBounds; y++)
            {
                for (var x = 0; x <= xBounds; x++)
                {
                    var count = CountAliveNeighbours(x, y);

                    if (count < 2)
                    {
                        nextGrid[x, y] = false;
                    }
                    else if (count > 3)
                    {
                        nextGrid[x, y] = false;
                    }
                    else if (count == 3)
                    {
                        nextGrid[x, y] = true;
                    }
                    else
                    {
                        nextGrid[x, y] = Grid[x, y];
                    }
                }
            }

            Grid = nextGrid;
        }

        private int CountAliveNeighbours(int x, int y)
        {
            var width = Grid.GetUpperBound(0)+1;
            var height = Grid.GetUpperBound(1)+1;
            var value = 0;

            for (var j = -1; j <= 1; j++)
            {
                if ((y + j) < 0 || y + j >= height)
                {
                    continue;
                }

                var k = (y + j + height) % height;

                for (var i = -1; i <= 1; i++)
                {
                    if (x + i < 0 || x + i >= width)
                    {
                        continue;
                    }

                    var h = (x + i + width) % width;

                    value += Grid[h, k] ? 1 : 0;
                }
            }

            return value - (Grid[x, y] ? 1 : 0);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            var xBounds = Grid.GetUpperBound(0);
            var yBounds = Grid.GetUpperBound(1);

            for (var y = 0; y <= yBounds; y++)
            {
                for (var x = 0; x <= xBounds; x++)
                {
                    var cell = Grid[x, y] ? FULL_BLOCK_CHAR : EMPTY_BLOCK_CHAR;
                    sb.Append(cell);
                    sb.Append(cell);
                }

                sb.AppendLine();
            }

            return sb.ToString();
        }
    }
}