using Darragh.BrainfuckInterpreter.Tokens;

namespace Darragh.BrainfuckInterpreter
{
    public class Parser
    {
        private string content;
        private ParserOptions options;

        public Parser(string content, ParserOptions options)
        {
            this.content = content;
            this.options = options;
        }

        public Token[] Parse()
        {
            string preparedContent = PrepareContent(content);
            ValidateSyntax(preparedContent, options.Syntax); // Even though this adds overhead, it is better to prevent invalid syntax early

            Token[] tokens = new Token[preparedContent.Length]; // It is reasonable to assume that each character will become a token
            int tokenIndex = 0;

            Stack<(char c, int index)> loopStack = new();

            foreach (char c in preparedContent)
            {
                int originalIndex = tokenIndex;

                switch (c)
                {
                    case var _ when c == options.Syntax.IncrementPointer:
                        tokens[tokenIndex++] = Token.INCREMENT_POINTER;
                        break;
                    case var _ when c == options.Syntax.DecrementPointer:
                        tokens[tokenIndex++] = Token.DECREMENT_POINTER;
                        break;
                    case var _ when c == options.Syntax.IncrementByte:
                        tokens[tokenIndex++] = Token.INCREMENT_BYTE;
                        break;
                    case var _ when c == options.Syntax.DecrementByte:
                        tokens[tokenIndex++] = Token.DECREMENT_BYTE;
                        break;
                    case var _ when c == options.Syntax.OutputByte:
                        tokens[tokenIndex++] = Token.OUTPUT_BYTE;
                        break;
                    case var _ when c == options.Syntax.InputByte:
                        tokens[tokenIndex++] = Token.INPUT_BYTE;
                        break;
                    case var _ when c == options.Syntax.LoopStart || c == options.Syntax.LoopEnd:
                        if (loopStack.Count > 0 && loopStack.Peek().c != c)
                        {
                            var (matchChar, matchIndex) = loopStack.Pop();
                            if (c == options.Syntax.LoopEnd)
                            {
                                tokens[matchIndex] = new Token(TokenBytecode.BRANCH_ZERO, tokenIndex);
                                tokens[tokenIndex++] = new Token(TokenBytecode.BRANCH_NON_ZERO, matchIndex);
                            }
                            else
                            {
                                tokens[matchIndex] = new Token(TokenBytecode.BRANCH_NON_ZERO, tokenIndex);
                                tokens[tokenIndex++] = new Token(TokenBytecode.BRANCH_ZERO, matchIndex);
                            }
                        }
                        else
                        {
                            loopStack.Push((c, tokenIndex));
                            tokens[tokenIndex++] = new Token(
                                c == options.Syntax.LoopStart ? TokenBytecode.BRANCH_ZERO : TokenBytecode.BRANCH_NON_ZERO,
                                0 // To set later
                            );
                        }
                        break;
                    default:
                        throw new ArgumentException($"Invalid character '{c}' found in the content."); // Safety net for unexpected characters
                }

                if (originalIndex == tokenIndex)
                {
                    throw new ArgumentException($"No token was created for character '{c}' at index {tokenIndex} in the content."); // Safety net for unexpected characters
                }
            }

            Array.Resize(ref tokens, tokenIndex); // Safety net for unexpected characters
            return tokens;
        }

        private string PrepareContent(string content)
        {
            // Strip invalid
            if (options.StripInvalid)
            {
                content = new string(content.Where(c => options.Syntax.IncrementPointer == c ||
                                                        options.Syntax.DecrementPointer == c ||
                                                        options.Syntax.IncrementByte == c ||
                                                        options.Syntax.DecrementByte == c ||
                                                        options.Syntax.OutputByte == c ||
                                                        options.Syntax.InputByte == c ||
                                                        options.Syntax.LoopStart == c ||
                                                        options.Syntax.LoopEnd == c).ToArray());
                return content; // No need to continue if we are stripping invalid characters
            }

            // Strip newlines
            if (options.StripNewlines)
            {
                content = content.Replace("\n", "").Replace("\r", "");
            }

            // Strip whitespaces
            if (options.StripWhitespaces)
            {
                content = content.Replace(" ", "");
            }

            // Handle comments
            if (options.Comments.Enabled)
            {
                int openIndex = content.IndexOf(options.Comments.OpenComment);
                while (openIndex != -1)
                {
                    int closeIndex = content.IndexOf(options.Comments.CloseComment, openIndex + options.Comments.OpenComment.Length);
                    if (closeIndex == -1) break; // No closing comment found
                    content = content.Remove(openIndex, closeIndex - openIndex + options.Comments.CloseComment.Length);
                    openIndex = content.IndexOf(options.Comments.OpenComment, openIndex);
                }
            }

            return content;
        }

        private void ValidateSyntax(string content, ParserOptions.SyntaxOptions syntaxOptions) // TODO: Loop scope validation
        {
            Stack<int> loopStack = new();

            foreach (char c in content)
            {
                if (c != syntaxOptions.IncrementPointer &&
                    c != syntaxOptions.DecrementPointer &&
                    c != syntaxOptions.IncrementByte &&
                    c != syntaxOptions.DecrementByte &&
                    c != syntaxOptions.OutputByte &&
                    c != syntaxOptions.InputByte &&
                    c != syntaxOptions.LoopStart &&
                    c != syntaxOptions.LoopEnd)
                {
                    throw new ArgumentException($"Invalid character '{c}' found in the content.");
                } else if (c == syntaxOptions.LoopStart || c == syntaxOptions.LoopEnd)
                {
                    if (loopStack.Count > 0 && loopStack.Peek() != c)
                    {
                        loopStack.Pop();
                    }
                    else
                    {
                        loopStack.Push(c);
                    }
                }
            }

            if (loopStack.Count > 0)
            {
                throw new ArgumentException("Unmatched loop start or end detected.");
            }
        }
    }
}
