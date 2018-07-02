using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationDevices.Controller
{
    public class GetBoolean
    {
        public static bool GetRandomBoolean()
        {
            Random rnd = new Random();
            return rnd.Next(0, 2) == 0;
        }
    }
}
