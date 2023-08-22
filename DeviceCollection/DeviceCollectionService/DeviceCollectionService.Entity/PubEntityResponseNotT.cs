using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCollectionService.Entity
{
    public class PubEntityResponseNotT
    {
        public object Success { get; set; }
        public long Code { get; set; }
        public string Msg { get; set; }

        public bool Flag { get; set; }

        public string? token { get; set; }
        public int DataCount { get; set; }
    }
}
