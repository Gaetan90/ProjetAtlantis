using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationDevices.Controller
{
    class GetInt
    {
        public static int GetRandomInt(int minimum, int maximum)
        {
            Random random = new Random();
            return random.Next(minimum, maximum);
        }
    }
}
