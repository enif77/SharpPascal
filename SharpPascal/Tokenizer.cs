/* SharpPascal - (C) 2020 - 2022 Premysl Fara 
 
SharpPascal is available under the zlib license:
This software is provided 'as-is', without any express or implied
warranty.  In no event will the authors be held liable for any damages
arising from the use of this software.
Permission is granted to anyone to use this software for any purpose,
including commercial applications, and to alter it and redistribute it
freely, subject to the following restrictions:
1. The origin of this software must not be misrepresented; you must not
   claim that you wrote the original software. If you use this software
   in a product, an acknowledgment in the product documentation would be
   appreciated but is not required.
2. Altered source versions must be plainly marked as such, and must not be
   misrepresented as being the original software.
3. This notice may not be removed or altered from any source distribution.
 
 */

namespace SharpPascal
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SharpPascal.Tokens;


    /// <summary>
    /// Makes tokens from a program source.
    /// </summary>
    public class Tokenizer
    {
        /// <summary>
        /// The end of the source character.
        /// </summary>
        public const int C_EOF = -1;

        // /// <summary>
        // /// The end of the line character.
        // /// </summary>
        // public const char C_EOLN = '\n';
               

        /// <summary>
        /// The currently parsed source.
        /// </summary>
        public ISourceReader Source { get; }

        /// <summary>
        /// The last character extracted from the program source.
        /// </summary>
        public int CurrentChar => Source.CurrentChar;

        // /// <summary>
        // /// The previous character extracted from the program source.
        // /// </summary>
        // public int PreviousChar { get; private set; }

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
        /// The current source position (from where was the last character).
        /// </summary>
        public int SourcePosition { get; private set; }
        

        /// <summary>
        /// Constructor.
        /// </summary>
        public Tokenizer(ISourceReader source)
        {
            Source = source ?? throw new ArgumentNullException(nameof(source));
            CurrentToken = new SimpleToken(TokenCode.TOK_EOF);
        }


        /// <summary>
        /// Extracts the next token found in the program source.
        /// </summary>
        public IToken NextToken()
        {
            // if (CurrentChar < 0)
            // {
            //     throw new CompilerException(CurrentLine, CurrentLinePosition, "Read beyond the Source end");
            // }

            var inComment = false;

            Source.NextChar();
            
            while (CurrentChar != C_EOF)
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
                    return CurrentToken = ParseNumber(1);
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
                            NextChar();

                            if (IsDigit(CurrentChar))
                            {
                                return CurrentToken = ParseNumber(1);
                            }

                            return CurrentToken = new SimpleToken(TokenCode.TOK_ADD_OP);
                        }
                    case '-':
                        {
                            NextChar();

                            if (IsDigit(CurrentChar))
                            {
                                return CurrentToken = ParseNumber(-1);
                            }

                            return CurrentToken = new SimpleToken(TokenCode.TOK_SUB_OP);
                        }
                    case '*': NextChar(); return CurrentToken = new SimpleToken(TokenCode.TOK_MUL_OP);
                    case '/': NextChar(); return CurrentToken = new SimpleToken(TokenCode.TOK_DIV_OP);
                    case '=': NextChar(); return CurrentToken = new SimpleToken(TokenCode.TOK_EQ_OP);
                    case '<':
                        {
                            NextChar();

                            if (CurrentChar == '>')
                            {
                                NextChar();

                                return CurrentToken = new SimpleToken(TokenCode.TOK_NEQ_OP);  // '<>'
                            }
                            else if (CurrentChar == '=')
                            {
                                NextChar();

                                return CurrentToken = new SimpleToken(TokenCode.TOK_LE_OP);  // '<='
                            }

                            return CurrentToken = new SimpleToken(TokenCode.TOK_LT_OP);  // '<'
                        }
                    case '>':
                        {
                            NextChar();

                            if (CurrentChar == '=')
                            {
                                NextChar();

                                return CurrentToken = new SimpleToken(TokenCode.TOK_GE_OP);  // '>='
                            }

                            return CurrentToken = new SimpleToken(TokenCode.TOK_GT_OP);  // '>'
                        }
                    case ';': NextChar(); return CurrentToken = new SimpleToken(TokenCode.TOK_SEP);
                    case ',': NextChar(); return CurrentToken = new SimpleToken(TokenCode.TOK_LIST_SEP);
                    case ':':
                        {
                            NextChar();

                            if (CurrentChar == '=')
                            {
                                NextChar();

                                return CurrentToken = new SimpleToken(TokenCode.TOK_ASGN_OP);
                            }
                            
                            return CurrentToken = new SimpleToken(TokenCode.TOK_DDOT);
                        }
                    case '(':
                        {
                            NextChar();

                            if (CurrentChar == '*')
                            {
                                NextChar();

                                SkipComment();

                                continue;
                            }
                            else
                            {
                                return CurrentToken = new SimpleToken(TokenCode.TOK_LBRA);
                            }
                        }
                    case ')': NextChar(); return CurrentToken = new SimpleToken(TokenCode.TOK_RBRA);
                    case '.': NextChar(); return CurrentToken = new SimpleToken(TokenCode.TOK_PROG_END);
                    case '\0': return CurrentToken = new SimpleToken(TokenCode.TOK_EOF);

                    default:
                        throw new CompilerException(CurrentLine, CurrentLinePosition, $"Unknown character '{CurrentChar}' found.");
                }
            }

            if (inComment)
            {
                throw new CompilerException(CurrentLine, CurrentLinePosition, "An end of comment expected.");
            }

            return CurrentToken = new SimpleToken(TokenCode.TOK_EOF);
        }


        /// <summary>
        /// The current program source.
        /// </summary>
        private string _source;

        /// <summary>
        /// The keyword - token map.
        /// </summary>
        private readonly Dictionary<string, TokenCode> _keyWordsMap = new Dictionary<string, TokenCode>()
        {
            { "AND", TokenCode.TOK_AND_OP },
            { "BEGIN", TokenCode.TOK_KEY_BEGIN },
            { "DIV", TokenCode.TOK_DIVI_OP },
            { "END", TokenCode.TOK_KEY_END },
            { "IN", TokenCode.TOK_IN_OP },
            { "MOD", TokenCode.TOK_MOD_OP },
            { "OR", TokenCode.TOK_OR_OP },
            { "PROGRAM", TokenCode.TOK_KEY_PROGRAM },
            { "VAR", TokenCode.TOK_KEY_VAR },
        };


        private void SkipComment()
        {
            // Eat '{' or '*';
            NextChar();

            var inComment = true;
            while (CurrentChar != C_EOF)
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
        /// Parses an identifier the ECMA-55 rules.
        /// </summary>
        /// <param name="c">The first character of the parsed identifier.</param>
        private IToken ParseIdent()
        {
            var strValueSb = new StringBuilder(CurrentChar.ToString());

            NextChar();
            while (IsDigit(CurrentChar) || IsLetter(CurrentChar))
            {
                strValueSb.Append(CurrentChar);

                NextChar();
            }

            var strValue = strValueSb.ToString().ToUpperInvariant();
            if (_keyWordsMap.ContainsKey(strValue))
            {
                return new SimpleToken(_keyWordsMap[strValue]);
            }

            return new IdentifierToken(strValue);
        }

        /// <summary>
        /// Parses the quoted string using the ECMA-55 rules.
        /// </summary>
        private IToken ParseString()
        {
            var strValueSb = new StringBuilder();

            NextChar();
            while (CurrentChar != C_EOF)
            {
                if (CurrentChar == '\'')
                {
                    NextChar();

                    if (CurrentChar != '\'')
                    {
                        return new StringToken(strValueSb.ToString());
                    }
                }

                strValueSb.Append(CurrentChar);
                NextChar();
            }

            throw new CompilerException(CurrentLine, CurrentLinePosition, "Unexpected end of a string.");
        }

        /// <summary>
        /// Parses an integer or real number.
        /// unsigned-integer :: digit-sequence .
        /// unsigned-number :: unsigned-integer | unsigned-real .
        /// unsigned-real :: ( digit-sequence '.' fractional-part [ 'e' scale-factor ] ) | ( digit-sequence 'e' scale-factor ) .
        /// scale-factor :: [ sign ] digit-sequence .
        /// fractional-part :: digit-sequence .
        /// sign :: '+' | '-' .
        /// </summary>
        /// <param name="sign">1 for positive numbers, -1 for negative numbers.</param>
        /// <returns></returns>
        private IToken ParseNumber(int sign)
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
                rValue = (double)iValue;

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

                rValue *= Math.Pow(10, fact);

                isReal = true;
            }

            return isReal
                ? new RealToken(rValue * sign)
                : new IntegerToken(iValue * sign);
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
            
            SourcePosition += 1;
            // PreviousChar = currentChar;
            CurrentLinePosition++;
            if (lastChar == '\n')
            {
                CurrentLine++;
                CurrentLinePosition = 1;
            }
        }

        // /// <summary>
        // /// Gets the next char after the current char.
        // /// Does not advance the source position.
        // /// </summary>
        // /// <returns>The next char after the current char.</returns>
        // private char PeakChar()
        // {
        //     var p = SourcePosition + 1;
        //     
        //     return (p >= 0 && p < Source.Length)
        //         ? Source[p]
        //         : C_EOF;
        // }

        /// <summary>
        /// Checks, if an character is a digit.
        /// digit :: '0' | '1' | '2' | '3' | '4' | '5' | '6' | '7' | '8' | '9' .
        /// </summary>
        /// <param name="c">A character.</param>
        /// <returns>True, if a character is a digit.</returns>
        private static bool IsDigit(int c)
        {
            return c >= '0' && c <= '9';
        }

        /// <summary>
        /// Checks, if an character is a white character.
        /// hwite-character = SPACE | TAB | '\r' | NEW-LINE .
        /// </summary>
        /// <param name="c">A character.</param>
        /// <returns>True, if a character is a white character.</returns>
        private static bool IsWhite(int c)
        {
            return c == ' ' || c == '\t' || c == '\r' || c == '\n';
        }

        /// <summary>
        /// Checks, if an character is a letter.
        /// </summary>
        /// <param name="c">A character.</param>
        /// <returns>True, if a character is a letter.</returns>
        private static bool IsLetter(int c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }

    }
}
