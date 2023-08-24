using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCollectionService.Entity
{
    public class DeviceOutResponse
    {
        public Guid code { get; set; }
        public string? tName { get; set; }
        public long lineCode { get; set; }
        public long runtStatus { get; set; }
        public string? nameShort { get; set; }
        public long tStatus { get; set; }
        public string? remark { get; set; }
        public string? Ip { get; set; }
        public long deviceType { get; set; }
    }
}
