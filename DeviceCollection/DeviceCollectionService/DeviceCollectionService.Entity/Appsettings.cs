using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCollectionService.Entity
{
    public class Appsettings
    {
        public Logging? Logging { get; set; }
        public UserInfo? UserInfo { get; set; }
    }

    public class UserInfo {
        public string? username { get; set; }
        public string? password { get; set; }
        public string? serverBaseUrl { get; set; }
    }

    public class Logging
    {
        public LogLevel? LogLevel { get; set; }
        public Logger? Logger { get; set; }
    }

    public class LogLevel
    {
        public string? Default { get; set; }
    }

    public class Logger
    {
        public string? Path { get; set; }
    }
}
