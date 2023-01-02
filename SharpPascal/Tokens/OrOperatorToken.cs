/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tokens
{
    /// <summary>
    /// Logical OR operator token.
    /// </summary>
    public sealed class OrOperatorToken : AToken
    {
        public OrOperatorToken(int linePosition, int line)
            : base(linePosition, line)
        {
            TokenCode = TokenCode.TOK_OR_OP;
        }


        public override string ToString() => "OR";
    }
}
