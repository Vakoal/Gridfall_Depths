using System;
using TextRpg1.Creatures.Character;

namespace TextRpg1.Creatures.Character;

internal class PrimaryAttributes
{
    public int Strength { get; set; } = 0;
    public int Agility { get; set; } = 0;
    public int Endurance { get; set; } = 0;
    public int Intelligence { get; set; } = 0;
    public int Perception { get; set; } = 0;
    public int Willpower { get; set; } = 0;

    public PrimaryAttributes()
    {

    }
    public PrimaryAttributes(int strength, int agility, int endurance, int intelligence, int perception, int willpower)
    {
        Strength = strength;
        Agility = agility;
        Endurance = endurance;
        Intelligence = intelligence;
        Perception = perception;
        Willpower = willpower;
    }

    public int this[int index]
    {
        get
        {
            return index switch
            {
                0 => Strength,
                1 => Agility,
                2 => Endurance,
                3 => Intelligence,
                4 => Perception,
                5 => Willpower,
                _ => -1,
            };
        }

        set
        {
            switch (index)
            {
                case 0: Strength = value;
                    break;
                case 1: Agility = value;
                    break;
                case 2: Endurance = value;
                    break;
                case 3: Intelligence = value;
                    break;
                case 4: Perception = value;
                    break;
                case 5: Willpower = value;
                    break;
            }
        }
    }

    public double GetAverage()
    {
        return (Strength + Agility + Endurance + Intelligence + Perception + Willpower) / 6;
    }
    public void ShowInfo()
    {
        Console.WriteLine($"Strength: {Strength}");
        Console.WriteLine($"Agility: {Agility}");
        Console.WriteLine($"Endurance: {Endurance}");
        Console.WriteLine($"Intelligence: {Intelligence}");
        Console.WriteLine($"Perception: {Perception}");
        Console.WriteLine($"Willpower: {Willpower}");

        Console.WriteLine();
    }
}
