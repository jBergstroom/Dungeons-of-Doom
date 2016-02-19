using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_of_Doom
{
    class Room
    {
        public Monster MonsterInRoom { get; set; }
        public Item ItemInRoom { get; set; }

        public bool discovered { get; set; }
    }
}
