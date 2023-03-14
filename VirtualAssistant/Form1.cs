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

        private void tbMsg_MouseClick(object sender, MouseEventArgs e)
        {
            tbMsg.Text = "";
        }

    }
}
