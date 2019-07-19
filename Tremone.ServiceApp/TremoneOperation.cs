using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Tremone.Model;
using NLog;

namespace Tremone.ServiceApp
{
    public class TremoneOperation
    {
        Timer timer = new Timer();

        public void Start()
        {
            WriteLog("Start Service");

            int timeUpdate = Convert.ToInt32(ConfigurationManager.AppSettings["TimeUpdate"].ToString());

            timer.Elapsed += new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = timeUpdate * 60 * 1000;
            timer.Enabled = true;
        }

        public void Stop()
        {
            WriteLog("Stop Service");
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            WriteLog("Service Working");
        }

        public void WriteLog(string message)
        {
            try
            {
                string GetIpUrl = ConfigurationManager.AppSettings["GetIpUrl"].ToString();
                string GetInfoUrl = ConfigurationManager.AppSettings["GetInfoUrl"].ToString();
                string PutLogUrl = ConfigurationManager.AppSettings["PutLogUrl"].ToString();
                string ProjectName = ConfigurationManager.AppSettings["ProjectName"].ToString();

                using (HttpClient client = new HttpClient())
                {
                    LogModel model = new LogModel();

                    model.LogTime = DateTime.Now;
                    model.LogName = ProjectName;
                    model.LogContent = message;
                    model.HostName = Dns.GetHostName();

                    model.LocalIpAddress.AddRange(Dns.GetHostEntry(Dns.GetHostName()).AddressList.Select(item => item.ToString()));

                    model.IpAddress = JsonConvert.DeserializeObject<IpModel>(client.GetStringAsync(GetIpUrl).Result).Ip;

                    IpInfoModel tempModel = JsonConvert.DeserializeObject<IpInfoModel>(client.GetStringAsync(GetInfoUrl + model.IpAddress).Result);

                    // This is Spaghetti :|
                    model.City = tempModel.City;
                    model.Country = tempModel.Country;
                    model.Isp = tempModel.Isp;
                    model.Lat = tempModel.Lat;
                    model.Lon = tempModel.Lon;

                    // Put Log Model Info
                    StringContent content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
                    client.PutAsync(PutLogUrl, content);

                    // For Surprise Egg :)
                    System.Threading.Thread.Sleep(2 * 1000);

                    // Disabled For Öylesine :)
                    //LogManager.GetCurrentClassLogger().Info("Success");
                }
            }
            catch (Exception exp)
            {
                //TODO : Singleton Pattern To Do
                //This is a slow-running method.
                LogManager.GetCurrentClassLogger().Error($"Error Message : {exp.Message}");
            }
        }
    }
}
