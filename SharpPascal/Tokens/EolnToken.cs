/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tokens
{
    /// <summary>
    /// An end-of-line token.
    /// </summary>
    public sealed class EolnToken : AToken
    {
        public EolnToken(int linePosition, int line)
            : base(linePosition, line)
        {
            TokenCode = TokenCode.TOK_EOLN;
        }


        public override string ToString() => "@EOLN";
    }
}
