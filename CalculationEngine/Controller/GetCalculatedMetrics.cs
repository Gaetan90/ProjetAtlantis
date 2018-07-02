using CalculationEngine.Model;
using CalculationEngine.Controller;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CalculationEngine.Controller
{
    public class GetCalculatedMetrics
    {
        public static void GetMetricsCalculated()
        {
            Console.WriteLine("Begin");
            //List<string> listSensor = new List<string> { "presenceSensor", "temperatureSensor", "brightnessSensor", "atmosphericPressureSensor", "humiditySensor", "soundLevelSensor", "gpsSensor", "co2Sensor", "ledDevice", "beeperDevice" };
            List<string> listSensor = new List<string> { "temperatureSensor", "brightnessSensor", "atmosphericPressureSensor", "humiditySensor", "soundLevelSensor", "co2Sensor" };
            List<string> listDateSend = new List<string> { "day", "week", "month" };

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
                    SendCalculatedMetric.CalculatedMetricSend(jsonMetric, sensorList, dateSendList);
                    listMetricsDouble = new List<double>();
                    //}
                });
                //}
            });
            Console.WriteLine("End");
            //Console.Read();
        }
    }
}
