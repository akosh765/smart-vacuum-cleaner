using SmartVacuumCleaner.Repository.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartVacuumCleaner.Repository
{
    public class Room : IRoom
    {
        public bool[,] Map { get; set; }

        //public int SizeX { get; set; }

        //public int SizeY { get => Map.GetLength(1); set { SizeY = value; } }

        public Coordinate RobotPosition { get; set; }

        public Room()
        {
            RobotPosition = new Coordinate();
        }
    }
}
