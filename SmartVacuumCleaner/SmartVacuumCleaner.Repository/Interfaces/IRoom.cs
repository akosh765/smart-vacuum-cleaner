namespace SmartVacuumCleaner.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SmartVacuumCleaner.Repository.Utils;

    /// <summary>
    /// interface that stores the map and the current coordinates of the vacuum cleaner.
    /// </summary>
    public interface IRoom
    {
        /// <summary>
        /// Gets or sets the property which holds the  map's layout.
        /// False values represent obstacles while true values repsent empty cells.
        /// </summary>
        bool[,] Map { get; set; }

        /// <summary>
        /// Gets or sets the property which stores the current coordinates of the vacuum cleaner.
        /// </summary>
        Coordinate RobotPosition { get; set; }
    }
}
