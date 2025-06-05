namespace Darragh.BrainfuckInterpreter.UI
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            CodeTextBox = new FastColoredTextBoxNS.FastColoredTextBox();
            ((System.ComponentModel.ISupportInitialize)CodeTextBox).BeginInit();
            SuspendLayout();
            // 
            // CodeTextBox
            // 
            CodeTextBox.AutoCompleteBrackets = true;
            CodeTextBox.AutoCompleteBracketsList = new char[]
    {
    '[',
    ']',
    ']',
    '['
    };
            CodeTextBox.AutoIndentChars = false;
            CodeTextBox.AutoIndentCharsPatterns = "^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>[^;=]+);\r\n^\\s*(case|default)\\s*[^:]*(?<range>:)\\s*(?<range>[^;]+);";
            CodeTextBox.AutoScrollMinSize = new Size(2475, 14);
            CodeTextBox.BackBrush = null;
            CodeTextBox.CharHeight = 14;
            CodeTextBox.CharWidth = 8;
            CodeTextBox.DisabledColor = Color.FromArgb(100, 180, 180, 180);
            CodeTextBox.Dock = DockStyle.Fill;
            CodeTextBox.Hotkeys = resources.GetString("CodeTextBox.Hotkeys");
            CodeTextBox.IsReplaceMode = false;
            CodeTextBox.Location = new Point(0, 0);
            CodeTextBox.Name = "CodeTextBox";
            CodeTextBox.Paddings = new Padding(0);
            CodeTextBox.SelectionColor = Color.FromArgb(60, 0, 0, 255);
            CodeTextBox.ServiceColors = (FastColoredTextBoxNS.ServiceColors)resources.GetObject("CodeTextBox.ServiceColors");
            CodeTextBox.Size = new Size(800, 450);
            CodeTextBox.TabIndex = 0;
            CodeTextBox.Text = resources.GetString("CodeTextBox.Text");
            CodeTextBox.Zoom = 100;
            CodeTextBox.TextChanged += CodeTextBox_TextChanged;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(CodeTextBox);
            Name = "MainForm";
            Text = "Brainfuck Interpreter";
            ((System.ComponentModel.ISupportInitialize)CodeTextBox).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private FastColoredTextBoxNS.FastColoredTextBox CodeTextBox;
    }
}
