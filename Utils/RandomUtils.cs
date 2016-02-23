using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public static class RandomUtils
    {
        public static int GetRandom(int lower, int upper)
        {
            Random rand = new Random();
            int returnVal = rand.Next(lower, upper);
            return returnVal;
        }
    }
}
