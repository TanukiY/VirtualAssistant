using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Forms;
using System.Drawing;

namespace VirtualAssistant
{
    public partial class InputBoxForm : Form
    {
        private TextBox textBox;
        private Label label;
        private int fontSize = 14;

        public InputBoxForm(string prompt)
        {
            InitializeComponents(prompt);
        }

        private void InitializeComponents(string prompt)
        {
            label = new Label();
            label.Text = "Введите название/ключ для файла";
            label.Location = new System.Drawing.Point(12, 12);
            label.Size = new System.Drawing.Size(200, 40);
            label.Font = new Font("Lato", 12);

            textBox = new TextBox();
            textBox.Location = new System.Drawing.Point(12, 70);
            textBox.Size = new System.Drawing.Size(200, 20);
            textBox.Font = new Font("Lato", 12);

            Button okButton = new Button();
            okButton.Location = new System.Drawing.Point(12, 120);
            okButton.Height = 40;
            okButton.Text = "OK";
            okButton.DialogResult = DialogResult.OK;
            okButton.Click += OkButton_Click;
            okButton.Font = new Font("Lato", 12);

            Button cancelButton = new Button();
            cancelButton.Height = 40;
            cancelButton.Location = new System.Drawing.Point(135, 120);
            cancelButton.Text = "Cancel";
            cancelButton.DialogResult = DialogResult.Cancel;
            cancelButton.Font = new Font("Lato", 12);

            Controls.Add(label);
            Controls.Add(textBox);
            Controls.Add(okButton);
            Controls.Add(cancelButton);

            Text = "Ключ пути";
            Font = new Font("Lato", 12);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            StartPosition = FormStartPosition.CenterScreen;
            MinimizeBox = false;
            MaximizeBox = false;
            AcceptButton = okButton;
            CancelButton = cancelButton;
            Height = 240;
            Width = 245;

            Label promptLabel = new Label();
            promptLabel.Text = prompt;
            promptLabel.Location = new System.Drawing.Point(12, 12);
            promptLabel.Size = new System.Drawing.Size(200, 20);
            Controls.Add(promptLabel);
        }

        public static string Show(string prompt)
        {
            using (var form = new InputBoxForm(prompt))
            {
                if (form.ShowDialog() == DialogResult.OK)
                {
                    return form.textBox.Text;
                }
            }
            return string.Empty;
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
