/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tokens
{
    /// <summary>
    /// A begin-of-block token.
    /// </summary>
    public sealed class BeginBlockToken : AToken
    {
        public BeginBlockToken(int linePosition, int line)
            : base(linePosition, line)
        {
            TokenCode = TokenCode.TOK_KEY_BEGIN;
        }


        public override string ToString() => "BEGIN";
    }
}
