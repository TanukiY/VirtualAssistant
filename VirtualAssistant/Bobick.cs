using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace VirtualAssistant
{
    internal static class Bobick
    {
        private static string UserMessage { get; set; }
        private static string BobickMessage { get; set; }

        readonly static Dictionary<string, Action> commandsDictionary = new Dictionary<string, Action>()
        {
            { "открой", OpenCommand },
            { "скажи", SayCommand },
            { "добавь путь", AddPathCommand },
            { "список путей", OutputPathCommand },
            { "выполни поиск", FindCommand }
        };

        public static string DistributionUserMessage(string userMessage)
        {
            UserMessage = userMessage;         
            BugCatcher bugCatcher= new BugCatcher();
            string bug = bugCatcher.TreatmentBug(UserMessage);
            if(bug!=null)
                return bug;
            Handler.AddUserMessage(UserMessage);

            string command = Handler.SearchCommandName();
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

        private static void SayCommand()
        {
            var sayMessage = Handler.DeleteUserCommand(UserMessage);
            AnswerFromBobick(sayMessage);
        }

        private static void OpenCommand()
        {
            var pathAndName = Handler.SearchingForPathFile();
            if (pathAndName == null){
                AnswerFromBobick($"Простите, такой программы нет ");
                return;
            }

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
        private static void AddPathCommand()
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

            string userInput = InputBoxForm.Show("Введите значение:");
            if (userInput == null || userInput == "")
            {
                AnswerFromBobick("Операция отменена");
                return;
            }
            Handler.AddPathInFile(userInput, selectedFile);
            AnswerFromBobick("Файл и ключ установлен");

        }

        private static void OutputPathCommand()
        {
            var showPath = Handler.ShowPath();
            AnswerFromBobick(showPath);
        }

        private static void FindCommand()
        {
            var searchCmd = Handler.DeleteUserCommand(UserMessage);
            Process.Start($"https://www.google.com/search?q={searchCmd}");
            AnswerFromBobick("Перевожу на браузер");
        }

        private static void AnswerFromBobick(string text)
        {
            BobickMessage = text;
        }
    }    
}
