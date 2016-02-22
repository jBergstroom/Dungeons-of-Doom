using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dungeons_of_Doom
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(100, 56);
            Game game = new Game();
            game.Start();
        }
    }
}
