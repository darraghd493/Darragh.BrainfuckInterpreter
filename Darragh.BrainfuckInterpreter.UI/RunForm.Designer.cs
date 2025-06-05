namespace Darragh.BrainfuckInterpreter.UI
{
    partial class RunForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            InputTextBox = new RichTextBox();
            OutputTextBox = new RichTextBox();
            SuspendLayout();
            // 
            // InputTextBox
            // 
            InputTextBox.Dock = DockStyle.Bottom;
            InputTextBox.Location = new Point(0, 418);
            InputTextBox.MaxLength = 1;
            InputTextBox.Multiline = false;
            InputTextBox.Name = "InputTextBox";
            InputTextBox.Size = new Size(800, 32);
            InputTextBox.TabIndex = 0;
            InputTextBox.Text = "";
            // 
            // OutputTextBox
            // 
            OutputTextBox.Dock = DockStyle.Fill;
            OutputTextBox.Location = new Point(0, 0);
            OutputTextBox.Name = "OutputTextBox";
            OutputTextBox.Size = new Size(800, 450);
            OutputTextBox.TabIndex = 1;
            OutputTextBox.Text = "";
            // 
            // RunForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(InputTextBox);
            Controls.Add(OutputTextBox);
            Name = "RunForm";
            Text = "Console";
            FormClosing += RunForm_FormClosing;
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox InputTextBox;
        private RichTextBox OutputTextBox;
    }
}