using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gridfall_Depths.Creatures.Character;
using Gridfall_Depths.Locations;

namespace Gridfall_Depths.Creatures
{
    internal class NPC : Creature
    {
        public bool IsSpeakable = false;
        public bool IsTradable = false;
        public NPC(string name, PrimaryAttributes primaryAttributes, Location homeLocation) : base(name, primaryAttributes, homeLocation)
        {
            CurrentLocation = homeLocation;
        }
        public override void Die()
        {

        }

        internal void Dialogue()
        {
            throw new NotImplementedException();
        }
    }
}
