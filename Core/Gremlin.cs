using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utils;

namespace Dungeons_of_Doom
{
    class Gremlin : Monster, ILuggable
    {
        public Gremlin(string name, int weight) : base(name, weight)
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
        void GremlinDamage()
        {
            AttackDamage = RandomUtils.GetRandom(1 * Game.difficulty, 3 * Game.difficulty);
        }
        void GremlinHealth()
        {
            Health = RandomUtils.GetRandom(4 * Game.difficulty, 8 * Game.difficulty);
        }
        public override int Health
        {
            get
            {
                return base.Health;
            }

            set
            {
                base.Health = value;
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
                base.AttackDamage = value;
            }
        }
    }
}
