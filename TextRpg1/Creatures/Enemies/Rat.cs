using Newtonsoft.Json;
using TextRpg1.Creatures.Character;
using TextRpg1.Locations;

namespace TextRpg1.Creatures.Enemies
{
    internal class Rat : Enemy
    {
        public override double Damage
        {
            get
            {
                return _damage + PrimaryAttributes.Strength + PrimaryAttributes.Agility / 2 + PrimaryAttributes.Intelligence / 3;
            }
            set
            {
                _damage = value;
            }
        }

        [JsonConstructor]
        public Rat()
        {

        }
        public Rat(Location homeLocation) : base(homeLocation)
        {
            PrimaryAttributes.Strength = 1;
            PrimaryAttributes.Agility = 14;
            PrimaryAttributes.Endurance = 1;
            PrimaryAttributes.Intelligence = 3;
            PrimaryAttributes.Perception = 12;
            PrimaryAttributes.Willpower = 1;

            Health = MaxHealth;

            RarityEffects();
        }
    }

    internal class MutatedRat : Rat
    {
        public override double Damage
        {
            get
            {
                return _damage + PrimaryAttributes.Strength + PrimaryAttributes.Agility / 2 + PrimaryAttributes.Intelligence / 3;
            }
            set
            {
                _damage = value;
            }
        }

        [JsonConstructor]
        public MutatedRat()
        {

        }
        public MutatedRat(Location homeLocation) : base(homeLocation)
        {
            PrimaryAttributes.Strength = 3;
            PrimaryAttributes.Agility = 12;
            PrimaryAttributes.Endurance = 3;
            PrimaryAttributes.Intelligence = 4;
            PrimaryAttributes.Perception = 12;
            PrimaryAttributes.Willpower = 2;

            Health = MaxHealth;

            RarityEffects();
        }
    }
}
