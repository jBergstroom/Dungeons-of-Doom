using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_of_Doom
{
    class Gremlin : Monster
    {
        public Gremlin(string name) : base(name)
        {

        }

        public override string Hit(Character opponent)
        {
            if (opponent.AttackDamage >= this.AttackDamage*2)
            {
                this.Health = 0;
                return $"The gremlin dies of fear";
            }
            else { return base.Hit(opponent); }
        }
        int gremlinDamage()
        {
            int dmg = 0;
            return dmg = RandomUtils.GetRandom(1 * Game.difficulty, 3 * Game.difficulty);
        }
        int GremlinHealth()
        {
            int health = 0;
            return health = RandomUtils.GetRandom(4 * Game.difficulty, 8 * Game.difficulty);
        }
        public override int Health
        {
            get
            {
                return base.Health;
            }

            set
            {
                base.Health = GremlinHealth();
            }
        }
        public override int AttackDamage
        {
            get
            {
                return base.AttackDamage;
            }

            set
            {
                base.AttackDamage = gremlinDamage();
            }
        }
    }
}
