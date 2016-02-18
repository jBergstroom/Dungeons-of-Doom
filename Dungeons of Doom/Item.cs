using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_of_Doom
{
    class Item : GameObject
    {

        public int Weight { get; set; }

        public Item(string name, int weight) : base(name)
        {
            Weight = weight;
        }
    }
}
