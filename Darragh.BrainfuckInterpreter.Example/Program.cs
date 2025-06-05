using Darragh.BrainfuckInterpreter;
using Darragh.BrainfuckInterpreter.Tokens;

Parser parser = new Parser(
    // Credit: https://github.com/rdebath/Brainfuck/blob/master/bitwidth.b
    ">[-]<[-]++++++++[->+++++++++<]>.----[--<+++>]<-.+++++++.><.+++.[-][[-]>[-]+++++++++[<+++++>-]<+...--------------.>++++++++++[<+++++>-]<.+++.-------.>+++++++++[<----->-]<.-.>++++++++[<+++++++>-]<++.-----------.--.-----------.+++++++.----.++++++++++++++.>++++++++++[<----->-]<..[-]++++++++++.[-]+++++++[.,]-]",
    ParserOptions.Default
);
Token[] tokens = parser.Parse();

// Output the parsed tokens
Console.WriteLine("Parsed Tokens:");
Console.WriteLine("Index\tBytecode");
for (int i = 0; i < tokens.Length; i++)
{
    Console.WriteLine($"{i}\t{tokens[i].Bytecode}");
}

// Interpret the tokens
Console.WriteLine("\nInterpreting Tokens:");
Interpreter interpreter = new Interpreter(tokens, InterpreterOptions.Default);
interpreter.OnOutput += data => Console.Write((char)data);
interpreter.OnInput += () => (byte)Console.ReadKey(true).KeyChar;
interpreter.Run();

// Prevent the console from closing immediately
Console.WriteLine("\nPress any key to exit...");
Console.ReadKey(true);