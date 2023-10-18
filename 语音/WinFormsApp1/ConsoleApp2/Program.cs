using System;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static readonly HttpClient client = new HttpClient();

    static async Task Main()
    {
        string accessToken = "your_access_token";
        string voiceId = "your_voice_id";
        string format = "mp3";
        string lang = "zh_CN";

        try
        {
            // 提交语音
            string addVoiceUrl = $"http://api.weixin.qq.com/cgi-bin/media/voice/addvoicetorecofortext?access_token={accessToken}&format={format}&voice_id={voiceId}&lang={lang}";
            HttpResponseMessage addVoiceResponse = await client.PostAsync(addVoiceUrl, null);

            if (addVoiceResponse.IsSuccessStatusCode)
            {
                string addVoiceResponseBody = await addVoiceResponse.Content.ReadAsStringAsync();
                Console.WriteLine($"Add Voice Response: {addVoiceResponseBody}");

                // 等待10秒
                await Task.Delay(10000);

                // 获取识别结果
                string queryResultUrl = $"http://api.weixin.qq.com/cgi-bin/media/voice/queryrecoresultfortext?access_token={accessToken}&voice_id={voiceId}&lang={lang}";
                HttpResponseMessage queryResultResponse = await client.PostAsync(queryResultUrl, null);

                if (queryResultResponse.IsSuccessStatusCode)
                {
                    string queryResultResponseBody = await queryResultResponse.Content.ReadAsStringAsync();
                    Console.WriteLine($"Query Result Response: {queryResultResponseBody}");
                }
                else
                {
                    Console.WriteLine($"Error getting recognition result: {queryResultResponse.StatusCode}");
                }
            }
            else
            {
                Console.WriteLine($"Error adding voice: {addVoiceResponse.StatusCode}");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception caught: {e.Message}");
        }
    }
}
