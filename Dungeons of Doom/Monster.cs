using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_of_Doom
{
    class Monster
    {
        string[] monsterType = { "Orc", "Greywolf", "Dragon", "Undead", "Spiderqueen", "Spectre", "Dalek", "Cyberman", "Cyclope", "Patrik"};
        public Monster(int type, int health, int ad)
        {
            Name = monsterType[type];
            Health = health;
            AttackDamage = ad;
        }

        public string Name { get; set; }
        public int Health { get; set; }
        public int AttackDamage { get; set; }


    }
}
