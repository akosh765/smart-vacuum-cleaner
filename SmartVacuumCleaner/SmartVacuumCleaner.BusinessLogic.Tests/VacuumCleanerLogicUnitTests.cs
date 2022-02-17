// <copyright file="VacuumCleanerLogicUnitTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SmartVacuumCleaner.BusinessLogic.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Moq;
    using NUnit.Framework;
    using SmartVacuumCleaner.BusinessLogic.Interfaces;
    using SmartVacuumCleaner.Repository;
    using SmartVacuumCleaner.Repository.Utils;

    /// <summary>
    /// Test class that provides Unit tests for the functionality of the VacuumCleanerLogic Class of the Business Logic layer.
    /// </summary>
    [TestFixture]
    public class VacuumCleanerLogicUnitTests
    {
        private RoomTestCases[] roomsToClean = new RoomTestCases[1];
        private IVacuumCleanerLogic vacuumCleanerLogic;
        private Mock<IVacuumCleanerRepository<IRoom>> mockVacuumRepository;

        /// <summary>
        /// Sets up the dependant fields and mocks.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.mockVacuumRepository = new Mock<IVacuumCleanerRepository<IRoom>>();
            this.mockVacuumRepository.Setup(x => x.Room).Returns(new Room());
            this.mockVacuumRepository.Setup(x => x.LoadRoomData(It.IsAny<string>())).Returns(new Room());
        }

        /// <summary>
        /// Tears down all resources.
        /// </summary>
        [TearDown]
        public void Teardown()
        {
        }

        /// <summary>
        /// Unit test for the next position calculation.
        /// </summary>
        /// <param name="orientation">The orientation of the robot.</param>
        /// <param name="startX">The starting X coordinate of the robot.</param>
        /// <param name="startY">The starting Y coordinate of the robot.</param>
        /// <param name="expectedX">The expected X coordinate for the next position.</param>
        /// <param name="expectedY">The expected Y coordinate for the next position.</param>
        [Category("MovementTest")]
        [TestCase(Orientation.Upward, 1, 0, 0, 0)]
        [TestCase(Orientation.Downward, 25, 25, 26, 25)]
        [TestCase(Orientation.Left, 2, -5, 2, -6)]
        [TestCase(Orientation.Right, 123, 67, 123, 68)]
        public void CalculateNextPosition_ReturnsWithCorrectPosition(Orientation orientation, int startX, int startY, int expectedX, int expectedY)
        {
            // Arrange
            this.vacuumCleanerLogic = new VacuumCleanerLogic(mockVacuumRepository.Object);

            VacuumCleaner vacuumCleaner = new VacuumCleaner()
            {
                Position = new Coordinate()
                {
                    X = startX,
                    Y = startY,
                },
                Orientation = orientation,
            };

            // Act
            Coordinate actualNextPosition = this.vacuumCleanerLogic.CalculateNextPosition(vacuumCleaner);

            // Assert
            Assert.AreEqual(actualNextPosition.X, expectedX);
            Assert.AreEqual(actualNextPosition.Y, expectedY);
        }

        /// <summary>
        /// Tests the VacuumFloor function whether it calls the right repository functions.
        /// </summary>
        /// <param name="map">The map to be the robottested on.</param>
        /// <param name="positionX">Starting X position.</param>
        /// <param name="positionY">Starting Y position.</param>
        /// <param name="expectedCountOfCleanTiles">Expected number of tiles cleaned</param>
        [Test]
        [Category("VacuumFloorTest")]
        [TestCaseSource(typeof(RoomTestCases), nameof(RoomTestCases.TestRooms))]
        public void VacuumFloor_CallsGetRoomExactlyOnce(bool[,] map, int positionX, int positionY, int expectedCountOfCleanTiles)
        {
            // Arrange
            this.mockVacuumRepository.Setup(x => x.Room).Returns(new Room() { Map = map });
            this.mockVacuumRepository.Setup(x => x.LoadRoomData(It.IsAny<string>()))
                .Returns(new Room() { Map = map, RobotPosition = new Coordinate() { X = positionX, Y = positionY } });
            this.vacuumCleanerLogic = new VacuumCleanerLogic(this.mockVacuumRepository.Object);

            // Act
            this.vacuumCleanerLogic.VacuumFloor();

            // Assert
            this.mockVacuumRepository.Verify(x => x.LoadRoomData(It.IsAny<string>()), Times.Exactly(1));
        }

        /// <summary>
        /// Tests the VacuumFloor function whether it calls the right repository functions.
        /// </summary>
        /// <param name="map">The map to be the robottested on.</param>
        /// <param name="positionX">Starting X position.</param>
        /// <param name="positionY">Starting Y position.</param>
        /// <param name="expectedCountOfCleanTiles">Expected number of tiles cleaned</param>
        [Test]
        [Category("VacuumFloorTest")]
        [TestCaseSource(typeof(RoomTestCases), nameof(RoomTestCases.TestRooms))]
        public void VacuumFloor_GetsOrSetsRoomAtLeasOnce(bool[,] map, int positionX, int positionY, int expectedCountOfCleanTiles)
        {
            // Arrange
            this.mockVacuumRepository.Setup(x => x.Room).Returns(new Room() { Map = map });
            this.mockVacuumRepository.Setup(x => x.LoadRoomData(It.IsAny<string>()))
                .Returns(new Room() { Map = map, RobotPosition = new Coordinate() { X = positionX, Y = positionY } });
            this.vacuumCleanerLogic = new VacuumCleanerLogic(this.mockVacuumRepository.Object);

            // Act
            this.vacuumCleanerLogic.VacuumFloor();

            // Assert
            this.mockVacuumRepository.Verify(x => x.Room, Times.AtLeastOnce());
        }

        /// <summary>
        /// Tests the VacuumFloor function whether it calls the right repository functions.
        /// </summary>
        /// <param name="map">The map to be the robottested on.</param>
        /// <param name="positionX">Starting X position.</param>
        /// <param name="positionY">Starting Y position.</param>
        /// <param name="expectedCountOfCleanTiles">Expected number of tiles cleaned</param>
        [Test]
        [Category("VacuumFloorTest")]
        [TestCaseSource(typeof(RoomTestCases), nameof(RoomTestCases.TestRooms))]
        public void VacuumFloor_AllTilesAreCleaned(bool[,] map, int positionX, int positionY, int expectedCountOfCleanTiles)
        {
            // Arrange
            this.mockVacuumRepository.Setup(x => x.Room).Returns(new Room() { Map = map });
            this.mockVacuumRepository.Setup(x => x.LoadRoomData(It.IsAny<string>()))
                .Returns(new Room() { Map = map, RobotPosition = new Coordinate() { X = positionX, Y = positionY } });
            this.vacuumCleanerLogic = new VacuumCleanerLogic(this.mockVacuumRepository.Object);

            // Act
            this.vacuumCleanerLogic.VacuumFloor();
            int actualCountOfCleanTiles = this.vacuumCleanerLogic.CleanCoordinates.Count();

            // Assert
            Assert.AreEqual(actualCountOfCleanTiles, expectedCountOfCleanTiles);
        }
    }
}
