using DeviceCollectionService.Entity;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using S7.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCollectionService.Common
{
    public class LocalTool
    {
        public async Task<bool> ConnectPlc(ILogger _logger, PlcEntity PlcEntity)
        {
            int i = 1;
            string[] strArr = PlcEntity.PlcIp.Split('&');
            //while (true)
            //{
            try
            {
                if (PlcEntity.S7Plc != null && PlcEntity.S7Plc.IsConnected) return true;
                PlcEntity.S7Plc = new Plc(CpuType.S71200, strArr[0], 0, 1);
                await PlcEntity.S7Plc.OpenAsync();
                //InsertLogger(_logger, "ConnectPlc", $" {strArr[0]}plc连接成功 :{DateTimeOffset.Now}");
                //_logger.LogInformation($" {strArr[0]}plc连接成功 :{DateTimeOffset.Now}");

            }
            catch (Exception)
            {
                _logger.LogInformation($" '{PlcEntity.PlcIp.Split('&')[0]}'  plc连接失败，1秒后尝试第{i}次连接 :{DateTimeOffset.Now}");
                i++;
                PlcEntity.S7Plc.Close();
                //await Task.Delay(1000);
                return false;
            }
            return true;
            //}
        }

        public bool InsertLogger(ILogger _logger, string method, string? msg)
        {
            try
            {
                Appsettings appsettings = GetLocalSetting();
                //if(DateTime.Now.)
                if (!File.Exists(appsettings.Logging.Logger.Path))
                {
                    //创建要写入得日志
                    string creaeText = $"Hello and Welcome,man ";
                    File.WriteAllText(appsettings.Logging.Logger.Path, creaeText);
                }
                //这个文本总是被添加，使文件随着时间推移而变长
                //如果它没有被删除
                string appendText = $"{method}; MSG:{msg}:{DateTime.Now}" + Environment.NewLine;
                File.AppendAllText(appsettings.Logging.Logger.Path, appendText);
                _logger.LogInformation($" {appendText}  Worker running at :{DateTimeOffset.Now}");
                return true;
            }
            catch (Exception)
            {
                _logger.LogInformation($":{DateTimeOffset.Now}");
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

        public bool IpPing(string ip)
        {
            bool online = false; //是否在线
            Ping ping = new Ping();
            PingReply pingReply = ping.Send(ip);
            if (pingReply.Status == IPStatus.Success)
            {
                online = true;
                Console.WriteLine("当前在线，已ping通！");
            }
            else
            {
                Console.WriteLine("不在线，ping不通！");
                
            }
            return online;
        }
    }
}
