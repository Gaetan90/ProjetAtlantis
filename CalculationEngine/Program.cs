using CalculationEngine.ServiceReferenceCalcul;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CalculationEngine
{
    class Program
    {
        public static void Main(string[] args)
        {
            /*ServiceCalculClient service = new ServiceCalculClient();
            ICollection<DataMetricView> dataMetrics = service.GetMetricByDeviceType("1");
            double r = CalculationEngine(dataMetrics);
            SendJson jsonObject = new SendJson();
            ICollection<Object> result = new Collection<Object>();
            result.Add(r);
            jsonObject.name = dataMetrics.First().metric.nameTypeDivice;
            jsonObject.description = "Description à affiner";
            jsonObject.results = result;
           // String values = '{ "results": "[N, 4567579.09]" , "name": +dataMetrics.First().metric.nameTypeDivice+, "description": "Description à affiner" }'.ToString();
            string jsonSerializedObj = JsonConvert.SerializeObject(jsonObject);
            Parallel.For(0, 100, i => sendItemAsync(jsonSerializedObj));*/
            //;



        }

        private static async Task sendItemAsync(string values)
        {
            HttpClient client = new HttpClient();
            var content = new StringContent(values.ToString(), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://10.167.128.61:8080/transaction/send", content);

            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseString.ToString());
            //Console.Read();
        }

        private static double CalculationEngine(ICollection<DataMetricView> dataMetrics)
        {
            ICollection<int> listValues = new Collection<int>();
            Parallel.ForEach(dataMetrics, value =>
               {
                   listValues.Add(Int32.Parse(value.value));
               }
            );

            return listValues.Average();
        }

        class SendJson
        {
            public string name { get; set; }
            public string description { get; set; }
            public ICollection<Object> results { get; set; }
        }
    }
}
