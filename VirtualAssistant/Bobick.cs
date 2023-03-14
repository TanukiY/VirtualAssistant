using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace VirtualAssistant
{
    internal class Bobick
    {
        TextBox tbMsg;
        RichTextBox rtbChat;
        Dictionary<string, string> dict;

        public Bobick(TextBox tbMsg, RichTextBox rtbChat)
        {
            this.rtbChat = rtbChat;
            this.tbMsg = tbMsg;
            var text = File.ReadAllText("path.json");
            dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);
        }

        public void command()
        {
            var message = tbMsg.Text;
            var command = message.Trim().Split()[0].ToLower();
            var text = message.Substring(command.Length+1);
            rtbChat.SelectionAlignment = HorizontalAlignment.Right;
            rtbChat.AppendText(message + "\n");

            if (command == "echo" || command == "скажи")
                echoCommand(text);
            else if (command == "open" || command == "открой")
                openCommand(text);
            else if(command == "добавь" || command=="add")
                addPathCommand(text);
        }

        public void echoCommand(string text)
        {
            chatAdd(text);
        }
        public void openCommand(string text)
        {
            try
            {
                Process.Start(text);
            }
            catch (Exception)
            {
                string res;
                dict.TryGetValue(text, out res);
                if (res != null)                    
                    Process.Start(res);
                else
                {
                    chatAdd("Простите, не удалось открыть программу");
                    return;
                }                    
            }
            chatAdd("Программа открыта");
        }

        public void addPathCommand(string text)
        {
            var key = text.Split()[0];
            var path = text.Substring(key.Length + 1);
            if(text.Substring(text.Length-4)!= ".exe")
            {
                chatAdd("Расширение файла должно быть .exe");
                return;
            }

            if (!File.Exists(path))
            {
                chatAdd("Такого файла нет, проаерьте путь");
                return;
            }
            dict.Add(key, path);
            string reading = JsonConvert.SerializeObject(dict);            
            File.WriteAllText("path.json", reading);
            chatAdd("Путь добавлен");
        }

        public void chatAdd(string text)
        {
            rtbChat.SelectionAlignment = HorizontalAlignment.Left;
            rtbChat.AppendText(text + "\n");
        }
    }
}
