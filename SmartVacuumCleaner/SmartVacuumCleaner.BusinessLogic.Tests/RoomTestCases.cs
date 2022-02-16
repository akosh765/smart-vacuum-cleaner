using SmartVacuumCleaner.Repository.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartVacuumCleaner.BusinessLogic.Tests
{
    public class RoomTestCases
    {
        public static object[] TestRooms =
        {
            new object[]
            {
                new bool[,]
                {
                    { true, true, true}, { true, true, true }, { true, true, true }
                },
                0,
                0,
                9
            },

            new object[]
            {
                new bool[,]
                {
                    { true, false }, { false, false }, { false, false }
                },
                0,
                0,
                1
            }
        };
    }
}
