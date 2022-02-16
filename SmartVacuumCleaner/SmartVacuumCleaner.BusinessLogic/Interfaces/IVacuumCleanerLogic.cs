using SmartVacuumCleaner.Repository;
using SmartVacuumCleaner.Repository.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartVacuumCleaner.BusinessLogic.Interfaces
{
    public interface IVacuumCleanerLogic
    {
        IVacuumCleanerRepository<IRoom> RobotRepository { get; set; }

        bool[,] Map { get; set; }

        List<Coordinate> CleanCoordinates { get; set; }

        IVacuumCleaner VacuumCleaner { get; set; }

        int VacuumFloor();

        Coordinate CalculateNextPosition(IVacuumCleaner vacuumCleaner);

        event Action MovementNotifier;
    }
}
