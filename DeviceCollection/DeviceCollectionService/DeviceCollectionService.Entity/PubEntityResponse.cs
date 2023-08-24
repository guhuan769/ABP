using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCollectionService.Entity
{
    public class PubEntityResponse<T> //where T : class
    {
        public object Success { get; set; }
        public long Code { get; set; }
        public string Msg { get; set; }

        public bool Flag { get; set; }
        public List<string> checkedKeys { get; set; }
        public List<T>? Data { get; set; }
        public T? DataEntity { get; set; }
        public int DataCount { get; set; }
    }
}
