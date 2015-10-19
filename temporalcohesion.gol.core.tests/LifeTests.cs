using FluentAssertions;
using NUnit.Framework;

namespace temporalcohesion.gol.core.tests
{
    [TestFixture]
    public class LifeTests
    {
        [Test]
        public void Should_Initialise_Life_Wtih_Default_HundredByHundred_Grid()
        {
            var life = new Life();

            life.Grid.Length.Should().Be(10000);
        }

        [Test]
        public void Should_Initialise_Grid_With_Custom_Size()
        {
            var life = new Life(10);

            life.Grid.Length.Should().Be(100);
        }

        [Test]
        public void Should_Initialise_All_Cells_In_Grid()
        {
            var life = new Life(2);

            var grid = life.Grid;

            grid[0, 0].Should().NotBeNull();
            grid[0, 1].Should().NotBeNull();
            grid[1, 0].Should().NotBeNull();
            grid[1, 1].Should().NotBeNull();
        }

        [Test]
        public void Should_Create_Cell_Which_Knows_Its_Position()
        {
            var life = new Life(2);

            var cell = life.Grid[1, 1];

            cell.X.Should().Be(1);
            cell.Y.Should().Be(1);
        }

        [Test]
        public void Should_Create_Cell_Which_Knows_Its_Neighbours()
        {
            // 3 x 3 grid of 9 cells
            var life = new Life(3);

            // the center cell
            var cell = life.Grid[1, 1];

            cell.Neighbours.Count.Should().Be(8);
            cell.Neighbours[0].Should().NotBeNull();
            cell.Neighbours[1].Should().NotBeNull();
            cell.Neighbours[2].Should().NotBeNull();
            cell.Neighbours[3].Should().NotBeNull();
            cell.Neighbours[4].Should().NotBeNull();
            cell.Neighbours[5].Should().NotBeNull();
            cell.Neighbours[6].Should().NotBeNull();
            cell.Neighbours[7].Should().NotBeNull();
        }

        [Test]
        public void Should_Create_Top_Left_Corner_Cell_Which_Knows_Its_Neighbours()
        {
            // 3 x 3 grid of 9 cells
            var life = new Life(3);

            // the top left corner cell
            var cell = life.Grid[0, 0];
            
            cell.Neighbours.Count.Should().Be(3);
        }

        [Test]
        public void Should_Create_Bottom_Right_Corner_Cell_Which_Knows_Its_Neighbours()
        {
            // 3 x 3 grid of 9 cells
            var life = new Life(3);

            // the bottm right corner cell
            var cell = life.Grid[2, 2];
            
            cell.Neighbours.Count.Should().Be(3);
            
        }

        [Test]
        public void Should_Create_Edge_Cell_Which_Knows_Its_Neighbours()
        {
            // 3 x 3 grid of 9 cells
            var life = new Life(3);

            // the middle left cell
            var cell = life.Grid[0, 1];
            
            cell.Neighbours.Count.Should().Be(5);
        }
    }
}