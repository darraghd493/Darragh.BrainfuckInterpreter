namespace Darragh.BrainfuckInterpreter.Tokens
{
    public class Token
    {
        /* Static tokens */
        public static readonly Token INCREMENT_POINTER = new Token(TokenBytecode.INCREMENT_POINTER);
        public static readonly Token DECREMENT_POINTER = new Token(TokenBytecode.DECREMENT_POINTER);
        public static readonly Token INCREMENT_BYTE = new Token(TokenBytecode.INCREMENT_BYTE);
        public static readonly Token DECREMENT_BYTE = new Token(TokenBytecode.DECREMENT_BYTE);
        public static readonly Token OUTPUT_BYTE = new Token(TokenBytecode.OUTPUT);
        public static readonly Token INPUT_BYTE = new Token(TokenBytecode.INPUT);

        /* Token Data */
        public byte Bytecode { get; }
        public int Jump { get; }

        public Token(byte bytecode)
        {
            Bytecode = bytecode;
            // Leave data uninitialised
        }

        public Token(byte bytecode, int data)
        {
            Bytecode = bytecode;
            Jump = data;
        }
    }
}
