using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimlationDevices
{
    class Program
    {
        public static void Main(string[] args)
        {
            
            
            Console.WriteLine("Begin process");
            for (int i = 0; i < 10; i++)
            {
                bool presenceSensor = new Random().Next(100) % 2 == 0;
                Console.WriteLine("Presence Sensor : " + presenceSensor);

                double temperatureSensor;
                Console.WriteLine("Presence Sensor : " + temperatureSensor);

                double lightSensor;
                Console.WriteLine("Light Sensor : " + lightSensor);

                double atmosphericPressureSensor;
                Console.WriteLine("Atmospheric Pressure Sensor : " + atmosphericPressureSensor);

                double humiditySensor;
                Console.WriteLine("Humidity Sensor : " + humiditySensor);

                double soundLevelSensor;
                Console.WriteLine("Sound Level Sensor : " + soundLevelSensor);

                double gpsSensor;
                Console.WriteLine("Presence Sensor : " + gpsSensor);

                double co2LevelSensor;
                Console.WriteLine("Presence Sensor : " + co2LevelSensor);

                double ledCommand;
                Console.WriteLine("Presence Sensor : " + ledCommand);

                double beeperCommand;
                Console.WriteLine("Presence Sensor : " + beeperCommand);

                System.Threading.Thread.Sleep(1000);
            }



            Console.WriteLine("End process");
            Console.Read();
        }
    }
}
