using VirtualAssistant;
using NUnit.Framework;

namespace Unit
{
    [TestFixture]
    public class BobickTest
    {
        [Test]
        public void TestZeroMessage()
        {
            var ot = Bobick.DistributionUserMessage("");
            Assert.AreEqual("Вы ничего не ввели", ot);
        }

        [Test]
        public void TestMsgNotCmd()
        {
            var ot = Bobick.DistributionUserMessage("afafaf");
            Assert.AreEqual("Извините, я Вас не понял, повтроите попытку", ot);
        }
    }

    [TestFixture]
    public class LevenshteinDistanceTest
    {
        [Test]
        public void TestMiss1()
        {
            var ot = LevenshteinDistance.Between("открой", "отклой");
            Assert.AreEqual(1, ot);
        }

        [Test]
        public void TestMissAll()
        {
            var ot = LevenshteinDistance.Between("открой", "dwgjom");
            Assert.AreEqual(6, ot);
        }

        [Test]
        public void TestMissSymbol()
        {
            var ot = LevenshteinDistance.Between("выполни поиск", "выплни поиск");
            Assert.AreEqual(1, ot);
        }
    }

    [TestFixture]
    public class ClassifierTest
    {
        [Test]
        public void TestOutput()
        {
            Classifier classifier = new Classifier();
            TrainingCommand[] trainingCommands;
            var nameOfTraingFile = "../../Source/trainingsample.csv";
            trainingCommands = ReaderTrainingCommand.ReadCommands(nameOfTraingFile);
            classifier.addDictionaryCmd(trainingCommands);
            var ot = classifier.Predict("отклой");
            Assert.AreEqual(new string[] { "открой", "отклой" }, ot);
        }

        [Test]
        public void TestNull()
        {
            Classifier classifier = new Classifier();
            TrainingCommand[] trainingCommands;
            var nameOfTraingFile = "../../Source/trainingsample.csv";
            trainingCommands = ReaderTrainingCommand.ReadCommands(nameOfTraingFile);
            classifier.addDictionaryCmd(trainingCommands);
            var ot = classifier.Predict("");
            Assert.AreEqual(null, ot);
        }

    }

    //ToDo
}
