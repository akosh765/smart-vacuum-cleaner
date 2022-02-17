// <copyright file="IRobotController.cs" company="PlaceholderCompany">
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
    /// Intrerface used to connect the view with the business logic layer.
    /// </summary>
    public interface IRobotController
    {
        /// <summary>
        /// Event to notify the change of the model.
        /// </summary>
        event Action NPC;

        /// <summary>
        /// Gets or sets the room's layout.
        /// </summary>
        bool[,] Map { get; set; }

        /// <summary>
        /// Gets or sets the position of the robot.
        /// </summary>
        Coordinate RobotPosition { get; set; }

        /// <summary>
        /// The vacuum cleaner starts cleaning.
        /// </summary>
        /// <returns>Number of tiles reached.</returns>
        int Vacuum();

        /// <summary>
        /// Instance of the logic.
        /// </summary>
        IVacuumCleanerLogic vacuumCleanerLogic { get; set; }
    }
}
