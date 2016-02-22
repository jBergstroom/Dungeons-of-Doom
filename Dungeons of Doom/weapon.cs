using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_of_Doom
{
    class Weapon : Item
    {
        public Weapon(string name, int weight, int damage): base (name, weight)
        {
            AttackDamage = damage;
        }

        public int AttackDamage { get; set; }

        public override void ModifyPlayer(Character player)
        {
            player.AttackDamage += 10;
        }
    }
}
