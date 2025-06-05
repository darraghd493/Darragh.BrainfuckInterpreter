namespace Darragh.BrainfuckInterpreter
{
    public class ParserOptions
    {
        /* Strips */
        public required bool StripNewlines { get; set; }
        public required bool StripWhitespaces { get; set; }
        public required bool StripInvalid { get; set; }

        /* Comments */
        public required CommentOptions Comments { get; set; }

        public class CommentOptions
        {
            public required bool Enabled { get; set; }
            
            /* Styling */
            public required string OpenComment { get; set; }
            public required string CloseComment { get; set; }

            public static readonly CommentOptions Default = new CommentOptions
            {
                Enabled = true,
                OpenComment = "/*",
                CloseComment = "*/"
            };
            public static readonly CommentOptions Disabled = new CommentOptions
            {
                Enabled = false,
                OpenComment = "",
                CloseComment = ""
            };
        }

        /* Syntax */
        public required SyntaxOptions Syntax { get; set; }

        public class SyntaxOptions
        {
            public required char IncrementPointer { get; set; }
            public required char DecrementPointer { get; set; }
            public required char IncrementByte { get; set; }
            public required char DecrementByte { get; set; }
            public required char OutputByte { get; set; }
            public required char InputByte { get; set; }
            public required char LoopStart { get; set; }
            public required char LoopEnd { get; set; }

            public static SyntaxOptions Default => new SyntaxOptions
            {
                IncrementPointer = '>',
                DecrementPointer = '<',
                IncrementByte = '+',
                DecrementByte = '-',
                OutputByte = '.',
                InputByte = ',',
                LoopStart = '[',
                LoopEnd = ']'
            };
        }

        /* Defaults */
        public static readonly ParserOptions Default = new ParserOptions
        {
            StripNewlines = true,
            StripWhitespaces = true,
            StripInvalid = true,
            Comments = CommentOptions.Disabled,
            Syntax = SyntaxOptions.Default
        };
        public static readonly ParserOptions Stylised = new ParserOptions
        {
            StripNewlines = true,
            StripWhitespaces = true,
            StripInvalid = false,
            Comments = CommentOptions.Default,
            Syntax = SyntaxOptions.Default
        };
        public static readonly ParserOptions Raw = new ParserOptions
        {
            StripNewlines = false,
            StripWhitespaces = false,
            StripInvalid = false,
            Comments = CommentOptions.Disabled,
            Syntax = SyntaxOptions.Default
        };
    }
}
