
using System.Linq;
using System.Reflection;
using TextRpg1.Creatures;
using TextRpg1.Creatures.Character;
using TextRpg1.Creatures.Enemies;
using TextRpg1.Items;
using static TextRpg1.Randomizer;

namespace TextRpg1.Locations;

internal class Location
{
    public bool IsDeadEnd = false;
    public bool IsLooted = false;
    public bool IsCleared = false;
    public bool IsVisited = false;

    public static int IdCounter { get; set; } = 1;

    public string Name { get; set; }
    public double RarityMultiplier { get; set; } = 1;
    public LocationType Type { get; set; }
    public List<Location> Neighbors { get; set; } = [];
    public List<string> Events { get; set; }
    public List<Creature> Habitants { get; set; } = [];
    public List<Item> Loot { get; set; } = [];

    public Location(string name, LocationType type)
    {
        Name = name;
        Type = type;
        Neighbors = new List<Location>();
        Events = new List<string>();

        GenerateLoot();
    }

    private void GenerateLoot()
    {
        int numberOfItems = (Type.Size + RandomInt(-1, +1) > 0 ? Type.Size + RandomInt(-1, +1) : 1);
        for (int i = 0; i < numberOfItems; i++)
        {
            Type randomItemType = Item.Types[RandomInt(0, Item.Types.Count() - 1)];
            var newItem = typeof(Randomizer).GetMethod("GenerateRandomItem")?.MakeGenericMethod(randomItemType).Invoke(null, [1.0]);
            Loot.Add(newItem as Item);
        }
    }

    public void GenerateNeighbors()
    {
        if(IsDeadEnd || Neighbors.Count > 1) return;

        List<LocationType> availableLocationTypes = LocationType.AllLocationTypes.Except(new[] { LocationType.HomeBase, LocationType.FactionBase }).ToList();//Сделать отдельный постоянный список

        int neighborsCount = RandomInt(Type.Size - 2, Type.Size + 2);
        neighborsCount = neighborsCount > 0 ? neighborsCount : 1;
        neighborsCount = neighborsCount > 4 ? 4 : neighborsCount;

        for (int i = 0; i <= neighborsCount; i++)
        {
            LocationType locationType = GenerateRandomNeighborType(availableLocationTypes);
            Location newLocation = new(locationType.ToString(), locationType); //Добавить логику именования

            newLocation.Neighbors.Add(this);
            Neighbors.Add(newLocation);
        }

        //Making sure than all locations except entrance(at zero index) and exit(random index) are dead ends
        int loopStart = this.Type == LocationType.HomeBase ? 0 : 1; //Home base doesn't have entrance location, so loop should start at 0 in that case
        int exitIndex = RandomInt(1, Neighbors.Count - 1);
        for(int i = loopStart; i < Neighbors.Count; i++)
        {
            if(i == exitIndex) continue;
            Neighbors[i].IsDeadEnd = true;
        }
    }

    static LocationType GenerateRandomNeighborType(List<LocationType> types) //The greater LocationType.frequency, the higher the chance of generating this location type
    {
        List<LocationType> frequentTypes = [];

        foreach(LocationType type in types)
        {
            for(int i = 0; i < type.Frequency; i++)
            {
                frequentTypes.Add(type);
            }
        }

        Random random = new Random();
        return frequentTypes[random.Next(0, frequentTypes.Count)];

    }
    public void GenerateHabitants()
    {
        List<Func<Enemy>> enemyConstructors =
        [
            () => new Rat(this),
            () => new MutatedRat(this)
        ];

        Random random = new Random();
        int habitantsQuantity = random.Next(Type.Size, Type.Size * 2);

        for (int i = 0; i < habitantsQuantity; i++)
        {
            var enemy = enemyConstructors[random.Next(0, enemyConstructors.Count)].Invoke();
            Habitants.Add(enemy);
        }
    }
    public void ShowNeighbors()
    {
        for(int i = 0; i < Neighbors.Count; i++)
        { 
            Console.WriteLine($"{i + 1}) {Neighbors[i]}");
        }
    }
    public override string ToString()
    {
        string result = Name.ToString();
        if (!IsDeadEnd) result += $" | Neighbors: {Neighbors.Count}";
        if (IsVisited) result += " | Visited";
        return result;
    }
    public static void DisplayAllLocations()
    {
        var allLocations = Assembly.GetExecutingAssembly().GetTypes()
                                   .Where(t => t.GetType() == typeof(Location)).ToList();
        foreach (var location in allLocations)
        {
            Console.WriteLine(location.Name);
        }
    }
}
public class LocationType(string type, int size, int frequency)
{
    public string Name { get; set; } = type;
    public int Frequency { get; set; } = frequency;
    public int Size { get; set; } = size;

    public static LocationType HomeBase { get; } = new LocationType("HomeBase", 3, 1);
    public static LocationType FactionBase { get; } = new LocationType("FactionBase", 1, 1);
    public static LocationType Tunnel { get; } = new LocationType("Tunnel", 2, 5);
    public static LocationType CollapsedTunnel { get; } = new LocationType("CollapsedTunnel", 2, 4);
    public static LocationType UtilityRoom { get; } = new LocationType("UtilityRoom", 1, 3);
    public static LocationType Outpost { get; } = new LocationType("Outpost", 2, 3);
    public static LocationType Town { get; } = new LocationType("Town", 4, 1);
    public static LocationType City { get; } = new LocationType("City", 5, 1);
    public static LocationType House { get; } = new LocationType("House", 1, 5);
    public static LocationType Factory { get; } = new LocationType("Factory", 2, 2);
    public static LocationType ResearchStation { get; } = new LocationType("ResearchStation", 2, 2);
    public static LocationType Caves { get; } = new LocationType("Caves", 3, 2);

    public static List<LocationType> AllLocationTypes = new List<LocationType>
    {
        HomeBase, FactionBase, Tunnel, UtilityRoom, Outpost,
        Town, City, House, Factory, ResearchStation, Caves
    };

    public override string ToString()
    {
        return Name.ToString();
    }
}


