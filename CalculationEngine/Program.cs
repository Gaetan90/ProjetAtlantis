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
using CalculationEngine.Model;
using CalculationEngine.Controller;

namespace CalculationEngine
{
    class Program
    {
        public SendCalculatedMetric SendCalculatedMetric
        {
            get => default(SendCalculatedMetric);
            set
            {
            }
        }

        public GetCalculatedMetrics GetCalculatedMetrics
        {
            get => default(GetCalculatedMetrics);
            set
            {
            }
        }

        public static void Main(string[] args)
        {
            GetCalculatedMetrics.GetMetricsCalculated();
        }
    }
}
