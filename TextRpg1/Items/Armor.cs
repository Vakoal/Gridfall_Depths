using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static TextRpg1.Randomizer;
using static TextRpg1.UI.UI;

namespace TextRpg1.Items
{
    internal class Armor : Item
    {
        [JsonProperty]
        private double _protectivePower;
        [JsonIgnore]
        public double ProtectivePower
        {
            get => Math.Round(_protectivePower);
            set => _protectivePower = value > 0 ? value : 0.6;
        }

        public ArmorTypes ArmorType { get; set; }

        [JsonConstructor]
        public Armor()
        {
            
        }
        public Armor(Rarity rarity, ArmorTypes armorType) : base()
        {
            ItemRarity = rarity;
            ArmorType = armorType;

            double armorTypeEffect = (int)armorType * 0.4;
            double protectivePowerRandomizer = RandomFloat(-1, 1) / 4;


            ProtectivePower = (2 + armorTypeEffect + protectivePowerRandomizer) * RarityModifier(rarity);
            Weight = ((int)armorType + 1) * 4 + RandomFloat(-2, 2);


            /*
             * if (rarity == Rarity.Defective)
            {
                ProtectivePower -= (int)rarity * 2 - RandomInt(1, 5);
                Weight += (int)rarity + RandomInt(1, 5);
            }
            else
            {
                ProtectivePower += (int)rarity * 2 - RandomInt(1, 5);
                Weight -= (int)rarity + RandomInt(1, 5);
            }
            */

            string rarityNamePart = ItemNames.RarityNameParts[ItemRarity][RandomInt(0, ItemNames.RarityNameParts[ItemRarity].Count - 1)];
            string armorTypeNamePart = ItemNames.ArmorTypeNameParts[armorType][RandomInt(0, ItemNames.ArmorTypeNameParts[armorType].Count - 1)];
            Name = $"{rarityNamePart} {armorTypeNamePart} armor".Trim();
        }

        public override string ToString()
        {
            return base.ToString() + $" | Type: {ArmorType} | Protection: {ProtectivePower}";
        }

        public void DisplayColored(bool IsInventoryMode = true)
        {
            Console.ForegroundColor = RarityColor(ItemRarity);
            Console.Write($"{Name}");
            Console.ResetColor();
            Console.Write($" | Weight: {Weight} | Type: {ArmorType} | Protection: {ProtectivePower}");

            if (IsInventoryMode && IsEquipped)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(" | Equipped");
                Console.ResetColor();
            }

            Console.WriteLine();
        }
    }

    public enum ArmorTypes
    {
        Synthetic,
        Metal,
        Adaptive
    }
}