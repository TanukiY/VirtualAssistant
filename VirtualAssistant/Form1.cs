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
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using VisioForge.Libs.NAudio.Wave;
using System.IO;
using VisioForge.Libs.WindowsMediaLib;
using System.Threading;

namespace VirtualAssistant
{   
    public partial class Form1 : Form
    {
        Bobick bobick;
        WaveFormat waveFormat = new WaveFormat(48000, 16, 1);
        MemoryStream stream = new MemoryStream();
        WaveInEvent waveInEvent = new WaveInEvent();
        SpeechKit speechKit = new SpeechKit();

        //ToDo: Своя форма, и так сойдет
        public Form1()
        {
            InitializeComponent();

            Color startColor = Color.FromArgb(136, 134, 230);
            Color endColor = Color.FromArgb(170, 192, 228);  
           
            LinearGradientBrush gradientBrush = new LinearGradientBrush(
                new Rectangle(0, 0, this.Width, this.Height), 
                startColor,                                    
                endColor,                                       
                LinearGradientMode.Horizontal                     
            );

            this.BackgroundImage = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(this.BackgroundImage);
            g.FillRectangle(gradientBrush, new Rectangle(0, 0, this.Width, this.Height));

            rtbChat.BackColor = Color.WhiteSmoke;
            panel1.BackColor = Color.WhiteSmoke;
            tbMsg.BackColor = Color.WhiteSmoke;

            rtbChat.Font = new Font("Lato", 12);
            tbMsg.Font = new Font("Lato", 12);
            btnSend.Font = new Font("Lato", 12);

            rtbChat.ForeColor = Color.Black;
            tbMsg.ForeColor = Color.Black;
            btnSend.ForeColor = Color.Black;

            this.MaximizeBox = false;    
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {            
            bobick = new Bobick();
            ExpectationFromTbMsg();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (tbMsg.Text!="Введите сообщение...")
                StartBobick();
        }

        private void tbMsg_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (tbMsg.Text == "Введите сообщение...")
            {
                if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up
                    || e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter)
                {
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                }
                else if(e.KeyCode!= Keys.Back)
                {
                    tbMsg.Text="";
                    tbMsg.ForeColor = Color.Black;
                }
            }
            else
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    StartBobick();
                }                    
                if ((tbMsg.Text.Length==1 && e.KeyCode==Keys.Back) || tbMsg.Text=="")
                    ExpectationFromTbMsg();

            }
        }


        //Блокировка нажатия мыши, когда надпись Введите соо...
        //ToDo Исправить выделение или придумать другой способ
        private void tbMsg_MouseDown(object sender, MouseEventArgs e)
        {
            if (tbMsg.Text == "Введите сообщение...")
            {
                if (e.Button == MouseButtons.Left)
                {
                    tbMsg.Capture = true;
                    tbMsg.Select(0, 0);
                }

            }
        }

        private void StartBobick()
        {
            rtbChat.SelectionAlignment = HorizontalAlignment.Right;
            rtbChat.AppendText(tbMsg.Text + "\n");

            var resultToAnswer = bobick.DistributionUserMessage(tbMsg.Text);
            
            rtbChat.SelectionAlignment = HorizontalAlignment.Left;
            rtbChat.AppendText(resultToAnswer + "\n");

            ExpectationFromTbMsg();
        }

        private void ExpectationFromTbMsg()
        {
            tbMsg.Select();
            tbMsg.ForeColor = Color.Gray;
            tbMsg.Text = "Введите сообщение...";
            tbMsg.Select(0, 0);
        }

        private void btn_voice_MouseDown(object sender, MouseEventArgs e)
        {
            btn_voice.BackColor= Color.Red;
            
            waveInEvent.WaveFormat = waveFormat;
            waveInEvent.DataAvailable += (senderWave, eWave) =>
            {
                stream.Write(eWave.Buffer, 0, eWave.BytesRecorded);
            };
            waveInEvent.StartRecording();
        }

        private async void btn_voice_MouseUp(object sender, MouseEventArgs e)
        {
            btn_voice.BackColor = Color.White;
            waveInEvent.StopRecording();
            var data = stream.ToArray();
            var textFromVoice = await SpeechKit.ConvertSpeechToText(data);
            Thread.Sleep(1000);
            rtbChat.SelectionAlignment = HorizontalAlignment.Right;
            rtbChat.AppendText(textFromVoice + "\n");
            var resultToAnswer = bobick.DistributionUserMessage(textFromVoice.ToString());
            rtbChat.SelectionAlignment = HorizontalAlignment.Left;
            rtbChat.AppendText(resultToAnswer + "\n");
        }
    }
}
