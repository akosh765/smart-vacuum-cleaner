namespace SmartVacuumCleaner.BusinessLogic.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using NUnit.Framework;
    using SmartVacuumCleaner.BusinessLogic.Interfaces;
    using SmartVacuumCleaner.Repository.Utils;

    /// <summary>
    /// Test class that provides Unit tests for the functionality of the VacuumCleaner Class.
    /// </summary>
    [TestFixture]
    public class VacuumCleanerUnitTests
    {
        private IVacuumCleaner vacuumCleaner;

        /// <summary>
        /// Sets up the vacuumCleaner instance before each test.
        /// </summary>
        [SetUp]
        public void SetUpVacuumCleaner()
        {
            this.vacuumCleaner = new VacuumCleaner();
        }

        /// <summary>
        /// Sets up the instances.
        /// </summary>
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
        }

        /// <summary>
        /// Tears down all resources.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
        }

        /// <summary>
        /// Tests the constructor of the vacuum cleaner.
        /// </summary>
        /// <param name="orientation">Orientation value.</param>
        /// <param name="x">X value.</param>
        /// <param name="y">Y value.</param>
        [Test]
        [Category("ConstructorTest")]
        public void ConstructWithParameters_SuccessfullyConstructed(
            [Values(Orientation.Right, Orientation.Upward)] Orientation orientation,
            [Range(-2, 2, 1)] int x,
            [Range(-2, 2, 1)] int y)
        {
            // Arrange
            Coordinate startingCoordinate = new Coordinate()
            { X = x,
              Y = y,
            };

            // Act
            this.vacuumCleaner = new VacuumCleaner(startingCoordinate, orientation);

            // Assert
            Assert.AreEqual(this.vacuumCleaner.Position.X, x);
            Assert.AreEqual(this.vacuumCleaner.Position.Y, y);
            Assert.AreEqual(this.vacuumCleaner.Orientation, orientation);
        }

        /// <summary>
        /// Tests the displacement values of the vacuum cleaner.
        /// </summary>
        /// <param name="orientation">Orientation.</param>
        /// <param name="expectedX">Expected result X coordinate.</param>
        /// <param name="expectedY">Expected result Y coordinate.</param>
        [Category("MovementTest")]
        [TestCase(Orientation.Upward, -1, 0)]
        [TestCase(Orientation.Downward, 1, 0)]
        [TestCase(Orientation.Left, 0, -1)]
        [TestCase(Orientation.Right, 0, 1)]
        public void GetDisplacementValues_GetsDisplacementValuesAccordingToOrientation(Orientation orientation, int expectedX, int expectedY)
        {
            // Arrange
            // Act
            Coordinate actualDisplacement = (this.vacuumCleaner as VacuumCleaner)
                .GetDisplacementValuesAccordingToOrientation(orientation);

            // Assert
            Assert.AreEqual(actualDisplacement.X, expectedX);
            Assert.AreEqual(actualDisplacement.Y, expectedY);
        }

        /// <summary>
        /// Tests the movement of the vacuum cleaner.
        /// </summary>
        /// <param name="orientation">Orientation.</param>
        /// <param name="startX">Start X coordinate.</param>
        /// <param name="startY">Start Y coordinate.</param>
        /// <param name="destX">Destination X coordinate.</param>
        /// <param name="destY">Destination Y coordinate.</param>
        [Category("MovementTest")]
        [TestCase(Orientation.Upward, 0, 0, -1, 0)]
        [TestCase(Orientation.Downward, 0, 0, 1, 0)]
        [TestCase(Orientation.Left, 0, 0, 0, -1)]
        [TestCase(Orientation.Right, 0, 0, 0, 1)]
        public void Step_TakesStepAccordingToOrientation(Orientation orientation, int startX, int startY, int destX, int destY)
        {
            // Arrange
            this.vacuumCleaner.Orientation = orientation;
            this.vacuumCleaner.Position = new Coordinate(startX, startY);

            // Act
            this.vacuumCleaner.Step();

            // Assert
            Assert.AreEqual(this.vacuumCleaner.Orientation, orientation);
            Assert.AreEqual(this.vacuumCleaner.Position.X, destX);
            Assert.AreEqual(this.vacuumCleaner.Position.Y, destY);
        }

        /// <summary>
        /// Tests the change of orientation.
        /// </summary>
        /// <param name="currentOrientation">Current orientation.</param>
        /// <param name="expectedOrientation">Expected result orientation.</param>
        [Category("OrientationTest")]
        [TestCase(Orientation.Upward, Orientation.Right)]
        [TestCase(Orientation.Right, Orientation.Downward)]
        [TestCase(Orientation.Downward, Orientation.Left)]
        [TestCase(Orientation.Left, Orientation.Upward)]
        public void TurnClockwise_OrientationChangesCorrectly(Orientation currentOrientation, Orientation expectedOrientation)
        {
            // Arrange
            this.vacuumCleaner.Orientation = currentOrientation;

            // Act
            this.vacuumCleaner.TurnClockwise();
            Orientation actualOrientation = this.vacuumCleaner.Orientation;

            // Assert
            Assert.AreEqual(actualOrientation, expectedOrientation);
        }

        /// <summary>
        /// Tests the change of orientation.
        /// </summary>
        /// <param name="currentOrientation">Current orientation.</param>
        /// <param name="expectedOrientation">Expected result orientation.</param>
        [Category("OrientationTest")]
        [TestCase(Orientation.Upward, Orientation.Left)]
        [TestCase(Orientation.Left, Orientation.Downward)]
        [TestCase(Orientation.Downward, Orientation.Right)]
        [TestCase(Orientation.Right, Orientation.Upward)]
        public void TurnCounterClockwise_OrientationChangesCorrectly(Orientation currentOrientation, Orientation expectedOrientation)
        {
            // Arrange
            this.vacuumCleaner.Orientation = currentOrientation;

            // Act
            this.vacuumCleaner.TurnCounterClockwise();
            Orientation actualOrientation = this.vacuumCleaner.Orientation;

            // Assert
            Assert.AreEqual(actualOrientation, expectedOrientation);
        }
    }
}
