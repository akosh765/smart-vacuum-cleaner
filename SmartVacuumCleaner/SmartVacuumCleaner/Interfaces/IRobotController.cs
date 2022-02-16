using SmartVacuumCleaner.BusinessLogic.Interfaces;
using SmartVacuumCleaner.Repository.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartVacuumCleaner
{
    public interface IRobotController
    {
        event Action NPC;

        bool[,] Map { get; set; }

        Coordinate RobotPosition { get; set; }

        int Vacuum();

        IVacuumCleanerLogic vacuumCleanerLogic { get; set; }
    }
}
