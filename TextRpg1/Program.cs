using TextRpg1;
using TextRpg1.Creatures.Character;
using TextRpg1.Items;
using TextRpg1.Locations;
using static TextRpg1.UI.UI;

Console.ResetColor();

int gameModeChoice = ShowMenu<string>(["New game", "Continue", "Items test"]);

Character hero;

if (gameModeChoice == 3)
{
    List<Item> TestItems = new();
    for (int i = 0; i < 10; i++) TestItems.Add(Randomizer.GenerateRandomItem<Weapon>(0.5));

    for (int i = 0; i < 10; i++) TestItems.Add(Randomizer.GenerateRandomItem<Weapon>(1));

    for (int i = 0; i < 10; i++) TestItems.Add(Randomizer.GenerateRandomItem<Weapon>(1.5));

    for (int i = 0; i < 10; i++) TestItems.Add(Randomizer.GenerateRandomItem<Weapon>(2));


    for (int i = 0; i < 10; i++) TestItems.Add(Randomizer.GenerateRandomItem<Armor>(0.5));

    for (int i = 0; i < 10; i++) TestItems.Add(Randomizer.GenerateRandomItem<Armor>(1));

    for (int i = 0; i < 10; i++) TestItems.Add(Randomizer.GenerateRandomItem<Armor>(1.5));

    for (int i = 0; i < 10; i++) TestItems.Add(Randomizer.GenerateRandomItem<Armor>(2));

    foreach (Item i in TestItems)
    {
        if (i is Armor armor)
        {
            armor.DisplayColored();
        }
        else if (i is Weapon weapon)
        {
            weapon.DisplayColored();
        }
    }
    Console.ReadLine();
}

if (gameModeChoice == 2) hero = Character.Load();

else
{
    Location homeBase = new("Homebase", LocationType.HomeBase);
    homeBase.GenerateNeighbors();

    hero = Character.CreateCharacter(homeBase);
}

List<CycleMode> menuItems = [CycleMode.LocationChange, CycleMode.Loot, CycleMode.Fight, CycleMode.OpenInventory];
while (hero is not null)
{
    if (hero.CurrentLocation.Type == LocationType.HomeBase && hero.IsDead)
    {
        hero.Health = hero.MaxHealth / 3;
        hero.IsDead = false;
        hero.Hunger = 0;
        hero.Thirst = 0;
    }
    
    ShowStatusInfo(hero);

    CycleMode currentCycleMode = menuItems[ShowMenu<CycleMode>(menuItems, "Choose your next action:") - 1];
    ShowStatusInfo(hero);

    switch (currentCycleMode)
    {
        case CycleMode.LocationChange:
            Location chosenLocation = hero.CurrentLocation.Neighbors[ShowMenu<Location>(hero.CurrentLocation.Neighbors, "Choose location to visit") - 1];
            ShowStatusInfo(hero);
            hero.ChangeLocation(chosenLocation);
            break;
        case CycleMode.Loot:
            hero.LootLocation();
            break;
        case CycleMode.Fight:
            hero.Fight();
            break;
        case CycleMode.OpenInventory:
            hero.OpenInventory();
            break;
        default:
            break;
    }

    hero.Save();
}

enum CycleMode
{
    LocationChange,
    Loot,
    Fight,
    OpenInventory,
}