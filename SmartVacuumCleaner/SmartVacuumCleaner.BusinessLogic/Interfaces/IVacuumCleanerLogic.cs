namespace SmartVacuumCleaner.BusinessLogic.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SmartVacuumCleaner.Repository;
    using SmartVacuumCleaner.Repository.Utils;

    /// <summary>
    /// Interface for the business logic
    /// </summary>
    public interface IVacuumCleanerLogic
    {
        /// <summary>
        /// Event to notify the view if the model changes.
        /// </summary>
        event Action MovementNotifier;

        /// <summary>
        /// Gets or sets the repository instance to connect the repository layer to the business logic layer.
        /// </summary>
        IVacuumCleanerRepository<IRoom> RobotRepository { get; set; }

        /// <summary>
        /// Gets or sets the map's layout.
        /// </summary>
        bool[,] Map { get; set; }

        /// <summary>
        /// Gets or sets the list containing the points that already have been cleaened.
        /// </summary>
        List<Coordinate> CleanCoordinates { get; set; }

        /// <summary>
        /// Gets or sets the vacuumCleaner object.
        /// </summary>
        IVacuumCleaner VacuumCleaner { get; set; }

        /// <summary>
        /// The vacuum cleaner starts vacuuming.
        /// </summary>
        /// <returns>An integer representing the number of clean tiles.</returns>
        int VacuumFloor();

        /// <summary>
        /// Calculates where the vacuumCleaner will reposition.
        /// </summary>
        /// <param name="vacuumCleaner">The vacuum cleaner object.</param>
        /// <returns>The coordinates of the next tile.</returns>
        Coordinate CalculateNextPosition(IVacuumCleaner vacuumCleaner);
    }
}
