namespace Darragh.BrainfuckInterpreter.Tokens
{
    public class TokenBytecode
    {
        public const byte INCREMENT_POINTER = 0x00; // Increment the data pointer by one
        public const byte DECREMENT_POINTER = 0x01; // Decrement the data pointer by one
        public const byte INCREMENT_BYTE = 0x02; // Increment the byte at the data pointer by one
        public const byte DECREMENT_BYTE = 0x03; // Decrement the byte at the data pointer by one
        public const byte OUTPUT = 0x04; // Output the byte at the data pointer
        public const byte INPUT = 0x05; // Requests an input byte and stores it in the byte at the data pointer
        public const byte BRANCH_ZERO = 0x06; // Branches to the next instruction after the next non-zero branch
        public const byte BRANCH_NON_ZERO = 0x07; // Branches to the next instruction after the prior zero branch
    }
}
