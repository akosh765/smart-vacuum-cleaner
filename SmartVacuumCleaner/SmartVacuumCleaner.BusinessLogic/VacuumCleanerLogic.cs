using SmartVacuumCleaner.BusinessLogic.Interfaces;
using SmartVacuumCleaner.Repository;
using SmartVacuumCleaner.Repository.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartVacuumCleaner.BusinessLogic
{
    public class VacuumCleanerLogic : IVacuumCleanerLogic
    {
        public event Action MovementNotifier;

        public IVacuumCleanerRepository<IRoom> RobotRepository { get; set; }

        public IRoom Room { get; set; }

        public bool[,] Map { get; set; }

        public List<Coordinate> CleanCoordinates { get; set; }

        public IVacuumCleaner VacuumCleaner { get; set; }

        private int orientationCount = 4;

        private int steps = 0;

        public VacuumCleanerLogic(IVacuumCleanerRepository<IRoom> robotRepository)
        {
            this.RobotRepository = robotRepository;
            this.Room = robotRepository.LoadRoomData(VacuumCleanerConfig.FilePath);
            this.Map = this.Room.Map;

            Coordinate VacuumCleanerStartPositiion = new Coordinate(this.RobotRepository.Room.RobotPosition.X,
                this.RobotRepository.Room.RobotPosition.Y);
            this.VacuumCleaner = new VacuumCleaner(VacuumCleanerStartPositiion, Orientation.Upward);
            this.VacuumCleaner.Position.X = this.Room.RobotPosition.X;
            this.VacuumCleaner.Position.Y = this.Room.RobotPosition.Y;

            this.CleanCoordinates = new List<Coordinate>();
        }

        public int VacuumFloor()
        {
            this.DFS();
            return CleanCoordinates.Count;
        }

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

            //this.CleanCoordinates.Add(new Coordinate(VacuumCleaner.Position.X, VacuumCleaner.Position.Y));
            CleanPosition(this.VacuumCleaner as VacuumCleaner);

            for (int i = 0; i < orientationCount; i++)
            {
                Coordinate nextPosition = this.CalculateNextPosition(VacuumCleaner);
                if (this.ValidateDesiredPosition(nextPosition))
                {
                    this.VacuumCleaner.Step();
                    OnStep();

                    this.DFS();
                    this.VacuumCleaner.TurnCounterClockwise();
                    this.VacuumCleaner.TurnCounterClockwise();
                    
                    this.VacuumCleaner.Step();
                    OnStep();

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
            System.Threading.Thread.Sleep(1);
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
