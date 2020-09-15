/* SharpPascal - (C) 2020 Premysl Fara 
 
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
        public const char C_EOF = Char.MinValue;

        /// <summary>
        /// The end of the line character.
        /// </summary>
        public const char C_EOLN = '\n';
               

        /// <summary>
        /// The currentlly parsed source.
        /// </summary>
        public string Source
        {
            get
            {
                return _source;
            }

            set
            {
                _source = value ?? string.Empty;

                SourcePosition = -1;
                CurrentToken = new SimpleToken(TokenCode.TOK_EOF);
                NextChar();
            }
        }

        /// <summary>
        /// The last character extracted from the program source.
        /// </summary>
        public char CurrentChar { get; private set; }

        /// <summary>
        /// The previous character extracted from the program source.
        /// </summary>
        public char PreviousChar { get; private set; }

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
        public Tokenizer(string source = null)
        {
            Source = source ?? string.Empty;
            CurrentLine = 1;
            CurrentToken = new SimpleToken(TokenCode.TOK_EOF);
        }


        /// <summary>
        /// Extracts the next token found in the program source.
        /// </summary>
        public IToken NextToken()
        {
            if (SourcePosition > Source.Length)
            {
                throw new CompilerException(CurrentLine, CurrentLinePosition, "Read beyond the Source end");
            }

            var inComment = false;

            while (CurrentChar != C_EOF)
            {
                // Skip white chars.
                while (IsWhite(CurrentChar))
                {
                    NextChar();
                }

                if (CurrentChar == '{')
                {
                    SkipComment();

                    continue;
                }

                if (CurrentChar == '(')
                {
                    if (PeakChar() == '*')
                    {
                        NextChar();

                        SkipComment();

                        continue;
                    }
                }

                if (IsLetter(CurrentChar))
                {
                    return CurrentToken = ParseIdent();
                }

                if (CurrentChar == '\'')
                {
                    return CurrentToken = ParseString();
                }

                switch (CurrentChar)
                {
                    case '+': NextChar(); return CurrentToken = new SimpleToken(TokenCode.TOK_ADD_OP);
                    case '-': NextChar(); return CurrentToken = new SimpleToken(TokenCode.TOK_SUB_OP);
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
                    case '(': NextChar(); return CurrentToken = new SimpleToken(TokenCode.TOK_LBRA);
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
                else if (CurrentChar == '*')
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
                else if (CurrentChar == '{')
                {
                    throw new CompilerException(CurrentLine, CurrentLinePosition, "An end of a comment expected.");
                }
                else if (CurrentChar == '(')
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
            else
            {
                return new IdentifierToken(strValue);
            }
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
                        return new StringToken(TokenCode.TOK_STR, strValueSb.ToString());
                    }
                }

                strValueSb.Append(CurrentChar);
                NextChar();
            }

            throw new CompilerException(CurrentLine, CurrentLinePosition, "Unexpected end of a string.");
        }


        /// <summary>
        /// Gets the next character from the current program line source.
        /// </summary>
        /// <returns>The next character from the current program line source.</returns>
        private void NextChar()
        {
            var p = SourcePosition + 1;
            if (p >= 0 && p < Source.Length)
            {
                SourcePosition = p;

                PreviousChar = CurrentChar;
                CurrentChar = Source[SourcePosition];
                CurrentLinePosition++;
                if (PreviousChar == '\n')
                {
                    CurrentLine++;
                    CurrentLinePosition = 1;
                }
            }
            else
            {
                SourcePosition = Source.Length;

                CurrentChar = C_EOF;
            }
        }

        /// <summary>
        /// Gets the next char after the current char.
        /// Does not advance the source position.
        /// </summary>
        /// <returns>The next char after the current char.</returns>
        private char PeakChar()
        {
            var p = SourcePosition + 1;
            
            return (p >= 0 && p < Source.Length)
                ? Source[p]
                : C_EOF;
        }

        /// <summary>
        /// Checks, if an character is a digit.
        /// </summary>
        /// <param name="c">A character.</param>
        /// <returns>True, if a character is a digit.</returns>
        public static bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        /// <summary>
        /// Checks, if an character is a white character.
        /// hwite-character = SPACE | TAB .
        /// </summary>
        /// <param name="c">A character.</param>
        /// <returns>True, if a character is a white character.</returns>
        public static bool IsWhite(char c)
        {
            return c == ' ' || c == '\t' || c == '\r' || c == '\n';
        }

        /// <summary>
        /// Checks, if an character is a letter.
        /// </summary>
        /// <param name="c">A character.</param>
        /// <returns>True, if a character is a letter.</returns>
        public static bool IsLetter(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }

    }
}
