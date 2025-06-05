using FastColoredTextBoxNS;

namespace Darragh.BrainfuckInterpreter.UI
{
    public partial class MainForm : Form
    {
        private readonly TextStyle BRANCHING_STYLE = new TextStyle(Brushes.Black, null, FontStyle.Regular);
        private readonly TextStyle POINTER_STYLE = new TextStyle(Brushes.Orange, null, FontStyle.Bold);
        private readonly TextStyle BYTE_STYLE = new TextStyle(Brushes.Green, null, FontStyle.Bold);
        private readonly TextStyle IO_STYLE = new TextStyle(Brushes.Blue, null, FontStyle.Bold);
        private readonly TextStyle OTHER_STYLE = new TextStyle(Brushes.Gray, null, FontStyle.Regular);

        public MainForm()
        {
            InitializeComponent();
            CreateMenu();
        }

        private void CreateMenu()
        {
            MenuStrip menuStrip = new MenuStrip();
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("File");
            ToolStripMenuItem runMenu = new ToolStripMenuItem("Run");
            ToolStripMenuItem helpMenu = new ToolStripMenuItem("Help");

            fileMenu.DropDownItems.Add("Open", null, FileOpen_Click);
            fileMenu.DropDownItems.Add("Save", null, FileSave_Click);
            fileMenu.DropDownItems.Add("Exit", null, (s, e) => Application.Exit());

            runMenu.Click += (s, e) =>
            {
                string content = CodeTextBox.Text;
                if (content.Length == 0)
                {
                    MessageBox.Show("Please enter some Brainfuck code to run.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                try
                {
                    RunForm runForm = new RunForm(CodeTextBox.Text);
                    runForm.Execute();
                    runForm.ShowDialog();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while running the code: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };

            helpMenu.Click += (s, e) => MessageBox.Show("Brainfuck Interpreter v1.0.0\nDeveloped by darragh493", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);

            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(runMenu);
            menuStrip.Items.Add(helpMenu);

            MainMenuStrip = menuStrip;
            Controls.Add(menuStrip);
        }

        private void FileOpen_Click(object? sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Brainfuck (*.b)|*.b|All Files (*.*)|*.*",
                DefaultExt = "b"
            };
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;
                MessageBox.Show($"File opened: {filePath}", "Open File", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void FileSave_Click(object? sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Brainfuck (*.b)|*.b|All Files (*.*)|*.*",
                DefaultExt = "b"
            };
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {

                MessageBox.Show($"File saved as: {saveFileDialog.FileName}", "Save File", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CodeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            e.ChangedRange.ClearStyle(BRANCHING_STYLE, POINTER_STYLE, BYTE_STYLE, IO_STYLE, OTHER_STYLE);
            e.ChangedRange.SetStyle(BRANCHING_STYLE, @"[\[\]]");
            e.ChangedRange.SetStyle(POINTER_STYLE, @"[><]");
            e.ChangedRange.SetStyle(BYTE_STYLE, @"[+-]");
            e.ChangedRange.SetStyle(IO_STYLE, @"[.,]");
            e.ChangedRange.SetStyle(OTHER_STYLE, @"[^><+\-.,\[\]]"); // Grey for everything else
        }
    }
}
