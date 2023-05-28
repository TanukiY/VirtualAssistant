namespace VirtualAssistant
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tbMsg = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.backFromChat = new System.Windows.Forms.Panel();
            this.GptCheckBox1 = new System.Windows.Forms.CheckBox();
            this.rtbChat = new System.Windows.Forms.RichTextBox();
            this.btnVoice = new System.Windows.Forms.Button();
            this.backFromChat.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbMsg
            // 
            this.tbMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbMsg.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tbMsg.Location = new System.Drawing.Point(16, 676);
            this.tbMsg.Margin = new System.Windows.Forms.Padding(7);
            this.tbMsg.Multiline = true;
            this.tbMsg.Name = "tbMsg";
            this.tbMsg.Size = new System.Drawing.Size(574, 79);
            this.tbMsg.TabIndex = 0;
            this.tbMsg.TabStop = false;
            this.tbMsg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbMsg_KeyDown);
            this.tbMsg.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tbMsg_MouseDown);
            // 
            // btnSend
            // 
            this.btnSend.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnSend.Location = new System.Drawing.Point(604, 676);
            this.btnSend.Margin = new System.Windows.Forms.Padding(7);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(85, 79);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = " Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // backFromChat
            // 
            this.backFromChat.BackColor = System.Drawing.Color.WhiteSmoke;
            this.backFromChat.Controls.Add(this.GptCheckBox1);
            this.backFromChat.Controls.Add(this.rtbChat);
            this.backFromChat.Location = new System.Drawing.Point(16, 16);
            this.backFromChat.Name = "backFromChat";
            this.backFromChat.Size = new System.Drawing.Size(768, 620);
            this.backFromChat.TabIndex = 3;
            // 
            // GptCheckBox1
            // 
            this.GptCheckBox1.AutoSize = true;
            this.GptCheckBox1.Checked = true;
            this.GptCheckBox1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.GptCheckBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GptCheckBox1.ForeColor = System.Drawing.Color.Black;
            this.GptCheckBox1.Location = new System.Drawing.Point(19, 581);
            this.GptCheckBox1.Name = "GptCheckBox1";
            this.GptCheckBox1.Size = new System.Drawing.Size(130, 24);
            this.GptCheckBox1.TabIndex = 5;
            this.GptCheckBox1.Text = "Activate GPT";
            this.GptCheckBox1.UseVisualStyleBackColor = true;
            this.GptCheckBox1.CheckedChanged += new System.EventHandler(this.GptCheckBox1_CheckedChanged);
            // 
            // rtbChat
            // 
            this.rtbChat.BackColor = System.Drawing.Color.White;
            this.rtbChat.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbChat.Cursor = System.Windows.Forms.Cursors.Default;
            this.rtbChat.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rtbChat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(87)))), ((int)(((byte)(235)))));
            this.rtbChat.Location = new System.Drawing.Point(19, 17);
            this.rtbChat.Margin = new System.Windows.Forms.Padding(7);
            this.rtbChat.Name = "rtbChat";
            this.rtbChat.ReadOnly = true;
            this.rtbChat.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            this.rtbChat.Size = new System.Drawing.Size(728, 554);
            this.rtbChat.TabIndex = 2;
            this.rtbChat.TabStop = false;
            this.rtbChat.Text = "";
            // 
            // btnVoice
            // 
            this.btnVoice.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnVoice.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnVoice.Location = new System.Drawing.Point(699, 676);
            this.btnVoice.Margin = new System.Windows.Forms.Padding(7);
            this.btnVoice.Name = "btnVoice";
            this.btnVoice.Size = new System.Drawing.Size(85, 79);
            this.btnVoice.TabIndex = 4;
            this.btnVoice.UseVisualStyleBackColor = false;
            this.btnVoice.MouseDown += new System.Windows.Forms.MouseEventHandler(this.btn_voice_MouseDown);
            this.btnVoice.MouseUp += new System.Windows.Forms.MouseEventHandler(this.btn_voice_MouseUp);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.ClientSize = new System.Drawing.Size(800, 771);
            this.Controls.Add(this.btnVoice);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.tbMsg);
            this.Controls.Add(this.backFromChat);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Bobick";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.backFromChat.ResumeLayout(false);
            this.backFromChat.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbMsg;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Panel backFromChat;
        private System.Windows.Forms.RichTextBox rtbChat;
        private System.Windows.Forms.Button btnVoice;
        private System.Windows.Forms.CheckBox GptCheckBox1;
    }
}

