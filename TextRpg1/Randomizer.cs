using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRpg1.Items;

namespace TextRpg1
{
    internal static class Randomizer
    {
        public enum Rarity
        {
            Defective = -4,
            Common = 1,
            Uncommon = 8,
            Rare = 14,
            Epic = 21,
            Legendary = 34
        }

        public static double RarityModifier(Rarity rarity)
        {
            return 1 + 0.1 * (int)rarity;
        }

        public static int RandomInt(int minValue, int maxValue)
        {
            Random random = new Random();
            return random.Next(minValue, maxValue + 1);
        }
        public static double RandomFloat(int minValue, int maxValue) //can go out of specified range. To fix
        {
            Random random = new Random();
            return random.Next(minValue, maxValue + 1) + random.NextDouble() - random.NextDouble();
        }
        public static double RandomPercent()
        {
            Random random = new Random();
            return random.Next(1, 10001) / 100.0;
        }
        public static Rarity RaritySelection(double rarityMultiplier = 1.0)
        {
            double r = RandomPercent() * rarityMultiplier;

            if      (r <=  5.0)              return Rarity.Defective;
            else if (r >   5.0 && r <= 65.0) return Rarity.Common;
            else if (r >  65.0 && r <= 90.0) return Rarity.Uncommon;
            else if (r >  90.0 && r <= 98.5) return Rarity.Rare;
            else if (r >  98.5 && r <= 99.7) return Rarity.Epic;
            else if (r >  99.7)              return Rarity.Legendary;

            else throw new Exception("Rarity selection error");
        }
        public static T GenerateRandomItem<T>(double rarityMultiplier = 1.0) where T: Item
        {
            Rarity rarity = RaritySelection(rarityMultiplier);

            if(typeof(T) == typeof(Weapon))
            {
                WeaponTypes weaponType = (WeaponTypes)RandomInt(0, 4);
                DamageTypes damageType = (DamageTypes)RandomInt(0, 4);

                return new Weapon(rarity, weaponType, damageType) as T;
            }
            if (typeof(T) == typeof(Armor))
            {
                ArmorTypes armorType;
                if (rarity == Rarity.Epic || rarity == Rarity.Legendary) { armorType = (ArmorTypes)RandomInt(0, 2); }
                else                                                     { armorType = (ArmorTypes)RandomInt(0, 1); }

                return new Armor(rarity, armorType) as T;
            }
            if(typeof(T) == typeof(Food))
            {
                switch (rarity)
                {
                    case Rarity.Defective:
                        return Food.Coffee as T;

                    case Rarity.Common:
                        return Food.RawMeat as T;

                    case Rarity.Uncommon:
                        return Food.Water as T;

                    case Rarity.Rare:
                        return Food.CandyBar as T;

                    case Rarity.Epic:
                        return Food.CannedFood as T;

                    case Rarity.Legendary:
                        return Food.MRE as T;

                    default:
                        return Food.RawMeat as T;
                }

            }
            if (typeof(T) == typeof(Medicine))
            {
                switch (rarity)
                {
                    case Rarity.Defective:
                        return Medicine.Vitamins as T;

                    case Rarity.Common:
                        return Medicine.Bandage as T;

                    case Rarity.Uncommon:
                        return Medicine.Antibiotics as T;

                    case Rarity.Rare:
                        return Medicine.Bandage as T;

                    case Rarity.Epic:
                        return Medicine.FirstAidKit as T;

                    case Rarity.Legendary:
                        return Medicine.Vitamins as T;

                    default:
                        return Medicine.Bandage as T;
                }
            }

            return null;
        }
        public static ConsoleColor RarityColor(Rarity rarity)
        {
            switch (rarity)
            {
                case Rarity.Defective:
                    return ConsoleColor.Gray;
                case Rarity.Common:
                    return ConsoleColor.White;
                case Rarity.Uncommon:
                    return ConsoleColor.Green;
                case Rarity.Rare:
                    return ConsoleColor.Blue;
                case Rarity.Epic:
                    return ConsoleColor.Magenta;
                case Rarity.Legendary:
                    return ConsoleColor.Yellow;

                default: return ConsoleColor.White;
            }
        }
    }
}
