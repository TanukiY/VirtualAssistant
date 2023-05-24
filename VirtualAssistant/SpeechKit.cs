using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VirtualAssistant.Properties;

namespace VirtualAssistant
{    
    internal class SpeechKit
    {
        private static string ApiURL { get; set; }
        public SpeechKit()
        {
            ApiURL = "https://stt.api.cloud.yandex.net/speech/v1/stt:recognize";
        }
        //Добавить async
        async static public Task<string> ConvertSpeechToText(byte[] data)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, ApiURL);
            client.DefaultRequestHeaders.Authorization 
                = new AuthenticationHeaderValue(
                    "Bearer", YCloudSetting.GetInstance().YandexCloudToken);

            string url = $"{ApiURL}" +
                         $"?folderId={YCloudSetting.GetInstance().YandexCloudFolderId}" +
                         $"&topic=general" +
                         $"&lang=ru-RU" +
                         $"&profanityFilter=false" +
                         $"&rawResults=false" +
                         $"&sampleRateHertz=48000" +
                         $"&format=lpcm";

            var response = await client.PostAsync(url, new ByteArrayContent(data));

            string responseStream = "";
            if (response.IsSuccessStatusCode)
            {
                responseStream = await response.Content.ReadAsStringAsync();
            }

            string result = "";
            try
            {
                var jobj = JObject.Parse(responseStream);
                result = jobj["result"].ToString();
            }
            catch (Exception e)
            {
                throw new Exception("Не удалось сформировать JSON файл");
            }

            return result;
        }
    }
}
