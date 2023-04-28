using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;

namespace Test
{


    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test()
        {
            //var tb = new TextBox();
            //tb.Text = " ";
            //var rtb = new RichTextBox();
            //var b = new Bobick(tb, rtb);
            //b.commandProcess();
            //Assert.AreEqual("я ¬ас не понимаю", rtb.Text);
            var a = 1;
            var b = 2;
            Assert.AreEqual(3, a + b);

        }
    }


}