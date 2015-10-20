using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace temporalcohesion.gol.core.tests
{
    [TestFixture]
    public class LifeTests
    {
        private IGridPopulationStrategy gridPopulationStrategy;
        
        [SetUp]
        public void Setup()
        {
            gridPopulationStrategy = new DefaultGridPopulationStrategy(32);
        }

        [Test]
        public void Should_Initialise_Life_Wtih_Default_HundredByHundred_Grid()
        {
            var life = new Life(100, 100, gridPopulationStrategy);

            life.Grid.Length.Should().Be(10000);
        }

        [Test]
        public void Should_Initialise_Grid_With_Custom_Size()
        {
            var life = new Life(10, 10, gridPopulationStrategy);

            life.Grid.Length.Should().Be(100);
        }

        [Test]
        public void Should_Initialise_All_Cells_In_Grid()
        {
            var life = new Life(2, 2, gridPopulationStrategy);

            var grid = life.Grid;

            grid[0, 0].Should().NotBeNull();
            grid[0, 1].Should().NotBeNull();
            grid[1, 0].Should().NotBeNull();
            grid[1, 1].Should().NotBeNull();
        }

        [Test]
        public void Should_Create_Cell_Which_Knows_Its_Position()
        {
            var life = new Life(2, 2, gridPopulationStrategy);

            var cell = life.Grid[1, 1];

            cell.X.Should().Be(1);
            cell.Y.Should().Be(1);
        }

        [Test]
        public void Should_Create_Cell_Which_Knows_Its_Neighbours()
        {
            // 3 x 3 grid of 9 cells
            var life = new Life(3, 3, gridPopulationStrategy);

            // the center cell
            var cell = life.Grid[1, 1];

            var neighbours = cell.GetNeighbours(life.Grid);
            neighbours.Count.Should().Be(8);
        }

        [Test]
        public void Should_Get_Cell_Neighbours_In_FiveByFiveGrid()
        {
            var life = new Life(5, 5, gridPopulationStrategy);

            var cell = life.Grid[2, 1];
            var neighbours = cell.GetNeighbours(life.Grid);

            neighbours.Count.Should().Be(8);
            neighbours.Should().Contain(c => c.X == 1 && c.Y == 0);
            neighbours.Should().Contain(c => c.X == 1 && c.Y == 1);
            neighbours.Should().Contain(c => c.X == 1 && c.Y == 2);
            neighbours.Should().Contain(c => c.X == 2 && c.Y == 0);
            neighbours.Should().Contain(c => c.X == 2 && c.Y == 2);
            neighbours.Should().Contain(c => c.X == 3 && c.Y == 0);
            neighbours.Should().Contain(c => c.X == 3 && c.Y == 1);
            neighbours.Should().Contain(c => c.X == 3 && c.Y == 2);
        }

        [Test]
        public void Should_Get_Cell_Neighbours_Near_Edge_In_FiveByFiveGrid()
        {
            var life = new Life(5, 5, gridPopulationStrategy);

            var cell = life.Grid[2, 0];
            var neighbours = cell.GetNeighbours(life.Grid);

            neighbours.Count.Should().Be(5);
            neighbours.Should().Contain(c => c.X == 1 && c.Y == 0);
            neighbours.Should().Contain(c => c.X == 1 && c.Y == 1);
            neighbours.Should().Contain(c => c.X == 2 && c.Y == 1);
            neighbours.Should().Contain(c => c.X == 3 && c.Y == 0);
            neighbours.Should().Contain(c => c.X == 3 && c.Y == 1);
        }

        [Test]
        public void Should_Create_Top_Left_Corner_Cell_Which_Knows_Its_Neighbours()
        {
            // 3 x 3 grid of 9 cells
            var life = new Life(3, 3, gridPopulationStrategy);

            // the top left corner cell
            var cell = life.Grid[0, 0];
            
            cell.GetNeighbours(life.Grid).Count.Should().Be(3);
        }

        [Test]
        public void Should_Create_Bottom_Right_Corner_Cell_Which_Knows_Its_Neighbours()
        {
            // 3 x 3 grid of 9 cells
            var life = new Life(3, 3, gridPopulationStrategy);

            // the bottm right corner cell
            var cell = life.Grid[2, 2];
            
            cell.GetNeighbours(life.Grid).Count.Should().Be(3);
        }

        [Test]
        public void Should_Create_Edge_Cell_Which_Knows_Its_Neighbours()
        {
            // 3 x 3 grid of 9 cells
            var life = new Life(3, 3, gridPopulationStrategy);

            // the middle left cell
            var cell = life.Grid[0, 1];
            var neighbours = cell.GetNeighbours(life.Grid);

            neighbours.Count.Should().Be(5);
            neighbours.Should().Contain(c => c.X == 0 && c.Y == 0);
            neighbours.Should().Contain(c => c.X == 1 && c.Y == 0);
            neighbours.Should().Contain(c => c.X == 1 && c.Y == 1);
            neighbours.Should().Contain(c => c.X == 0 && c.Y == 2);
            neighbours.Should().Contain(c => c.X == 1 && c.Y == 2);
        }

        [Test]
        public void Should_Create_Cells_Which_Are_Alive_And_Dead()
        {
            var life = new Life(3, 3, gridPopulationStrategy);

            var list = life.Grid.Cast<Cell>().ToList();

            list.Count(x => x.Alive).Should().BeGreaterThan(0);
            list.Count(x => !x.Alive).Should().BeGreaterThan(0);
        }

        [Test]
        public void Should_Evaluate_First_Rule_Of_Life()
        {
            var life = new Life(3, 3, gridPopulationStrategy);
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
            var life = new Life(3, 3, gridPopulationStrategy);
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
            var life = new Life(3, 3, gridPopulationStrategy);
            life.Grid[0, 0].Alive = true;
            life.Grid[0, 1].Alive = true;
            life.Grid[0, 2].Alive = false;
            life.Grid[1, 0].Alive = true;
            life.Grid[1, 1].Alive = false;
            life.Grid[1, 2].Alive = false;
            life.Grid[2, 0].Alive = false;
            life.Grid[2, 1].Alive = false;
            life.Grid[2, 2].Alive = false;

            life.Tick();

            life.Grid[0, 1].Alive.Should().BeTrue();
        }

        [Test]
        public void Should_Evaluate_Third_Rule_Of_Life_For_Three_Alive_Neighbours()
        {
            var life = new Life(3, 3, gridPopulationStrategy);
            life.Grid[0, 0].Alive = true;
            life.Grid[0, 1].Alive = true;
            life.Grid[0, 2].Alive = true;
            life.Grid[1, 0].Alive = true;
            life.Grid[1, 1].Alive = false;
            life.Grid[1, 2].Alive = false;
            life.Grid[2, 0].Alive = false;
            life.Grid[2, 1].Alive = false;
            life.Grid[2, 2].Alive = false;

            life.Tick();

            life.Grid[0, 1].Alive.Should().BeTrue();
        }

        [Test]
        public void Should_Evaluate_Fourth_Rule_Of_Life_As_Blinker()
        {
            var life = new Life(3, 3, gridPopulationStrategy);
            life.Grid[0, 0].Alive = false;
            life.Grid[0, 1].Alive = false;
            life.Grid[0, 2].Alive = false;
            life.Grid[1, 0].Alive = true;
            life.Grid[1, 1].Alive = true;
            life.Grid[1, 2].Alive = true;
            life.Grid[2, 0].Alive = false;
            life.Grid[2, 1].Alive = false;
            life.Grid[2, 2].Alive = false;

            life.Tick();

            life.Grid[0, 0].Alive.Should().BeFalse();
            life.Grid[0, 1].Alive.Should().BeTrue();
            life.Grid[0, 2].Alive.Should().BeFalse();
            life.Grid[1, 0].Alive.Should().BeFalse();
            life.Grid[1, 1].Alive.Should().BeTrue();
            life.Grid[1, 2].Alive.Should().BeFalse();
            life.Grid[2, 0].Alive.Should().BeFalse();
            life.Grid[2, 1].Alive.Should().BeTrue();
            life.Grid[2, 2].Alive.Should().BeFalse();

            life.Tick();

            life.Grid[0, 0].Alive.Should().BeFalse();
            life.Grid[0, 1].Alive.Should().BeFalse();
            life.Grid[0, 2].Alive.Should().BeFalse();
            life.Grid[1, 0].Alive.Should().BeTrue();
            life.Grid[1, 1].Alive.Should().BeTrue();
            life.Grid[1, 2].Alive.Should().BeTrue();
            life.Grid[2, 0].Alive.Should().BeFalse();
            life.Grid[2, 1].Alive.Should().BeFalse();
            life.Grid[2, 2].Alive.Should().BeFalse();

            life.Tick();

            life.Grid[0, 0].Alive.Should().BeFalse();
            life.Grid[0, 1].Alive.Should().BeTrue();
            life.Grid[0, 2].Alive.Should().BeFalse();
            life.Grid[1, 0].Alive.Should().BeFalse();
            life.Grid[1, 1].Alive.Should().BeTrue();
            life.Grid[1, 2].Alive.Should().BeFalse();
            life.Grid[2, 0].Alive.Should().BeFalse();
            life.Grid[2, 1].Alive.Should().BeTrue();
            life.Grid[2, 2].Alive.Should().BeFalse();
        }

        [Test]
        public void Should_Adhere_To_Life_Rules_Over_Two_Ticks()
        {
            var life = new Life(3, 3, gridPopulationStrategy);
            life.Grid[0, 0].Alive = true;
            life.Grid[0, 1].Alive = false;
            life.Grid[0, 2].Alive = false;
            life.Grid[1, 0].Alive = true;
            life.Grid[1, 1].Alive = true;
            life.Grid[1, 2].Alive = false;
            life.Grid[2, 0].Alive = true;
            life.Grid[2, 1].Alive = true;
            life.Grid[2, 2].Alive = false;
            
            life.Tick();
            life.Tick();

            life.Grid[1, 0].Alive.Should().BeFalse();
            life.Grid[1, 1].Alive.Should().BeFalse();
        }

        [Test]
        public void Bug_Block_Of_Six_Should_Move()
        {
            var life = new Life(5, 5, gridPopulationStrategy);
            life.Grid[0, 0].Alive = false;
            life.Grid[0, 1].Alive = false;
            life.Grid[0, 2].Alive = false;
            life.Grid[0, 3].Alive = false;
            life.Grid[0, 4].Alive = false;
            life.Grid[1, 0].Alive = false;
            life.Grid[1, 1].Alive = true;
            life.Grid[1, 2].Alive = true;
            life.Grid[1, 3].Alive = false;
            life.Grid[1, 4].Alive = false;
            life.Grid[2, 0].Alive = false;
            life.Grid[2, 1].Alive = true;
            life.Grid[2, 2].Alive = true;
            life.Grid[2, 3].Alive = false;
            life.Grid[2, 4].Alive = false;
            life.Grid[3, 0].Alive = false;
            life.Grid[3, 1].Alive = true;
            life.Grid[3, 2].Alive = true;
            life.Grid[3, 3].Alive = false;
            life.Grid[3, 4].Alive = false;
            life.Grid[4, 0].Alive = false;
            life.Grid[4, 1].Alive = false;
            life.Grid[4, 2].Alive = false;
            life.Grid[4, 3].Alive = false;
            life.Grid[4, 4].Alive = false;

            life.Tick();

            life.Grid[2, 0].Alive.Should().BeTrue();
            life.Grid[2, 1].Alive.Should().BeFalse();
        }
    }
}