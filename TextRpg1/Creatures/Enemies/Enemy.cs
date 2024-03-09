using System.Reflection;
using TextRpg1.Creatures.Character;
using TextRpg1.Locations;

namespace TextRpg1.Creatures.Enemies;

internal class Enemy : Creature
{
    public bool IsHostile = true;
    public readonly EnemyRarity Rarity = EnemyRarity.Common;

    public Enemy(Location homeLocation) : base(homeLocation)
    {
        Rarity = RaritySelection(homeLocation.RarityMultiplier);
    }

    public override void Die()
    {
        HomeLocation.Habitants.Remove(this);
        Console.WriteLine($"{this.Name} died");
    }

    public enum EnemyRarity
    {
        Defective = -4,
        Common = 1,
        Uncommon = 2,
        Rare = 4,
        Epic = 6,
        Legendary = 8
    }

    protected EnemyRarity RaritySelection(double rarityMultiplier)
    {
        Random random = new Random();
        double r = random.Next(1, 10001) / 100.0 * rarityMultiplier;

        if      (r <= 5.0)              return EnemyRarity.Defective;
        else if (r >  5.0 && r <= 65.0) return EnemyRarity.Common;
        else if (r > 65.0 && r <= 90.0) return EnemyRarity.Uncommon;
        else if (r > 90.0 && r <= 98.5) return EnemyRarity.Rare;
        else if (r > 98.5 && r <= 99.7) return EnemyRarity.Epic;
        else                            return EnemyRarity.Legendary;

    }

    protected void RarityEffects()
    {
        int numberOfProperties = typeof(PrimaryAttributes).GetProperties().Length;
        Random random = new Random();

        for (int i = 1; i < (Math.Abs((int)Rarity) + 1); i++)
        {
            if (Rarity == EnemyRarity.Defective)
                PrimaryAttributes[random.Next(0, numberOfProperties)] -= 1;
            else
                PrimaryAttributes[random.Next(0, numberOfProperties)] += 1;
        }
    }

    static void GenerateName()
    {
        
    }

    public override void ShowInfo()
    {
        base.ShowInfo();

        Console.WriteLine($"Rarity: {Rarity}");
        Console.WriteLine();
    }
}