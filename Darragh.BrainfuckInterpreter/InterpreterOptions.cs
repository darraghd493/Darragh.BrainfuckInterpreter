namespace Darragh.BrainfuckInterpreter
{
    public class InterpreterOptions
    {
        public int Bytes { get; set; }
        public bool Wraparound { get; set; }

        public static InterpreterOptions Default => new InterpreterOptions
        {
            Bytes = 30000,
            Wraparound = false
        };
    }
}
