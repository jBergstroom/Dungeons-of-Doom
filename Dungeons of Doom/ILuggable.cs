using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_of_Doom
{
    interface ILuggable
    {
        int Weight { get; set; }
        string Name { get; set; }
        //void PickUp();
    }
}
