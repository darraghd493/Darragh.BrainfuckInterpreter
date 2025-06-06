# Darragh.BrainfuckInterpreter

> Brainfuck is a minimalistic esoteric programming language created by Urban Müller in 1993.

This is a brainfuck parser and interpreter written in C#.

**Components:**

- parser
- interpreter

**Modules:**

- Interpreter (`Darragh.BrainfuckInterpreter`)
- Example (`Darragh.BrainfuckInterpreter.Example`)
- UI (`Darragh.BrainfuckInterpreter.UI`)
    - Written using WinForms

## Installation

~~This project is currently not on Nuget. You will need to clone it in order to use it.~~

This project is now available on Nuget as `Darragh.BrainfuckInterpreter`.
```bash
dotnet add package Darragh.BrainfuckInterpreter
```

## Usage

```cs
using Darragh.BrainfuckInterpreter;
using Darragh.BrainfuckInterpreter.Tokens;

// Parse a program
Parser parser = new Parser(
    ".",
    ParserOptions.Default
);
Token[] tokens = parser.Parse();

// Interpret a program
Interpreter interpreter = new Interpreter(tokens, InterpreterOptions.Default);
interpreter.OnOutput += data => Console.Write((char)data);
interpreter.OnInput += () => (byte)Console.ReadKey(true).KeyChar;
interpreter.Run();
```

[View example here.](./Darragh.BrainfuckInterpreter.Example/Program.cs)

## Notes

### Jump Pointers

The parser stores jump pointers within tokens. This is assigned depending on the token generated, rather than being identifed at interpretation time.
