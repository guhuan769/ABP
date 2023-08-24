using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCollectionService.Entity
{
    public class PubProductionlineinfoResponse
    {
        /// <summary>
        /// 产线CODE
        /// </summary>
        public long code { get; set; }
        /// <summary>
        /// 产线名称
        /// </summary>
        public string tName { get; set; }
        /// <summary>
        /// 执行状态
        /// </summary>
        public long executeState { get; set; }

        /// <summary>
        /// 配件类型CODE
        /// </summary>
        public long partsTypeCode { get; set; }

        /// <summary>
        /// 配件类型名称
        /// </summary>
        public string partsTypetName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public long tStatus { get; set; }

    }
}
