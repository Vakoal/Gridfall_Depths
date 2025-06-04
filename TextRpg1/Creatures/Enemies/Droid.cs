using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gridfall_Depths.Locations;

namespace Gridfall_Depths.Creatures.Enemies
{
    internal class ScoutDroid : Enemy
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
        public ScoutDroid()
        {

        }
        public ScoutDroid(Location homeLocation) : base(homeLocation)
        {
            PrimaryAttributes.Strength = 4;
            PrimaryAttributes.Agility = 12;
            PrimaryAttributes.Endurance = 3;
            PrimaryAttributes.Intelligence = 5;
            PrimaryAttributes.Perception = 14;
            PrimaryAttributes.Willpower = -1;

            Health = MaxHealth;

            RarityEffects();
        }
    }

    internal class BattleDroid : Enemy
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
        public BattleDroid()
        {

        }
        public BattleDroid(Location homeLocation) : base(homeLocation)
        {
            PrimaryAttributes.Strength = 7;
            PrimaryAttributes.Agility = 15;
            PrimaryAttributes.Endurance = 8;
            PrimaryAttributes.Intelligence = 11;
            PrimaryAttributes.Perception = 18;
            PrimaryAttributes.Willpower = -1;

            Health = MaxHealth;

            RarityEffects();
        }
    }
}
