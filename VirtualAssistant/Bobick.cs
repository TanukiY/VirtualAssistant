using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace VirtualAssistant
{
    internal class Bobick
    {
        private Handler handler;
        private string UserMessage { get; set; }
        private string BobickMessage { get; set; }

        readonly Dictionary<string, Action> commandsDictionary;

        public Bobick()
        {
            commandsDictionary = new Dictionary<string, Action>()
            {
                { "открой", OpenCommand },
                { "скажи", SayCommand },
                { "добавь путь", AddPathCommand },
                { "список путей", OutputPathCommand },
                { "выполни поиск", FindCommand }
            };
        }
        public string DistributionUserMessage(string userMessage)
        {
            UserMessage = userMessage;         
            BugCatcher bugCatcher= new BugCatcher();
            string bug = bugCatcher.TreatmentBug(UserMessage);
            if(bug!=null)
                return bug;
            handler = new Handler(UserMessage);

            string command = handler.SearchCommandName();
            if (command == null)
            {
                AnswerFromBobick("Извините, я Вас не понял, повтроите попытку");
            }
            else if (commandsDictionary.ContainsKey(command))
            {
                commandsDictionary[command].Invoke();
            }
            return BobickMessage;
        }

        private void SayCommand()
        {
            var sayMessage = handler.DeleteUserCommand(UserMessage);
            AnswerFromBobick(sayMessage);
        }

        private void OpenCommand()
        {
            var pathAndName = handler.SearchingForPathFile();
            var name = pathAndName[0];
            var path = pathAndName[1];
            try
            {
                Process.Start(name);
            }
            catch (Exception)
            {
                if (path != null)
                    Process.Start(path);
                else
                {
                    AnswerFromBobick($"Простите, не удалось открыть программу {name}");
                    return;
                }
            }
            AnswerFromBobick($"Программа {name} открыта");
        }

        //ToDo: сделать свой input, а то этот убогий
        //ToDo: проверка на уникальный ключ и null
        private void AddPathCommand()
        {
            string selectedFile = "";

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Title = "Выберите файл";
                openFileDialog.Filter = "Все файлы (*.*)|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedFile = openFileDialog.FileName;
                }
            }
                
            string input = Microsoft.VisualBasic.Interaction.InputBox("Введите ключ-название для указанного пути:", "Ключ пути", "tg");

            handler.AddPathInFile(input, selectedFile);
        }

        private void OutputPathCommand()
        {
            var showPath = handler.ShowPath();
            AnswerFromBobick(showPath);
        }

        private void FindCommand()
        {
            var searchCmd = handler.DeleteUserCommand(UserMessage);
            Process.Start($"https://www.google.com/search?q={searchCmd}");
            AnswerFromBobick("Перевожу на браузер");
        }

        private void AnswerFromBobick(string text)
        {
            BobickMessage = text;
        }
    }
}
