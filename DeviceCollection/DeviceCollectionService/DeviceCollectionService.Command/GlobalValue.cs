using DeviceCollectionService.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCollectionService.Command
{
    public class GlobalValue
    {
        public PubEntityResponseNotT PubEntityResponseNotT { get; set; }
        public string? serverBaseUrl { get; set; }
        /// <summary>
        /// 是否登录成功
        /// </summary>
        public bool isLogin { get; set; }
    }
}
