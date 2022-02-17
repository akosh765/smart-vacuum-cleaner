namespace SmartVacuumCleaner.Repository
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Class that implements the IVacuumCleanerRepository interface and stores the map and the current coordinates of the vacuum cleaner.
    /// </summary>
    public class VacuumCleanerRepository : IVacuumCleanerRepository<IRoom>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VacuumCleanerRepository"/> class.
        /// </summary>
        public VacuumCleanerRepository()
        {
        }

        /// <summary>
        /// Gets or sets the Room property.
        /// </summary>
        public IRoom Room { get; set; }

        /// <summary>
        /// Loads a map from a text file and constructs a Room instance.
        /// </summary>
        /// <param name="filepath">The filepath to the txt file</param>
        /// <returns>IRoom instance.</returns>
        public IRoom LoadRoomData(string filepath)
        {
            StreamReader mapReader = new StreamReader(VacuumCleanerConfig.FilePath, Encoding.UTF8);
            IRoom newRoom = new Room();

            string[] mapSizeLine = mapReader.ReadLine().Split(';');
            int mapSize_X = int.Parse(mapSizeLine[0]);
            int mapSize_Y = int.Parse(mapSizeLine[1]);
            newRoom.Map = new bool[mapSize_X, mapSize_Y];

            string[] robotPositionLine = mapReader.ReadLine().Split(';');
            newRoom.RobotPosition.X = int.Parse(robotPositionLine[0]);
            newRoom.RobotPosition.Y = int.Parse(robotPositionLine[1]);

            for (int x = 0; x < newRoom.Map.GetLength(0); x++)
            {
                string[] positions = mapReader.ReadLine().Split(' ');
                for (int y = 0; y < newRoom.Map.GetLength(1); y++)
                {
                    newRoom.Map[x, y] = !(positions[y] == VacuumCleanerConfig.ObstacleSign.ToString());
                }
            }

            this.Room = newRoom;
            return newRoom;
        }
    }
}
