﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCollectionService.IDAL
{
    public interface ILineDAL
    {
        Task<string> GetEnableLineTotal();
        Task<string> InsertPart(string data);
    }
}
