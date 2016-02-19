using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_of_Doom
{
    class Gremlin : Monster
    {
        public Gremlin(string name, int health, int ad) : base(name ,health, ad)
        {

        }

        public override void Hit(Character opponent)
        {
            if (opponent.AttackDamage >= this.AttackDamage*2)
            {
                Console.WriteLine($"The gremlin dies of fear");
                this.Health = 0;
            }
            else { base.Hit(opponent); }
        }
    }
}
