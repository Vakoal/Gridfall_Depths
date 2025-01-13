using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TextRpg1.Creatures.Character;

namespace TextRpg1.UI
{
    internal class UI
    {
        static int LocationChoice<T>(List<T> inputList, string message)
        {
            Console.WriteLine(message);
            foreach (T inputItem in inputList)
        {
            int neighborIndex = -1;
            while (!int.TryParse(Console.ReadLine(), out neighborIndex) || !(neighborIndex > 0 && neighborIndex <= neighborsCount))
            {
                Console.WriteLine("Invalid value. Enter location number:");
            }
            return neighborIndex - 1;
        }

        public static CycleMode MenuChoice(Character hero, List<CycleMode> menuActions)
        {
            Console.WriteLine("Choose your next action");

            int menuItemNumber = 1;
            foreach(CycleMode action in menuActions)
            {
                Console.WriteLine($"{menuItemNumber}) {action}");
                menuItemNumber++;
            }

            string menuItemChoice = Console.ReadLine();
            while (menuItemChoice == null || !Regex.IsMatch(menuItemChoice, "^[1-4]$"))
            {
                Console.WriteLine("Invalid input. Enter a number of menu item.");
            }

            int result = int.Parse(menuItemChoice);
            if (menuActions[result-1] == CycleMode.LocationChange)
            {

            }

        }
    }
}
