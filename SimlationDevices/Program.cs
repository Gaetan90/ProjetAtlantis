using SimlationDevices.ServiceReferenceDevice;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.RepresentationModel;
using System.Web.Script.Serialization;
using System.Net;
using Newtonsoft.Json;

namespace SimlationDevices
{
    public class Employee
    {
        public int id { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
    }

    public class TypeDevices
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public class RootObjectJsonUrl
    {
        public string adressMac { get; set; }
        public List<Employee> employees { get; set; }
        public int id { get; set; }
        public string nom { get; set; }
        public TypeDevices typeDevices { get; set; }
    }
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
            //string jsonarray = "http://wcfwebservice.azurewebsites.net/Service.svc/devices/devices";
            //ServiceReferenceDevice.ServiceDeviceClient service = new ServiceReferenceDevice.ServiceDeviceClient();
            //ServiceReferenceDevice.Metric metric = service.GetMetric();
            var jsonUrlDevices = new WebClient().DownloadString("http://wcfwebservice.azurewebsites.net/Service.svc/devices/devices");
            var resultJsonUrl = JsonConvert.DeserializeObject<RootObjectJsonUrl>(jsonUrlDevices);
            string adressMac = "";
            foreach (var urlresult in resultJsonUrl.adressMac)
            {
                adressMac = urlresult.name.fr;
            }
            Console.WriteLine("Begin metric sender");
            string idDevice = "adressMac1";
            SendMetrics(idDevice);
            Console.WriteLine("End metric sender");
            
            Console.Read();
        }
        public static string SendMetrics(string idDevice)
        {
            string metricSend;
            string metricDateDevice = ""; 
            string deviceTypeDevice = ""; 
            string metricValueDevice = "";
            var newMetricSend = new Metrics
            {
                metricDate = metricDateDevice,
                deviceType = deviceTypeDevice,
                metricValue = metricValueDevice
            };
            var json = new JavaScriptSerializer().Serialize(newMetricSend);
            //Console.WriteLine(json);
            var httpWebRequest = (HttpWebRequest)WebRequest.Create($"http://wcfwebservice.azurewebsites.net/Service.svc/devices/devices/{idDevice}");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            Console.WriteLine(httpWebRequest.ToString());

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
                Console.WriteLine(streamWriter);

            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
            }
            Console.WriteLine(" ");
            System.Threading.Thread.Sleep(1000);
            metricSend = json;
            return metricSend;
        }

        public static void test()
        {
            ServiceDeviceClient service = new ServiceDeviceClient();
            ICollection<DeviceView> devices = service.GetAllDevice();
            Console.Read();
        }

    }


}
