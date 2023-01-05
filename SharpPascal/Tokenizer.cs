/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SharpPascal.Tokens;


    /// <summary>
    /// Makes tokens from a program source.
    /// </summary>
    public class Tokenizer : ITokenizer
    {
        /// <summary>
        /// The currently parsed source.
        /// </summary>
        private ISourceReader Source { get; }

        /// <summary>
        /// The last character extracted from the program source.
        /// </summary>
        public int CurrentChar => Source.CurrentChar;
        
        /// <summary>
        /// The last token extracted from the program source.
        /// </summary>
        public IToken CurrentToken { get; private set; }

        /// <summary>
        /// The current line position (1 .. N).
        /// </summary>
        public int CurrentLinePosition { get; private set; }

        /// <summary>
        /// The current line (1 .. N).
        /// </summary>
        public int CurrentLine { get; private set; }


        /// <summary>
        /// Constructor.
        /// </summary>
        public Tokenizer(ISourceReader source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            CurrentLinePosition = 1;
            CurrentLine = 1;
            
            // This reads the first char from the source also.
            CurrentToken = ParseEof();
        }


        /// <summary>
        /// Extracts the next token found in the program source.
        /// </summary>
        public IToken NextToken()
        {
            while (IsEoF(CurrentChar) == false)
            {
                // Skip white chars.
                while (IsWhite(CurrentChar))
                {
                    NextChar();
                }

                if (IsLetter(CurrentChar))
                {
                    return CurrentToken = ParseIdent();
                }

                if (IsDigit(CurrentChar))
                {
                    return CurrentToken = ParseNumber(1, CurrentLinePosition, CurrentLine);
                }

                if (CurrentChar == '\'')
                {
                    return CurrentToken = ParseString();
                }

                switch (CurrentChar)
                {
                    case '{':
                    {
                        SkipComment();

                        continue;
                    }

                    case '+':
                    {
                        var currentLinePosition = CurrentLinePosition;
                        var currentLine = CurrentLine;
                            
                        NextChar();

                        if (IsDigit(CurrentChar))
                        {
                            return CurrentToken = ParseNumber(1, currentLinePosition, currentLine);
                        }

                        return CurrentToken = new SimpleToken(TokenCode.TOK_ADD_OP, "+", currentLinePosition, currentLine);
                    }
                    
                    case '-':
                    {
                        var currentLinePosition = CurrentLinePosition;
                        var currentLine = CurrentLine;
                        
                        NextChar();

                        if (IsDigit(CurrentChar))
                        {
                            return CurrentToken = ParseNumber(-1, currentLinePosition, currentLine);
                        }

                        return CurrentToken = new SimpleToken(TokenCode.TOK_SUB_OP, "-", currentLinePosition, currentLine);
                    }

                    case '*': return CurrentToken = ParseSimpleToken(TokenCode.TOK_MUL_OP, "*");
                    case '/': return CurrentToken = ParseSimpleToken(TokenCode.TOK_DIV_OP, "/");
                    case '=': return CurrentToken = ParseSimpleToken(TokenCode.TOK_EQ_OP, "=");

                    case '<':
                    {
                        var currentLinePosition = CurrentLinePosition;
                        var currentLine = CurrentLine;
                        
                        NextChar();

                        if (CurrentChar == '>')
                        {
                            NextChar();

                            return CurrentToken = new SimpleToken(TokenCode.TOK_NEQ_OP, "<>", currentLinePosition, currentLine);
                        }

                        if (CurrentChar == '=')
                        {
                            NextChar();

                            return CurrentToken = new SimpleToken(TokenCode.TOK_LE_OP, "<=", currentLinePosition, currentLine);
                        }

                        return CurrentToken = new SimpleToken(TokenCode.TOK_LT_OP, "<", currentLinePosition, currentLine);
                    }
                    
                    case '>':
                    {
                        var currentLinePosition = CurrentLinePosition;
                        var currentLine = CurrentLine;
                        
                        NextChar();

                        if (CurrentChar == '=')
                        {
                            NextChar();

                            return CurrentToken = new SimpleToken(TokenCode.TOK_GE_OP, ">=", currentLinePosition, currentLine);
                        }

                        return CurrentToken = new SimpleToken(TokenCode.TOK_GT_OP, ">", currentLinePosition, currentLine);
                    }

                    case ';': return CurrentToken = ParseSimpleToken(TokenCode.TOK_SEP, ";");
                    case ',': return CurrentToken = ParseSimpleToken(TokenCode.TOK_LIST_SEP, ",");

                    case ':':
                    {
                        var currentLinePosition = CurrentLinePosition;
                        var currentLine = CurrentLine;
                        
                        NextChar();

                        if (CurrentChar == '=')
                        {
                            NextChar();

                            return CurrentToken = new SimpleToken(TokenCode.TOK_ASGN_OP, ":=", currentLinePosition, currentLine);
                        }
                        
                        return CurrentToken = new SimpleToken(TokenCode.TOK_DDOT, ":", currentLinePosition, currentLine);
                    }
                    
                    case '(':
                    {
                        var currentLinePosition = CurrentLinePosition;
                        var currentLine = CurrentLine;
                        
                        NextChar();

                        if (CurrentChar == '*')
                        {
                            NextChar();

                            SkipComment();

                            continue;
                        }
                        
                        if (CurrentChar == '.')
                        {
                            NextChar();

                            return CurrentToken = new SimpleToken(TokenCode.TOK_LEFT_BRACKET, "[", currentLinePosition, currentLine);    
                        }

                        return CurrentToken = new SimpleToken(TokenCode.TOK_LEFT_PAREN, "(", currentLinePosition, currentLine);
                    }
                    
                    case ')': return CurrentToken = ParseSimpleToken(TokenCode.TOK_RIGHT_PAREN, ")");
                    case '[': return CurrentToken = ParseSimpleToken(TokenCode.TOK_LEFT_BRACKET, "[");
                    case ']': return CurrentToken = ParseSimpleToken(TokenCode.TOK_RIGHT_BRACKET, "]");
                    
                    case '@':
                    case '^': return CurrentToken = ParseSimpleToken(TokenCode.TOK_POINTER, "^");

                    case '.':
                    {
                        var currentLinePosition = CurrentLinePosition;
                        var currentLine = CurrentLine;
                        
                        NextChar();
                        
                        if (CurrentChar == ')')
                        {
                            NextChar();

                            return CurrentToken = new SimpleToken(TokenCode.TOK_RIGHT_BRACKET, "]", currentLinePosition, currentLine);
                        }

                        return CurrentToken = new SimpleToken(TokenCode.TOK_PROG_END, ".", currentLinePosition, currentLine);
                    }
                    
                    default:
                    {
                        if (IsEoF(CurrentChar))
                        {
                            return CurrentToken = ParseEof();
                        }

                        throw new CompilerException(
                            CurrentLine, 
                            CurrentLinePosition, 
                            $"Unknown character '{CurrentChar}' found.");
                    }
                }
            }

            return CurrentToken = ParseEof();
        }
        
        
        /// <summary>
        /// The keyword - token map.
        /// </summary>
        private readonly Dictionary<string, TokenCode> _keyWordsMap = new()
        {
            { "AND", TokenCode.TOK_KEY_AND },
            { "ARRAY", TokenCode.TOK_KEY_ARRAY },
            { "BEGIN", TokenCode.TOK_KEY_BEGIN },
            { "CASE", TokenCode.TOK_KEY_CASE },
            { "CONST", TokenCode.TOK_KEY_CONST },
            { "DIV", TokenCode.TOK_KEY_DIV },
            { "DO", TokenCode.TOK_KEY_DO },
            { "DOWNTO", TokenCode.TOK_KEY_DOWNTO },
            { "ELSE", TokenCode.TOK_KEY_ELSE },
            { "END", TokenCode.TOK_KEY_END },
            { "FILE", TokenCode.TOK_KEY_FILE },
            { "FOR", TokenCode.TOK_KEY_FOR },
            { "FUNCTION", TokenCode.TOK_KEY_FUNCTION },
            { "GOTO", TokenCode.TOK_KEY_GOTO },
            { "IF", TokenCode.TOK_KEY_IF },
            { "IN", TokenCode.TOK_KEY_IN },
            { "LABEL", TokenCode.TOK_KEY_LABEL },
            { "MOD", TokenCode.TOK_KEY_MOD },
            { "NIL", TokenCode.TOK_KEY_NIL },
            { "NOT", TokenCode.TOK_KEY_NOT },
            { "OF", TokenCode.TOK_KEY_OF },
            { "OR", TokenCode.TOK_KEY_OR },
            { "PACKED", TokenCode.TOK_KEY_PACKED },
            { "PROCEDURE", TokenCode.TOK_KEY_PROCEDURE },
            { "PROGRAM", TokenCode.TOK_KEY_PROGRAM },
            { "RECORD", TokenCode.TOK_KEY_RECORD },
            { "REPEAT", TokenCode.TOK_KEY_REPEAT },
            { "SET", TokenCode.TOK_KEY_SET },
            { "THEN", TokenCode.TOK_KEY_THEN },
            { "TO", TokenCode.TOK_KEY_TO },
            { "TYPE", TokenCode.TOK_KEY_TYPE },
            { "UNTIL", TokenCode.TOK_KEY_UNTIL },
            { "VAR", TokenCode.TOK_KEY_VAR },
            { "WHILE", TokenCode.TOK_KEY_WHILE },
            { "WITH", TokenCode.TOK_KEY_WITH }
        };
        
        
        private void SkipComment()
        {
            // Eat '{' or '*';
            NextChar();

            var inComment = true;
            while (IsEoF(CurrentChar) == false)
            {
                // The '}' end of a comment?
                if (CurrentChar == '}')
                {
                    inComment = false;

                    NextChar();

                    break;
                }

                if (CurrentChar == '*')
                {
                    NextChar();

                    // The "*)" end of a comment?
                    if (CurrentChar == ')')
                    {
                        inComment = false;

                        NextChar();

                        break;
                    }

                    continue;
                }

                if (CurrentChar == '{')
                {
                    throw new CompilerException(CurrentLine, CurrentLinePosition, "An end of a comment expected.");
                }

                if (CurrentChar == '(')
                {
                    NextChar();

                    if (CurrentChar == '*')
                    {
                        throw new CompilerException(CurrentLine, CurrentLinePosition, "An end of a comment expected.");
                    }

                    continue;
                }

                NextChar();
            }

            if (inComment)
            {
                throw new CompilerException(CurrentLine, CurrentLinePosition, "An end of a comment expected.");
            }
        }

        /// <summary>
        /// Parses a simple token.
        /// </summary>
        /// <param name="tokenCode">A token code.</param>
        /// <param name="stringRepresentation">A string representation of this token.</param>
        private IToken ParseSimpleToken(TokenCode tokenCode, string stringRepresentation)
        {
            var currentLinePosition = CurrentLinePosition;
            var currentLine = CurrentLine;
                        
            NextChar();
            
            return new SimpleToken(tokenCode, stringRepresentation, currentLinePosition, currentLine);
        }
        
        /// <summary>
        /// Creates the EOF representing token from the current source position. 
        /// </summary>
        private IToken ParseEof()
        {
            return ParseSimpleToken(TokenCode.TOK_EOF, "@EOF");
        }
        
        /// <summary>
        /// Parses an identifier or an keyword.
        /// identifier :: letter { letter | digit } .
        /// </summary>
        private IToken ParseIdent()
        {
            var currentLinePosition = CurrentLinePosition;
            var currentLine = CurrentLine;
            
            var strValueSb = new StringBuilder();

            strValueSb.Append((char)CurrentChar);
            
            NextChar();
            while (IsDigit(CurrentChar) || IsLetter(CurrentChar))
            {
                strValueSb.Append((char)CurrentChar);

                NextChar();
            }

            var strValue = strValueSb.ToString().ToUpperInvariant();
            
            return _keyWordsMap.ContainsKey(strValue)
                ? new SimpleToken(_keyWordsMap[strValue], strValue, currentLinePosition, currentLine)
                : new IdentifierToken(strValue, currentLinePosition, currentLine);
        }

        /// <summary>
        /// Parses a string.
        /// string :: string-terminator [ string-character | string-terminator-escape ] string-terminator .
        /// string-terminator :: '''
        /// string-terminator-escape :: "''" .
        /// string-character :: any char except string-terminator .
        /// </summary>
        private IToken ParseString()
        {
            var currentLinePosition = CurrentLinePosition;
            var currentLine = CurrentLine;
            
            var strValueSb = new StringBuilder();

            NextChar();
            while (IsEoF(CurrentChar) == false)
            {
                if (CurrentChar == '\'')
                {
                    NextChar();

                    if (CurrentChar != '\'')
                    {
                        return new StringLiteralToken(strValueSb.ToString(), currentLinePosition, currentLine);
                    }
                }

                strValueSb.Append((char)CurrentChar);
                NextChar();
            }

            throw new CompilerException(CurrentLine, CurrentLinePosition, "Unexpected end of a string.");
        }

        /// <summary>
        /// Parses an integer or a real number.
        /// unsigned-integer :: digit-sequence .
        /// unsigned-number :: unsigned-integer | unsigned-real .
        /// unsigned-real :: ( digit-sequence '.' fractional-part [ 'e' scale-factor ] ) | ( digit-sequence 'e' scale-factor ) .
        /// scale-factor :: [ sign ] digit-sequence .
        /// fractional-part :: digit-sequence .
        /// sign :: '+' | '-' .
        /// </summary>
        /// <param name="sign">A number sign. 1 is a positive number, -1 is a negative number.</param>
        /// <param name="linePosition">A line position of the first char of the returned token.</param>
        /// <param name="line">At which line is the first char of the returned token.</param>
        private IToken ParseNumber(int sign, int linePosition, int line)
        {
            var isReal = false;
            var iValue = 0;
            var rValue = 0.0;

            while (IsDigit(CurrentChar))
            {
                iValue = (iValue * 10) + (CurrentChar - '0');

                if (iValue < 0)
                {
                    throw new CompilerException(CurrentLine, CurrentLinePosition, "Numeric constant overflow.");
                }

                NextChar();
            }

            // digit-sequence '.' fractional-part
            if (CurrentChar == '.')
            {
                rValue = iValue;

                // Eat '.'.
                NextChar();

                if (IsDigit(CurrentChar) == false)
                {
                    throw new CompilerException(CurrentLine, CurrentLinePosition, "A fractional part of a real number expected.");
                }

                var scale = 1.0;
                var frac = 0.0;
                while (IsDigit(CurrentChar))
                {
                    frac = (frac * 10.0) + (CurrentChar - '0');
                    scale *= 10.0;

                    NextChar();
                }

                rValue += frac / scale;

                isReal = true;
            }

            // digit-sequence [ '.' fractional-part ] 'e' scale-factor
            if (CurrentChar == 'e' || CurrentChar == 'E')
            {
                rValue = isReal ? rValue : iValue;

                // Eat 'e'.
                NextChar();

                // Support for the scale factor sign.
                var factSign = 1.0;
                if (CurrentChar == '-')
                {
                    NextChar();
                    factSign = -1.0;
                }
                else if (CurrentChar == '+')
                {
                    NextChar();
                }

                if (IsDigit(CurrentChar) == false)
                {
                    throw new CompilerException(CurrentLine, CurrentLinePosition, "A scale factor of a real number expected.");
                }

                var fact = 0.0;
                while (IsDigit(CurrentChar))
                {
                    fact = (fact * 10.0) + (CurrentChar - '0');

                    NextChar();
                }

                rValue *= Math.Pow(10, fact * factSign);

                isReal = true;
            }

            return isReal
                ? new RealNumberToken(rValue * sign, linePosition, line)
                : new IntegerNumberToken(iValue * sign, linePosition, line);
        }

        /// <summary>
        /// Gets the next character from the current program line source.
        /// </summary>
        /// <returns>The next character from the current program line source.</returns>
        private void NextChar()
        {
            var lastChar = CurrentChar;
            var c = Source.NextChar();
            if (c < 0)
            {
               return; 
            }

            CurrentLinePosition++;
            if (lastChar == '\n')
            {
                CurrentLine++;
                CurrentLinePosition = 1;
            }
        }

        /// <summary>
        /// Checks, if a character is the end-of-file mark.
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        private static bool IsEoF(int c) => c < 0;

        /// <summary>
        /// Checks, if an character is a digit.
        /// digit :: '0' | '1' | '2' | '3' | '4' | '5' | '6' | '7' | '8' | '9' .
        /// </summary>
        /// <param name="c">A character.</param>
        /// <returns>True, if a character is a digit.</returns>
        private static bool IsDigit(int c)
        {
            return c is >= '0' and <= '9';
        }

        /// <summary>
        /// Checks, if an character is a white character.
        /// white-character = SPACE | TAB | '\r' | NEW-LINE .
        /// </summary>
        /// <param name="c">A character.</param>
        /// <returns>True, if a character is a white character.</returns>
        private static bool IsWhite(int c)
        {
            return c is ' ' or '\t' or '\r' or '\n';
        }

        /// <summary>
        /// Checks, if an character is a letter.
        /// </summary>
        /// <param name="c">A character.</param>
        /// <returns>True, if a character is a letter.</returns>
        private static bool IsLetter(int c)
        {
            return c is >= 'a' and <= 'z' or >= 'A' and <= 'Z';
        }
    }
}
