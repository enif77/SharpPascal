/* Copyright (C) Premysl Fara and Contributors */

namespace SharpPascal.Tokens
{
    /// <summary>
    /// A begin-of-program token.
    /// </summary>
    public sealed class BeginProgramToken : AToken
    {
        public BeginProgramToken(int linePosition, int line)
            : base(linePosition, line)
        {
            TokenCode = TokenCode.TOK_KEY_PROGRAM;
        }


        public override string ToString() => "PROGRAM";
    }
}
