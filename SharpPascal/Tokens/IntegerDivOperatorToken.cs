/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tokens
{
    /// <summary>
    /// DIV operator token.
    /// </summary>
    public sealed class IntegerDivOperatorToken : AToken
    {
        public IntegerDivOperatorToken(int linePosition, int line)
            : base(linePosition, line)
        {
            TokenCode = TokenCode.TOK_DIVI_OP;
        }


        public override string ToString() => "DIV";
    }
}
