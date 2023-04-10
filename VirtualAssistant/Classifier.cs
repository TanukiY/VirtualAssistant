using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualAssistant
{
    internal class Classifier
    {
        private IEnumerable<DictionaryCmd> data;
        const byte Possible_Typo = 20;
        public void addDictionaryCmd(IEnumerable<DictionaryCmd> dictionaryCmd)
        {
            this.data = dictionaryCmd;
        }
        public string[] Predict(string message)
        {
            var synonym = "";
            var writeCmd = "";
            DictionaryCmd currentBest = null;
            var shortest = Double.MaxValue;
            foreach (var item in message.Split(' '))
            {
                synonym += item;   
                foreach (DictionaryCmd obs in this.data)
                {
                    double dist = LevenshteinDistance.Between(obs.Synonym, synonym);
                    if (dist <= shortest || (dist * 100) / synonym.Length< Possible_Typo)
                    {
                        shortest = dist;
                        currentBest = obs;
                        writeCmd = synonym;
                    }
                }
                synonym += " ";
            }
            return new string[] { currentBest.Reference, writeCmd };
        }
    }
}
