using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TextRpg1.Creatures.Character;
using TextRpg1.Items;
using TextRpg1.Locations;

namespace TextRpg1.UI
{
    internal static class UI
    {
        public static List<(string, ConsoleColor)> MessagesStack = [];
        public static int ShowMenu<T>(List<T> inputList, string? message = null, List<string>? additionalMenuItems = null)
        {
            if(message != null)
            Console.WriteLine(message);

            int menuItemNumber = 1;
            foreach (T inputItem in inputList)
            {
                if (inputItem is Armor armor)
                {
                    Console.Write($"{menuItemNumber}) ");
                    armor.DisplayColored();
                }
                else if (inputItem is Weapon weapon)
                {
                    Console.Write($"{menuItemNumber}) ");
                    weapon.DisplayColored();
                }

                else Console.WriteLine($"{menuItemNumber}) {inputItem}");

                menuItemNumber++;
            }
            if (additionalMenuItems != null)
            {
                foreach (string additionalMenuItem in additionalMenuItems)
                {
                    Console.WriteLine($"{menuItemNumber}) {additionalMenuItem}");
                    menuItemNumber++;
                }
            }

            int additionalMenuItemsCount = additionalMenuItems is null ? 0 : additionalMenuItems.Count;
            int userChoice = 0;
            while (!int.TryParse(Console.ReadLine(), out userChoice) || userChoice < 1 || userChoice > (inputList.Count + additionalMenuItemsCount))
            {
                Console.WriteLine($"Invalid value. Enter number from 1 to {inputList.Count + additionalMenuItemsCount}:");
            }

            return userChoice;
        }
        public static void ShowStatusInfo(Character hero)
        {
            string info = $"""
            HP: {hero.Health}/{hero.MaxHealth}
            Hunger: {hero.Hunger}
            Thirst: {hero.Thirst}
            Current location: {hero.CurrentLocation}
            """;

            Console.Clear();
            Console.WriteLine(info);


            Console.Write("Equipped weapon: ");
            if (hero.Equipment["Weapon"] is Weapon weapon)
                weapon.DisplayColored(false);
            else Console.WriteLine();

            Console.Write("Equipped armor: ");
            if (hero.Equipment["Armor"] is Armor armor)
                armor.DisplayColored(false);
            else Console.WriteLine();

            Console.WriteLine(new string('-', Console.WindowWidth - 1));

            if (MessagesStack.Count != 0)
            {
                Console.WriteLine();
                foreach ((string text, ConsoleColor color) message in MessagesStack)
                {
                    Console.ForegroundColor = message.color;
                    Console.WriteLine(message.text);
                    Console.ResetColor();
                }

                MessagesStack.Clear();
            }
            Console.WriteLine();
        }
    }
}
