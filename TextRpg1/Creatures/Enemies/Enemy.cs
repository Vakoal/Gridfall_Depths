using Newtonsoft.Json;
using System.Reflection;
using TextRpg1.Creatures.Character;
using TextRpg1.Items;
using TextRpg1.Locations;
using static TextRpg1.Randomizer;

namespace TextRpg1.Creatures.Enemies;

internal class Enemy : Creature
{
    [JsonIgnore]
    public override double Damage
    {
        get
        {
            return _damage + PrimaryAttributes.GetAverage();
        }
        set
        {
            _damage = value;
        }
    }
    public readonly Rarity Rarity = Rarity.Common;

    [JsonConstructor]
    public Enemy()
    {

    }
    public Enemy(Location homeLocation) : base(homeLocation)
    {
        Rarity = RaritySelection(homeLocation.RarityMultiplier);
    }

    public override void Die()
    {
        HomeLocation.Habitants.Remove(this);
        Console.WriteLine($"{Name} died");
    }

    protected void RarityEffects()
    {
        int numberOfProperties = typeof(PrimaryAttributes).GetProperties().Length;

        for (int i = 1; i < (Math.Abs((int)Rarity) + 1); i++)
        {
            if (Rarity == Rarity.Defective)
                PrimaryAttributes[RandomInt(0, numberOfProperties)] -= 1;
            else
                PrimaryAttributes[RandomInt(0, numberOfProperties)] += 1;
        }
    }
    public override void ShowInfo()
    {
        base.ShowInfo();

        Console.WriteLine($"Rarity: {Rarity}");
        Console.WriteLine();
    }
}