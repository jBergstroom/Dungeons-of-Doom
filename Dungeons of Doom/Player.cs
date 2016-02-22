using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_of_Doom
{
    sealed class Player : Character
    {
        public Player(string name, int health, int ad) : base(name, health, ad)
        {
            BackPack = new List<Item>();
        }

        public int X { get; set; }
        public int Y { get; set; }
        public List<Item> BackPack { get; set; }

        public override void Hit(Character opponent)
        {


            Random rng = new Random(); ;

            
            int hitVal = rng.Next(1, 21);
            if (hitVal > 10)
            {
                int damage = rng.Next((this.AttackDamage / 4), this.AttackDamage + 1);
                
                Console.WriteLine($"You rolled {hitVal}! You strike the monster for {damage}!");
                opponent.Health -= damage;
                Console.WriteLine($" {opponent.Name} has {opponent.Health} HP left.");
            }
            else
            {
                Console.WriteLine($"You rolled {hitVal}. You miss!");
            }



        }

    }
}
