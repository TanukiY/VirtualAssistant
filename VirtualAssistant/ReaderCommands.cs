using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualAssistant
{
    internal class ReaderDictionaryCmd
    {
        private static DictionaryCmd CommandsFactory(string data)
        {
            var commaSeperated = data.Split(',');
            var reference = commaSeperated[0];
            var synonym = commaSeperated[1];

            return new DictionaryCmd(reference, synonym);
        }
        public static DictionaryCmd[] ReadCommands(string dataPath)
        {
            var data =
                File.ReadAllLines(dataPath)
                .Skip(1)
                .Select(CommandsFactory)
                .ToArray();

            return data;
        }
    }
}
