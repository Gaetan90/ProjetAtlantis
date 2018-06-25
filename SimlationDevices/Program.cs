using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;

namespace SimlationDevices
{
    class Program
    {
        public static void Main(string[] args)
        {
            //ServiceReferenceDevice.ServiceDeviceClient service = new ServiceReferenceDevice.ServiceDeviceClient();

            //ServiceReferenceDevice.Metric metric = service.GetMetric();

            
            Console.WriteLine("Begin process");
            for (int i = 0; i < 10; i++)
            {
                bool presenceSensor = new Random().Next(100) % 2 == 0;
                Console.WriteLine("Presence Sensor : " + presenceSensor);

                var temperatureSensor = 1;
                Console.WriteLine("Presence Sensor : " + temperatureSensor);

                double lightSensor = 1;
                Console.WriteLine("Light Sensor : " + lightSensor);

                double atmosphericPressureSensor = 1;
                Console.WriteLine("Atmospheric Pressure Sensor : " + atmosphericPressureSensor);

                double humiditySensor = 1;
                Console.WriteLine("Humidity Sensor : " + humiditySensor);

                double soundLevelSensor = 1;
                Console.WriteLine("Sound Level Sensor : " + soundLevelSensor);

                double gpsSensor = 1;
                Console.WriteLine("GPS Sensor : " + gpsSensor);

                double co2LevelSensor = 1;
                Console.WriteLine("CO2 Level Sensor : " + co2LevelSensor);

                double ledCommand = 1;
                Console.WriteLine("Led Command : " + ledCommand);

                double beeperCommand = 1;
                Console.WriteLine("Beeper Command : " + beeperCommand);
                Console.WriteLine(" ");
                Console.WriteLine(" ");

                System.Threading.Thread.Sleep(1000);
            }
            
            Console.WriteLine("End process");
            Console.Read();
        }
    }
}
