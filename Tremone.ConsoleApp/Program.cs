using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using Tremone.Model;

namespace Tremone.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string GetIpUrl = "https://api.ipify.org/?format=json";
                string GetInfoUrl = "http://ip-api.com/json/";

                using (HttpClient client = new HttpClient())
                {
                    string hostName = Dns.GetHostName();
                    string ipAddress = JsonConvert.DeserializeObject<IpModel>(client.GetStringAsync(GetIpUrl).Result).Ip;
                    IpInfoModel infoModel = JsonConvert.DeserializeObject<IpInfoModel>(client.GetStringAsync(GetInfoUrl + ipAddress).Result);

                    Console.WriteLine($"Host : { hostName }");
                    Console.WriteLine($"Ip : { ipAddress }");
                    Console.WriteLine($"City : { infoModel.City }");
                    Console.WriteLine($"Country : { infoModel.Country }");
                    Console.WriteLine($"Isp : { infoModel.Isp }");
                    Console.WriteLine($"Lat Lon : { infoModel.Lat } , { infoModel.Lon }");
                }

            }
            catch (Exception exp)
            {
                Console.WriteLine($"Error : {exp.Message}");
            }

            Console.ReadLine();
        }
    }
}
