namespace SmartVacuumCleaner.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SmartVacuumCleaner.Repository.Utils;

    /// <summary>
    /// Class that implements the IRoom interface, used to represent a room.
    /// </summary>
    public class Room : IRoom
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Room"/> class.
        /// </summary>
        public Room()
        {
            this.RobotPosition = new Coordinate();
        }

        /// <summary>
        /// Gets or sets the room's layout.
        /// </summary>
        public bool[,] Map { get; set; }

        /// <summary>
        /// Gets or sets the starting coordinate of the vacuum cleaner.
        /// </summary>
        public Coordinate RobotPosition { get; set; }
    }
}
