using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Flakcore.Utils
{
    class Util
    {
        public static int RandomPositiveNegative()
        {
            int number = new Random().Next(0, 2);
            Console.Write(number);
            if (number == 1)
                return 1;
            else
                return -1;
        }
    }
}
