using DeviceCollectionService.IDAL;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DeviceCollectionService.DAL
{
    public class LoginDAL :  ILoginDAL
    {
        private readonly IWebDataAccess _webDataAccess;

        public LoginDAL(IWebDataAccess webDataAccess)
        {
            _webDataAccess = webDataAccess;
        }
        public async Task<string> LoginByUserNameAndPwd(string data)
        {
            StringContent content = new StringContent(data);
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            return await _webDataAccess.PostDatas($"api/LoginLoginByUserNameAndPwd", content);
        }
    }
}
