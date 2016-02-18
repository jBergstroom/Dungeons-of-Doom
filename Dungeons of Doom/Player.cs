using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_of_Doom
{
    class Player
    {
        public Player(string name, int health, int ad)
        {
            Name = name;
            Health = health;
            AttackDamage = ad;
            BackPack = new List<Item>();
        }

        public string Name { get; set; }
        public int Health { get; set; }
        public int AttackDamage { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public List<Item> BackPack { get; set; }

    }
}
