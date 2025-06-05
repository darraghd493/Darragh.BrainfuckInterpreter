using Darragh.BrainfuckInterpreter.Tokens;
using System.Reflection.Emit;

namespace Darragh.BrainfuckInterpreter
{
    /* Triggers */
    public delegate byte InputTrigger();

    public delegate void OutputTrigger(byte data);

    /* Interpreter */
    public class Interpreter
    {
        /* Configuration */
        public InterpreterOptions options { get; set; }
        private Token[] tokens;

        /* Events */
        public event InputTrigger? OnInput;
        public event OutputTrigger? OnOutput;

        /* Storage */
        private byte[] data;
        private int dataPointer;
        private int instructionPointer;

        public Interpreter(Token[] tokens, InterpreterOptions options)
        {
            this.tokens = tokens;
            this.options = options;

            // Prepare bytes - brainf*** mandates that the bytes provided must all be of the value 0
            data = new byte[options.Bytes];
            for (int i = 0; i < options.Bytes; i++)
            {
                data[i] = 0;
            }
            dataPointer = instructionPointer = 0;
        }

        /* Handle execution */
        public void Run()
        {
            while (!IsComplete())
            {
                Step();
            }
        }

        public void Step()
        {
            Token token = tokens[instructionPointer];
            instructionPointer++;

            int dp = dataPointer; // Cached for faster local access

            switch (token.Bytecode)
            {
                case TokenBytecode.INCREMENT_POINTER:
                    dp++;
                    if (options.Wraparound && dp >= data.Length)
                    {
                        dp = 0;
                    }
                    break;
                case TokenBytecode.DECREMENT_POINTER:
                    dp--;
                    if (options.Wraparound && dp < 0)
                    {
                        dp = data.Length - 1;
                    }
                    break;
                case TokenBytecode.INCREMENT_BYTE:
                    data[dp]++;
                    break;
                case TokenBytecode.DECREMENT_BYTE:
                    data[dp]--;
                    break;
                case TokenBytecode.OUTPUT:
                    ProvideOutput(data[dp]);
                    break;
                case TokenBytecode.INPUT:
                    data[dp] = RequestInput();
                    break;
                case TokenBytecode.BRANCH_ZERO:
                    if (data[dp] == 0)
                    {
                        instructionPointer = token.Jump;
                    }
                    break;
                case TokenBytecode.BRANCH_NON_ZERO:
                    if (data[dp] != 0)
                    {
                        instructionPointer = token.Jump;
                    }
                    break;
                default:
                    throw new InvalidOperationException($"Unknown token bytecode: {token.Bytecode}");
            }

            dataPointer = dp;
        }

        public bool IsComplete()
        {
            return instructionPointer >= tokens.Length || instructionPointer < 0;
        }

        /* Handle input/output */
        private byte RequestInput()
        {
            if (OnInput != null)
            {
                foreach (InputTrigger trigger in OnInput.GetInvocationList())
                {
                    return trigger.Invoke();
                }
            }

            return 0;
        }

        private void ProvideOutput(byte data)
        {
            if (OnOutput == null)
            {
                return;
            }

            foreach (OutputTrigger trigger in OnOutput.GetInvocationList())
            {
                trigger.Invoke(data);
            }
        }
    }
}
