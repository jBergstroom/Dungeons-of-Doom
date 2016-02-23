using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    internal class Room
    {
        public Monster MonsterInRoom { get; set; }
        public Item ItemInRoom { get; set; }

        public bool Discovered { get; set; }
        public bool Wall { get; set; }
    }
}
