using DeviceCollectionService.Command;
using DeviceCollectionService.IDAL;
using System.Net;
using System.Net.Http.Headers;

namespace DeviceCollectionService.DAL
{
    /// <summary>
    /// 主要处理 请求WebApi 的过程
    /// </summary>
    public class WebDataAccess : IWebDataAccess
    {
        //private HttpClient httpClient = new HttpClient();
        private readonly GlobalValue _globalValue;
        public WebDataAccess(GlobalValue globalValue)
        {
            _globalValue = globalValue;
        }

        /// <summary>
        /// [dbo].[pub_company]
        /// </summary>
        /// <param name="url">请求的URL地址</param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<string> PostDatas(string url, HttpContent content)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"{_globalValue?.serverBaseUrl}/");
                if (_globalValue != null && _globalValue.PubEntityResponseNotT != null && !string.IsNullOrEmpty(_globalValue.PubEntityResponseNotT.token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _globalValue.PubEntityResponseNotT.token);
                }
                var resp = await client.PostAsync(url, content);
                return await resp.Content.ReadAsStringAsync();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">请求的URL地址</param>
        /// <param name="content"></param>
        /// <returns></returns>
        public async Task<string> DeleteDatas(string url)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"{_globalValue?.serverBaseUrl}/");
                if (_globalValue != null && _globalValue.PubEntityResponseNotT != null && !string.IsNullOrEmpty(_globalValue.PubEntityResponseNotT.token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _globalValue.PubEntityResponseNotT.token);
                }
                var resp = await client.DeleteAsync(url);
                return await resp.Content.ReadAsStringAsync();
            }
        }


        public MultipartFormDataContent GetFormData(Dictionary<string, HttpContent> contents)
        {
            var postContent = new MultipartFormDataContent();
            string boundary = string.Format("--{0}", DateTime.Now.Ticks.ToString("x"));
            postContent.Headers.Add("ContentType", $"multipart/form-data,boundary={boundary}");

            foreach (var item in contents)
            {
                // 需要进行传递的键值对:比如:username=admin 
                postContent.Add(item.Value, item.Key);
            }
            return postContent;
        }

        //Get方式进行请求
        public async Task<string> GetDatas(string url)
        {
            using (var client = new HttpClient())
            {

                if (_globalValue != null && _globalValue.PubEntityResponseNotT != null && !string.IsNullOrEmpty(_globalValue.PubEntityResponseNotT.token))
                {
                    client.DefaultRequestHeaders.Authorization = new
                    AuthenticationHeaderValue("Bearer", _globalValue.PubEntityResponseNotT.token);
                }
                client.BaseAddress = new Uri($"{_globalValue?.serverBaseUrl}/");
                client.Timeout = new TimeSpan(0, 0, 10);
                var resp = await client.GetAsync(url);
                return await resp.Content.ReadAsStringAsync();
            }
        }

        public Action<int> UploadPrograssChanged;
        public Action UploadCompleted;
        WebClient webClient = new WebClient();
        public void Upload(string url, string file, Action<object, UploadProgressChangedEventArgs> prograssChanged, Action completed
            , Dictionary<string, object> headers = null)
        {
            //多文件上传使用 HttpWebRequest
            //大文件分组上传

            using (WebClient client = new WebClient())
            {
                //添加鉴权Token
                client.Headers.Add("Authorization", "Bearer " + _globalValue.PubEntityResponseNotT.token);// + token
                if (prograssChanged != null)
                    client.UploadProgressChanged += (se, ev) => prograssChanged(se, ev);
                client.UploadFileCompleted += (se, ev) => completed();

                if (headers != null)
                {
                    foreach (var item in headers)
                    {
                        //if (item.Value != null)
                        client.Headers.Add(item.Key, item.Value.ToString());
                    }
                }
                client.UploadFileAsync(new Uri($"{_globalValue?.serverBaseUrl}/" + url), "POST", file);
            }
        }

        /// <summary>
        /// 大文件分片上传
        /// </summary>
        /// <param name="filePath"></param>
        public async void UploadFile(string url, string filePath, Dictionary<string, object> headers = null)
        {
            using (var client = new HttpClient())
            {
                int chunkSize = 1024 * 1024 * 5; // 1MB
                // 读取文件内容
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                using (var binaryReader = new BinaryReader(fileStream))
                {
                    byte[] buffer = new byte[chunkSize]; // 设置缓冲区大小
                    int bytesRead;
                    if (headers != null)
                    {
                        foreach (var item in headers)
                        {
                            //if (item.Value != null)
                            client.DefaultRequestHeaders.Add(item.Key, item.Value.ToString());
                        }
                    }
                    // 分块上传文件
                    while ((bytesRead = binaryReader.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        // 构建请求内容
                        var content = new MultipartFormDataContent();
                        content.Add(new ByteArrayContent(buffer, 0, bytesRead), "file", Path.GetFileName(filePath));
                        content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                        // 发送请求到 Web API 的上传接口
                        var response = await client.PostAsync(new Uri($"{_globalValue?.serverBaseUrl}/" + url), content);

                        // 处理上传响应，可以根据需要进行错误处理或其他操作
                        if (response.IsSuccessStatusCode)
                        {
                            // 块上传成功
                            Console.WriteLine("Chunk uploaded successfully.");
                        }
                        else
                        {
                            // 块上传失败
                            Console.WriteLine("Chunk upload failed.");
                        }
                    }

                    // 通知服务器文件上传已完成
                    //var completionResponse = await client.PostAsync("http://your-api-url/complete-upload", null);
                    //if (completionResponse.IsSuccessStatusCode)
                    //{
                    //    Console.WriteLine("File upload completed successfully.");
                    //}
                    //else
                    //{
                    //    Console.WriteLine("File upload completion failed.");
                    //}
                }
            }
        }


    }
}
