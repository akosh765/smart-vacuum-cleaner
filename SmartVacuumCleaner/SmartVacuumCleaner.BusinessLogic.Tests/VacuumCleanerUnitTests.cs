using NUnit.Framework;
using SmartVacuumCleaner.BusinessLogic.Interfaces;
using SmartVacuumCleaner.Repository.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartVacuumCleaner.BusinessLogic.Tests
{
    [TestFixture]
    public class VacuumCleanerUnitTests
    {
        public IVacuumCleaner vacuumCleaner;

        [SetUp]
        public void SetUpVacuumCleaner()
        {
            vacuumCleaner = new VacuumCleaner();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {

        }

        [TearDown]
        public void TearDown()
        {

        }

        [Test]
        [Category("ConstructorTest")]
        public void ConstructWithParameters_SuccessfullyConstructed(
            [Values()] Orientation orientation,
            [Range(-2, 2, 1)] int x,
            [Range(-2, 2, 1)] int y)
        {
            //Arrange
            Coordinate startingCoordinate = new Coordinate()
            { X = x,
              Y = y
            };

            //Act
            vacuumCleaner = new VacuumCleaner(startingCoordinate, orientation);

            //Assert
            Assert.AreEqual(vacuumCleaner.Position.X, x);
            Assert.AreEqual(vacuumCleaner.Position.Y, y);
            Assert.AreEqual(vacuumCleaner.Orientation, orientation);
        }

        [Category("MovementTest")]
        [TestCase(Orientation.Upward, -1, 0)]
        [TestCase(Orientation.Downward, 1, 0)]
        [TestCase(Orientation.Left, 0, -1)]
        [TestCase(Orientation.Right, 0, 1)]
        public void GetDisplacementValues_GetsDisplacementValuesAccordingToOrientation(Orientation orientation, int expectedX, int expectedY)
        {
            //Arrange
            //Act
            Coordinate actualDisplacement = (vacuumCleaner as VacuumCleaner)
                .GetDisplacementValuesAccordingToOrientation(orientation);

            //Assert
            Assert.AreEqual(actualDisplacement.X, expectedX);
            Assert.AreEqual(actualDisplacement.Y, expectedY);
        }


        [Category("MovementTest")]
        [TestCase(Orientation.Upward, 0, 0, -1, 0)]
        [TestCase(Orientation.Downward, 0, 0, 1, 0)]
        [TestCase(Orientation.Left, 0, 0, 0, -1)]
        [TestCase(Orientation.Right, 0, 0, 0, 1)]
        public void Step_TakesStepAccordingToOrientation(Orientation orientation, int startX, int startY, int destX, int destY)
        {
            //Arrange
            vacuumCleaner.Orientation = orientation;
            vacuumCleaner.Position = new Coordinate(startX, startY);

            //Act
            vacuumCleaner.Step();

            //Assert
            Assert.AreEqual(vacuumCleaner.Orientation, orientation);
            Assert.AreEqual(vacuumCleaner.Position.X, destX);
            Assert.AreEqual(vacuumCleaner.Position.Y, destY);
        }


        [Category("OrientationTest")]
        [TestCase(Orientation.Upward, Orientation.Right)]
        [TestCase(Orientation.Right, Orientation.Downward)]
        [TestCase(Orientation.Downward, Orientation.Left)]
        [TestCase(Orientation.Left, Orientation.Upward)]
        public void TurnClockwise_OrientationChangesCorrectly(Orientation currentOrientation, Orientation expectedOrientation)
        {
            //Arrange
            vacuumCleaner.Orientation = currentOrientation;

            //Act
            vacuumCleaner.TurnClockwise();
            Orientation actualOrientation = vacuumCleaner.Orientation;

            //Assert
            Assert.AreEqual(actualOrientation, expectedOrientation);
        }

        [Category("OrientationTest")]
        [TestCase(Orientation.Upward, Orientation.Left)]
        [TestCase(Orientation.Left, Orientation.Downward)]
        [TestCase(Orientation.Downward, Orientation.Right)]
        [TestCase(Orientation.Right, Orientation.Upward)]
        public void TurnCounterClockwise_OrientationChangesCorrectly(Orientation currentOrientation, Orientation expectedOrientation)
        {
            //Arrange
            vacuumCleaner.Orientation = currentOrientation;

            //Act
            vacuumCleaner.TurnCounterClockwise();
            Orientation actualOrientation = vacuumCleaner.Orientation;

            //Assert
            Assert.AreEqual(actualOrientation, expectedOrientation);
        }
    }
}
