using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TextRpg1.Creatures;
using TextRpg1.Creatures.Character;
using static TextRpg1.Randomizer;

namespace TextRpg1.Items
{
    internal class Item
    {
        public bool IsTradable = true;
        public bool IsEquipped = false;

        public string Name { get; set; } = "generic";
        [JsonProperty]
        double _weight;
        [JsonIgnore]
        public double Weight
        {
            get { return Math.Round(_weight, 1); }
            set
            {
                _weight = value > 0 ? value : 0.1;
            }
        }
        [JsonProperty]
        public Rarity ItemRarity { get; set; } = Rarity.Common;

        public static List<Type> Types = Assembly.GetExecutingAssembly().GetTypes()
                                                 .Where(t => t.IsSubclassOf(typeof(Item))).ToList();
        public Item()
        {

        }

        public override string ToString()
        {
            if (IsEquipped) return $"{Name} | Weight: {Weight} | Equipped";
            else return $"{Name} | Weight: {Weight}";
        }
        internal void EquipOrUnequip(Character hero)
        {
            if (GetType() == typeof(Weapon))
            {
                if (hero.Equipment["Weapon"] == this)
                {
                    IsEquipped = false;
                    hero.Equipment["Weapon"] = null;
                    Console.WriteLine("Weapon unequipped");
                }
                else
                {
                    if (hero.Equipment["Weapon"] != null)
                        hero.Equipment["Weapon"].IsEquipped = false;

                    hero.Equipment["Weapon"] = this;
                    IsEquipped = true;

                    DisplayColoredName();
                    Console.WriteLine($" equipped");
                }
            }
            else if (GetType() == typeof(Armor))
            {
                if (hero.Equipment["Armor"] == this)
                {
                    IsEquipped = false;
                    hero.Equipment["Armor"] = null;
                    Console.WriteLine($"Armor unequipped");
                }
                else
                {
                    if (hero.Equipment["Armor"] != null)
                        hero.Equipment["Armor"].IsEquipped = false;

                    IsEquipped = true;
                    hero.Equipment["Armor"] = this;

                    DisplayColoredName();
                    Console.WriteLine($" equipped");
                }
            }
            else Console.WriteLine("You cannot equip this");
        }
        internal void ThrowAway(Character hero)
        {
            hero.Inventory.Remove(this);
            hero.CurrentLocation.Loot.Add(this);

            if      (this == hero.Equipment["Weapon"]) hero.Equipment["Weapon"] = null;
            else if (this == hero.Equipment["Armor"])  hero.Equipment["Armor"] = null;

            DisplayColoredName();
            Console.WriteLine(" is removed from inventory");
        }
        internal void Use(Character hero)
        {
            if (this is Food food)
            {
                hero.Inventory.Remove(this);
                hero.Hunger -= food.Satiety;
                hero.Thirst -= food.ThirstQuench;
                Console.WriteLine($"You used {Name}. Your hunger is sated by {food.Satiety} and your thirst quenched by {food.ThirstQuench}");
                Console.WriteLine($"Your current hunger is {hero.Hunger}. Your current thirst is {hero.Thirst}");
            }
            if (this is Medicine medicine)
            {
                hero.Inventory.Remove(this);
                hero.Health += medicine.HealingPower;
                Console.WriteLine($"You used {Name} and restored {medicine.HealingPower} health. Your current health is {hero.Health}/{hero.MaxHealth}");
            }
        }
        public void DisplayColoredName()
        {
            Console.ForegroundColor = RarityColor(ItemRarity);
            Console.Write($"{Name}");
            Console.ResetColor();
        }
    }

    internal static class ItemNames
    {
        public static Dictionary<Rarity, List<string>> RarityNameParts = new Dictionary<Rarity, List<string>>
        {
            {Rarity.Defective, new List<string> {"Defective", "Faulty", "Broken", "Flawed", "Malfunctioning"}},
            {Rarity.Common,    new List<string> {""}},
            {Rarity.Uncommon,  new List<string> {"Uncommon", "Fine", "Neat", "Well-made", "Accomplished"}},
            {Rarity.Rare,      new List<string> {"Unusual", "Rare", "Powerful", "Exceptional", "Outstanding"}},
            {Rarity.Epic,      new List<string> {"Epic", "Splendid", "Stupendous", "Regal", "Grand"}},
            {Rarity.Legendary, new List<string> { "Legendary", "Glorious", "Magnificent", "Heroic", "Grandiose"}}
        };

        public static Dictionary<DamageTypes, List<string>> DamageTypeNameParts = new Dictionary<DamageTypes, List<string>>
        {
            {DamageTypes.Ballistic, new List<string> {""}},
            {DamageTypes.Corrosive, new List<string> {"Corrosive", "Acid", "Chemical"}},
            {DamageTypes.Thermal,   new List<string> {"Thermal", "Laser", "Heat"}},
            {DamageTypes.Electric,  new List<string> { "Electric", "Charged", "Electrifying"}},
            {DamageTypes.Radiation, new List<string> { "Radioactive", "Emission", "Isotope", "Irradiated", "Gamma", "Neutron", "Plutonium"}}
        };
        public static Dictionary<WeaponTypes, List<string>> WeaponTypeNameParts = new Dictionary<WeaponTypes, List<string>>
        {
            {WeaponTypes.Pistol,    new List<string> {"Pistol", "Handgun", "Revolver"}},
            {WeaponTypes.Automatic, new List<string> {"Assault rifle", "Submachine gun"}},
            {WeaponTypes.Shotgun,   new List<string> { "Shotgun", "Blunderbuss"}},
            {WeaponTypes.Rifle,     new List<string> { "Rifle"}},
            {WeaponTypes.Heavy,     new List<string> { "Flamethrower", "Launcher", "Machine gun"}}
        };

        public static Dictionary<ArmorTypes, List<string>> ArmorTypeNameParts = new Dictionary<ArmorTypes, List<string>>
        {
            {ArmorTypes.Synthetic,    new List<string> {"Cloth", "Synthetic", "Fiber"}},
            {ArmorTypes.Metal, new List<string> {"Steel", "Alloy", "Metal"}},
            {ArmorTypes.Adaptive,   new List<string> { "Adaptive", "Nano"}}
        };
    }
}
