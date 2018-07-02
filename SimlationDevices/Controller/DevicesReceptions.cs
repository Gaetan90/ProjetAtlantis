using Newtonsoft.Json;
using SimulationDevices.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SimulationDevices.Controller
{
    public class DevicesReceptions
    {
        public static bool GetRandomBoolean()
        {
            Random rnd = new Random();
            return rnd.Next(0, 2) == 0;
        }
        public static void GetListOfDevices()
        {
            Console.WriteLine("\nBegin Get Devices\n");
            var jsonUrlDevices = new WebClient().DownloadString("http://wcfwebservice.azurewebsites.net/Service.svc/devices/devices");
            var listJsonDevice = JsonConvert.DeserializeObject<List<ListOfDevices>>(jsonUrlDevices);
            string adressMacDevice = "";
            string nomDevice = "";
            string typeDevicesDevice = "";
            int i = 0;
            List<ListOfDevices> listDevice = new List<ListOfDevices>();
            Parallel.ForEach(listJsonDevice, urlresult =>
            {
                i++;
                adressMacDevice = urlresult.addressMac;
                nomDevice = urlresult.name;
                typeDevicesDevice = urlresult.typeDevices.name;
                listDevice.Add(new ListOfDevices
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
            });
            Console.WriteLine("\nEnd Get Devices\n");
            Console.WriteLine($"Nombre de devices : {i}");
            int j = 0;
            //Thread[] deviceThreads = new Thread[i];
            Parallel.ForEach(listDevice, deviceList =>
            {
                /*deviceThreads[j] = new Thread(() => SendMetrics(deviceList.addressMac, deviceList.typeDevices.name));
                deviceThreads[j].Start();*/
                j++;
                DevicesMetricsSend(deviceList.addressMac, deviceList.typeDevices.name);
            });
           // Console.Read();
        }
        public static double GetRandomDouble(double minimum, double maximum)
        {
            Random random = new Random();
            return random.NextDouble() * (maximum - minimum) + minimum;
        }
        public static string GetRandomGPS()
        {
            String value = "N;48;51;45,81;E;2;17;15,331";
            string result = "";
            Char delimiter = ';';
            String[] substrings = value.Split(delimiter);
            List<string> gpsstring = new List<string>();
            foreach (var substring in substrings)
            {
                Console.WriteLine(substring);
                gpsstring.Add(substring);
            }
            //Console.WriteLine($"1 : {gpsstring[0]}");//change pas N
            //Console.WriteLine($"2 : {gpsstring[1]}");//change pas 48
            //Console.WriteLine($"3 : {gpsstring[2]}");//int pas 51 49-54
            //Console.WriteLine($"4 : {gpsstring[3]}");//double pas 45.81 1-99
            //Console.WriteLine($"5 : {gpsstring[4]}");//change pas E
            //Console.WriteLine($"6 : {gpsstring[5]}");//change pas 2
            //Console.WriteLine($"7 : {gpsstring[6]}");//int pas 17  15-25
            //Console.WriteLine($"8 : {gpsstring[7]}");//double 15.331 1-99
            result = $"{gpsstring[0]};";
            result += $"{gpsstring[1]};";
            result += $"{GetRandomInt(49, 54)};";
            result += $"{GetRandomDouble(1, 99)};";
            result += $"{gpsstring[4]};";
            result += $"{gpsstring[5]};";
            result += $"{GetRandomInt(15, 25)};";
            result += $"{GetRandomDouble(1, 99)}";
            Console.WriteLine(result);
            return result;
        }
        public static int GetRandomInt(int minimum, int maximum)
        {
            Random random = new Random();
            return random.Next(minimum, maximum);
        }
        public static void DevicesMetricsSend(string idDevice, string deviceType)
        {
            Console.WriteLine($"BEGIN SEND METRICS : {idDevice} - {deviceType} Time : {DateTime.UtcNow.ToString("o")}");
            int countForSend = 0;
            List<Metrics> listOfMetrics = new List<Metrics>();
            var jsonMetric = "";
            // Parallel.For(0, 10000, index =>
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
                        metricValueDevice = GetRandomGPS();
                        break;

                    case ("co2Sensor"):
                        metricValueDevice = GetRandomDouble(300, 600).ToString();
                        break;

                    case ("ledDevice"):
                        string ledDeviceResult = "";
                        if (GetRandomBoolean() == true) { ledDeviceResult = "ON"; } else { ledDeviceResult = "OFF"; }
                        metricValueDevice = ledDeviceResult;
                        break;

                    case ("beeperDevice"):
                        string beeperDeviceResult = "";
                        if (GetRandomBoolean() == true) { beeperDeviceResult = "ON"; } else { beeperDeviceResult = "OFF"; }
                        metricValueDevice = beeperDeviceResult;
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
                jsonMetric += new JavaScriptSerializer().Serialize(newMetricSend);
                MetricSend(jsonMetric, idDevice);
                countForSend = 0;
                jsonMetric = "";
                listOfMetrics = new List<Metrics>();
                Thread.Sleep(1000);
            }
            //);
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
            }
        }
    }
}
