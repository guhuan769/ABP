using S7.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCollectionService.Entity
{
    public class PlcEntity
    {
        public Plc S7Plc { get; set; }
        public string PlcIp { get; set; }
    }
}
