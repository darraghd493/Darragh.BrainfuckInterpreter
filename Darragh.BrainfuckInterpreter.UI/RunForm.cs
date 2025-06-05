using Darragh.BrainfuckInterpreter.Tokens;
using System.Text;

namespace Darragh.BrainfuckInterpreter.UI // TODO: Clean up code
{
    public partial class RunForm : Form
    {
        private Thread? thread;
        private string content;
        private volatile bool stopping = false;

        public RunForm(string content)
        {
            InitializeComponent();
            this.content = content;
        }

        private void RunForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            stopping = true;
            if (thread != null && thread.IsAlive)
            {
                thread.Join(500);
                thread.Interrupt();
            }
        }

        public void Execute()
        {
            Parser parser = new Parser(content, ParserOptions.Default);
            Token[] tokens = parser.Parse();

            thread = new Thread(() =>
            {
                Interpreter interpreter = new Interpreter(tokens, InterpreterOptions.Default);

                interpreter.OnOutput += (output) =>
                {
                    string text = Encoding.ASCII.GetString(new byte[] { output });
                    OutputTextBox.Invoke(() => OutputTextBox.AppendText(text));
                };

                interpreter.OnInput += () =>
                {
                    byte input = 0;

                    InputTextBox.Invoke(() =>
                    {
                        InputTextBox.Clear();
                        InputTextBox.Focus();
                    });

                    while (true)
                    {
                        if (stopping)
                        {
                            throw new ThreadInterruptedException();
                        }

                        string text = string.Empty;

                        InputTextBox.Invoke(() => text = InputTextBox.Text);

                        if (!string.IsNullOrEmpty(text))
                        {
                            input = (byte)text[0];
                            InputTextBox.Invoke(() => InputTextBox.Clear());
                            break;
                        }

                        Thread.Sleep(50);
                    }

                    return input;
                };


                try
                {
                    interpreter.Run();
                }
                catch (ThreadInterruptedException)
                {
                    // Graceful stop on close
                }
            })
            {
                IsBackground = true
            };

            thread.Start();
        }
    }
}
