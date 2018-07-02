using Newtonsoft.Json;
using SimulationDevices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SimulationDevices.Controller
{
    class GetDevices
    {
        public static void GetListOfDevices()
        {
            Console.WriteLine("\nBegin Get Devices\n");
            var jsonUrlDevices = new WebClient().DownloadString("http://wcfwebservice.azurewebsites.net/Service.svc/devices/devices");
            var listJsonDevice = JsonConvert.DeserializeObject<List<ListOfDevices>>(jsonUrlDevices);
            //Console.WriteLine(jsonUrlDevices);
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
                SendDevicesMetrics.DevicesMetricsSend(deviceList.addressMac, deviceList.typeDevices.name);
            });
            Console.Read();
        }
    }
}
