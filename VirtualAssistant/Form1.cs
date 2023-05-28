using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using VisioForge.Libs.NAudio.Wave;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace VirtualAssistant
{   
    public partial class Form1 : Form
    {
        WaveFormat waveFormat = new WaveFormat(48000, 16, 1);
        MemoryStream stream;
        WaveInEvent waveInEvent;

        //ToDo: Своя форма, и так сойдет
        public Form1()
        {
            InitializeComponent();

            Color startColor = Color.FromArgb(136, 134, 230);
            Color endColor = Color.FromArgb(170, 192, 228);  
           
            LinearGradientBrush gradientBrush = new LinearGradientBrush(
                new Rectangle(0, 0, Width, Height), 
                startColor,                                    
                endColor,                                       
                LinearGradientMode.Horizontal                     
            );

            BackgroundImage = new Bitmap(Width, Height);
            Graphics g = Graphics.FromImage(BackgroundImage);
            g.FillRectangle(gradientBrush, new Rectangle(0, 0, Width, Height));
            MaximizeBox = false;
            MaximumSize = Size;
            MinimumSize = Size;
        }
        private void InitializedBtnSend()
        {
            btnSend.Font = new Font("Lato", 12);
            btnSend.ForeColor = Color.Black;
        }
        private void InitializedTbMsg()
        {
            tbMsg.BackColor = Color.WhiteSmoke;
            tbMsg.Font = new Font("Lato", 12);
            tbMsg.ForeColor = Color.Black;
        }
        private void InitializedRtbChat()
        {
            rtbChat.BackColor = Color.WhiteSmoke;
            rtbChat.Font = new Font("Lato", 12);
            rtbChat.ForeColor = Color.Black;
            rtbChat.ReadOnly = true;
            rtbChat.Enabled = true;
        }
        private void InitializedBtnVoice()
        {
            btnVoice.FlatAppearance.BorderSize = 0;
            btnVoice.BackColor = Color.White;
            btnVoice.BackgroundImage = Image.FromFile("../../Source/image/micro.png");
            btnVoice.BackgroundImageLayout = ImageLayout.Center;
        }
        private void Form1_Load(object sender, EventArgs e)
        {            
            InitializedRtbChat();
            InitializedBtnVoice();
            InitializedBtnSend();
            InitializedTbMsg();

            ExpectationFromTbMsg();
        }

        private void btnSend_Click(object sender, EventArgs e)
        {            
            if (tbMsg.Text!="Введите сообщение...")
            {
                BlockedAll();
                StartBobick();
            }                
        }

        private void tbMsg_KeyDown(object sender, KeyEventArgs e)
        {           
            if (tbMsg.Text == "Введите сообщение...")
            {
                if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Right || e.KeyCode == Keys.Up
                    || e.KeyCode == Keys.Down || e.KeyCode == Keys.Enter
                    || e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.ControlKey)
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
                    BlockedAll();
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
                if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
                {
                    tbMsg.Capture = true;
                    tbMsg.Select(0, 0);
                }
            }
        }

        private async void StartBobick()
        {
            OutputText("right", tbMsg.Text);
            rtbChat.AppendText("---------------------------" + "\n");
            var resultToAnswer = "";
            if (GptCheckBox1.Checked)
            {
                var gptTask = GptAssistent.StartGPTAsync(tbMsg.Text);
                ExpectationFromTbMsg();
                rtbChat.AppendText(" ");
                while (!gptTask.IsCompleted)
                {                    
                    int lastLineIndex = rtbChat.Lines.Count()-1;
                    int lastLineStartIndex = rtbChat.GetFirstCharIndexOfCurrentLine();
                    int lastLineLength = rtbChat.Lines[lastLineIndex].Length;
                    rtbChat.Select(lastLineStartIndex, lastLineLength);
                    rtbChat.SelectionAlignment = HorizontalAlignment.Left;

                    rtbChat.SelectedText = "Печатает.";

                    lastLineLength = rtbChat.Lines[lastLineIndex].Length;
                    rtbChat.Select(lastLineStartIndex, lastLineLength);
                    await Task.Delay(500);
                    rtbChat.SelectedText = "Печатает..";

                    lastLineLength = rtbChat.Lines[lastLineIndex].Length;
                    rtbChat.Select(lastLineStartIndex, lastLineLength);
                    await Task.Delay(500);
                    rtbChat.SelectedText = "Печатает...";

                    await Task.Delay(500);
                }
                resultToAnswer = await gptTask;

                int lastLineIndex1 = rtbChat.Lines.Count() - 1;
                int lastLineStartIndex1 = rtbChat.GetFirstCharIndexOfCurrentLine();
                int lastLineLength1 = rtbChat.Lines[lastLineIndex1].Length;
                rtbChat.Select(lastLineStartIndex1, lastLineLength1);
                lastLineLength1 = rtbChat.Lines[lastLineIndex1].Length;
                rtbChat.Select(lastLineStartIndex1, lastLineLength1);
                rtbChat.SelectedText = resultToAnswer + "\n";
                rtbChat.AppendText("---------------------------" + "\n");                
            }                
            else
            {                
                resultToAnswer = Bobick.DistributionUserMessage(tbMsg.Text);
                OutputText("left", resultToAnswer);                
                rtbChat.AppendText("---------------------------" + "\n");
                ExpectationFromTbMsg();
            }
                        
            btnVoice.Enabled = true;
            tbMsg.Enabled = true;
            btnSend.Enabled = true;
            GptCheckBox1.Enabled = true;
            tbMsg.Focus();
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
            btnVoice.BackColor= Color.Red;
            waveInEvent = new WaveInEvent();
            stream = new MemoryStream();
            waveInEvent.WaveFormat = waveFormat;
            waveInEvent.DataAvailable += (senderWave, eWave) =>
            {
                stream.Write(eWave.Buffer, 0, eWave.BytesRecorded);
            };
            waveInEvent.StartRecording();            
        }

        private void OutputText(string orientation, string text)
        {
            if(orientation == "right")
            {
                rtbChat.SelectionAlignment = HorizontalAlignment.Right;
                rtbChat.AppendText(text + "\n");
            }
            else
            {
                rtbChat.SelectionAlignment = HorizontalAlignment.Left;
                rtbChat.AppendText(text + "\n");
            }               
        }

        private async void btn_voice_MouseUp(object sender, MouseEventArgs e)
        {
            BlockedAll();
            btnVoice.BackColor = Color.White;
            waveInEvent.StopRecording();
            var data = stream.ToArray();
            if (data.Length < 1000)
                return;
            var textFromVoice = await SpeechKit.ConvertSpeechToText(data);
            waveInEvent.Dispose();
            stream.Dispose();
            if (textFromVoice == "")
                return;
            tbMsg.Text = textFromVoice;
            StartBobick();            
        }

        private void BlockedAll()
        {
            btnVoice.Enabled = false;
            tbMsg.Enabled = false;
            btnSend.Enabled = false;
            GptCheckBox1.Enabled = false;
        }

        private void GptCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            tbMsg.Focus();
        }
    }
}
