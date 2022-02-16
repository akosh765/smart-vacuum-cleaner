using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartVacuumCleaner.Repository.Utils
{
    public class Coordinate
    {
        public int X { get; set; }

        public int Y { get; set; }

        public Coordinate()
        {

        }

        public Coordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj != null)
            {
                if (obj is Coordinate coordinate)
                {
                    return this.X == coordinate.X && this.Y == coordinate.Y;
                }
            }
            return false;
        }
    }
}
