using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Dungeons_of_Doom
{
    class Monster : Character, ILuggable
    {
        public static int monsterAmount { get; set; }
        public Monster(string name, int weight) : base(name, monsterHealth(), monsterDamage(), weight)
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
        static int monsterDamage()
        {
            int dmg = 0;
            return dmg = RandomUtils.GetRandom(1 * Game.difficulty, 3 * Game.difficulty);
        }
        static int monsterHealth()
        {
            int health = 0;
            return health = RandomUtils.GetRandom(8 * Game.difficulty, 15 * Game.difficulty);
        }

        public void PickUp(Player player)
        {
            player.BackPack.Add(this);
        }
    }
}
