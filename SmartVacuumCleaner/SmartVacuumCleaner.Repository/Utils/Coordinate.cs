namespace SmartVacuumCleaner.Repository.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Utility class to provide (X;Y) value pairs to store coordinates.
    /// </summary>
    public class Coordinate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Coordinate"/> class.
        /// </summary>
        public Coordinate()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Coordinate"/> class.
        /// </summary>
        /// <param name="x">The x coordinate.</param>
        /// <param name="y">The y coordinate.</param>
        public Coordinate(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        /// <summary>
        /// Gets or sets the X coordinate.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Gets or sets the Y coordinate.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Checks wheter the coordinates of current instance equal to the coordinates of parameter.
        /// </summary>
        /// <param name="obj">Object that this instace gets compared to.</param>
        /// <returns>Boolean value.</returns>
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
