using CalculationEngine.ServiceReferenceCalcul;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;

namespace CalculationEngine
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Begin");
            //List<string> listSensor = new List<string> { "presenceSensor", "temperatureSensor", "brightnessSensor", "atmosphericPressureSensor", "humiditySensor", "soundLevelSensor", "gpsSensor", "co2Sensor", "ledDevice", "beeperDevice" };
            List<string> listSensor = new List<string> { "temperatureSensor", "brightnessSensor", "atmosphericPressureSensor", "humiditySensor", "soundLevelSensor", "co2Sensor"};
            List<string> listDateSend = new List<string> {"day", "week", "month"};

            Parallel.ForEach(listSensor, sensorList =>
            //foreach (var sensorList in listSensor)
            {
                Parallel.ForEach(listDateSend, dateSendList =>
                //foreach (var dateSendList in listDateSend)
                {
                    Console.WriteLine($"Sensor : {sensorList}");
                    Console.WriteLine($"date : {dateSendList}");
                    var jsonUrlDevices = new WebClient().DownloadString($"http://wcfwebservice.azurewebsites.net/Service.svc/calculs/{sensorList}/{dateSendList}");
                    var listJsonDevice = JsonConvert.DeserializeObject<List<Metrics>>(jsonUrlDevices);
                    List<double> listMetricsDouble = new List<double>();
                    //List<string> listMetricsString = new List<string>();
                    Parallel.ForEach(listJsonDevice, urlresult =>
                    //foreach (var urlresult in listJsonDevice)
                    {
                        //Console.WriteLine($"Valeur : {urlresult.value}");
                        listMetricsDouble.Add(Double.Parse(urlresult.value));
                    //}
                    });
                        double average = listMetricsDouble.Average();
                    //Console.WriteLine($"Moyenne : {average}");
                    var newMetricDouble = new ResultDouble
                    {
                        result = average
                    };
                    var jsonMetric = new JavaScriptSerializer().Serialize(newMetricDouble);
                    //Console.WriteLine(jsonMetric);
                    //Console.WriteLine(countForSend);
                    CalculatedMetricSend(jsonMetric, sensorList, dateSendList);
                    listMetricsDouble = new List<double>();
                //}
                });
            //}
            });
            Console.WriteLine("End");
            //Console.Read();
        }
        public static void CalculatedMetricSend(string json, string sensor, string date)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create($"http://10.167.129.218:80/Service.svc/calculs/{sensor}/{date}");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            Console.WriteLine($"http://wcfwebservice.azurewebsites.net/Service.svc/calculs/{sensor}/{date}");
            Console.WriteLine(json);
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
        public class Metrics
        {
            public string id;
            public string metric;
            public string value;
        }
        public class ResultDouble
        {
            public double result;
        }
        public class ResultString
        {
            public string result;
        }
    }

}
