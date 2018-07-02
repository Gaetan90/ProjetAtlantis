using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationDevices.Controller
{
    public class GetGPS
    {
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
            result += $"{GetInt.GetRandomInt(49, 54)};";
            result += $"{GetDouble.GetRandomDouble(1, 99)};";
            result += $"{gpsstring[4]};";
            result += $"{gpsstring[5]};";
            result += $"{GetInt.GetRandomInt(15, 25)};";
            result += $"{GetDouble.GetRandomDouble(1, 99)}";
            Console.WriteLine(result);
            return result;
            //N; 48; 50; 31,1960036755521; E; 2; 18; 31,1960036755521
        }
    }
}
