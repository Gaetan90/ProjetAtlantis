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

            List<string> listSensor = new List<string> { "presenceSensor", "temperatureSensor", "brightnessSensor", "atmosphericPressureSensor", "humiditySensor", "soundLevelSensor", "gpsSensor", "co2Sensor", "ledDevice", "beeperDevice" };
            List<string> listDateSend = new List<string> {"day", "week", "month"};

            foreach (var sensorList in listSensor)
            {
                foreach (var dateSendList in listDateSend)
                {

                }
            }

            var jsonUrlDevices = new WebClient().DownloadString("http://wcfwebservice.azurewebsites.net/Service.svc/calculs/temperatureSensor/week");
            var listJsonDevice = JsonConvert.DeserializeObject<List<Metrics>>(jsonUrlDevices);
            //Console.WriteLine(jsonUrlDevices);
            int i = 0;
            List<double> listMetrics = new List<double>();
            foreach (var urlresult in listJsonDevice)
            {
                i++;
                Console.WriteLine($"Device : {i} ");
                Console.WriteLine($"ID : {urlresult.id}");
                Console.WriteLine($"Métric : {urlresult.metric}");
                Console.WriteLine($"Valeur : {urlresult.value}");
                Console.WriteLine("");
                listMetrics.Add(Double.Parse(urlresult.value));

            }
            double average = listMetrics.Average();
            Console.WriteLine($"Moyenne : {average}");


            var newMetricDouble = new ResultDouble
            {
                result = average
            };

            var jsonMetric = new JavaScriptSerializer().Serialize(newMetricDouble);
            Console.WriteLine(jsonMetric);
            //Console.WriteLine(countForSend);

            CalculatedMetricSend(jsonMetric);
            listMetrics = new List<double>();




            Console.WriteLine("End");
            Console.Read();
        }
        public static void CalculatedMetricSend(string json)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create($"http://localhost:54435/Service.svc/calculs/temperatureSensor/week");
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
        public class Metrics
        {
            public string id;
            public string metric;
            public string value;
        }
        public class ResultInt
        {
            public int result;
        }
        public class ResultDouble
        {
            public double result;
        }
        public class ResultBoolean
        {
            public bool result;
        }
        public class ResultString
        {
            public string result;
        }
    }

}
