using SmartVacuumCleaner.Repository.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartVacuumCleaner.BusinessLogic.Interfaces
{
    public interface IVacuumCleaner
    {
        Coordinate Position { get; set; }

        Orientation Orientation { get; set; }

        void Step();

        void TurnClockwise();

        void TurnCounterClockwise();

        //Coordinate CalculateNextPosition();
    }
}
