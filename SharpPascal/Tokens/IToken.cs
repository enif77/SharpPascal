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

namespace SharpPascal.Tokens
{
    public enum TokenCode
    {
        /// <summary>
        /// The end of the file token.
        /// </summary>
        TOK_EOF,

        /// <summary>
        /// The end of the line token.
        /// </summary>
        TOK_EOLN,

        /// <summary>
        /// The program end mark '.'.
        /// </summary>
        TOK_PROG_END,

        /// <summary>
        /// The statement separator ';'.
        /// </summary>
        TOK_SEP,

        /// <summary>
        /// The list separator ','.
        /// </summary>
        TOK_LIST_SEP,

        /// <summary>
        /// The ':'.
        /// </summary>
        TOK_DDOT,

        // '(' and ')'.
        TOK_LBRA,
        TOK_RBRA,

        /// <summary>
        /// A string literal.
        /// </summary>
        TOK_STR,

        /// <summary>
        /// An identifier.
        /// </summary>
        TOK_IDENT,

        // Keywords.
        TOK_KEY_BEGIN,
        TOK_KEY_END,
        TOK_KEY_PROGRAM,
        TOK_KEY_VAR,
    };


    public interface IToken
    {
        TokenCode TokenCode { get; }
        bool BooleanValue { get; }
        float IntegerValue { get; }
        float RealValue { get; }
        string StringValue { get; }
    }
}
