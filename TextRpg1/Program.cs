using Gridfall_Depths.Creatures.Character;
using Gridfall_Depths.Locations;
using static Gridfall_Depths.UI.UI;

Console.ResetColor();

int gameModeChoice = ShowMenu<string>(["New game", "Continue"]);

Character hero;


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