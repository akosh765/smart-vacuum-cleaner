// <copyright file="VacuumCleaner.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SmartVacuumCleaner.BusinessLogic
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SmartVacuumCleaner.BusinessLogic.Interfaces;
    using SmartVacuumCleaner.Repository.Utils;

    /// <summary>
    /// Class to represent the vacummCleaner.
    /// </summary>
    public class VacuumCleaner : IVacuumCleaner
    {
        private Dictionary<Orientation, Coordinate> displacementValues = new Dictionary<Orientation, Coordinate>()
            {
                { Orientation.Upward, new Coordinate { X = -1, Y = 0} },
                { Orientation.Downward, new Coordinate { X = 1, Y = 0} },
                { Orientation.Left, new Coordinate { X = 0, Y = -1} },
                { Orientation.Right, new Coordinate { X = 0, Y = 1} },
            };

        /// <summary>
        /// Initializes a new instance of the <see cref="VacuumCleaner"/> class.
        /// </summary>
        public VacuumCleaner()
        {
            this.Position = new Coordinate(0, 0);
            this.Orientation = Orientation.Upward;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VacuumCleaner"/> class.
        /// </summary>
        /// <param name="position">The position value.</param>
        /// <param name="orientation">The orientation value.</param>
        public VacuumCleaner(Coordinate position, Orientation orientation = Orientation.Upward)
        {
            this.Position = position;
            this.Orientation = orientation;
        }

        /// <summary>
        /// Gets or sets the current position.
        /// </summary>
        public Coordinate Position { get; set; }

        /// <summary>
        /// Gets or sets the current orientation.
        /// </summary>
        public Orientation Orientation { get; set; }

        /// <summary>
        /// Gets or sets the current displacement values.
        /// </summary>
        public Dictionary<Orientation, Coordinate> DisplacementValues { get => this.displacementValues; set => this.displacementValues = value; }

        /// <summary>
        /// The vacuum cleaner moves one tile.
        /// </summary>
        public void Step()
        {
            Coordinate displacementValues = this.GetDisplacementValuesAccordingToOrientation(this.Orientation);
            int displacementX = displacementValues.X;
            int displacementY = displacementValues.Y;

            this.Position.X += displacementX;
            this.Position.Y += displacementY;
        }

        /// <summary>
        /// The vacuum cleaner changes its orientation by turning clockwise.
        /// </summary>
        public void TurnClockwise()
        {
            this.Orientation = (Orientation)(((int)this.Orientation + 1) % 4);
        }

        /// <summary>
        /// The vacuum cleaner changes its orientation by turning counterclockwise.
        /// </summary>
        public void TurnCounterClockwise()
        {
            if (--this.Orientation < 0)
            {
                this.Orientation = Orientation.Left;
            }
        }

        /// <summary>
        /// Gets the current displacement coordinates according to the orientation.
        /// </summary>
        /// <param name="orientation">The current orientation.</param>
        /// <returns>Coordinates of the displacement</returns>
        public Coordinate GetDisplacementValuesAccordingToOrientation(Orientation orientation)
        {
            return this.displacementValues[orientation];
        }
    }
}
