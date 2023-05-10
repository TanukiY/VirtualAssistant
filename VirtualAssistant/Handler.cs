using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace VirtualAssistant
{
    internal class Handler
    {        
        private TrainingCommand[] trainingCommands;
        private Classifier classifier = new Classifier();

        private Dictionary<string, int> dictCmdMatches;
        private Dictionary<string, string> dictForSaveUserCmd;
        private Dictionary<string, string> dictPath;
        private string UserCommand { get; set; }
        private string[] MassUserMsg { get; set; }
        public Handler(string userMessage)
        {
            MassUserMsg = userMessage.Split(' ');
           
            var nameOfTraingFile = "../../Source/trainingsample.csv";
            trainingCommands = ReaderTrainingCommand.ReadCommands(nameOfTraingFile);
            classifier.addDictionaryCmd(trainingCommands);

            dictCmdMatches = new Dictionary<string, int> {
                {"открой", 0 },
                {"скажи", 0 },
                {"добавь путь", 0 },
                {"список путей", 0 },
                {"выполни поиск", 0 }
            };

            dictForSaveUserCmd = new Dictionary<string, string> { };

            var nameOfPathFile = File.ReadAllText("../../Source/path.json");
            dictPath = JsonConvert.DeserializeObject<Dictionary<string, string>>(nameOfPathFile);
        }    
        
        //ToDo: посмотреть и переделать
        public string SearchCommandName()
        {
            foreach (var word in MassUserMsg)
            {
                string[] commandsName = classifier.Predict(word);
                if (commandsName == null)
                    continue;
                dictCmdMatches[commandsName[0]] += 1;
                dictForSaveUserCmd[commandsName[0]] = commandsName[1];
            }
            var max = int.MinValue;
            string best = null;
            foreach (var cmd in dictCmdMatches)
            {
                if (cmd.Value >= max)
                {
                    max = cmd.Value;
                    best = cmd.Key;
                }
            }
            
            if (dictForSaveUserCmd.ContainsKey(best))
                UserCommand = dictForSaveUserCmd[best];

            if (max == 0)
                return null; 
            else
                return best;
        } 

        public string[] SearchingForPathFile()
        {
            string[] app = null;
            foreach (var word in MassUserMsg)
            {
                if (dictPath.ContainsKey(word))
                    app = new string[] { word, dictPath[word] };
            }
            return app;
        }

        public void AddPathInFile(string key, string path)
        {
            dictPath.Add(key, path);
            string reading = JsonConvert.SerializeObject(dictPath);
            File.WriteAllText("../../Source/path.json", reading);
        }

        public string DeleteUserCommand(string userMessage)
        {            
            return userMessage.Replace(UserCommand, ""); ;
        }

        public string ShowPath()
        {
            string str = "";
            foreach (var item in dictPath)
            {
                str += item.Key + ": " + item.Value + "\n";
            }
            return str;
        }
    }
}
