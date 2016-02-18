using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_of_Doom
{
    class weapons : Item
    {
        public weapons(string name, int weight, int damage): base (name, weight)
        {
            AttackDamage = damage;
        }

        public int AttackDamage { get; set; }
    }
}
