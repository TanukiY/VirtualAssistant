using System.Collections.Generic;
using System.Security.Policy;

namespace VirtualAssistant
{
    internal class Classifier
    {
        private IEnumerable<TrainingCommand> data;
        const byte Possible_Typo = 20;
        public void addDictionaryCmd(IEnumerable<TrainingCommand> trainingCommand)
        {
            this.data = trainingCommand;
        }
        public string[] Predict(string message)
        {
            var synonym = "";
            var writeCmd = "";
            TrainingCommand currentBest = null;
            var shortest = int.MaxValue;
            foreach (var item in message.Split(' '))
            {
                if (item == "")
                    continue;
                synonym += item;   
                foreach (TrainingCommand obs in this.data)
                {
                    int dist = LevenshteinDistance.Between(obs.Synonym, synonym);
                    if (dist <= shortest && (dist * 100) / synonym.Length <= Possible_Typo)
                    {
                        shortest = dist;
                        currentBest = obs;
                        writeCmd = synonym;
                    }
                }
                synonym += " ";
            }

            if (currentBest == null)
                return null;
            else
                return new string[] { currentBest.Reference, writeCmd };
        }
    }
}
