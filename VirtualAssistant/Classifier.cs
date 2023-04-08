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
        public void addDictionaryCmd(IEnumerable<DictionaryCmd> dictionaryCmd)
        {
            this.data = dictionaryCmd;
        }
        public string Predict(string synonym)
        {
            DictionaryCmd currentBest = null;
            var shortest = Double.MaxValue;
            foreach (DictionaryCmd obs in this.data)
            {
                double dist = LevenshteinDistance.Between(obs.Synonym, synonym);
                if (dist < shortest)
                {
                    shortest = dist;
                    currentBest = obs;
                }
            }
            return currentBest.Reference;
        }
    }
}
