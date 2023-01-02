/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tokens
{
    /// <summary>
    /// MOD operator token.
    /// </summary>
    public sealed class ModOperatorToken : AToken
    {
        public ModOperatorToken(int linePosition, int line)
            : base(linePosition, line)
        {
            TokenCode = TokenCode.TOK_MOD_OP;
        }


        public override string ToString() => "MOD";
    }
}
