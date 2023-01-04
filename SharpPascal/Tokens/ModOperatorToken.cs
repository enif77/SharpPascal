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
            Code = TokenCode.TOK_KEY_MOD;
        }


        public override string ToString() => "MOD";
    }
}
