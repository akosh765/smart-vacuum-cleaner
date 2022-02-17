// <copyright file="RobotController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SmartVacuumCleaner
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SmartVacuumCleaner.BusinessLogic.Interfaces;
    using SmartVacuumCleaner.Repository.Utils;

    /// <summary>
    /// Controller class to connect the view and business logic layers.
    /// </summary>
    public class RobotController : IRobotController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RobotController"/> class.
        /// </summary>
        /// <param name="vacuumCleanerLogic">Vacummcleaner instance</param>
        public RobotController(IVacuumCleanerLogic vacuumCleanerLogic)
        {
            this.vacuumCleanerLogic = vacuumCleanerLogic;
            this.RobotPosition = this.vacuumCleanerLogic.VacuumCleaner.Position;
            this.Map = vacuumCleanerLogic.Map;
            this.vacuumCleanerLogic.MovementNotifier += this.OnChange;
        }

        /// <summary>
        /// Gets or sets the main event that notifies the view.
        /// </summary>
        public event Action NPC;

        /// <summary>
        /// Gets or sets the map's layout..
        /// </summary>
        public bool[,] Map { get; set; }

        /// <summary>
        /// Gets or sets the current position of the robot.
        /// </summary>
        public Coordinate RobotPosition { get; set; }

        /// <summary>
        /// Gets or sets the business logic instance.
        /// </summary>
        public IVacuumCleanerLogic vacuumCleanerLogic { get; set; }

        /// <summary>
        /// Starts the cleaning process. Main function of the program.
        /// </summary>
        /// <returns>The number of tiles cleaned.</returns>
        public int Vacuum()
        {
            return this.vacuumCleanerLogic.VacuumFloor();
        }

        private void OnChange()
        {
            this.NPC?.Invoke();
        }
    }
}
