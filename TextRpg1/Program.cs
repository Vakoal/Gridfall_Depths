using TextRpg1.Creatures.Character;
using TextRpg1.Locations;
using TextRpg1.UI;

Location homeBase = new Location("Homebase", LocationType.HomeBase);
homeBase.GenerateNeighbors();

Character hero = Character.CreateCharacter(homeBase);
hero.ShowInfo();


while (hero != null)
{
    CycleMode nextLoopMode = default;
    switch (UI.MenuChoice(hero, nextLoopMode))
    {
        case CycleMode.LocationChange:
            hero.ChangeLocation()
            break;
        case CycleMode.Loot:
            break;
        case CycleMode.Fight:
            break;
        case CycleMode.Dialogue:
            break;
        case CycleMode.Trade:
            break;
        default:
            break;
    }
}

enum CycleMode
{
    LocationChange,
    Loot,
    Fight,
    Dialogue,
    Trade
}