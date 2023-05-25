using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisioForge.Libs.MediaFoundation.OPM;

namespace VirtualAssistant
{
    
    internal static class GptAssistent
    {
        public const string GptUrl  = "https://api.openai.com/v1/chat/completions";
        static private readonly List<Message> messages = new List<Message>();
        static private readonly HttpClient httpClient = new HttpClient();


        static GptAssistent() {
            httpClient.DefaultRequestHeaders.Add("Authorization",
                $"Bearer {Setting.GetInstance().GPTToken}");

            var welcomeMessage = new Message() { Role = "user", 
                Content = "Ты - ассистент Бобик, отыгрывает кота. Давай ответы в роли кота" };
            messages.Add(welcomeMessage);            
        }

        public static async Task<string> StartGPTAsync(string content)
        {
            if (string.IsNullOrEmpty(content)) 
                return null;
            
            var userMessage = new Message() { Role = "user", Content = content };
            messages.Add(userMessage);

            var requestData = new Request()
            {
                Model = "gpt-3.5-turbo",
                Messages = messages
            };

            string jsonString = JsonConvert.SerializeObject(requestData).ToLower();

            //освобождение памяти
            var requestContent = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(GptUrl, requestContent);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                var responseObject = JsonConvert.DeserializeObject<Response>(responseContent);
                return responseObject?.Choices?[0]?.Message?.Content;
            }
            else
            {
                MessageBox.Show("Ошибка при обращении к API GPT", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                throw new Exception("Ошибка при обращении к API GPT");
            }            
        }

    }
    class Message
    {
        public string Role { get; set; } = "";
        public string Content { get; set; } = "";
    }
    class Request
    {
        public string Model { get; set; } = "";
        public List<Message> Messages { get; set; } = new List<Message>();
    }
    class Response
    {
        public List<Choice> Choices { get; set; } = new List<Choice>();
    }
    class Choice
    {
        public Message Message { get; set; }
    }
}
