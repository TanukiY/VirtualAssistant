using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace VirtualAssistant
{
    public partial class Form1 : Form
    {
        Bobick bobick;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            bobick = new Bobick(tbMsg, rtbChat);
            tbMsgActive();
        }        

        private void btnSend_Click(object sender, EventArgs e)
        {
            bobickStart();
        }

        private void tbMsg_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (tbMsg.Text == "Введите сообщение...")
            {
                tbMsg.Text = "";
                tbMsg.ForeColor = Color.Black;
            }
        }

        private void tbMsg_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                bobickStart();
        }

        public void bobickStart()
        {
            bobick.commandProcess();
            tbMsgActive();
        }

        public void tbMsgActive()
        {
            tbMsg.Select();
            tbMsg.ForeColor = Color.Gray;
            tbMsg.Text = "Введите сообщение...";
            tbMsg.Select(0, 0);
        }       
    }
}
