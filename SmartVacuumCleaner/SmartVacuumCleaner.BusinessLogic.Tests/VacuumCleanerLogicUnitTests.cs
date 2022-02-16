using Moq;
using NUnit.Framework;
using SmartVacuumCleaner.BusinessLogic.Interfaces;
using SmartVacuumCleaner.Repository;
using SmartVacuumCleaner.Repository.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartVacuumCleaner.BusinessLogic.Tests
{
    [TestFixture]
    public class VacuumCleanerLogicUnitTests
    {
        private RoomTestCases[] roomsToClean = new RoomTestCases[1];
        private IVacuumCleanerLogic vacuumCleanerLogic;
        private Mock<IVacuumCleanerRepository<IRoom>> mockVacuumRepository;

        [SetUp]
        public void Setup()
        {
            this.mockVacuumRepository = new Mock<IVacuumCleanerRepository<IRoom>>();
            this.mockVacuumRepository.Setup(x => x.Room).Returns(new Room());
            this.mockVacuumRepository.Setup(x => x.LoadRoomData(It.IsAny<string>())).Returns(new Room());
        }

        [TearDown]
        public void Teardown()
        {

        }

        [Category("MovementTest")]
        [TestCase(Orientation.Upward, 1, 0, 0, 0)]
        [TestCase(Orientation.Downward, 25, 25, 26, 25)]
        [TestCase(Orientation.Left, 2, -5, 2, -6)]
        [TestCase(Orientation.Right, 123, 67, 123, 68)]
        public void CalculateNextPosition_ReturnsWithCorrectPosition(Orientation orientation, int startX, int startY, int expectedX, int expectedY)
        {
            //Arrange
            this.vacuumCleanerLogic = new VacuumCleanerLogic(mockVacuumRepository.Object);

            VacuumCleaner vacuumCleaner = new VacuumCleaner()
            {
                Position = new Coordinate()
                {
                    X = startX, Y = startY
                },
                Orientation = orientation
            };

            //Act
            Coordinate actualNextPosition = vacuumCleanerLogic.CalculateNextPosition(vacuumCleaner);

            //Assert
            Assert.AreEqual(actualNextPosition.X, expectedX);
            Assert.AreEqual(actualNextPosition.Y, expectedY);
        }

        [Test]
        [Category("VacuumFloorTest")]
        [TestCaseSource(typeof(RoomTestCases), nameof(RoomTestCases.TestRooms))]
        public void VacuumFloor_CallsGetRoomExactlyOnce(bool[,] map, int positionX, int positionY, int stepsTaken)
        {
            //Arrange
            this.mockVacuumRepository.Setup(x => x.Room).Returns(new Room() { Map = map });
            this.mockVacuumRepository.Setup(x => x.LoadRoomData(It.IsAny<string>()))
                .Returns(new Room() { Map = map, RobotPosition = new Coordinate() { X = positionX, Y = positionY } });
            this.vacuumCleanerLogic = new VacuumCleanerLogic(mockVacuumRepository.Object);

            //Act
            this.vacuumCleanerLogic.VacuumFloor();

            //Assert
            this.mockVacuumRepository.Verify(x => x.LoadRoomData(It.IsAny<string>()), Times.Exactly(1));
        }

        [Test]
        [Category("VacuumFloorTest")]
        [TestCaseSource(typeof(RoomTestCases), nameof(RoomTestCases.TestRooms))]
        public void VacuumFloor_GetsOrSetsRoomAtLeasOnce(bool[,] map, int positionX, int positionY, int stepsTaken)
        {
            //Arrange
            this.mockVacuumRepository.Setup(x => x.Room).Returns(new Room() { Map = map });
            this.mockVacuumRepository.Setup(x => x.LoadRoomData(It.IsAny<string>()))
                .Returns(new Room() { Map = map, RobotPosition = new Coordinate() { X = positionX, Y = positionY } });
            this.vacuumCleanerLogic = new VacuumCleanerLogic(mockVacuumRepository.Object);

            //Act
            this.vacuumCleanerLogic.VacuumFloor();

            //Assert
            this.mockVacuumRepository.Verify(x => x.Room, Times.AtLeastOnce());
        }

        [Test]
        [Category("VacuumFloorTest")]
        [TestCaseSource(typeof(RoomTestCases), nameof(RoomTestCases.TestRooms))]
        public void VacuumFloor_AllTilesAreCleaned(bool[,] map, int positionX, int positionY, int expectedCountOfCleanTiles)
        {
            //Arrange
            this.mockVacuumRepository.Setup(x => x.Room).Returns(new Room() { Map = map });
            this.mockVacuumRepository.Setup(x => x.LoadRoomData(It.IsAny<string>()))
                .Returns(new Room() { Map = map, RobotPosition = new Coordinate() { X = positionX, Y = positionY } });
            this.vacuumCleanerLogic = new VacuumCleanerLogic(mockVacuumRepository.Object);

            //Act
            this.vacuumCleanerLogic.VacuumFloor();
            int actualCountOfCleanTiles = vacuumCleanerLogic.CleanCoordinates.Count();

            //Assert
            Assert.AreEqual(actualCountOfCleanTiles, expectedCountOfCleanTiles);
        }
    }
}
