using DeviceCollectionService.Entity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCollectionService.Common
{
    public class LocalTool
    {
        public bool GetLogger(ILogger _logger, string method, string? msg)
        {
            try
            {
                Appsettings appsettings = GetLocalSetting();
                if (!File.Exists(appsettings.Logging.Logger.Path))
                {
                    //创建要写入得日志
                    string creaeText = $"Hello and Welcome,man ";
                    File.WriteAllText(appsettings.Logging.Logger.Path, creaeText);
                }
                //这个文本总是被添加，使文件随着时间推移而变长
                //如果它没有被删除
                string appendText = $"{method},{msg}:{DateTime.Now}" + Environment.NewLine;
                File.AppendAllText(appsettings.Logging.Logger.Path, appendText);
                _logger.LogInformation("Worker running at :{time}", DateTimeOffset.Now);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Appsettings GetLocalSetting()
        {
            string regJson = "";
            using (StreamReader streamReader = new StreamReader(@"appsettings.json", Encoding.UTF8))
            {//这个路径默认是读.host解决方案下的文件
                regJson = streamReader.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<Appsettings>(regJson);//定义了一个相同结构的实体类
        }
    }
}
