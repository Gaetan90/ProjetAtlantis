using SimlationDevices.ServiceReferenceDevice;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;
using System.Web.Script.Serialization;


namespace SimlationDevices
{
    public class Device
    {
        public string id;
        public string name;
        public string deviceType;
    }
    public class Metrics
    {
        public string metricDate;
        public string deviceType;
        public string metricValue;
    }
    class Program
    {
        public static void Main(string[] args)
        {
            //ServiceReferenceDevice.ServiceDeviceClient service = new ServiceReferenceDevice.ServiceDeviceClient();
            //ServiceReferenceDevice.Metric metric = service.GetMetric();
            /*Console.WriteLine("Begin device creation");
            string idDevice;
            string nameDevice;
            string typeDevice;
            string objDevice;
            Console.WriteLine("Enter value of 'idDevice':");
            idDevice = Console.ReadLine();

            Console.WriteLine("Enter value of 'nameDevice':");
            nameDevice = Console.ReadLine();

            Console.WriteLine("Enter value of 'typeDevice':");
            typeDevice = Console.ReadLine();

            objDevice = AddDevice(idDevice, nameDevice, typeDevice);
            Console.WriteLine(objDevice);
            Console.WriteLine("End device creation");
            */
            

            Console.Read();
        }
        public static string SendMetrics()
        {
            string metricSend;
            metricSend = "";

            return metricSend;
        }
        public static string AddDevice(string idDevice, string nameDevice, string typeDevice)
        {
            string newdevice;
            var devicenew = new Device
            {
                id = idDevice,
                name = nameDevice,
                deviceType = typeDevice
            };
            var json = new JavaScriptSerializer().Serialize(devicenew);
            //Console.WriteLine(json);
            Console.WriteLine(" ");
            System.Threading.Thread.Sleep(1000);
            newdevice = json;
            return newdevice;
        }

        public static void test()
        {
            ServiceDeviceClient service = new ServiceDeviceClient();
            ICollection<DeviceView> devices = service.GetAllDevice();
            Console.Read();
        }

    }


}
