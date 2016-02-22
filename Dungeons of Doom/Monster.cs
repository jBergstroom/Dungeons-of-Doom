using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_of_Doom
{
    class Monster : Character
    {
        public static int monsterAmount { get; set; }
        public Monster(string name, int health, int ad) : base(name, health, ad)
        {
            monsterAmount++;
        }

        public override string Hit(Character opponent)
        {

            
            int monsterHit = RandomUtils.GetRandom(1, 101);
            if (monsterHit > 70)
            {
                int monsterdamage = RandomUtils.GetRandom(opponent.AttackDamage / 2, opponent.AttackDamage);
                opponent.Health -= monsterdamage;
                return $"{this.Name} hits you back. {this.Name} hits you for {monsterdamage}";

            }
            else { return $"You are lucky. {this.Name} missed!"; }
        }
    }
}
