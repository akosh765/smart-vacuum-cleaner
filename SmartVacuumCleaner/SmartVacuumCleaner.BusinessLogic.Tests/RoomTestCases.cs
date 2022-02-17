namespace SmartVacuumCleaner.BusinessLogic.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SmartVacuumCleaner.Repository.Utils;

    /// <summary>
    /// Class that provides test data for the Unit tests of the business logic.
    /// </summary>
    public class RoomTestCases
    {
        /// <summary>
        /// Gets or sets the object array of test rooms.
        /// </summary>
        public static object[] TestRooms =
        {
            new object[]
            {
                new bool[,]
                {
                    { true, true, true },
                    { true, true, true },
                    { true, true, true },
                },
                0,
                0,
                9,
            },

            new object[]
            {
                new bool[,]
                {
                    { true, false },
                    { false, false },
                    { false, false },
                },
                0,
                0,
                1,
            },
        };
    }
}
