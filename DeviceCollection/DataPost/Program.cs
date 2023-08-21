using Newtonsoft.Json.Linq;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;
using S7.Net;

namespace DataPost
{
    class Program
    {
        private static Logger logger;
        private static RestClient restClient;
        private static string token;
        private static async Task Main(string[] args)
        {
            logger = LogManager.GetCurrentClassLogger();
            var dataTrans = ReadConfiguration<DataTrans>("config.json");
            if (dataTrans == null)
            {
                Console.ReadKey();
                return;
            }
            restClient = new RestClient(dataTrans.Url);
            var request = new RestRequest(dataTrans.LoginPath, Method.Post);
            request.AddParameter("user", dataTrans.UserName);
            request.AddParameter("password", dataTrans.PassWord);
            var loginCount = 0;
            while (true)
            {
                var result = JsonConvert.DeserializeObject<LoginReturn>((await restClient.ExecuteAsync(request)).Content);
                if (result?.code == 0)
                {
                    token = result.token;
                    Log(LogType.Info, $"登录成功,Token:{result.token}");
                    break;
                }
                else
                {
                    loginCount++;
                    Log(LogType.Error, $"登录失败{result?.msg},5秒后尝试第{loginCount}次重新登录");
                    await Task.Delay(5000);
                }
            }
            await Task.WhenAll(from dataInfo in dataTrans.DataGroup
                where dataInfo.PostGroup.Any(p => p.Enabled)
                select DataTransTask(dataInfo, dataTrans.RetryCount, dataTrans.RetryDelay));
        }
        private static T ReadConfiguration<T>(string path)
        {
            if (!File.Exists(path))
            {
                Log(LogType.Error, "读取配置文件错误");
                return default;
            }
            using var reader = new StreamReader(path);
            return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
        }
        private static void Log(LogType type, string text)
        {
            switch (type)
            {
                case LogType.Info:
                    logger.Info(text);
                    break;
                case LogType.Warning:
                    logger.Warn(text);
                    break;
                case LogType.Error:
                    logger.Error(text);
                    break;
                case LogType.Exception:
                    logger.Fatal(text);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
        private static async Task<bool> ConnectPlc(DataInfo dataInfo)
        {
            //连接Plc
            if (dataInfo.S7Plc != null && dataInfo.S7Plc.IsConnected) return true;
            var i = 1;
            while (true)
            {
                try
                {
                    dataInfo.S7Plc ??= new Plc(CpuType.S71200, dataInfo.PlcIp, 0, 1);
                    await dataInfo.S7Plc.OpenAsync();
                    Log(LogType.Info, $"[{dataInfo.PlcIp}]plc连接成功");
                }
                catch (Exception ex)
                {
                    Log(LogType.Error, $"[{dataInfo.PlcIp}]plc连接失败，1秒后尝试第{i}次重连：{ex.Message}");
                    i++;
                    dataInfo.S7Plc.Close();
                    await Task.Delay(1000);
                }
                if (!dataInfo.S7Plc.IsConnected) continue;
                break;
            }
            return true;
        }

        private static async Task DataTransTask(DataInfo dataInfo, int retryCount, int retryDelay)
        {
            var countList = dataInfo.PostGroup.Select(data => 0).ToList();
            while (true)
            {
                await ConnectPlc(dataInfo);
                for (var i = 0; i < dataInfo.PostGroup.Count; i++)
                {
                    var data = dataInfo.PostGroup[i];
                    if(!data.Enabled) continue;
                    var count = 0;
                    try
                    {
                        count = (int)((uint)await dataInfo.S7Plc.ReadAsync(data.PlcAddress)).ConvertToFloat();
                        if (countList[i] == count) 
                            continue;
                        else
                            countList[i] = count;
                    }
                    catch (Exception ex)
                    {
                        Log(LogType.Error, $"读取{data.Name}数据失败：{ex.Message},尝试重连Plc");
                        dataInfo.S7Plc.Close();
                        await Task.Delay(1000);
                        await ConnectPlc(dataInfo);
                        continue;
                    }
                    var retry = 0;
                    while (retry < retryCount)
                    {
                        var request = new RestRequest(data.PostPath, Method.Post);
                        request.AddParameter("token", token);
                        request.AddParameter("node_id", data.NodeId);
                        request.AddParameter("group_id", data.GroupId);
                        request.AddParameter("output", count);
                        request.AddParameter("timestamp",
                            (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
                        request.AddParameter("plc", 1);
                        request.AddParameter("status", 0);
                        var response = (await restClient.ExecuteAsync(request)).Content;
                        var result = JsonConvert.DeserializeObject<ProductionReturn>(response);
                        if (result is not { code: 0 })
                        {
                            retry++;
                            Log(LogType.Error, $"{data.Name}发送数据失败{result?.msg},{retryDelay/1000.0}秒后尝试第{retry}次重发");
                            await Task.Delay(retryDelay);
                        }
                        else
                        {
                            Log(LogType.Info, $"{data.Name}发送数据{count}");
                            break;
                        }
                    }
                    if (retry >= retryCount) Log(LogType.Warning, $"{data.Name}第{retryCount}次重发失败，等待下次发送");
                }
                await Task.Delay(dataInfo.TimerInterval);
            }
        }

        private enum LogType
        {
            Info,
            Warning,
            Error,
            Exception
        }
    } 
    class LoginReturn
    {
        public LoginReturn(int _code, string _msg, string _token)
        {
            code = _code;
            msg = _msg;
            token = _token;
        }

        public int code { get; set; }
        public string msg { get; set; }
        public string token { get; set; }
    }
    class ProductionReturn
    {
        public ProductionReturn(int _code, string _msg)
        {
            code = _code;
            msg = _msg;
        }

        public int code { get; set; }
        public string msg { get; set; }
    }
}
