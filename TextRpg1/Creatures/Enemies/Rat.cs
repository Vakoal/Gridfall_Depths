using TextRpg1.Creatures.Character;
using TextRpg1.Locations;

namespace TextRpg1.Creatures.Enemies
{
    internal class Rat : Enemy
    {
        public Rat(Location homeLocation) : base(homeLocation)
        {
            PrimaryAttributes.Strength = 1;
            PrimaryAttributes.Agility = 14;
            PrimaryAttributes.Endurance = 1;
            PrimaryAttributes.Intelligence = 3;
            PrimaryAttributes.Perception = 12;
            PrimaryAttributes.Willpower = 1;

            RarityEffects();
        }
    }

    internal class MutatedRat : Rat
    {
        public MutatedRat(Location homeLocation) : base(homeLocation)
        {
            PrimaryAttributes.Strength = 3;
            PrimaryAttributes.Agility = 12;
            PrimaryAttributes.Endurance = 3;
            PrimaryAttributes.Intelligence = 4;
            PrimaryAttributes.Perception = 12;
            PrimaryAttributes.Willpower = 2;

            RarityEffects();
        }
    }
}
