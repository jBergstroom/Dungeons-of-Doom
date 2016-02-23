using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_of_Doom
{
    sealed class Player : Character
    {
        public Player(string name, int health, int ad, int weight) : base(name, health, ad, weight)
        {
            BackPack = new List<ILuggable>();
        }

        public int X { get; set; }
        public int Y { get; set; }
        public List<ILuggable> BackPack { get; set; }

        public override string Hit(Character opponent)
        {
            int hitVal = RandomUtils.GetRandom(1, 21);
            if (hitVal > 10)
            {
                int damage = RandomUtils.GetRandom((this.AttackDamage / 4), this.AttackDamage + 1);
                
                opponent.Health -= damage;
                return $"You rolled {hitVal}! You strike the monster for {damage}! {opponent.Name} has {opponent.Health} HP left.";
            }
            else
            {
                return $"You rolled {hitVal}. You miss!";
            }
        }

    }
}
