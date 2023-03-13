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
            if (tbMsg.Text != "Введите сообщение...")
            {
                rtbChat.SelectionAlignment = HorizontalAlignment.Right;
                rtbChat.AppendText(tbMsg.Text + "\n");
                rtbChat.SelectionAlignment = HorizontalAlignment.Left;
                rtbChat.AppendText(tbMsg.Text + "\n");
            }              
        }

        private void tbMsg_MouseClick(object sender, MouseEventArgs e)
        {
            tbMsg.Text = "";
        }

    }
}
