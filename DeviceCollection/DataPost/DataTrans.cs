using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using S7.Net;

namespace DataPost
{
    public class DataTrans
    {
        public string Url { get; set; }
        public string LoginPath { get; set; }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public int RetryCount { get; set; }
        public int RetryDelay { get; set; }
        public List<DataInfo> DataGroup { get; set; }
       
    }

    public class DataInfo
    {
        public Plc S7Plc;
        public string PlcIp { get; set; }
        public int TimerInterval { get; set; }
        public List<PostInfo> PostGroup { get; set; }
    }

    public class PostInfo
    {
        public bool Enabled { get; set; }
        public string PostPath { get; set; }
        public string Name { get; set; }
        public string PlcAddress { get; set; }
        public int NodeId { get; set; }
        public int GroupId { get; set; }
    }
}
