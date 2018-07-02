using SimlationDevices.ServiceReferenceDevice;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Threading;
using SimulationDevices.Model;
using SimulationDevices.Controller;

namespace SimulationDevices
{
    class Program
    {
        public static void Main(string[] args)
        {
            DevicesReceptions.GetListOfDevices();
        }
    }
}
