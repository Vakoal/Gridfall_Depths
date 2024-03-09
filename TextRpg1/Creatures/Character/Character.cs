namespace TextRpg1.Creatures.Character;

using System.Text.RegularExpressions;
using TextRpg1.Creatures;
using TextRpg1.Locations;

internal class Character : Creature
{
    public Location CurrentLocation { get; set; }
    public CharacterProfessions CharacterProfessions { get; set; }

    public Character(string name, PrimaryAttributes primaryAttributes, Location homeLocation) : base(name, primaryAttributes, homeLocation)
    {
        CurrentLocation = homeLocation;
        CharacterProfessions = new CharacterProfessions();
    }

    public static Character CreateCharacter(Location homeLocation)
    {
        PrimaryAttributes primaryAttributes = new PrimaryAttributes();
        string? name;
        bool IsNameValid = false;
        do
        {

            Console.WriteLine("Введите имя персонажа. От 2 до 15 букв.");
            name = Console.ReadLine();
            if (name != null && Regex.IsMatch(name, @"^[a-zA-Zа-яА-ЯёЁ]{2,15}$"))
                IsNameValid = true;
            else Console.WriteLine("Неверный формат имени. Повторите ввод.");
        }
        while (!IsNameValid);

        int totalAttriburePoints = 60;
        while (totalAttriburePoints > 0)
        {
            primaryAttributes = new PrimaryAttributes();
            totalAttriburePoints = 60;
            Console.WriteLine("Распределите 60 очков характеристик. " +
                              "Каждая харктеристика при создании персонажа может быть от 1 до 20 очков. " +
                              "10 - показатель обычного человека.");

            do
            {
                Console.WriteLine("Введите показатель Силы. Отвечает за физическое воздействие и переноску грузов.");// заменить описание
                if (int.TryParse(Console.ReadLine(), out int points) && points > 0 && points < 20 && points <= totalAttriburePoints)
                {
                    primaryAttributes.Strength = points;
                    totalAttriburePoints -= points;
                    Console.WriteLine($"Осталось {totalAttriburePoints} очков.");
                }

                else if (points < totalAttriburePoints && points < 20)
                    Console.WriteLine("Ошибка ввода. Введите число от 1 до 20.");
                else Console.WriteLine("Недостаточно очков.");
            }
            while (primaryAttributes.Strength == 0);

            do
            {
                Console.WriteLine("Введите показатель Ловкости. Отвечает за скорость, координацию и гибкость.");
                if (int.TryParse(Console.ReadLine(), out int points) && points > 0 && points < 20 && points <= totalAttriburePoints)
                {
                    primaryAttributes.Agility = points;
                    totalAttriburePoints -= points;
                    Console.WriteLine($"Осталось {totalAttriburePoints} очков.");
                }

                else if (points < totalAttriburePoints)
                    Console.WriteLine("Ошибка ввода. Введите число от 1 до 20.");
                else Console.WriteLine("Недостаточно очков.");
            }
            while (primaryAttributes.Agility == 0);

            do
            {
                Console.WriteLine("Введите показатель Выносливости. Отвечает за здоровье, энергию и физическую стойкость.");
                if (int.TryParse(Console.ReadLine(), out int points) && points > 0 && points < 20 && points <= totalAttriburePoints)
                {
                    primaryAttributes.Endurance = points;
                    totalAttriburePoints -= points;
                    Console.WriteLine($"Осталось {totalAttriburePoints} очков.");
                }

                else if (points < totalAttriburePoints)
                    Console.WriteLine("Ошибка ввода. Введите число от 1 до 20.");
                else Console.WriteLine("Недостаточно очков.");
            }
            while (primaryAttributes.Endurance == 0);

            do
            {
                Console.WriteLine("Введите показатель Интеллекта. Отвечает за получаемый опыт, скорость роста навыков и умений. Также, напрямую влияет на способность персонажа к мышлению и его память.");
                if (int.TryParse(Console.ReadLine(), out int points) && points > 0 && points < 20 && points <= totalAttriburePoints)
                {
                    primaryAttributes.Intelligence = points;
                    totalAttriburePoints -= points;
                    Console.WriteLine($"Осталось {totalAttriburePoints} очков.");
                }

                else if (points < totalAttriburePoints)
                    Console.WriteLine("Ошибка ввода. Введите число от 1 до 20.");
                else Console.WriteLine("Недостаточно очков.");
            }
            while (primaryAttributes.Intelligence == 0);

            do
            {
                Console.WriteLine("Введите показатель Восприятия. Отвечает за эффективность использования органов чувств. Напрямую влияет на точность и реакцию.");
                if (int.TryParse(Console.ReadLine(), out int points) && points > 0 && points < 20 && points <= totalAttriburePoints)
                {
                    primaryAttributes.Perception = points;
                    totalAttriburePoints -= points;
                    Console.WriteLine($"Осталось {totalAttriburePoints} очков.");
                }

                else if (points < totalAttriburePoints)
                    Console.WriteLine("Ошибка ввода. Введите число от 1 до 20.");
                else Console.WriteLine("Недостаточно очков.");
            }
            while (primaryAttributes.Perception == 0);

            do
            {
                Console.WriteLine("Введите показатель Воли. Отвечает за ментальную стойкость, в различной степени влияет на большинство ваших действий.");
                if (int.TryParse(Console.ReadLine(), out int points) && points > 0 && points < 20 && points <= totalAttriburePoints)
                {
                    primaryAttributes.Willpower = points;
                    totalAttriburePoints -= points;
                    Console.WriteLine($"Осталось {totalAttriburePoints} очков.");
                }

                else if (points < totalAttriburePoints)
                    Console.WriteLine("Ошибка ввода. Введите число от 1 до 20.");
                else Console.WriteLine("Недостаточно очков.");
            }
            while (primaryAttributes.Willpower == 0);

            if (totalAttriburePoints > 0) Console.WriteLine("Вы распределили не все очки характеристик. Повторите ввод."); //заменить на распределение оставшихся очков. Также добавить предложение повторить распределение.
        }

        return new Character(name, primaryAttributes, homeLocation);
    }

    public void Action(string action)
    {

    }

    public void ChangeLocation(Location targetLocation)
    {
        this.CurrentLocation = targetLocation;
        Console.WriteLine($"Вы входите в локацию {targetLocation}");

        targetLocation.GenerateNeighbors();
    }

    public static void LootLocation()
    {
        
    }

    public override void Die()
    {
        CurrentLocation = HomeLocation;
        //Потеря инвентаря
        //Потеря части очков опыта. Потеря уровня?
    }

    public override void ShowInfo()
    {
        base.ShowInfo();

        Console.WriteLine($"CurrentLocation: {CurrentLocation}");
        Console.WriteLine();
    }
}
