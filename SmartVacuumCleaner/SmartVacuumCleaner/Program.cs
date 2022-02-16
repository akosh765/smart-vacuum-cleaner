using SmartVacuumCleaner.BusinessLogic;
using SmartVacuumCleaner.BusinessLogic.Interfaces;
using SmartVacuumCleaner.Repository;
using SmartVacuumCleaner.Repository.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartVacuumCleaner
{
    public class Program
    {
        private static RobotController controller;
        private const int CharacterOffset = 2;
        private const int HorizontalConsoleOffset = 10;
        private const int VerticalConsoleOffset = 3;

        static void Main(string[] args)
        {
            IVacuumCleanerLogic logic = new VacuumCleanerLogic(new VacuumCleanerRepository());
            controller = new RobotController(logic);

            controller.NPC += VisualizeRoom;
            //controller.vacuumCleanerLogic.NPC += VisualizeRoom;

            SetUpConsole();

            int tilesCleaned = controller.Vacuum();

            GetSummary(tilesCleaned);
            Console.ReadKey();
        }

        private static void VisualizeRoom()
        {
            Console.Clear();
            Console.WriteLine("A 2D view of the room to be cleaned can be seen below");

            for (int x = 0; x < controller.Map.GetLength(0); x++)
            {
                Console.SetCursorPosition(HorizontalConsoleOffset, VerticalConsoleOffset + x);
                for (int y = 0; y < controller.Map.GetLength(1); y++)
                {
                    Console.Write((controller.Map[x, y] == false ? "x" : "-") + " "); ;
                }
                Console.Write("\n");
            }

            Console.SetCursorPosition(HorizontalConsoleOffset + controller.RobotPosition.Y * CharacterOffset,
                VerticalConsoleOffset + controller.RobotPosition.X);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("o");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private static void GetSummary(int tilesCleaned)
        {
            Console.SetCursorPosition(5, 15);
            double ratio = (((double)tilesCleaned) / ((double)controller.Map.Length)) * 100;
            Console.WriteLine($"Room cleaned!\n Tiles reached: {tilesCleaned}/{controller.Map.Length} ~ {ratio.ToString("#.##")}%");
        }

        private static void SetUpConsole()
        {
            Console.CursorVisible = false;
        }
    }
}
