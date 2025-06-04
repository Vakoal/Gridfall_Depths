using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gridfall_Depths.Items
{
    internal class Medicine: Item
    {
        public int HealingPower { get; set; }

        public Medicine(string name, double weight, int heal) : base()
        {
            Name = name;
            Weight = weight;
            HealingPower = heal;
        }

        public static Medicine Bandage { get; set; } = new Medicine("Bandage", 0.2, 10);
        public static Medicine FirstAidKit { get; set; } = new Medicine("First aid kit", 2, 100);
        public static Medicine Vitamins { get; set; } = new Medicine("Vitamins", 0.5, 25);
        public static Medicine Antibiotics { get; set; } = new Medicine("Antibiotics", 0.5, 50);

        public override string ToString()
        {
            return base.ToString() + $" | Heal: {HealingPower}";
        }
    }
}
