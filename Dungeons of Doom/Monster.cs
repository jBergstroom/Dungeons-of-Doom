using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_of_Doom
{
    class Monster : Character
    {
        
        public Monster(string name, int health, int ad) : base(name, health, ad)
        {
            
        }

        public override void Hit(Character opponent)
        {

            Random rng = new Random();
            int monsterHit = rng.Next(1, 101);
            if (monsterHit > 70)
            {
                int monsterdamage = rng.Next(opponent.AttackDamage / 2, opponent.AttackDamage);
                Console.WriteLine($"{this.Name} hits you back. {this.Name} hits you for {monsterdamage}");
                opponent.Health -= monsterdamage;

            }
            else { Console.WriteLine($"You are lucky. {this.Name} missed!"); }
        }
    }
}
