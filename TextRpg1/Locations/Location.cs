
using System.Linq;
using System.Reflection;
using TextRpg1.Creatures;
using TextRpg1.Creatures.Character;
using TextRpg1.Creatures.Enemies;

namespace TextRpg1.Locations;

internal class Location
{
    public bool IsDeadEnd = false;
    public string Name { get; set; }
    public double RarityMultiplier { get; set; } = 1;
    public LocationType Type { get; set; }
    public List<Location> Neighbors { get; set; }
    public List<string> Events { get; set; }
    public List<Creature> Habitants = new List<Creature>();


    public Location(string name, LocationType type)
    {
        Name = name;
        Type = type;
        Neighbors = new List<Location>();
        Events = new List<string>();

        GenerateHabitants();
    }

    public void GenerateNeighbors()
    {
        if(IsDeadEnd || Neighbors.Count > 1) return;

        List<LocationType> availableTypes = LocationType.AllLocationTypes.Except(new [] { LocationType.HomeBase, LocationType.FactionBase }).ToList();//Сделать отдельный постоянный список

        Random random = new Random();
        int neighborsCount = random.Next(Type.Size - 2, Type.Size + 2);
        neighborsCount = neighborsCount >= 1 ? neighborsCount : 1;

        for(int i = 0; i < neighborsCount; i++)
        {
            LocationType locationType = GenerateRandomNeighborType(availableTypes);
            Location newLocation = new Location(locationType.ToString(), locationType); //Добавить логику именования

            newLocation.Neighbors.Add(this);
            Neighbors.Add(newLocation);
        }

        //Making sure than all locations except entrance(at zero index) and exit(random) are dead ends
        int exitIndex = random.Next(1, Neighbors.Count + 1);
        for(int i = 1; i < Neighbors.Count; i++)
        {
            if(i == exitIndex) continue;
            Neighbors[i].IsDeadEnd = true;
        }
        Console.WriteLine($"Dead End: {IsDeadEnd}. Generated: {Neighbors.Count} neighbors for this {Name} location");
    }

    LocationType GenerateRandomNeighborType(List<LocationType> types) //Чем выше LocationType.frequency тем выше шанс выбора этого типа
    {
        List<LocationType> frequentTypes = new List<LocationType>();

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

    void GenerateHabitants()
    {
        List<Func<Enemy>> enemyConstructors = new List<Func<Enemy>>
        {
            () => new Rat(this),
            () => new MutatedRat(this)
        };

        Random random = new Random();
        int habitantsQuantity = random.Next(Type.Size * 2, Type.Size * 4);

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
            Console.WriteLine($"{i + 1}) {Neighbors[i].Name}");
        }
    }
    public override string ToString()
    {
        return Name.ToString();
    }
}
public class LocationType
{
    public string Name { get; set; }
    public int Frequency { get; set; } = 1;
    public int Size { get; set; } = 1;

    public LocationType(string type, int size, int frequency)
    {
        Name = type;
        Size = size;
        Frequency = frequency;
    }

    public static LocationType HomeBase { get; } = new LocationType("HomeBase", 3, 1);
    public static LocationType FactionBase { get; } = new LocationType("FactionBase", 1, 1);
    public static LocationType Tunnel { get; } = new LocationType("Tunnel", 2, 1);
    public static LocationType CollapsedTunnel { get; } = new LocationType("CollapsedTunnel", 2, 1);
    public static LocationType UtilityRoom { get; } = new LocationType("UtilityRoom", 1, 1);
    public static LocationType Outpost { get; } = new LocationType("Outpost", 2, 3);
    public static LocationType Town { get; } = new LocationType("Town", 4, 3);
    public static LocationType City { get; } = new LocationType("City", 5, 5);
    public static LocationType House { get; } = new LocationType("House", 1, 1);
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


