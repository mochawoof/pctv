using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pctv
{
    public class InputDialog : Form
    {
        public InputDialog()
        {
            InitializeComponent();
        }

        public string input = "";

        public DialogResult Show(string prompt, string defaultInput, string[] suggestions)
        {
            promptLabel.Text = prompt;
            inputBox.Text = defaultInput;
            inputBox.Items.AddRange(suggestions);
            this.ActiveControl = inputBox;
            return ShowDialog();
        }

        public DialogResult Show(string prompt, string defaultInput)
        {
            return Show(prompt, defaultInput, new string[0]);
        }

        private void InitializeComponent()
        {
            promptLabel = new Label();
            cancelButton = new Button();
            okButton = new Button();
            inputBox = new ComboBox();
            SuspendLayout();
            // 
            // promptLabel
            // 
            promptLabel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            promptLabel.Location = new Point(12, 9);
            promptLabel.Name = "promptLabel";
            promptLabel.Size = new Size(260, 39);
            promptLabel.TabIndex = 0;
            // 
            // cancelButton
            // 
            cancelButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            cancelButton.Location = new Point(116, 80);
            cancelButton.Name = "cancelButton";
            cancelButton.Size = new Size(75, 23);
            cancelButton.TabIndex = 1;
            cancelButton.Text = "Cancel";
            cancelButton.UseVisualStyleBackColor = true;
            // 
            // okButton
            // 
            okButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            okButton.Location = new Point(197, 80);
            okButton.Name = "okButton";
            okButton.Size = new Size(75, 23);
            okButton.TabIndex = 2;
            okButton.Text = "OK";
            okButton.UseVisualStyleBackColor = true;
            okButton.Click += okButton_Click;
            // 
            // inputBox
            // 
            inputBox.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            inputBox.FormattingEnabled = true;
            inputBox.Location = new Point(12, 51);
            inputBox.Name = "inputBox";
            inputBox.Size = new Size(260, 23);
            inputBox.TabIndex = 3;
            // 
            // InputDialog
            // 
            AcceptButton = okButton;
            CancelButton = cancelButton;
            ClientSize = new Size(284, 108);
            Controls.Add(inputBox);
            Controls.Add(okButton);
            Controls.Add(cancelButton);
            Controls.Add(promptLabel);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            Name = "InputDialog";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Input";
            ResumeLayout(false);
        }

        private Label promptLabel;
        private Button cancelButton;
        private Button okButton;

        private void okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            input = inputBox.Text;
            this.Close();
        }

        private ComboBox inputBox;
    }
}
