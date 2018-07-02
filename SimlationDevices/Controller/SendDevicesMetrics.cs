using SimlationDevices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace SimlationDevices.Controller
{
    class SendDevicesMetrics
    {
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
                        if (GetBoolean.GetRandomBoolean() == true) { presenceSensorResult = "1"; } else { presenceSensorResult = "0"; }
                        metricValueDevice = presenceSensorResult;
                        break;

                    case ("temperatureSensor"):
                        metricValueDevice = GetDouble.GetRandomDouble(19, 41).ToString();
                        break;

                    case ("brightnessSensor"):
                        metricValueDevice = GetInt.GetRandomInt(0, 1001).ToString();
                        break;

                    case ("atmosphericPressureSensor"):
                        metricValueDevice = GetDouble.GetRandomDouble(900, 1101).ToString();
                        break;

                    case ("humiditySensor"):
                        metricValueDevice = GetDouble.GetRandomDouble(20, 90).ToString();
                        break;

                    case ("soundLevelSensor"):
                        metricValueDevice = GetInt.GetRandomInt(60, 160).ToString();
                        break;

                    case ("gpsSensor"):
                        metricValueDevice = GetGPS.GetRandomGPS();
                        break;

                    case ("co2Sensor"):
                        metricValueDevice = GetDouble.GetRandomDouble(300, 600).ToString();
                        break;

                    case ("ledDevice"):
                        string ledDeviceResult = "";
                        if (GetBoolean.GetRandomBoolean() == true) { ledDeviceResult = "ON"; } else { ledDeviceResult = "OFF"; }
                        metricValueDevice = ledDeviceResult;
                        break;

                    case ("beeperDevice"):
                        string beeperDeviceResult = "";
                        if (GetBoolean.GetRandomBoolean() == true) { beeperDeviceResult = "ON"; } else { beeperDeviceResult = "OFF"; }
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
                //Console.WriteLine($"Device : {deviceTypeDevice} Value : {metricValueDevice}");
                //jsonMetric += new JavaScriptSerializer().Serialize(newMetricSend);
                jsonMetric += new JavaScriptSerializer().Serialize(newMetricSend);
                //jsonMetric += new JavaScriptSerializer().Serialize(newMetricSend);
                //var JsonDevice = JsonConvert.SerializeObject(newMetricSend);

                //Console.WriteLine($"json metric : {jsonMetric}");
                SendMetrics.MetricSend(jsonMetric, idDevice);
                countForSend = 0;
                jsonMetric = "";
                listOfMetrics = new List<Metrics>();
                //Console.WriteLine($"End metric sender for {idDevice}");
                Thread.Sleep(1000);
            }
            //);
            Console.WriteLine($"END SEND METRICS : {idDevice} - {deviceType} Time : {DateTime.UtcNow.ToString("o")}");
        }
    }
}
