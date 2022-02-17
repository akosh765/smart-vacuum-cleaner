namespace SmartVacuumCleaner.Repository
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Utility class to provide easy-to-read configuration.
    /// </summary>
    public class VacuumCleanerConfig
    {
        /// <summary>
        /// Gets or sets the map filepath.
        /// </summary>
        public static string FilePath = "maps/map4.txt";

        /// <summary>
        /// Gets or sets the sign associated with obstacles.
        /// </summary>
        public static char ObstacleSign = 'x';

        /// <summary>
        /// Gets or sets the sign associated with the vacuum cleaner robot.
        /// </summary>
        public static char VaccumCleanerSign = 'o';
    }
}
