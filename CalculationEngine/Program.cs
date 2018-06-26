using CalculationEngine.ServiceReferenceCalcul;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CalculationEngine
{
    class Program
    {
        public static void Main(string[] args)
        {
            ServiceCalculClient service = new ServiceCalculClient();
            ICollection<DataMetricView> dataMetrics = service.GetMetricByDeviceType(1);
            double result = CalculationEngine(dataMetrics);

            var values = new Dictionary<string, string>{
               { "results", "["+result+"]" },
               { "name", dataMetrics.First().metric.nameTypeDivice },
               { "description", "Description à affiner" }
            };
            sendItemAsync(values).Wait();



        }

        private static async Task sendItemAsync(Dictionary<string, string> values)
        {
            HttpClient client = new HttpClient();
            var content = new FormUrlEncodedContent(values);

            var response = await client.PostAsync("http://10.167.128.61:8282/transaction/calculation-reception", content);

            var responseString = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseString.ToString());
            Console.Read();
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
    }
}
