/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tokens
{
    /// <summary>
    /// An end-of-file.
    /// </summary>
    public sealed class EofToken : AToken
    {
        public EofToken(int linePosition, int line)
            : base(linePosition, line)
        {
            Code = TokenCode.TOK_EOF;
        }


        public override string ToString() => "@EOF";
    }
}
