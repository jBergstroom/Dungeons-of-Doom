using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    internal class Potion : Item
    {
        public Potion(string name, int weight, int heal) : base(name, weight)
        {
            Heal = heal;
        }
        public int Heal { get; set; }

        public override void PickUp(Player player)
        {
            player.Health += 20;
        }
    }
}