/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tokens
{
    /// <summary>
    /// IN operator token.
    /// </summary>
    public sealed class InOperatorToken : AToken
    {
        public InOperatorToken(int linePosition, int line)
            : base(linePosition, line)
        {
            TokenCode = TokenCode.TOK_IN_OP;
        }


        public override string ToString() => "IN";
    }
}
