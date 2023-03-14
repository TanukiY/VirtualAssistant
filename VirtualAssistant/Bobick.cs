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
                    chatAdd("Простите, не удалось открыть программу");
            }            
        }

        public void chatAdd(string text)
        {
            rtbChat.SelectionAlignment = HorizontalAlignment.Left;
            rtbChat.AppendText(text + "\n");
        }
    }
}
