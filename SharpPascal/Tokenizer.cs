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
                NextChar();
            }
        }

        /// <summary>
        /// The last character extracted from the program source.
        /// </summary>
        public char CurrentChar { get; private set; }


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
        }


        /// <summary>
        /// Extracts the next token found in the program source.
        /// </summary>
        public IToken NextToken()
        {
            if (SourcePosition > Source.Length)
            {
                throw new Exception("Read beyond the Source end");
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
                    NextChar();

                    if (CurrentChar == '*')
                    {
                        SkipComment();

                        continue;
                    }

                    PreviousChar();
                }


                if (IsLetter(CurrentChar))
                {
                    return ParseIdent();
                }

                if (CurrentChar == '\'')
                {
                    return ParseString();
                }

                switch (CurrentChar)
                {
                    case ';': NextChar(); return new SimpleToken(TokenCode.TOK_SEP);
                    case '(': NextChar(); return new SimpleToken(TokenCode.TOK_LBRA);
                    case ')': NextChar(); return new SimpleToken(TokenCode.TOK_RBRA);
                    case '.': NextChar(); return new SimpleToken(TokenCode.TOK_PROG_END);

                    default:
                        throw new Exception($"Unknown character '{CurrentChar}' found.");
                }
            }

            if (inComment)
            {
                throw new Exception("An end of comment expected.");
            }

            return new SimpleToken(TokenCode.TOK_EOF);
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
            { "BEGIN", TokenCode.TOK_KEY_BEGIN },
            { "END", TokenCode.TOK_KEY_END },
            { "PROGRAM", TokenCode.TOK_KEY_PROGRAM },
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
                    throw new Exception("An end of a comment expected.");
                }
                else if (CurrentChar == '(')
                {
                    NextChar();

                    if (CurrentChar == '*')
                    {
                        throw new Exception("An end of a comment expected.");
                    }

                    continue;
                }

                NextChar();
            }

            if (inComment)
            {
                throw new Exception("An end of a comment expected.");
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

            throw new Exception("Unexpected end of a string.");
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

                CurrentChar = Source[SourcePosition];
            }
            else
            {
                SourcePosition = Source.Length;

                CurrentChar = C_EOF;
            }
        }

        /// <summary>
        /// Gets the previous character from the current program line source.
        /// </summary>
        /// <returns>The previous character from the current program line source.</returns>
        private void PreviousChar()
        {
            if (SourcePosition > 0 && SourcePosition <= Source.Length)
            {
                SourcePosition--;

                CurrentChar = Source[SourcePosition];
            }
            else if (SourcePosition > Source.Length)
            {
                SourcePosition = Source.Length;

                CurrentChar = C_EOF;
            }
            else
            {
                SourcePosition = -1;

                CurrentChar = C_EOF;
            }
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
