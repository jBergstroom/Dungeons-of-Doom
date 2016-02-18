using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_of_Doom
{
    class Item
    {
        public string Name { get; set; }
        public int Weight { get; set; }

        public Item(string name, int weight)
        {
            Name = name;
            Weight = weight;
        }
    }
}
