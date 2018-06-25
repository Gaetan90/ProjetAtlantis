using SimlationDevices.ServiceReferenceDevice;
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
            /*for (int i = 0; i < 10; i++)
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
            }*/
            // Setup the input
            var input = new StringReader(Document);
            var text = new StreamReader(@"D:\swaggeratlantis.yaml");
            Console.WriteLine("Contents of WriteText.txt = {0}", text);

            // Load the stream
            var yaml = new YamlStream();
            yaml.Load(text);

            // Examine the stream
            var mapping =
                (YamlMappingNode)yaml.Documents[0].RootNode;

            foreach (var entry in mapping.Children)
            {
                Console.WriteLine("Mapping " +((YamlScalarNode)entry.Key).Value);
            }

            // List all the items
            var items = (YamlSequenceNode)mapping.Children[new YamlScalarNode("info")];
            foreach (YamlMappingNode item in items)
            {
                Console.WriteLine(
                    "{0}\t{1}",
                    item.Children[new YamlScalarNode("name")],
                    item.Children[new YamlScalarNode("description")]
                );
            }


            Console.WriteLine("End process");
            Console.Read();
        }
        private const string Document = @"---
            receipt:    Oz-Ware Purchase Invoice
            date:        2007-08-06
            customer:
                given:   Dorothy
                family:  Gale

            items:
                - part_no:   A4786
                  descrip:   Water Bucket (Filled)
                  price:     1.47
                  quantity:  4

                - part_no:   E1628
                  descrip:   High Heeled ""Ruby"" Slippers
                  price:     100.27
                  quantity:  1

            bill-to:  &id001
                street: |
                        123 Tornado Alley
                        Suite 16
                city:   East Westville
                state:  KS

            ship-to:  *id001

            specialDelivery:  >
                Follow the Yellow Brick
                Road to the Emerald City.
                Pay no attention to the
                man behind the curtain.
...";

        public static void test()
        {
            ServiceDeviceClient service = new ServiceDeviceClient();
            ICollection<DeviceView> devices = service.GetAllDevice();
            Console.Read();
        }
    }
}
