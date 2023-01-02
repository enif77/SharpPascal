/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tokens
{
    /// <summary>
    /// An end-of-block token.
    /// </summary>
    public sealed class EndBlockToken : AToken
    {
        public EndBlockToken(int linePosition, int line)
            : base(linePosition, line)
        {
            TokenCode = TokenCode.TOK_KEY_END;
        }


        public override string ToString() => "END";
    }
}
