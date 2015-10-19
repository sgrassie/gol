namespace temporalcohesion.gol.core
{
    public class Life
    {
        public Life(int size = 100)
        {
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
                    Grid[i, j] = cell;
                }
            }
        }
    }
}