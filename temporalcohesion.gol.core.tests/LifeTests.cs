using System.Linq;
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
            var life = new Life(0, 100);

            life.Grid.Length.Should().Be(10000);
        }

        [Test]
        public void Should_Initialise_Grid_With_Custom_Size()
        {
            var life = new Life(1, 10);

            life.Grid.Length.Should().Be(100);
        }

        [Test]
        public void Should_Initialise_All_Cells_In_Grid()
        {
            var life = new Life(1, 2);

            var grid = life.Grid;

            grid[0, 0].Should().NotBeNull();
            grid[0, 1].Should().NotBeNull();
            grid[1, 0].Should().NotBeNull();
            grid[1, 1].Should().NotBeNull();
        }

        [Test]
        public void Should_Create_Cell_Which_Knows_Its_Position()
        {
            var life = new Life(1, 2);

            var cell = life.Grid[1, 1];

            cell.X.Should().Be(1);
            cell.Y.Should().Be(1);
        }

        [Test]
        public void Should_Create_Cell_Which_Knows_Its_Neighbours()
        {
            // 3 x 3 grid of 9 cells
            var life = new Life(1, 3);

            // the center cell
            var cell = life.Grid[1, 1];

            var neighbours = cell.GetNeighbours(life.Grid);
            neighbours.Count.Should().Be(8);
        }

        [Test]
        public void Should_Create_Top_Left_Corner_Cell_Which_Knows_Its_Neighbours()
        {
            // 3 x 3 grid of 9 cells
            var life = new Life(1, 3);

            // the top left corner cell
            var cell = life.Grid[0, 0];
            
            cell.GetNeighbours(life.Grid).Count.Should().Be(3);
        }

        [Test]
        public void Should_Create_Bottom_Right_Corner_Cell_Which_Knows_Its_Neighbours()
        {
            // 3 x 3 grid of 9 cells
            var life = new Life(1, 3);

            // the bottm right corner cell
            var cell = life.Grid[2, 2];
            
            cell.GetNeighbours(life.Grid).Count.Should().Be(3);
        }

        [Test]
        public void Should_Create_Edge_Cell_Which_Knows_Its_Neighbours()
        {
            // 3 x 3 grid of 9 cells
            var life = new Life(1, 3);

            // the middle left cell
            var cell = life.Grid[0, 1];
            
            cell.GetNeighbours(life.Grid).Count.Should().Be(5);
        }

        [Test]
        public void Should_Create_Cells_Which_Are_Alive_And_Dead()
        {
            var life = new Life(1, 3);

            var list = life.Grid.Cast<Cell>().ToList();

            list.Count(x => x.Alive).Should().BeGreaterThan(0);
            list.Count(x => !x.Alive).Should().BeGreaterThan(0);
        }

        [Test]
        public void Should_Evaluate_First_Rule_Of_Life()
        {
            var life = new Life(1, 3);
            life.Grid[0, 0].Alive = true;
            life.Grid[1, 0].Alive = true;
            life.Grid[0, 1].Alive = false;
            life.Grid[1, 1].Alive = false;

            life.Tick();

            life.Grid[0, 0].Alive.Should().BeFalse();
        }

        [Test]
        public void Should_Evaluate_Second_Rule_Of_Life()
        {
            var life = new Life(1, 3);
            life.Grid[0, 0].Alive = true;
            life.Grid[0, 1].Alive = true;
            life.Grid[0, 2].Alive = true;
            life.Grid[1, 0].Alive = true;
            life.Grid[1, 1].Alive = true;

            life.Tick();

            life.Grid[0, 1].Alive.Should().BeFalse();

        }

        [Test]
        public void Should_Evaluate_Third_Rule_Of_Life_For_Two_Alive_Neighbours()
        {
            var life = new Life(1, 3);
            life.Grid[0, 0].Alive = true;
            life.Grid[0, 1].Alive = true;
            life.Grid[0, 2].Alive = false;
            life.Grid[1, 0].Alive = true;
            life.Grid[1, 1].Alive = false;
            life.Grid[1, 2].Alive = false;

            life.Tick();

            life.Grid[0, 1].Alive.Should().BeTrue();
        }

        [Test]
        public void Should_Evaluate_Third_Rule_Of_Life_For_Three_Alive_Neighbours()
        {
            var life = new Life(1, 3);
            life.Grid[0, 0].Alive = true;
            life.Grid[0, 1].Alive = true;
            life.Grid[0, 2].Alive = true;
            life.Grid[1, 0].Alive = true;
            life.Grid[1, 1].Alive = false;
            life.Grid[1, 2].Alive = false;

            life.Tick();

            life.Grid[0, 1].Alive.Should().BeTrue();
        }

        [Test]
        public void Should_Evaluate_Fourth_Rule_Of_Life()
        {
            var life = new Life(1, 3);
            life.Grid[0, 0].Alive = true;
            life.Grid[0, 1].Alive = false;
            life.Grid[0, 2].Alive = true;
            life.Grid[1, 0].Alive = true;
            life.Grid[1, 1].Alive = false;
            life.Grid[1, 2].Alive = false;

            life.Tick();

            life.Grid[0, 1].Alive.Should().BeTrue();
        }
    }
}