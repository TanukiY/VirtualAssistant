using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
        DictionaryCmd[] dictionaryCmds;
        Classifier classifier = new Classifier();

        public Bobick(TextBox tbMsg, RichTextBox rtbChat)
        {
            this.rtbChat = rtbChat;
            this.tbMsg = tbMsg;
            var text = File.ReadAllText("path.json");
            dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);

            var dictionaryCmdsPath = "trainingsample.csv";
            this.dictionaryCmds = ReaderDictionaryCmd.ReadCommands(dictionaryCmdsPath);
            classifier.addDictionaryCmd(dictionaryCmds);
        }

        public void commandProcess()
        {
            var message = tbMsg.Text;
            rtbChat.SelectionAlignment = HorizontalAlignment.Right;
            rtbChat.AppendText(message + "\n");
            string[] commands = classifier.Predict(message);
            var text = message.Replace(commands[1], "").Trim();
            switch (commands[0])
            {
                case "открой":
                    openCommand(text);
                    break;
                case "скажи":
                    echoCommand(text);
                    break;
                case "добавь путь":
                    addPathCommand(text);
                    break;
                case "список путей":
                    outputPathCommand();
                    break;
                case "выполни поиск":
                    findCommand(text);
                    break;
                default:                    
                    break;
            }
        }

        private void echoCommand(string text)
        {
            chatAdd(text);
        }
        
        private void openCommand(string text)
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
                    chatAdd("Простите, не удалось открыть программу " + text);
                    return;
                }                    
            }
            chatAdd("Программа открыта");
        }

        private void addPathCommand(string text)
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

        private void outputPathCommand()
        {
            foreach (var item in dict)
            {
                chatAdd(item.Key + ": " + item.Value);
            }
        }

        private void findCommand(string text)
        {
            Process.Start($"https://www.google.com/search?q={text}");
            chatAdd("Перевожу на браузер");
        }
        private void chatAdd(string text)
        {
            rtbChat.SelectionAlignment = HorizontalAlignment.Left;
            rtbChat.AppendText(text + "\n");
        }
    }
}
