using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRpg1.Locations;

namespace TextRpg1.Creatures.Enemies
{
    internal class Bandit : Enemy
    {
        public override double Damage
        {
            get
            {
                return _damage + PrimaryAttributes.Perception + PrimaryAttributes.Intelligence / 2;
            }
            set
            {
                _damage = value;
            }
        }

        [JsonConstructor]
        public Bandit()
        {

        }
        public Bandit(Location homeLocation) : base(homeLocation)
        {
            PrimaryAttributes.Strength = 10;
            PrimaryAttributes.Agility = 10;
            PrimaryAttributes.Endurance = 10;
            PrimaryAttributes.Intelligence = 10;
            PrimaryAttributes.Perception = 10;
            PrimaryAttributes.Willpower = 10;

            Health = MaxHealth;

            RarityEffects();
        }
    }

    internal class Mutant : Enemy
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
        public Mutant()
        {

        }
        public Mutant(Location homeLocation) : base(homeLocation)
        {
            PrimaryAttributes.Strength = 14;
            PrimaryAttributes.Agility = 12;
            PrimaryAttributes.Endurance = 15;
            PrimaryAttributes.Intelligence = 6;
            PrimaryAttributes.Perception = 13;
            PrimaryAttributes.Willpower = 7;

            Health = MaxHealth;

            RarityEffects();
        }
    }
}
