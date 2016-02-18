using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_of_Doom
{
    class Potion : Item
    {
        public Potion(string name, int weight, int heal) : base(name, weight)
        {
            Heal = heal;
        }
        public int Heal { get; set; }
    }
}
