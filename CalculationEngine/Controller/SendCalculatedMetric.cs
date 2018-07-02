using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CalculationEngine.Controller
{
    public class SendCalculatedMetric
    {
        public static void CalculatedMetricSend(string json, string sensor, string date)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create($"http://wcfwebservice.azurewebsites.net/Service.svc/calculs/{sensor}/{date}");
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
    }
}
