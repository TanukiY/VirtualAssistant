using System;
using System.Windows.Forms;
using VirtualAssistant;
using NUnit.Framework;

namespace Unit
{
    [TestFixture]
    public class UnitTest1
    {
        [Test]
        public void TestMethod1()
        {
            var tb = new TextBox();
            var rtb = new RichTextBox();
            tb.Text = " ";
            var b = new Bobick(tb, rtb);
            b.commandProcess();
            Assert.AreEqual("Я Вас не понял, попробуйте еще раз", rtb.Text);
        }
    }
}
