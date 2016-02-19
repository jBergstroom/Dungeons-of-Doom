using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_of_Doom
{
    abstract class Character : GameObject
    {
        public Character(string name, int health, int ad): base(name)
        {
            Health = health;
            AttackDamage = ad;
        }
        
        public int Health { get; set; }
        public int AttackDamage { get; set; }

        public abstract void Hit(Character opponent);
        
    }
}
