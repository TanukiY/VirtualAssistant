using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VirtualAssistant
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            var command = tbMsg.Text.Trim().Split()[0].ToLower();
            var text = tbMsg.Text.Substring(command.Length);

            if (command == "echo" || command == "скажи")
            {
                rtbChat.SelectionAlignment = HorizontalAlignment.Right;
                rtbChat.AppendText(text + "\n");
                rtbChat.SelectionAlignment = HorizontalAlignment.Left;
                rtbChat.AppendText(text + "\n");
            }              
        }

        private void tbMsg_MouseClick(object sender, MouseEventArgs e)
        {
            tbMsg.Text = "";
        }

    }
}
