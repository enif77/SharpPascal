/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tokens
{
    /// <summary>
    /// An end-of-program token.
    /// </summary>
    public sealed class EndProgramToken : AToken
    {
        public EndProgramToken(int linePosition, int line)
            : base(linePosition, line)
        {
            TokenCode = TokenCode.TOK_PROG_END;
        }


        public override string ToString() => ".";
    }
}
