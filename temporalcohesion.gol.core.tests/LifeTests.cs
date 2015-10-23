using System;
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
        public void Should_Create_Cells_Which_Are_And_Dead()
        {
            var life = new Life(3, 3, gridPopulationStrategy);

            var list = life.Grid.Cast<bool>().ToList();

            list.Count(x => x).Should().BeGreaterThan(0);
            list.Count(x => !x).Should().BeGreaterThan(0);
        }

        [Test]
        public void Should_Evaluate_First_Rule_Of_Life()
        {
            var life = new Life(3, 3, gridPopulationStrategy);
            life.Grid[0, 0] = true;
            life.Grid[0, 1] = true;
            life.Grid[0, 2] = false;
            life.Grid[1, 0] = false;
            life.Grid[1, 1] = false;
            life.Grid[1, 2] = false;
            life.Grid[2, 1] = false;
            life.Grid[2, 1] = false;
            life.Grid[2, 2] = false;

            life.Tick();

            life.Grid[0, 0].Should().BeFalse();
        }

        [Test]
        public void Should_Evaluate_Second_Rule_Of_Life()
        {
            var life = new Life(3, 3, gridPopulationStrategy);
            life.Grid[0, 0] = true;
            life.Grid[0, 1] = true;
            life.Grid[0, 2] = true;
            life.Grid[1, 0] = true;
            life.Grid[1, 1] = true;
            life.Grid[1, 2] = false;
            life.Grid[2, 0] = false;
            life.Grid[2, 1] = false;
            life.Grid[2, 2] = false;

            life.Tick();

            life.Grid[0, 1].Should().BeFalse();

        }

        [Test]
        public void Should_Evaluate_Third_Rule_Of_Life_For_Two_Neighbours()
        {
            var life = new Life(3, 3, gridPopulationStrategy);
            life.Grid[0, 0] = true;
            life.Grid[0, 1] = true;
            life.Grid[0, 2] = false;
            life.Grid[1, 0] = true;
            life.Grid[1, 1] = false;
            life.Grid[1, 2] = false;
            life.Grid[2, 0] = false;
            life.Grid[2, 1] = false;
            life.Grid[2, 2] = false;

            life.Tick();

            life.Grid[0, 1].Should().BeTrue();
        }

        [Test]
        public void Should_Evaluate_Third_Rule_Of_Life_For_Three_Neighbours()
        {
            var life = new Life(3, 3, gridPopulationStrategy);
            life.Grid[0, 0] = true;
            life.Grid[0, 1] = true;
            life.Grid[0, 2] = true;
            life.Grid[1, 0] = true;
            life.Grid[1, 1] = false;
            life.Grid[1, 2] = false;
            life.Grid[2, 0] = false;
            life.Grid[2, 1] = false;
            life.Grid[2, 2] = false;

            life.Tick();

            life.Grid[0, 1].Should().BeTrue();
        }

        [Test]
        public void Should_Evaluate_Fourth_Rule_Of_Life_As_Blinker()
        {
            var life = new Life(3, 3, gridPopulationStrategy);
            life.Grid[0, 0] = false;
            life.Grid[0, 1] = false;
            life.Grid[0, 2] = false;
            life.Grid[1, 0] = true;
            life.Grid[1, 1] = true;
            life.Grid[1, 2] = true;
            life.Grid[2, 0] = false;
            life.Grid[2, 1] = false;
            life.Grid[2, 2] = false;

            life.Tick();

            life.Grid[0, 0].Should().BeFalse();
            life.Grid[0, 1].Should().BeTrue();
            life.Grid[0, 2].Should().BeFalse();
            life.Grid[1, 0].Should().BeFalse();
            life.Grid[1, 1].Should().BeTrue();
            life.Grid[1, 2].Should().BeFalse();
            life.Grid[2, 0].Should().BeFalse();
            life.Grid[2, 1].Should().BeTrue();
            life.Grid[2, 2].Should().BeFalse();

            life.Tick();

            life.Grid[0, 0].Should().BeFalse();
            life.Grid[0, 1].Should().BeFalse();
            life.Grid[0, 2].Should().BeFalse();
            life.Grid[1, 0].Should().BeTrue();
            life.Grid[1, 1].Should().BeTrue();
            life.Grid[1, 2].Should().BeTrue();
            life.Grid[2, 0].Should().BeFalse();
            life.Grid[2, 1].Should().BeFalse();
            life.Grid[2, 2].Should().BeFalse();

            life.Tick();

            life.Grid[0, 0].Should().BeFalse();
            life.Grid[0, 1].Should().BeTrue();
            life.Grid[0, 2].Should().BeFalse();
            life.Grid[1, 0].Should().BeFalse();
            life.Grid[1, 1].Should().BeTrue();
            life.Grid[1, 2].Should().BeFalse();
            life.Grid[2, 0].Should().BeFalse();
            life.Grid[2, 1].Should().BeTrue();
            life.Grid[2, 2].Should().BeFalse();
        }

        [Test]
        public void Should_Adhere_To_Life_Rules_Over_Two_Ticks()
        {
            var life = new Life(3, 3, gridPopulationStrategy);
            life.Grid[0, 0] = true;
            life.Grid[0, 1] = false;
            life.Grid[0, 2] = false;
            life.Grid[1, 0] = true;
            life.Grid[1, 1] = true;
            life.Grid[1, 2] = false;
            life.Grid[2, 0] = true;
            life.Grid[2, 1] = true;
            life.Grid[2, 2] = false;
            
            life.Tick();
            life.Tick();

            life.Grid[1, 0].Should().BeFalse();
            life.Grid[1, 1].Should().BeFalse();
        }

        [Test]
        public void Bug_Block_Of_Six_Should_Move()
        {
            var life = new Life(5, 5, gridPopulationStrategy);
            life.Grid[0, 0] = false;
            life.Grid[0, 1] = false;
            life.Grid[0, 2] = false;
            life.Grid[0, 3] = false;
            life.Grid[0, 4] = false;
            life.Grid[1, 0] = false;
            life.Grid[1, 1] = true;
            life.Grid[1, 2] = true;
            life.Grid[1, 3] = false;
            life.Grid[1, 4] = false;
            life.Grid[2, 0] = false;
            life.Grid[2, 1] = true;
            life.Grid[2, 2] = true;
            life.Grid[2, 3] = false;
            life.Grid[2, 4] = false;
            life.Grid[3, 0] = false;
            life.Grid[3, 1] = true;
            life.Grid[3, 2] = true;
            life.Grid[3, 3] = false;
            life.Grid[3, 4] = false;
            life.Grid[4, 0] = false;
            life.Grid[4, 1] = false;
            life.Grid[4, 2] = false;
            life.Grid[4, 3] = false;
            life.Grid[4, 4] = false;

            life.Tick();

            life.Grid[2, 0].Should().BeTrue();
            life.Grid[2, 1].Should().BeFalse();
        }

        [Test]
        public void Bug_Should_Create_Grid_For_Rectangular_Dimensions_Without_Crashing()
        {
            Action create = () => new Life(50, 25, gridPopulationStrategy);

            create.ShouldNotThrow<Exception>();
        }
    }
}