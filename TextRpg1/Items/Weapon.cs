using Newtonsoft.Json;
using static Gridfall_Depths.Randomizer;

namespace Gridfall_Depths.Items
{
    internal class Weapon : Item
    {
        [JsonProperty]
        double _damage;
        [JsonIgnore]
        public double Damage
        {
            get { return Math.Round(_damage); }
            set
            {
                _damage = value >= 0.6 ? value : 0.6;
            }
        }
        public DamageTypes DamageType { get; set; }
        public WeaponTypes WeaponType { get; set; }
        [JsonConstructor]
        public Weapon()
        {
            
        }
        public Weapon(Rarity rarity, WeaponTypes weaponType, DamageTypes damageType) : base()
        {
            ItemRarity = rarity;
            DamageType = damageType;
            WeaponType = weaponType;

            double weaponTypeEffect = (int)weaponType * 0.5;
            double damageTypeEffect = (int)damageType * 0.2;
            double damageRandomizer = RandomFloat(-1, 1) / 4;


            Weight = (int)weaponType + 1 + RandomFloat(-1, 1) - (1 * RarityModifier(rarity));
            Damage = (3 + weaponTypeEffect + damageTypeEffect + damageRandomizer) * RarityModifier(rarity);

            /*for (int i = 1; i < Math.Abs((int)rarity * 2) + 1; i++)
            {
                switch (RandomInt(0, 1))
                {
                    case 0:
                        if (rarity == Rarity.Defective)
                            Damage -= 2;
                        else
                            Damage += 2;
                        break;

                    case 1:
                        if (rarity == Rarity.Defective)
                            Weight += 0.3;
                        else
                            Weight -= 0.3;
                        break;
                }
            }*/

            string rarityNamePart = ItemNames.RarityNameParts[ItemRarity][RandomInt(0, ItemNames.RarityNameParts[ItemRarity].Count - 1)];
            string damageTypeNamePart = ItemNames.DamageTypeNameParts[damageType][RandomInt(0, ItemNames.DamageTypeNameParts[damageType].Count - 1)];
            string weaponTypeNamePart = ItemNames.WeaponTypeNameParts[weaponType][RandomInt(0, ItemNames.WeaponTypeNameParts[weaponType].Count - 1)];
            Name = $"{rarityNamePart} {damageTypeNamePart} {weaponTypeNamePart}".Trim();
        }

        public override string ToString()
        {
            return base.ToString() + $" | Type: {DamageType} | Damage: {Damage}";
        }
        public void DisplayColored(bool IsInventoryMode = true)
        {
            Console.ForegroundColor = RarityColor(ItemRarity);
            Console.Write($"{Name}");
            Console.ResetColor();
            Console.Write($" | Weight: {Weight} | Type: {DamageType} | Damage: {Damage}");

            if (IsInventoryMode && IsEquipped)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(" | Equipped");
                Console.ResetColor();
            }
            else Console.WriteLine();
        }
    }
    public enum WeaponTypes
    {
        Pistol,
        Rifle,
        Shotgun,
        Automatic,
        Heavy
    }

    public enum DamageTypes
    {
        Ballistic,
        Corrosive,
        Electric,
        Thermal,
        Radiation
    }

}
