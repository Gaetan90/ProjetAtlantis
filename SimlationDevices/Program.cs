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
using System.Threading;

namespace SimlationDevices
{
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
            //string idDevice = "";
            string nomDevice = "";
            string typeDevicesDevice = "";
            

            int i = 0;
            List<RootObjectJsonUrl> listDevice = new List<RootObjectJsonUrl>();

            foreach (var urlresult in listJsonDevice)
            {
                i++;
                adressMacDevice = urlresult.adressMac;
                nomDevice = urlresult.nom;
                typeDevicesDevice = urlresult.typeDevices.name;
                listDevice.Add(new RootObjectJsonUrl
                {
                    adressMac = urlresult.adressMac,
                    nom = urlresult.nom,
                    typeDevices = urlresult.typeDevices
                });
                Console.WriteLine($"Device {i} : ");
                Console.WriteLine($"Adresse Mac : {adressMacDevice}");
                Console.WriteLine($"Nom Device : {nomDevice}");
                Console.WriteLine($"Type Device : {typeDevicesDevice}");
                Console.WriteLine("");
                Console.WriteLine("");
                
            }
            Console.WriteLine($"Nombre de devices : {i}");
            int j = 0;
            foreach (var deviceList in listDevice)
            {
                Thread[] deviceThreads = new Thread[i];
                deviceThreads[j] = new Thread(() => SendMetrics(deviceList.adressMac, deviceList.typeDevices.name));
                deviceThreads[j].Start();
            }

            Console.WriteLine("\nEnd Get Devices\n");
            Console.WriteLine("Begin metric sender");


            SendMetrics(adressMacDevice, typeDevicesDevice);
            Console.WriteLine("End metric sender");

            Console.Read();
        }
        public static string GetuserDevices()
        {
            string userdevicelist ="";
            //get les devices des users via une requete a définir avec gaetan

            return userdevicelist;
        }
        public static void SendMetrics(string idDevice, string deviceType)
        {
            string metricSend;
            //string metricDateDevice = "2018-06-26T07:44:09.981Z"; 
            string metricDateDevice = DateTime.UtcNow.ToString("o"); 
            string deviceTypeDevice = "gpsSensor"; 
            string metricValueDevice = "N;48;51;45,81;E;2;17;15,331";
            var newMetricSend = new Metrics
            {
                metricDate = metricDateDevice,
                deviceType = deviceTypeDevice,
                metricValue = metricValueDevice
            };
            var json = new JavaScriptSerializer().Serialize(newMetricSend);
            Console.WriteLine(json);
            Console.WriteLine($"http://wcfwebservice.azurewebsites.net/Service.svc/devices/device/{idDevice}/telemetry");
            var httpWebRequest = (HttpWebRequest)WebRequest.Create($"http://wcfwebservice.azurewebsites.net/Service.svc/devices/device/{idDevice}/telemetry");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Console.WriteLine(result);
            }
            Console.WriteLine(" ");
            System.Threading.Thread.Sleep(1000);
            metricSend = json;
            //return metricSend;
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
            service.saveMetrics("1", "{ 'metricDate': '2018/12/01',  'deviceType': '2',  'metricValue': ['30','20','10'] }");
            Console.Read();
        }

    }
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

}
