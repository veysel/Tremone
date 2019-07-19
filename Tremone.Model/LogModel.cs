using System;
using System.Collections.Generic;
using System.Text;

namespace Tremone.Model
{
    public class LogModel
    {
        public DateTime LogTime { get; set; }
        public string LogName { get; set; }
        public string LogContent { get; set; }
        public string HostName { get; set; }
        public List<string> LocalIpAddress { get; set; }
        public string IpAddress { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Isp { get; set; }
        public string Lat { get; set; }
        public string Lon { get; set; }

        public LogModel()
        {
            LocalIpAddress = new List<string>();
        }
    }
}