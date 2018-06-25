using SimlationDevices.ServiceReferenceDevice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimlationDevices
{
    class Program
    {
        static void Main(string[] args)
        {

            Program.test();
            //Boolean presenceSensor = false;
            //double temperatureSensor = null;
            //double lightSensor = null;
            //double atmosphericPressureSensor = null;
            //double humiditySensor = null;
            //double soundLevelSensor = null;
            //double gpsSensor = null;
            //double co2LevelSensor = null;
            //double ledCommand = null;
            //double beeperCommand = null;
            //Console.WriteLine("Begin process");




            Console.WriteLine("End process");
        }

        public static void test()
        {
            ServiceDeviceClient service = new ServiceDeviceClient();
            ICollection<DeviceView> devices = service.GetAllDevice();
            Console.Read();
        }
    }
}
