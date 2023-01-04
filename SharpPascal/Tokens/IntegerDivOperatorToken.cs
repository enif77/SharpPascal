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
            Code = TokenCode.TOK_KEY_DIV;
        }


        public override string ToString() => "DIV";
    }
}
