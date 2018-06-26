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
using Newtonsoft.Json.Linq;

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
            Console.WriteLine("\nBegin Get Devices\n");

            var jsonUrlDevices = new WebClient().DownloadString("http://wcfwebservice.azurewebsites.net/Service.svc/devices/devices");
            //Console.WriteLine(jsonUrlDevices);
            var listJsonDevice = JsonConvert.DeserializeObject<List<RootObjectJsonUrl>>(jsonUrlDevices);
            //Console.WriteLine(listJsonDevice);
            string adressMacDevice = "";
            string idDevice = "";
            string nomDevice = "";
            string typeDevicesDevice = "";
            int i = 0;
            foreach (var urlresult in listJsonDevice)
            {
                i++;
                adressMacDevice = urlresult.adressMac;
                nomDevice = urlresult.nom;
                typeDevicesDevice = urlresult.typeDevices.name;
                Console.WriteLine($"Device {i} : ");
                Console.WriteLine($"Adresse Mac : {adressMacDevice}");
                Console.WriteLine($"Nom Device : {nomDevice}");
                Console.WriteLine($"Type Device : {typeDevicesDevice}");
                Console.WriteLine("");
                Console.WriteLine("");

            }
            Console.WriteLine("\nEnd Get Devices\n");

            Console.WriteLine("Begin metric sender");
            SendMetrics(adressMacDevice);
            Console.WriteLine("End metric sender");

            Console.Read();

            /*var values = new Dictionary<string, string>{
               { "result", "["+result+"]" },
               { "name", dataMetrics.First().metric.nameTypeDivice },
               { "description", "Description à affiner" }
            };
            HttpClient client = new HttpClient();
            var content = new FormUrlEncodedContent(values);
 
            var response = await client.PostAsync("http://www.example.com/recepticle.aspx", content);
 
            var responseString = await response.Content.ReadAsStringAsync();
            Console.Read();
             
             */
        }
        public static string GetuserDevices()
        {
            string userdevicelist ="";


            return userdevicelist;
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
            Console.WriteLine($"http://wcfwebservice.azurewebsites.net/Service.svc/devices/devices/{idDevice}");
            var httpWebRequest = (HttpWebRequest)WebRequest.Create($"http://wcfwebservice.azurewebsites.net/Service.svc/devices/devices/{idDevice}");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            /*
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
            }*/
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
