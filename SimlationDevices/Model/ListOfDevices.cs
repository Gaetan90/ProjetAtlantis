using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationDevices.Model
{
    public class ListOfDevices
    {
        public string addressMac { get; set; }
        public List<Employee> employees { get; set; }
        public int id { get; set; }
        public string name { get; set; }
        public TypeDevices typeDevices { get; set; }
    }
}
