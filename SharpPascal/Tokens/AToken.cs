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
    public abstract class AToken : IToken
    {
        public TokenCode TokenCode { get; protected set; }

        public bool BooleanValue { get; protected set; }
        public float IntegerValue { get; protected set; }
        public float RealValue { get; protected set; }
        public string StringValue { get; protected set; }


        protected AToken()
        {
        }


        public override string ToString()
        {
            switch (TokenCode)
            {
                case TokenCode.TOK_SEP: return ";";
                case TokenCode.TOK_LBRA: return "(";
                case TokenCode.TOK_RBRA: return ")";
                case TokenCode.TOK_PROG_END: return ".";
                
                case TokenCode.TOK_KEY_BEGIN: return "BEGIN";
                case TokenCode.TOK_KEY_END: return "END";
                case TokenCode.TOK_KEY_PROGRAM: return "PROGRAM";

                case TokenCode.TOK_EOF: return "@EOF";
                case TokenCode.TOK_EOLN: return "@EOLN";
            }

            throw new CompilerException("Unknown token " + TokenCode + ".");
        }
    }
}
