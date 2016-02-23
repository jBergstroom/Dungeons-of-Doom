using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    interface ILuggable
    {
        int Weight { get; set; }
        string Name { get; set; }
        void PickUp(Player player);
        
    }
}
