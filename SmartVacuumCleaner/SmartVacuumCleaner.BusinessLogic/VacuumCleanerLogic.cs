// <copyright file="VacuumCleanerLogic.cs" company="PlaceholderCompany">
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
    using SmartVacuumCleaner.Repository;
    using SmartVacuumCleaner.Repository.Utils;

    /// <summary>
    /// VacuumCleaner logic for the business logic layer.
    /// </summary>
    public class VacuumCleanerLogic : IVacuumCleanerLogic
    {
        private int orientationCount = 4;

        /// <summary>
        /// Initializes a new instance of the <see cref="VacuumCleanerLogic"/> class.
        /// </summary>
        /// <param name="robotRepository">The robot repository instance.</param>
        public VacuumCleanerLogic(IVacuumCleanerRepository<IRoom> robotRepository)
        {
            this.RobotRepository = robotRepository;
            this.Room = robotRepository.LoadRoomData(VacuumCleanerConfig.FilePath);
            this.Map = this.Room.Map;

            Coordinate vacuumCleanerStartPositiion = new Coordinate(
                this.RobotRepository.Room.RobotPosition.X,
                this.RobotRepository.Room.RobotPosition.Y);
            this.VacuumCleaner = new VacuumCleaner(vacuumCleanerStartPositiion, Orientation.Upward);
            this.VacuumCleaner.Position.X = this.Room.RobotPosition.X;
            this.VacuumCleaner.Position.Y = this.Room.RobotPosition.Y;

            this.CleanCoordinates = new List<Coordinate>();
        }

        /// <summary>
        /// Event used to notify the view whenever the model changes.
        /// </summary>
        public event Action MovementNotifier;

        /// <summary>
        /// Gets or set the RobotRepository property.
        /// </summary>
        public IVacuumCleanerRepository<IRoom> RobotRepository { get; set; }

        /// <summary>
        /// Gets or sets the room.
        /// </summary>
        public IRoom Room { get; set; }

        /// <summary>
        /// Gets or sets the map's layout.
        /// </summary>
        public bool[,] Map { get; set; }

        /// <summary>
        /// Gets or sets the list containing the clean coordinates.
        /// </summary>
        public List<Coordinate> CleanCoordinates { get; set; }

        /// <summary>
        /// Gets or sets the vacuum cleaner object.
        /// </summary>
        public IVacuumCleaner VacuumCleaner { get; set; }

        /// <summary>
        /// The vacuum cleaner starts cleaning the room.
        /// </summary>
        /// <returns>The number of cleaned tiles.</returns>
        public int VacuumFloor()
        {
            this.DFS();
            return this.CleanCoordinates.Count;
        }

        /// <summary>
        /// Calculates the position the vacuum cleaner would move to.
        /// </summary>
        /// <param name="vacuumCleaner">The vacuum cleaner.</param>
        /// <returns>The coordinates of the next position.</returns>
        public Coordinate CalculateNextPosition(IVacuumCleaner vacuumCleaner)
        {
            Coordinate displacementValues = (vacuumCleaner as VacuumCleaner).GetDisplacementValuesAccordingToOrientation(vacuumCleaner.Orientation);

            int nextPositionX = vacuumCleaner.Position.X + displacementValues.X;
            int nextPositionY = vacuumCleaner.Position.Y + displacementValues.Y;

            return new Coordinate(nextPositionX, nextPositionY);
        }

        private void DFS()
        {
            Coordinate positionToCheck = new Coordinate(this.VacuumCleaner.Position.X, this.VacuumCleaner.Position.Y);
            if (this.CleanCoordinates.Contains(positionToCheck))
            {
                return;
            }

            // this.CleanCoordinates.Add(new Coordinate(VacuumCleaner.Position.X, VacuumCleaner.Position.Y));
            this.CleanPosition(this.VacuumCleaner as VacuumCleaner);

            for (int i = 0; i < this.orientationCount; i++)
            {
                Coordinate nextPosition = this.CalculateNextPosition(this.VacuumCleaner);
                if (this.ValidateDesiredPosition(nextPosition))
                {
                    this.VacuumCleaner.Step();
                    this.OnStep();

                    this.DFS();
                    this.VacuumCleaner.TurnCounterClockwise();
                    this.VacuumCleaner.TurnCounterClockwise();

                    this.VacuumCleaner.Step();
                    this.OnStep();

                    this.VacuumCleaner.TurnClockwise();
                    this.VacuumCleaner.TurnClockwise();
                }

                this.VacuumCleaner.TurnClockwise();
            }
        }

        private void CleanPosition(VacuumCleaner vacuumCleaner)
        {
            Coordinate cleanedPosition = new Coordinate(vacuumCleaner.Position.X, vacuumCleaner.Position.Y);
            this.CleanCoordinates.Add(cleanedPosition);
        }

        private void OnStep()
        {
            this.MovementNotifier?.Invoke();
            System.Threading.Thread.Sleep(1000);
        }

        private bool ValidateDesiredPosition(Coordinate position)
        {
            bool isInMap = position.X >= 0 &&
                position.X < this.Room.Map.GetLength(0) &&
                position.Y >= 0 &&
                position.Y < this.Room.Map.GetLength(1);

            if (!isInMap)
            {
                return false;
            }

            bool isNotStoredYet = !this.CleanCoordinates.Contains(position);

            bool isEnabled = this.RobotRepository.Room.Map[position.X, position.Y] == true;

            return isInMap && isNotStoredYet && isEnabled;
        }
    }
}
