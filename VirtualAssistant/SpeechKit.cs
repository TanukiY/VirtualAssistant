using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VirtualAssistant.Properties;
using System.Windows.Forms;
using VisioForge.Libs.MediaFoundation.OPM;

namespace VirtualAssistant
{
    internal static class SpeechKit
    {
        private const string ApiURL = "https://stt.api.cloud.yandex.net/speech/v1/stt:recognize";
        private static HttpClient client = new HttpClient();

        async static public Task<string> ConvertSpeechToText(byte[] data)
        {
            //var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, ApiURL);
            client.DefaultRequestHeaders.Authorization
                = new AuthenticationHeaderValue(
                    "Bearer", Setting.GetInstance().YandexCloudToken);

            string url = $"{ApiURL}" +
                         $"?folderId={Setting.GetInstance().YandexCloudFolderId}" +
                         $"&topic=general" +
                         $"&lang=ru-RU" +
                         $"&profanityFilter=false" +
                         $"&rawResults=false" +
                         $"&sampleRateHertz=48000" +
                         $"&format=lpcm";

            var response = await client.PostAsync(url, new ByteArrayContent(data));

            string responseContent = "";
            if (response.IsSuccessStatusCode)
            {
                responseContent = await response.Content.ReadAsStringAsync();
            }
            else
            {
                MessageBox.Show("Ошибка при обращении к Yandex SpeechKit API", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            string result = "";
            try
            {
                var jobj = JObject.Parse(responseContent);
                result = jobj["result"].ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show("Не удалось сформировать JSON файл: " + e.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return result;
        }
    }
}
