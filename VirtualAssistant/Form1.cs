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
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            bobick.command();
        }

        private void tbMsg_Enter(object sender, EventArgs e)
        {
            if (tbMsg.Text == "Введите сообщение...")
            {
                tbMsg.Text = "";
            }
        }

        private void tbMsg_Leave(object sender, EventArgs e)
        {
            if (tbMsg.Text == "")
            {
                tbMsg.Text = "Введите сообщение...";
            }
        }
    }
}
