using SmartVacuumCleaner.Repository.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartVacuumCleaner.Repository
{
    public interface IRoom
    {
        bool[,] Map { get; set; }

        Coordinate RobotPosition { get; set; }
    }
}
