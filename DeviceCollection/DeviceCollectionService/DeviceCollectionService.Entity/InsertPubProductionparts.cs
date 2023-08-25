using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCollectionService.Entity
{
    public class InsertPubProductionparts
    {
        public FromPubProductionparts fromPubProductionparts { get; set; } = new FromPubProductionparts();
    }

    public class FromPubProductionparts
    {
        //产量
        public int production { get; set; }
        //二维码
        public string qrCode { get; set; } = string.Empty;
        //生产线
        public long productionLineCode { get; set; }
        //备注
        public string? remark { get; set; }
        /// <summary>
        /// 设备CODE
        /// </summary>
        public Guid DeviceCode { get; set; }
    }
}
