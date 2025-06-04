namespace Gridfall_Depths.Creatures;

using Gridfall_Depths.Creatures.Character;
using Gridfall_Depths.Items;
using Gridfall_Depths.Locations;
using static Gridfall_Depths.UI.UI;
using static Randomizer;
using Newtonsoft.Json;

internal abstract class Creature
{
    public bool IsDefended { get; set; } = false;

    public string Name { get; set; } = "Unknown";
    [JsonIgnore]
    public double MaxHealth
    {
        get
        {
            return PrimaryAttributes.Endurance * 15;
        }
    } // add checking for health complying with MaxHealth
    [JsonProperty]
    double _health = 1;
    [JsonIgnore]
    public virtual double Health 
    {
        get { return Math.Round(_health); }
        set
        {
            if (value <= 0)
            {
                _health = 1;
                Die();
            }
            else if (value > MaxHealth)
            {
                _health = MaxHealth;
            }
            else _health = value;
        }
    }
    [JsonProperty]
    protected double _damage = 1;
    public virtual double Damage { get; set; } = 1;
    [JsonProperty]
    double _defense = 1;
    [JsonIgnore]
    public double Defense
    {
        get
        {
            if (IsDefended) return _defense + PrimaryAttributes.Agility / 2 + PrimaryAttributes.Intelligence / 2; // change "Defended" bonus to percentage
            else return _defense + PrimaryAttributes.Agility / 2;
        }

        private set { _defense = value; }
    }
    protected double _armor = 0;
    public virtual double Armor { get => _armor; set => _armor = value; }
    [JsonProperty]
    double _attack = 1;
    [JsonIgnore]
    public double Attack
    {
        get
        {
            return _attack + PrimaryAttributes.Perception / 2 + PrimaryAttributes.Agility / 2; //add dependence on strength for melee?
        }

        private set { _attack = value; }
    }
    public Location HomeLocation;
    public Location CurrentLocation;
    public PrimaryAttributes PrimaryAttributes { get; set; } = new();
    public List<Item> Inventory { get; set; } = [];

    [JsonConstructor]
    public Creature()
    {

    }
    public Creature(Location homeLocation)
    {
        HomeLocation = homeLocation;
        CurrentLocation = homeLocation;
    }
    public Creature(string name, PrimaryAttributes primaryAttributes, Location homeLocation) : this(homeLocation)
    {
        Name = name;
        PrimaryAttributes = primaryAttributes;
        Health = MaxHealth;
    }

    public virtual void ShowInfo()
    {

        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Health: {Health}");
        Console.WriteLine($"Damage: {Damage}");
        Console.WriteLine($"Defense: {Defense}");
        Console.WriteLine($"Accuracy: {Attack}");
        Console.WriteLine($"Home Location: {HomeLocation}");
        Console.WriteLine();

        PrimaryAttributes.ShowInfo();
    }
    public void AttackEnemy(Creature enemy)
    {
        if (Attack + RandomInt(-4,4) > enemy.Defense)
        {
            double resultingDamage = Math.Clamp(Damage - enemy.Armor, 1, 999);
            enemy.Health -= resultingDamage;

            MessagesStack.Add(($"{Name} attacks {enemy.Name} and deals {resultingDamage} damage.", ConsoleColor.Red));
        }
        else
        {
            MessagesStack.Add(($"{Name} fails to hit the {enemy.Name}.", ConsoleColor.Red));
        }
        MessagesStack.Add(($"{enemy.Name}: {enemy.Health}/{enemy.MaxHealth} HP", ConsoleColor.Red));
    }   
    public abstract void Die();
}
