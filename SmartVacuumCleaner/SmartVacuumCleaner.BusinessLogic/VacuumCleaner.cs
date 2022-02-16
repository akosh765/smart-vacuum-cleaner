using SmartVacuumCleaner.BusinessLogic.Interfaces;
using SmartVacuumCleaner.Repository.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartVacuumCleaner.BusinessLogic
{
    public class VacuumCleaner : IVacuumCleaner
    {
        public Coordinate Position { get; set; }
        public Orientation Orientation { get; set; }

        public Dictionary<Orientation, Coordinate> DisplacementValues { get => displacementValues; set => displacementValues = value; }

        private Dictionary<Orientation, Coordinate> displacementValues = new Dictionary<Orientation, Coordinate>()
            {
                { Orientation.Upward, new Coordinate { X = -1, Y = 0} },
                { Orientation.Downward, new Coordinate { X = 1, Y = 0} },
                { Orientation.Left, new Coordinate { X = 0, Y = -1} },
                { Orientation.Right, new Coordinate { X = 0, Y = 1} },
            };



        public VacuumCleaner()
        {
            this.Position = new Coordinate(0, 0);
            this.Orientation = Orientation.Upward;
        }

        public VacuumCleaner(Coordinate position, Orientation orientation = Orientation.Upward)
        {
            this.Position = position;
            this.Orientation = orientation;
        }

        public void Step()
        {
            Coordinate displacementValues = GetDisplacementValuesAccordingToOrientation(Orientation);
            int displacementX = displacementValues.X;
            int displacementY = displacementValues.Y;

            this.Position.X += displacementX;
            this.Position.Y += displacementY;
        }

        public void TurnClockwise()
        {
            this.Orientation = (Orientation)(((int)this.Orientation + 1) % 4);
        }

        public void TurnCounterClockwise()
        {
            if (--Orientation < 0)
            {
                this.Orientation = Orientation.Left;
            }
        }

        public Coordinate GetDisplacementValuesAccordingToOrientation(Orientation orientation)
        {
            return displacementValues[orientation];
        }
    }
}
