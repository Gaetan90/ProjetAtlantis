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

namespace SimlationDevices
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("\nBegin Get Devices\n");
            var jsonUrlDevices = new WebClient().DownloadString("http://wcfwebservice.azurewebsites.net/Service.svc/devices/devices");
            var listJsonDevice = JsonConvert.DeserializeObject<List<RootObjectJsonUrl>>(jsonUrlDevices);
            //Console.WriteLine(jsonUrlDevices);
            string adressMacDevice = "";
            string nomDevice = "";
            string typeDevicesDevice = "";
            int i = 0;
            List<RootObjectJsonUrl> listDevice = new List<RootObjectJsonUrl>();
            foreach (var urlresult in listJsonDevice)
            {
                i++;
                adressMacDevice = urlresult.addressMac;
                nomDevice = urlresult.name;
                typeDevicesDevice = urlresult.typeDevices.name;
                listDevice.Add(new RootObjectJsonUrl
                {
                    addressMac = urlresult.addressMac,
                    name = urlresult.name,
                    typeDevices = urlresult.typeDevices
                });
                Console.WriteLine($"Device : {i} ");
                Console.WriteLine($"Adresse Mac : {adressMacDevice}");
                Console.WriteLine($"Nom Device : {nomDevice}");
                Console.WriteLine($"Type Device : {typeDevicesDevice}");
                Console.WriteLine("");
            }
            Console.WriteLine("\nEnd Get Devices\n");
            Console.WriteLine($"Nombre de devices : {i}");
            int j = 0;
            Thread[] deviceThreads = new Thread[i];
            foreach (var deviceList in listDevice)
            {
                deviceThreads[j] = new Thread(() => SendMetrics(deviceList.addressMac, deviceList.typeDevices.name));
                deviceThreads[j].Start();
                j++;
            }
            Console.Read();
        }

        public static void SendMetrics(string idDevice, string deviceType)
        {
            Console.WriteLine($"BEGIN SEND METRICS : {idDevice} - {deviceType} Time : {DateTime.UtcNow.ToString("o")}");
            int countForSend = 0;
            List<Metrics> listOfMetrics = new List<Metrics>();
            var jsonMetric = "";
            for (int i = 0; i < 10000; i++)
            {
                countForSend++;
                //string metricDateDevice = "2018-06-26T07:44:09.981Z"; // ça doit ressembler à ça
                string metricDateDevice = DateTime.UtcNow.ToString("o");
                //string metricDateDevice = DateTime.Now.ToString("o"); 
                string deviceTypeDevice = deviceType;
                string metricValueDevice = "";

                switch (deviceTypeDevice)
                {
                    case ("presenceSensor"):
                        string presenceSensorResult = "";
                        if (GetRandomBoolean() == true) { presenceSensorResult = "1"; } else { presenceSensorResult = "0"; }
                        metricValueDevice = presenceSensorResult;
                        break;

                    case ("temperatureSensor"):
                        metricValueDevice = GetRandomDouble(19, 41).ToString();
                        break;

                    case ("brightnessSensor"):
                        metricValueDevice = GetRandomInt(0, 1001).ToString();
                        break;

                    case ("atmosphericPressureSensor"):
                        metricValueDevice = GetRandomDouble(900, 1101).ToString();
                        break;

                    case ("humiditySensor"):
                        metricValueDevice = GetRandomDouble(20, 90).ToString();
                        break;

                    case ("soundLevelSensor"):
                        metricValueDevice = GetRandomInt(60, 160).ToString();
                        break;

                    case ("gpsSensor"):
                        metricValueDevice = "N;48;51;45,81;E;2;17;15,331";
                        break;

                    case ("co2Sensor"):
                        metricValueDevice = GetRandomDouble(300, 600).ToString();
                        break;

                    case ("ledDevice"):
                        metricValueDevice = "ON";
                        break;

                    case ("beeperDevice"):
                        metricValueDevice = "ON";
                        break;

                    default:
                        metricValueDevice = "ERROR";
                        break;
                }

                var newMetricSend = new Metrics
                {
                    metricDate = metricDateDevice,
                    deviceType = deviceTypeDevice,
                    metricValue = metricValueDevice
                };
                listOfMetrics.Add(newMetricSend);
                /*if (deviceTypeDevice == "atmosphericPressureSensor" || deviceTypeDevice == "temperatureSensor")
                {
                    Console.WriteLine($"Device : {deviceTypeDevice} Value : {metricValueDevice}");
                }*/
                jsonMetric += new JavaScriptSerializer().Serialize(newMetricSend);

                if (countForSend == 1)
                {
                    MetricSend(jsonMetric, idDevice);
                    countForSend = 0;
                    jsonMetric = "";
                    listOfMetrics = new List<Metrics>();
                }
                Console.WriteLine($"End metric sender for {idDevice}");
                Thread.Sleep(1000);
            }
            Console.WriteLine($"END SEND METRICS : {idDevice} - {deviceType} Time : {DateTime.UtcNow.ToString("o")}");
        }
        public static void MetricSend(string json, string idDevice)
        {
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
                //Console.WriteLine($"result : {result}");
            }
        }
        public static bool GetRandomBoolean()
        {
            Random rnd = new Random();
            return rnd.Next(0, 2) == 0;
        }
        public static double GetRandomDouble(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
        public static int GetRandomInt(int minimum, int maximum)
        {
            Random random = new Random();
            return random.Next(minimum, maximum);
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
        public string addressMac { get; set; }
        public List<Employee> employees { get; set; }
        public int id { get; set; }
        public string name { get; set; }
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
