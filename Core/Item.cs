using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_of_Doom
{
    abstract class Item : GameObject, ILuggable
    {

        public int Weight { get; set; }

        public Item(string name, int weight) : base(name)
        {
            Weight = weight;
        }

        //public abstract void ModifyPlayer(Character player);

        public virtual void PickUp(Player player)
        {
            player.BackPack.Add(this);
        }
    }
}
