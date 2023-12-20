using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elon.DashboardCenter.Application.LogDashboards
{
    public class LogDashboardsOptions
    {
        public string? DashboardName { get; set; }
        public string? DashboardDescription { get; set; }

        public bool DashboardEnable { get; set; } = true;
        public string? DashboardType { get; set; }

        public void Init(string msg)
        {
            Console.WriteLine($"This is {nameof(LogDashboards)} Init {msg}");
        }


    }
}
