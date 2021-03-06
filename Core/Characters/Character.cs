﻿using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    internal abstract class Character : GameObject
    {
        public Character(string name, int health, int ad, int weight): base(name)
        {
            Health = health;
            AttackDamage = ad;
            Weight = weight;
        }
        
        public virtual int Health { get; set; }
        public virtual int AttackDamage { get; set; }

        public abstract string Hit(Character opponent);

        

        public int Weight { get; set; }

    }
}
