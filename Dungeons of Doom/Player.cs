using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_of_Doom
{
    class Player : Character
    {
        public Player(string name, int health, int ad):base (name, health, ad)
        {
            BackPack = new List<Item>();
        }

        public int X { get; set; }
        public int Y { get; set; }
        public List<Item> BackPack { get; set; }

    }
}
