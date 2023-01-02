/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tokens
{
    /// <summary>
    /// Logical AND operator token.
    /// </summary>
    public sealed class AndOperatorToken : AToken
    {
        public AndOperatorToken(int linePosition, int line)
            : base(linePosition, line)
        {
            TokenCode = TokenCode.TOK_AND_OP;
        }


        public override string ToString() => "AND";
    }
}
