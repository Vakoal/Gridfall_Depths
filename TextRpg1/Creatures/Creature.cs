using System.Reflection;
using TextRpg1.Creatures.Character;
using TextRpg1.Locations;

namespace TextRpg1.Creatures;

internal abstract class Creature
{
    public string Name { get; set; } = "Unknown";

    int health = 1;
    public int Health
    {
        get { return health; } 
        set
        {
            if (value <= 0)
            {
                health = 0;
                Die();
            } 
            else health = value;
        }
    }
    public int Attack { get; set; } = 1;
    public int Damage { get; set; } = 1;
    private int _defense = 1;
    public int Defense
    {
        get
        {
            return _defense + PrimaryAttributes.Agility / 2;
        }

        private set { _defense = value; }
    }
    public int Armor { get; set; }
    private int _accuracy = 1;
    public int Accuracy
    {
        get
        {
            return _accuracy + PrimaryAttributes.Perception / 2 + PrimaryAttributes.Agility / 3;
        }

        private set { _accuracy = value; }
    }
    public Location HomeLocation { get; set; }
    public PrimaryAttributes PrimaryAttributes { get; set; } = new();

    public Creature(Location homeLocation)
    {
        HomeLocation = homeLocation;
    }
    public Creature(string name, PrimaryAttributes primaryAttributes, Location homeLocation) :this(homeLocation)
    {
        Name = name;
        PrimaryAttributes = primaryAttributes;
    }

    public Creature(string name, int health, int attack, int damage, int defense, int accuracy, PrimaryAttributes primaryAttributes, Location homeLocation) : this(name, primaryAttributes, homeLocation)
    {
        Health = health;
        Attack = attack;
        Damage = damage;
        Defense = defense;
        Accuracy = accuracy;
    }

    public virtual void ShowInfo()
    {

        Console.WriteLine($"Name: {Name}");
        Console.WriteLine($"Health: {Health}");
        Console.WriteLine($"Attack: {Attack}");
        Console.WriteLine($"Damage: {Damage}");
        Console.WriteLine($"Defense: {Defense}");
        Console.WriteLine($"Accuracy: {Accuracy}");
        Console.WriteLine($"Home Location: {HomeLocation}");
        Console.WriteLine();

        PrimaryAttributes.ShowInfo();
    }

    public void AttackEnemy(Creature enemy)
    {
        Random random = new Random();
        if(Accuracy + random.Next(-5,6) > enemy.Defense)
        {
            int damage = Math.Max(1, Attack - enemy.Armor);
            enemy.Health -= damage;
            Console.WriteLine($"{Name} атакует {enemy.Name} и наносит {damage} урона.");
        }
        else
        {
            Console.WriteLine($"{Name} промахивается по {enemy.Name}.");
        }
        
    }
    
    public abstract void Die();
}
