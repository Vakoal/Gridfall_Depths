using Newtonsoft.Json;
using Gridfall_Depths.Creatures.Character;
using Gridfall_Depths.Locations;
using static Gridfall_Depths.Randomizer;

namespace Gridfall_Depths.Creatures.Enemies;

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