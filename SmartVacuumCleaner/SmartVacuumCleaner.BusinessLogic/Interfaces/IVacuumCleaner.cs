namespace SmartVacuumCleaner.BusinessLogic.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SmartVacuumCleaner.Repository.Utils;

    /// <summary>
    /// Interface for the vacuum cleaner.
    /// </summary>
    public interface IVacuumCleaner
    {
        /// <summary>
        /// Gets or sets the position of the robot.
        /// </summary>
        Coordinate Position { get; set; }

        /// <summary>
        /// Gets or sets the orientation of the robot.
        /// </summary>
        Orientation Orientation { get; set; }

        /// <summary>
        /// Makes the robot take one step.
        /// </summary>
        void Step();

        /// <summary>
        /// Turns the robot clockwise by 90 degrees.
        /// </summary>
        void TurnClockwise();

        /// <summary>
        /// Turns the robot counterclockwise by 90 degrees.
        /// </summary>
        void TurnCounterClockwise();
    }
}
