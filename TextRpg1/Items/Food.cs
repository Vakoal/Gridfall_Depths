using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TextRpg1.Randomizer;

namespace TextRpg1.Items
{
    internal class Food : Item
    {
        public int Satiety { get; set; }
        public int ThirstQuench { get; set; }

        public Food(string name, double weight, int satiety, int thirstQuench) : base()
        {
            Name = name;
            Weight = weight;
            Satiety = satiety;
            ThirstQuench = thirstQuench;
        }

        public static Food RawMeat { get; set; } = new Food("RawMeat", 1, 3, 1);
        public static Food Water { get; set; } = new Food("Water", 1, 0, 5);
        public static Food Coffee { get; set; } = new Food("Coffee", 1, 1, 2);
        public static Food MRE { get; set; } = new Food("MRE", 3, 12, 4);
        public static Food CannedFood { get; set; } = new Food("CannedFood", 2, 5, 1);
        public static Food CandyBar { get; set; } = new Food("CandyBar", 0.2, 1, 0);

        public override string ToString()
        {
            return base.ToString() + $" | Satiety: {Satiety} | Thirst quench: {ThirstQuench}";
        }
    }
}
