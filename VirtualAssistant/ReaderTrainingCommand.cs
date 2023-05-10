using System.IO;
using System.Linq;

namespace VirtualAssistant
{
    internal class ReaderTrainingCommand
    {
        private static TrainingCommand CommandsFactory(string data)
        {
            var commaSeperated = data.Split(',');
            var reference = commaSeperated[0];
            var synonym = commaSeperated[1];

            return new TrainingCommand(reference, synonym);
        }
        public static TrainingCommand[] ReadCommands(string dataPath)
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
