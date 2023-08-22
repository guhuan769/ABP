using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace DeviceCollectionService.IDAL
{
    public interface IWebDataAccess
    {
        Task<string> PostDatas(string url, HttpContent content);
        MultipartFormDataContent GetFormData(Dictionary<string, HttpContent> contents);
        Task<string> GetDatas(string url);
        void Upload(string url, string file, Action<object, UploadProgressChangedEventArgs> prograssChanged, Action completed, Dictionary<string, object> headers = null);
        void UploadFile(string url, string filePath, Dictionary<string, object> headers = null);
    }
}
