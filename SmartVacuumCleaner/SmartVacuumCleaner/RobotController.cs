using SmartVacuumCleaner.BusinessLogic.Interfaces;
using SmartVacuumCleaner.Repository.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartVacuumCleaner
{
    public class RobotController : IRobotController
    {
        public bool[,] Map { get; set; }

        public Coordinate RobotPosition { get; set; }
        public IVacuumCleanerLogic vacuumCleanerLogic { get; set; }

        public event Action NPC;

        public RobotController(IVacuumCleanerLogic vacuumCleanerLogic)
        {
            this.vacuumCleanerLogic = vacuumCleanerLogic;
            this.RobotPosition = this.vacuumCleanerLogic.VacuumCleaner.Position;
            this.Map = vacuumCleanerLogic.Map;
            this.vacuumCleanerLogic.MovementNotifier += this.OnChange;
        }

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
